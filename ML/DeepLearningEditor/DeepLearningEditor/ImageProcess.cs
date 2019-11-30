using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace DeepLearningEditor
{
    public class ImageProcess
    {
        /// <summary>
        ///  根据图片文件的路径使用文件流打开，并保存为byte[] 
        /// </summary>
        /// <param name="imagepath"></param>
        /// <returns></returns>
        static public byte[] GetPictureData(string imagepath)
        {

            FileStream fs = new FileStream(imagepath, FileMode.Open);//可以是其他重载方法 
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }


        static public BitmapImage ByteArrayToBitmapImage(byte[] byteArray)
        {
            BitmapImage bmp = null;

            try
            {
                bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.StreamSource = new MemoryStream(byteArray);
                bmp.EndInit();
            }
            catch
            {
                bmp = null;
            }

            return bmp;
        }

    }
}
