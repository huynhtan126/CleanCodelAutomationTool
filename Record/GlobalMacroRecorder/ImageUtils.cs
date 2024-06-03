using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace GlobalMacroRecorder
{
    public class ImageUtils
    {
        [DllImport("msvcrt.dll")]
        private static extern int memcmp(IntPtr b1, IntPtr b2, long count);

        //public static bool CompareMemCmp(Bitmap b1, Bitmap b2)
        //{
        //    if ((b1 == null) != (b2 == null)) return false;
        //    if (b1.Size != b2.Size) return false;

        //    var bd1 = b1.LockBits(new Rectangle(new Point(0, 0), b1.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);
        //    var bd2 = b2.LockBits(new Rectangle(new Point(0, 0), b2.Size), ImageLockMode.ReadOnly, PixelFormat.Format32bppArgb);

        //    try
        //    {
        //        IntPtr bd1scan0 = bd1.Scan0;
        //        IntPtr bd2scan0 = bd2.Scan0;

        //        int stride = bd1.Stride;
        //        int len = stride * b1.Height;

        //        return memcmp(bd1scan0, bd2scan0, len) == 0;
        //    }
        //    finally
        //    {
        //        b1.UnlockBits(bd1);
        //        b2.UnlockBits(bd2);
        //    }
        //}

        public static bool CompareMemCmp(Bitmap img1, Bitmap img2)
        {
            string img1_ref, img2_ref;
            //img1 = new Bitmap(fname1);
            //img2 = new Bitmap(fname2);
            //progressBar1.Maximum = img1.Width;
            var count1 = 0;
            var count2 = 0;
            var flag = true;
            if (img1.Width == img2.Width && img1.Height == img2.Height)
            {
                for (int i = 0; i < img1.Width; i++)
                {
                    for (int j = 0; j < img1.Height; j++)
                    {
                        img1_ref = img1.GetPixel(i, j).ToString();
                        img2_ref = img2.GetPixel(i, j).ToString();
                        if (img1_ref != img2_ref)
                        {
                            count2++;
                            flag = false;
                            img2.SetPixel(i, j, Color.Aqua);
                            //break;
                        }
                        count1++;
                    }
                    //progressBar1.Value++;
                }
                double percent = count2 * 100.0 / count1;
                if (flag == false)
                {
                    if (percent < 0.005)
                    {
                        return true;
                    }
                    else
                        return false;
                }

                //if (flag == false)
                //MessageBox.Show("Sorry, Images are not same , " + count2 + " wrong pixels found");
                //else
                //MessageBox.Show(" Images are same , " + count1 + " same pixels found and " + count2 + " wrong pixels found");
            }
            else
            {
                flag = false;
            }
            return flag;
        }

    }
}
