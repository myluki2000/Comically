using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Comically.data
{
    public class Comic
    {
        public ComicInfo ComicInfo { get; set; }
        public string ComicDirectory { get; set; }

        public string CoverImage
        {
            get
            {
                string[] coverFiles = Directory.GetFiles(ComicDirectory, "cover.*");
                if (coverFiles.Length > 0)
                {
                    // return cover image in comic directory if it exists
                    return Convert.ToBase64String(File.ReadAllBytes(coverFiles[0]));
                }
                else
                {
                    // return first image of comic otherwise
                    string[] volPaths = Directory.GetDirectories(ComicDirectory);

                    if (volPaths.Length <= 0) return "";

                    string[] imgPaths = Directory.GetFiles(volPaths[0]);

                    if (imgPaths.Length <= 0) return "";

                    return Convert.ToBase64String(File.ReadAllBytes(imgPaths[0]));

                }
            }
        }

        public List<Volume> Volumes
        {
            get
            {
                if (Directory.GetDirectories(ComicDirectory, "Volume ").Length > 0)
                {
                    // if volume directories exist read them
                    return Directory.GetDirectories(ComicDirectory).Select(volDir => new Volume(volDir)).ToList();
                }
                else
                {
                    // if not create a "virtual" volume
                    return new List<Volume>() { new Volume(ComicDirectory) };
                }
            }
        }
    }
}
