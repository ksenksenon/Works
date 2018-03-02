using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;



namespace Picture
{
    public class Picture
    {
        public string Url { get; set; }
        public Image Image { get; set; }
        public float Angle { get; set; }

        public Picture(string url, float angle)
        {
            Url = url;
            Angle = angle;
            using (WebClient webClient = new WebClient())
            {
                byte[] data = webClient.DownloadData(Url);
                using (MemoryStream mem = new MemoryStream(data))
                {
                    Image = Image.FromStream(mem);
                }
            }
            using (var graphics = Graphics.FromImage(Image))
            {
                string text = "Ksenia";
                Font font = new Font("Arial", 150, FontStyle.Bold);
                SizeF stringSize = graphics.MeasureString(text, font);

                var textWidth1 = Image.Width * 0.8f;
                var textHeight1 = textWidth1 * stringSize.Height / stringSize.Width;

                var rectX1 = ((float)Image.Width - textWidth1) / 2.0f;
                var rectY1 = ((float)Image.Height - textHeight1) / 2.0f;
                LinearGradientBrush brush = new LinearGradientBrush(new Point(0, Convert.ToInt32(stringSize.Width)), new Point(0, Convert.ToInt32(stringSize.Height)), Color.FromArgb(128, 255, 0, 0), Color.FromArgb(128, 0, 0, 255));

                Pen pen = new Pen(Color.Red, 1);
                if (rectX1 > 0 && rectX1 < Image.Width && rectY1 > 0 && rectY1 < Image.Height)
                {
                    var rect = new RectangleF(rectX1, rectY1, textWidth1, textHeight1);
                    
                    var sx =  rect.Width / stringSize.Width;
                    var sy = rect.Height / stringSize.Height;

                    graphics.ScaleTransform(sx, sy, MatrixOrder.Prepend);
                    graphics.TranslateTransform((float)rectX1 / sx, (float)rectY1 / sy, MatrixOrder.Prepend);
                    var center = new PointF((rect.Width / 2.0f) / sx, (rect.Height / 2.0f) / sy);
                    graphics.TranslateTransform(center.X, center.Y, MatrixOrder.Prepend);
                    graphics.RotateTransform(Angle, MatrixOrder.Prepend);
                    graphics.TranslateTransform(-center.X, -center.Y, MatrixOrder.Prepend);

                    graphics.DrawString(text, font, brush, 0, 0);
                }
                else
                {
                    var textHeight2 = Image.Height * 0.8f;
                    var textWidth2 = textHeight2 * stringSize.Width / stringSize.Height;
                    var rectX2 = ((float)Image.Width - textWidth2) / 2.0f;
                    var rectY2 = ((float)Image.Height - textHeight2) / 2.0f;

                    var rect = new RectangleF(rectX2, rectY2, textWidth2, textHeight2);

                    var sx = rect.Width / stringSize.Width;
                    var sy = rect.Height / stringSize.Height;

                    graphics.ScaleTransform(sx, sy, MatrixOrder.Prepend);
                    graphics.TranslateTransform((float)rectX2 / sx, (float)rectY2 / sy, MatrixOrder.Prepend);
                    var center = new PointF((rect.Width / 2.0f) / sx, (rect.Height / 2.0f) / sy);
                    graphics.TranslateTransform(center.X, center.Y, MatrixOrder.Prepend);
                    graphics.RotateTransform(Angle, MatrixOrder.Prepend);
                    graphics.TranslateTransform(-center.X, -center.Y, MatrixOrder.Prepend);

                    graphics.DrawString(text, font, brush, 0, 0);
                }
            }
        }

        public void SaveToFile(string path)
        {
            Image.Save(path, ImageFormat.Png);
        }
    }
}
