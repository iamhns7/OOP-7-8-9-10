using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ML_math_image_process
{
    public partial class Form1 : Form
    {
        private Bitmap originalImage;

        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 1;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        originalImage = new Bitmap(openFileDialog.FileName);
                        pictureBoxOriginal.Image = originalImage;
                        lblInfo.Text = "Image loaded successfully.";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error loading image: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("Please open an image first.", "No Image", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Get threshold value from trackbar
            int threshold = trackBarThreshold.Value;

            // Process image: Grayscale + Threshold
            Bitmap processedImage = ProcessImage(originalImage, threshold);

            // Display processed image
            pictureBoxProcessed.Image = processedImage;

            // Calculate average brightness and make ML decision
            double averageBrightness = CalculateAverageBrightness(processedImage);
            string decision = MakeMLDecision(averageBrightness, threshold);

            // Update info label
            lblInfo.Text = $"Threshold: {threshold} | Average Brightness: {averageBrightness:F2} | Decision: {decision}";
        }

        private Bitmap ProcessImage(Bitmap original, int threshold)
        {
            Bitmap processed = new Bitmap(original.Width, original.Height);

            // Optimize: Use LockBits for faster pixel access
            System.Drawing.Imaging.BitmapData originalData = original.LockBits(
                new Rectangle(0, 0, original.Width, original.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            System.Drawing.Imaging.BitmapData processedData = processed.LockBits(
                new Rectangle(0, 0, processed.Width, processed.Height),
                System.Drawing.Imaging.ImageLockMode.WriteOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* origPtr = (byte*)originalData.Scan0;
                byte* procPtr = (byte*)processedData.Scan0;
                int bytes = Math.Abs(originalData.Stride) * original.Height;
                int stride = originalData.Stride;

                for (int y = 0; y < original.Height; y++)
                {
                    for (int x = 0; x < original.Width; x++)
                    {
                        int index = y * stride + x * 3;
                        byte blue = origPtr[index];
                        byte green = origPtr[index + 1];
                        byte red = origPtr[index + 2];

                        // Math: Luminance equation (standard grayscale conversion)
                        // Luminance = 0.299*R + 0.587*G + 0.114*B
                        int grayValue = (int)(0.299 * red + 0.587 * green + 0.114 * blue);

                        // Threshold processing
                        byte finalValue = (byte)(grayValue > threshold ? 255 : 0);

                        procPtr[index] = finalValue;       // Blue
                        procPtr[index + 1] = finalValue;   // Green
                        procPtr[index + 2] = finalValue;   // Red
                    }
                }
            }

            original.UnlockBits(originalData);
            processed.UnlockBits(processedData);

            return processed;
        }

        private double CalculateAverageBrightness(Bitmap image)
        {
            long totalBrightness = 0;
            int pixelCount = image.Width * image.Height;

            // Optimize: Use LockBits for faster access
            System.Drawing.Imaging.BitmapData imageData = image.LockBits(
                new Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);

            unsafe
            {
                byte* ptr = (byte*)imageData.Scan0;
                int stride = imageData.Stride;

                for (int y = 0; y < image.Height; y++)
                {
                    for (int x = 0; x < image.Width; x++)
                    {
                        int index = y * stride + x * 3;
                        totalBrightness += ptr[index]; // Since it's grayscale, R=G=B
                    }
                }
            }

            image.UnlockBits(imageData);

            // Math: Average calculation
            return (double)totalBrightness / pixelCount;
        }

        // ML (Simplified): Decision rule based on average brightness
        private string MakeMLDecision(double averageBrightness, int threshold)
        {
            // Simple decision rule: if average brightness is above threshold, it's "Bright", otherwise "Dark"
            if (averageBrightness > threshold)
            {
                return "Bright";
            }
            else
            {
                return "Dark";
            }
        }

        private void trackBarThreshold_ValueChanged(object sender, EventArgs e)
        {
            // Update label when threshold changes
            if (originalImage != null)
            {
                lblInfo.Text = $"Threshold: {trackBarThreshold.Value} (Adjust and click Process)";
            }
        }
    }
}
