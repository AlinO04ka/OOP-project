using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows;
using System.Windows.Controls;
using System.Drawing;

namespace Ded_Project
{
    static class Image
    {
        static string file_image;

        static public void Open()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            Nullable<bool> result = openFileDialog.ShowDialog();
            file_image = openFileDialog.FileName;
            SaveImage(file_image);
        }

        static public BitmapImage OpenNumb()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Файлы изображений (*.bmp, *.jpg, *.png)|*.bmp;*.jpg;*.png";
            Nullable<bool> result = openFileDialog.ShowDialog();
            file_image = openFileDialog.FileName;
            return LoadImage(Convert(file_image));
        }

        static public void SaveImage(string image)
        {
            try
            {
                byte[] imageData;
                using (FileStream file = new FileStream(image, FileMode.Open))
                {
                    imageData = new byte[file.Length];
                    file.Read(imageData, 0, imageData.Length);
                }

                using (HotelEntities db = new HotelEntities())
                {
                    Profile user = db.Profiles.FirstOrDefault(c => c.Entry.login == User.user.Login);
                    if (user != null)
                    {
                        user.imageData = imageData;
                        db.SaveChanges();
                    }
                }
            }
            catch { }
        }

        static public byte[] Convert(string path)
        {
            try
            {
                byte[] imageData;
                using (FileStream file = new FileStream(path, FileMode.Open))
                {
                    imageData = new byte[file.Length];
                    file.Read(imageData, 0, imageData.Length);
                }
                return imageData;
            }
            catch
            {
                return null;
            }
        }

        static public byte[] Convert(BitmapImage image)
        {
            try
            {
                Bitmap bit;
                byte[] imageData;
                using (MemoryStream outStream = new MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(image));
                    enc.Save(outStream);
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);
                    bit = new Bitmap(bitmap);
                }

                using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
                {
                    bit.Save(stream, System.Drawing.Imaging.ImageFormat.Bmp);
                    stream.Position = 0;
                    imageData = new byte[stream.Length];
                    stream.Read(imageData, 0, (int)stream.Length);
                    stream.Close();
                }
                return imageData;
            }
            catch
            {
                return null;
            }
        }

        static public BitmapImage LoadImage(byte[] imageData)
        {
            if (imageData != null)
            {
                BitmapImage bitmap = null;

                MemoryStream stream = new MemoryStream(imageData);
                System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
                bitmap = new BitmapImage();
                bitmap.BeginInit();
                MemoryStream stream2 = new MemoryStream();
                image.Save(stream2, System.Drawing.Imaging.ImageFormat.Bmp);
                stream2.Seek(0, SeekOrigin.Begin);
                bitmap.StreamSource = stream2;
                bitmap.EndInit();
                return bitmap;
            }
            return null;
        }
    }
}
