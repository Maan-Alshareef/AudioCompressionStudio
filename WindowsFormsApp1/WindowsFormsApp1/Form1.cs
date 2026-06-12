using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.Wave;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        const int DemoDelayMs = 0;
        string loadedFilePath = "", compressedFilePath = "";
        short[] samples;
        byte[] compressedData;
        int sampleRate = 44100, quantLevels = 256, stepSize = 250;
        short channels = 1, bitsPerSample = 16;
        CancellationTokenSource cts;

        IWavePlayer outputDevice;
        AudioFileReader audioReader;

        class CmpInfo { public int Algorithm, SampleCount; public byte[] Data; }

        public Form1()
        {
            InitializeComponent();
            AllowDrop = true;

            btnDecompress.Enabled = true;
            btnDecompress.Text = "🔓 Browse && Decompress .cmp File";
        }

        void Form1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None;
        }

        void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length > 0) LoadAudio(files[0]);
        }

        void BtnBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Title = "Select audio file";
                ofd.Filter = "Audio Files|*.wav;*.mp3;*.m4a;*.aac;*.wma;*.flac;*.aiff;*.aif|All Files|*.*";
                if (ofd.ShowDialog() == DialogResult.OK) LoadAudio(ofd.FileName);
            }
        }

        void LoadAudio(string path)
        {
            try
            {
                StopAudio();
                loadedFilePath = path;
                compressedFilePath = "";
                compressedData = null;
                btnPlay.Enabled = true;
                btnStop.Enabled = false;
                btnCompress.Enabled = true;
                btnDecompress.Enabled = true;

                TimeSpan duration;
                samples = ReadAudioAsPcm16(path, out duration);

                FileInfo fi = new FileInfo(path);
                lblFileInfo.Text =
                    "AUDIO FILE METADATA:\n\n" +
                    "File Name: " + Path.GetFileName(path) + "\n" +
                    "File Size: " + (fi.Length / 1024.0).ToString("F2") + " KB\n" +
                    "Duration: " + duration.TotalSeconds.ToString("F2") + " sec\n" +
                    "Sampling Rate: " + sampleRate + " Hz\n" +
                    "Channels: " + channels + "\n" +
                    "Bit Rate: " + (sampleRate * channels * bitsPerSample / 1000) + " Kbps\n" +
                    "Encoding: decoded to PCM 16-bit for processing";

                ResetDashboard("Audio loaded successfully.");
            }
            catch (Exception ex)
            {
                samples = null;
                btnCompress.Enabled = false;
                btnDecompress.Enabled = true;
                MessageBox.Show("Error loading audio: " + ex.Message);
            }
        }

        short[] ReadAudioAsPcm16(string path, out TimeSpan duration)
        {
            using (AudioFileReader reader = new AudioFileReader(path))
            {
                sampleRate = reader.WaveFormat.SampleRate;
                channels = (short)reader.WaveFormat.Channels;
                bitsPerSample = 16;
                duration = reader.TotalTime;

                int total = (int)(reader.Length / 4);
                float[] buffer = new float[8192];
                short[] result = new short[total];
                int pos = 0, read;

                while ((read = reader.Read(buffer, 0, buffer.Length)) > 0)
                {
                    if (pos + read > result.Length) Array.Resize(ref result, pos + read);
                    for (int i = 0; i < read; i++)
                        result[pos + i] = (short)(Clamp(buffer[i], -1f, 1f) * short.MaxValue);
                    pos += read;
                }

                if (pos != result.Length) Array.Resize(ref result, pos);
                if (result.Length == 0) throw new Exception("No audio samples found.");
                return result;
            }
        }

        void BtnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(loadedFilePath)) return;
                StopAudio();
                audioReader = new AudioFileReader(loadedFilePath);
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioReader);
                outputDevice.Play();
                btnStop.Enabled = true;
            }
            catch (Exception ex) { MessageBox.Show("Playback error: " + ex.Message); }
        }

        void BtnStop_Click(object sender, EventArgs e)
        {
            StopAudio();
            btnStop.Enabled = false;
        }

        void StopAudio()
        {
            if (outputDevice != null) { outputDevice.Stop(); outputDevice.Dispose(); outputDevice = null; }
            if (audioReader != null) { audioReader.Dispose(); audioReader = null; }
        }

        async void BtnCompress_Click(object sender, EventArgs e)
        {
            if (samples == null) { MessageBox.Show("Please load an audio file first."); return; }

            quantLevels = ReadInt(txtQuantLevels.Text, 256, 2, 256);
            stepSize = ReadInt(txtStepSize.Text, 250, 1, 5000);
            int algo = cmbAlgorithm.SelectedIndex;
            Stopwatch sw = Stopwatch.StartNew();
            StartWork();

            try
            {
                compressedData = await Task.Run(() => Compress(samples, algo, cts.Token));
                sw.Stop();

                using (SaveFileDialog sfd = new SaveFileDialog { Filter = "Compressed File (*.cmp)|*.cmp", FileName = "compressed_output.cmp" })
                {
                    if (sfd.ShowDialog() != DialogResult.OK) return;
                    SaveCmp(sfd.FileName, algo, compressedData);
                    compressedFilePath = sfd.FileName;
                }

                btnDecompress.Enabled = true;
                ShowReport(sw.ElapsedMilliseconds);
            }
            catch (OperationCanceledException) { ResetDashboard("Compression cancelled."); }
            catch (Exception ex) { MessageBox.Show("Compression error: " + ex.Message); }
            finally { EndWork(); }
        }

        byte[] Compress(short[] input, int algo, CancellationToken token)
        {
            if (algo == 0) return CompressMuLaw(input, token);
            if (algo == 1) return CompressDelta(input, token);
            return CompressDpcm(input, token);
        }

        byte[] CompressMuLaw(short[] input, CancellationToken token)
        {
            byte[] output = new byte[input.Length];
            double mu = quantLevels - 1.0;
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < input.Length; i++)
            {
                token.ThrowIfCancellationRequested();
                double x = input[i] / 32768.0;
                double y = Math.Sign(x) * Math.Log(1 + mu * Math.Abs(x)) / Math.Log(1 + mu);
                output[i] = (byte)((y + 1) * 127.5);
                Tick(i, input.Length, i + 1, sw, token);
            }
            return output;
        }

        byte[] CompressDelta(short[] input, CancellationToken token)
        {
            byte[] output = new byte[input.Length];
            int predicted = 0;
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < input.Length; i++)
            {
                token.ThrowIfCancellationRequested();
                if (input[i] >= predicted) { output[i] = 1; predicted += stepSize; }
                else { output[i] = 0; predicted -= stepSize; }
                predicted = ClampInt(predicted, short.MinValue, short.MaxValue);
                Tick(i, input.Length, (i + 1) / 8, sw, token);
            }
            return output;
        }

        byte[] CompressDpcm(short[] input, CancellationToken token)
        {
            byte[] output = new byte[input.Length];
            short prev = 0;
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < input.Length; i++)
            {
                token.ThrowIfCancellationRequested();
                int diff = input[i] - prev;
                output[i] = (byte)ClampInt((diff / stepSize) + 128, 0, 255);
                prev = input[i];
                Tick(i, input.Length, i + 1, sw, token);
            }
            return output;
        }

        async void BtnDecompress_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog ofd = new OpenFileDialog())
                {
                    ofd.Title = "Select compressed file to decompress";
                    ofd.Filter = "Compressed File (*.cmp)|*.cmp|All Files (*.*)|*.*";

                    if (ofd.ShowDialog() != DialogResult.OK)
                        return;

                    compressedFilePath = ofd.FileName;
                }

                CmpInfo cmp = LoadCmp(compressedFilePath);

                txtQuantLevels.Text = quantLevels.ToString();
                txtStepSize.Text = stepSize.ToString();

                if (cmp.Algorithm >= 0 && cmp.Algorithm < cmbAlgorithm.Items.Count)
                    cmbAlgorithm.SelectedIndex = cmp.Algorithm;

                StartWork();

                short[] restored = await Task.Run(() =>
                    Decompress(cmp.Data, cmp.Algorithm, cmp.SampleCount, cts.Token)
                );

                using (SaveFileDialog sfd = new SaveFileDialog())
                {
                    sfd.Filter = "WAV File (*.wav)|*.wav";
                    sfd.FileName = "recovered_output.wav";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        WriteWav(sfd.FileName, restored);
                        lblReport.BackColor = Color.FromArgb(233, 236, 239);
                        lblReport.Text = "Decompression finished. Output saved as WAV file.";
                    }
                }
            }
            catch (OperationCanceledException)
            {
                ResetDashboard("Decompression cancelled.");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Decompression error: " + ex.Message);
            }
            finally
            {
                EndWork();
            }
        }

        short[] Decompress(byte[] data, int algo, int count, CancellationToken token)
        {
            short[] output = new short[count];
            int predicted = 0;
            short prev = 0;
            Stopwatch sw = Stopwatch.StartNew();

            for (int i = 0; i < count; i++)
            {
                token.ThrowIfCancellationRequested();

                if (algo == 0)
                {
                    double y = data[i] / 127.5 - 1.0;
                    double mu = quantLevels - 1.0;
                    double x = Math.Sign(y) * (Math.Pow(1 + mu, Math.Abs(y)) - 1) / mu;
                    output[i] = (short)(x * 32767);
                }
                else if (algo == 1)
                {
                    predicted += data[i] == 1 ? stepSize : -stepSize;
                    predicted = ClampInt(predicted, short.MinValue, short.MaxValue);
                    output[i] = (short)predicted;
                }
                else
                {
                    int value = prev + ((data[i] - 128) * stepSize);
                    prev = (short)ClampInt(value, short.MinValue, short.MaxValue);
                    output[i] = prev;
                }

                Tick(i, count, i + 1, sw, token);
            }

            return output;
        }

        void SaveCmp(string path, int algo, byte[] data)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(path)))
            {
                bw.Write("ACMP"); bw.Write(1); bw.Write(algo);
                bw.Write(sampleRate); bw.Write(channels); bw.Write(bitsPerSample);
                bw.Write(quantLevels); bw.Write(stepSize); bw.Write(samples.Length);
                bw.Write(data.Length); bw.Write(data);
            }
        }

        CmpInfo LoadCmp(string path)
        {
            using (BinaryReader br = new BinaryReader(File.OpenRead(path)))
            {
                if (br.ReadString() != "ACMP") throw new Exception("Invalid compressed file.");
                br.ReadInt32();
                int algo = br.ReadInt32();
                sampleRate = br.ReadInt32();
                channels = br.ReadInt16();
                bitsPerSample = br.ReadInt16();
                quantLevels = br.ReadInt32();
                stepSize = br.ReadInt32();
                int count = br.ReadInt32();
                int len = br.ReadInt32();
                return new CmpInfo { Algorithm = algo, SampleCount = count, Data = br.ReadBytes(len) };
            }
        }

        void WriteWav(string path, short[] data)
        {
            using (BinaryWriter bw = new BinaryWriter(File.Create(path)))
            {
                int byteRate = sampleRate * channels * bitsPerSample / 8;
                int dataSize = data.Length * 2;

                bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF")); bw.Write(36 + dataSize);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVEfmt ")); bw.Write(16);
                bw.Write((short)1); bw.Write(channels); bw.Write(sampleRate); bw.Write(byteRate);
                bw.Write((short)(channels * bitsPerSample / 8)); bw.Write(bitsPerSample);
                bw.Write(System.Text.Encoding.ASCII.GetBytes("data")); bw.Write(dataSize);

                byte[] bytes = new byte[dataSize];
                Buffer.BlockCopy(data, 0, bytes, 0, dataSize);
                bw.Write(bytes);
            }
        }

        void Tick(int i, int total, long virtualCompressed, Stopwatch sw, CancellationToken token)
        {
            if (i % 3000 != 0 && i != total - 1) return;

            int progress = (int)((i + 1) * 100.0 / total);
            double saving = Math.Max(0, (1 - (virtualCompressed / (total * 2.0))) * 100);
            double speed = (i + 1) / Math.Max(1.0, sw.ElapsedMilliseconds);

            BeginInvoke(new Action(() =>
            {
                progressBar.Value = Math.Min(100, progress);
                chartPerformance.Series["Savings Ratio %"].Points.AddY(saving);
                chartPerformance.Series["Speed (kSamples/sec)"].Points.AddY(speed);
            }));

            if (DemoDelayMs > 0) token.WaitHandle.WaitOne(DemoDelayMs);
        }

        void ShowReport(long ms)
        {
            long originalFile = new FileInfo(loadedFilePath).Length;
            long pcmSize = samples.Length * 2L;
            long compSize = new FileInfo(compressedFilePath).Length;
            double ratio = pcmSize / (double)Math.Max(1, compSize);
            double saving = (1 - compSize / (double)Math.Max(1, pcmSize)) * 100;

            lblReport.BackColor = Color.FromArgb(233, 236, 239);
            lblReport.Text =
                "REPORT:\n" +
                "Original File: " + (originalFile / 1024.0).ToString("F2") + " KB | PCM Size: " + (pcmSize / 1024.0).ToString("F2") + " KB\n" +
                "Compressed: " + (compSize / 1024.0).ToString("F2") + " KB | Ratio: " + ratio.ToString("F2") + ":1\n" +
                "Saving vs PCM: " + saving.ToString("F2") + "% | Time: " + ms + " ms\n" +
                "Algorithm: " + cmbAlgorithm.SelectedItem + " | Levels=" + quantLevels + " | Step=" + stepSize;
        }

        int ReadInt(string text, int def, int min, int max)
        {
            int x;
            if (!int.TryParse(text, out x)) x = def;
            return ClampInt(x, min, max);
        }

        void StartWork()
        {
            cts = new CancellationTokenSource();
            btnCancel.Enabled = true;
            btnCompress.Enabled = false;
            btnDecompress.Enabled = false;
            progressBar.Value = 0;
            lblReport.BackColor = Color.FromArgb(233, 236, 239);
            chartPerformance.Series["Savings Ratio %"].Points.Clear();
            chartPerformance.Series["Speed (kSamples/sec)"].Points.Clear();
        }

        void EndWork()
        {
            btnCancel.Enabled = false;
            btnCompress.Enabled = samples != null;

            btnDecompress.Enabled = true;
        }

        void BtnReset_Click(object sender, EventArgs e)
        {
            if (cts != null) cts.Cancel();
            StopAudio();

            compressedFilePath = "";
            compressedData = null;
            quantLevels = 256;
            stepSize = 250;
            txtQuantLevels.Text = "256";
            txtStepSize.Text = "250";
            if (cmbAlgorithm.Items.Count > 0) cmbAlgorithm.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(loadedFilePath) && File.Exists(loadedFilePath))
            {
                LoadAudio(loadedFilePath);
                btnStop.Enabled = false;
                ResetDashboard("Original audio values restored.");
            }
            else
            {
                btnPlay.Enabled = false;
                btnStop.Enabled = false;
                btnCompress.Enabled = false;
                btnCancel.Enabled = false;
                btnDecompress.Enabled = true;
                sampleRate = 44100;
                channels = 1;
                bitsPerSample = 16;
                lblFileInfo.Text = "AUDIO FILE METADATA:\n\nNo file loaded. Please drop or browse an audio clip to inspect properties.";
                ResetDashboard("No audio file loaded.");
            }
        }

        void BtnCancel_Click(object sender, EventArgs e)
        {
            if (cts != null) cts.Cancel();
        }

        void ResetDashboard(string message)
        {
            progressBar.Value = 0;
            chartPerformance.Series["Savings Ratio %"].Points.Clear();
            chartPerformance.Series["Speed (kSamples/sec)"].Points.Clear();
            lblReport.BackColor = Color.FromArgb(233, 236, 239);
            lblReport.Text = message;
        }

        float Clamp(float x, float min, float max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }

        int ClampInt(int x, int min, int max)
        {
            if (x < min) return min;
            if (x > max) return max;
            return x;
        }

        void PanelLeft_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panelLeft.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

        void PanelRight_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panelRight.ClientRectangle, Color.LightGray, ButtonBorderStyle.Solid);
        }

        void PanelDragDrop_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, panelDragDrop.ClientRectangle, Color.RoyalBlue, ButtonBorderStyle.Dashed);
        }
    }
}
