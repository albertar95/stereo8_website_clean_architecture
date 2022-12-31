using ImageMagick;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.ImageManager
{
    public class ImageReduce
    {
        public static bool Save(int width, int height, string filepath, string file)
        {
            try
            {
                using (MagickImage tmpimage = new MagickImage(Convert.FromBase64String(file)))
                {
                    tmpimage.Format = MagickFormat.WebP;
                    tmpimage.Resize(width, height);
                    tmpimage.Quality = 75;
                    tmpimage.Write(filepath);
                    if (File.Exists(filepath))
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static bool Remove(string filepath) 
        {
            File.Delete(filepath);
            return !File.Exists(filepath);
        }
    }
}
