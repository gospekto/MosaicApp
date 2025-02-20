using MosaicCLibrary;
using System;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Imaging;
using System.Diagnostics;
using System.Threading;

namespace MosaicProgram
{
    public partial class Form1 : Form
    {
        //[DllImport(@"C:\Users\gospekto\Documents\GitHub\MosaicApp\x64\Debug\MosaicASM.dll", CallingConvention = CallingConvention.Cdecl)]
        [DllImport(@"C:\Users\gospekto\Documents\GitHub\MosaicApp\x64\Release\MosaicASM.dll")]
        private static extern void ApplyMosaic(byte[] source, byte[] result, int height, int width, int stride, int tileX, int tileY, int tileSize);

        public Form1()
        {
            InitializeComponent();
            threadNumber.Value = Environment.ProcessorCount;
            ThreadNumberCount.Text = threadNumber.Value.ToString();
            TileSizeCount.Text = tileSize.Value.ToString();
        }


        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void btnChoosePhoto_Click(object sender, EventArgs e)
        {
            // Utwórz i skonfiguruj okno dialogowe
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Title = "Wybierz zdjêcie",
                Filter = "Pliki graficzne|*.jpg;*.jpeg;*.png;*.bmp;*.gif",
                Multiselect = false // Tylko jeden plik mo¿e byæ wybrany
            };

            // Wyœwietl okno dialogowe i sprawdŸ, czy u¿ytkownik wybra³ plik
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Wczytaj wybrane zdjêcie
                    string filePath = openFileDialog.FileName;
                    Image selectedImage = Image.FromFile(filePath);

                    // Wyœwietl zdjêcie w kontrolce PictureBox
                    pictureBox1.Image = selectedImage;
                    pictureBox1.SizeMode = PictureBoxSizeMode.Zoom; // Dopasuj rozmiar obrazu
                }
                catch (Exception ex)
                {
                    // Obs³uga b³êdów
                    MessageBox.Show($"Wyst¹pi³ b³¹d podczas wczytywania pliku: {ex.Message}",
                                    "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Nie wybrano obrazu", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int threadCount = threadNumber.Value; // Liczba w¹tków
            int tileSizepx = tileSize.Value;   // Rozmiar kafelka

            Stopwatch stopwatch = Stopwatch.StartNew();

            try
            {
                using Bitmap sourceBitmap = new Bitmap(pictureBox1.Image);
                using Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

                ProcessImage(sourceBitmap, resultBitmap, tileSizepx, threadCount);

                stopwatch.Stop();
                pictureBox2.Image?.Dispose();
                pictureBox2.Image = new Bitmap(resultBitmap);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;

                MessageBox.Show($"Gotowe\nCzas wykonania: {stopwatch.ElapsedMilliseconds} ms\n" +
                                $"Rozmiar kafelka: {tileSizepx}px",
                    "Gotowe", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wyst¹pi³ b³¹d: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProcessImage(Bitmap source, Bitmap result, int tileSizepx, int threadCount)
        {
            int width = source.Width;
            int height = source.Height;

            BitmapData sourceData = source.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);
            BitmapData resultData = result.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format32bppRgb);

            try
            {
                int stride = sourceData.Stride;
                byte[] sourceBuffer = new byte[Math.Abs(stride) * height];
                byte[] resultBuffer = new byte[Math.Abs(stride) * height];

                Marshal.Copy(sourceData.Scan0, sourceBuffer, 0, sourceBuffer.Length);

                //IntPtr sourcePtr = Marshal.AllocHGlobal(sourceBuffer.Length);
                //IntPtr resultPtr = Marshal.AllocHGlobal(resultBuffer.Length);
                //Marshal.Copy(sourceBuffer, 0, sourcePtr, sourceBuffer.Length);     
                int tilesPerRow = (int)Math.Ceiling(width / (double)tileSizepx);
                int tilesPerColumn = (int)Math.Ceiling(height / (double)tileSizepx);

                // Podzial obrazu na równe czeœci dla watkow
                int[] startIndex = new int[threadCount];
                int[] finishIndex = new int[threadCount];

                // Oblicza liczbê pe³nych kafelków w pionie
                int totalTiles = height / tileSizepx + (height % tileSizepx == 0 ? 0 : 1);
                int tilesPerThread = totalTiles / threadCount;

                for (int i = 0; i < threadCount; i++)
                {
                    // Pocz¹tek zakresu w pikselach
                    startIndex[i] = i * tilesPerThread * tileSizepx * width * 4;

                    if (i == threadCount - 1)
                    {
                        finishIndex[i] = height * width * 4;
                    }
                    else
                    {
                        finishIndex[i] = (i + 1) * tilesPerThread * tileSizepx * width * 4;
                    }
                }

                Parallel.For(0, threadCount, i =>
                {

                    if (ClibSelector.Checked)
                    {
                        MosaicC.ApplyMosaic(sourceBuffer, resultBuffer, height, width, stride, startIndex[i], finishIndex[i], tileSizepx);
                    }
                    else if (ASMlibSelector.Checked)
                    {
                        ApplyMosaic(sourceBuffer, resultBuffer, height, width, stride, startIndex[i], finishIndex[i], tileSizepx);
                    }
                    else
                    {
                        MessageBox.Show($"Nie wybrano biblioteki", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                });

                Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            }
            finally
            {
                source.UnlockBits(sourceData);
                result.UnlockBits(resultData);
            }
        }


        private void threadNumber_Scroll(object sender, EventArgs e)
        {
            ThreadNumberCount.Text = threadNumber.Value.ToString();
        }

        private void tileSize_Scroll(object sender, EventArgs e)
        {
            TileSizeCount.Text = tileSize.Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image == null)
            {
                MessageBox.Show("Brak obrazu do zapisania!", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            saveFileDialog1.Filter = "JPEG Image|.jpg|PNG Image|.png|Bitmap Image|*.bmp";
            saveFileDialog1.Title = "Zapisz obraz";

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileExtension = Path.GetExtension(saveFileDialog1.FileName).ToLower();

                try
                {
                    ImageFormat format = ImageFormat.Bmp; // Default to BMP if extension is unknown
                    switch (fileExtension)
                    {
                        case ".jpg":
                        case ".jpeg":
                            format = ImageFormat.Jpeg;
                            break;
                        case ".png":
                            format = ImageFormat.Png;
                            break;
                            // No need for BMP since it's the default
                    }

                    using (var bitmap = new Bitmap(pictureBox2.Image))
                    {
                        bitmap.Save(saveFileDialog1.FileName, format);
                    }

                    MessageBox.Show("Obraz zosta³ zapisany!", "Sukces", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wyst¹pi³ b³¹d podczas zapisywania obrazu: {ex.Message}", "B³¹d", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
