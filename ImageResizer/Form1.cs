using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace ImageResizer
{
    public partial class Form1 : Form
    {
        ToolTip _toolTip = new ToolTip();

        public Form1()
        {
            InitializeComponent();
        }

       

        /// <summary>
        /// Create the directories that is needed for the resource files to go in. in this case it will be images.
        /// </summary>
        /// <param name="selectedPath">The path of the selected file</param>
        private void createFolders(string selectedPath)
        {
            Directory.CreateDirectory(selectedPath + "Resources");

            Directory.CreateDirectory(selectedPath + "Resources\\drawable");
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-ldpi");
           
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-mdpi");
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-hdpi");
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-xhdpi");
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-xxhdpi"); // origional file for now................
            Directory.CreateDirectory(selectedPath + "Resources\\drawable-xxxhdpi");
        }

        public void resizeImage(string imageFile, string outputFile, double scaleFactor)
        {
            using (var srcImage = Image.FromFile(imageFile,true))
            {
                var newWidth = (int)(srcImage.Width * scaleFactor);
                var newHeight = (int)(srcImage.Height * scaleFactor);
                using (var newImage = new Bitmap(newWidth, newHeight))
                    using (var graphics = Graphics.FromImage(newImage))
                    {
                        graphics.SmoothingMode = SmoothingMode.AntiAlias;
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.DrawImage(srcImage, new Rectangle(0, 0, newWidth, newHeight));
                        newImage.Save(outputFile);
                    }
            }
        }

        private static ImageCodecInfo GetEncoderInfo(String mimeType)
        {
            int j;
            ImageCodecInfo[] encoders;
            encoders = ImageCodecInfo.GetImageEncoders();
            for (j = 0; j < encoders.Length; ++j)
            {
                if (encoders[j].MimeType == mimeType)
                    return encoders[j];
            }
            return null;
        }

        private void resizeImages()
        {
            
            
            //TODO: Create ldpi
            //TODO: Create mdpi
            //TODO: Create hdpi
            //TODO: Create xhdpi
            //TODO: Create xxhdpi
            //TODO: Create xxhdpi
            //TODO: Create xxxhdpi

        }

        // Select and create files and directoried from here
        private void btnGetImage_Click(object sender, EventArgs e)
        {
            ComboItem selectedSize = (ComboItem)SourceSizeComboBox.SelectedItem;
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Filter = " Supported Image Files |*.jpg;*.jpeg;*.png;*.gif;*.bmp;*.ico";
            DialogResult result = openFile.ShowDialog();
            if(result != DialogResult.Cancel)
            {
                createFolders(openFile.FileName.Replace(openFile.SafeFileName,""));
               
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-ldpi\\" + openFile.SafeFileName, ScreenSize.ldpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-mdpi\\" + openFile.SafeFileName, ScreenSize.mdpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-hdpi\\" + openFile.SafeFileName, ScreenSize.hdpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-xhdpi\\" + openFile.SafeFileName, ScreenSize.xhdpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable\\" + openFile.SafeFileName, ScreenSize.xxhdpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-xxhdpi\\" + openFile.SafeFileName, ScreenSize.xxhdpi / selectedSize.Value);
                resizeImage(openFile.FileName, openFile.FileName.Replace(openFile.SafeFileName, "") +
                    "\\Resources\\drawable-xxxhdpi\\" + openFile.SafeFileName, ScreenSize.xxxhdpi / selectedSize.Value); 
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var newItem = new ComboItem();
            newItem.Text = "  ldpi";
            newItem.Value = ScreenSize.ldpi;
            SourceSizeComboBox.Items.Add(newItem);

            newItem = new ComboItem();
            newItem.Text = "  mdpi";
            newItem.Value = ScreenSize.mdpi;
            SourceSizeComboBox.Items.Add(newItem);

            newItem = new ComboItem();
            newItem.Text = "  hdpi";
            newItem.Value = ScreenSize.hdpi;
            SourceSizeComboBox.Items.Add(newItem);

            newItem = new ComboItem();
            newItem.Text = "  xhdpi";
            newItem.Value = ScreenSize.xhdpi;
            SourceSizeComboBox.Items.Add(newItem);

            newItem = new ComboItem();
            newItem.Text = "  xxhdpi";
            newItem.Value = ScreenSize.xxhdpi;
            SourceSizeComboBox.Items.Add(newItem);

            newItem = new ComboItem();
            newItem.Text = "  xxxhdpi";
            newItem.Value = ScreenSize.xxxhdpi;
            SourceSizeComboBox.Items.Add(newItem);
            SourceSizeComboBox.SelectedIndex = 4;
        }

        private void pictureBox8_MouseHover(object sender,EventArgs e)
        {
            var pos = this.PointToClient(Cursor.Position);
            _toolTip.IsBalloon = true;
            _toolTip.ToolTipTitle = "What is  Source Size";
            _toolTip.Show("This is the size of the file that you want to copy and resize to the various drawable size folders.\n" +
                          "So, if your source image fits best on xhdpi screen size, select the xdpi as the source.\n" +
                          " \n" +
                          "We recomend using bigger source images like xxhdpi or xxxhdpi, to keep quality high.\n", this, pos.X, pos.Y - 100);
        }

        private void pictureBox8_MouseLeave(object sender, EventArgs e)
        {
            _toolTip.Hide(this);
        }

    }

    public class ComboItem
    {
        public string Text { get; set; }
        public double Value { get; set; }

        public override string ToString()
        {
            return Text;
        }
    }

    public static class ScreenSize
    {
        public const double ldpi = 219.00;
        public const double mdpi = 293.00;
        public const double hdpi = 439.00;
        public const double xhdpi = 589.00;
        public const double xxhdpi = 874.00;
        public const double xxxhdpi = 1172.00;

    }
    
}
