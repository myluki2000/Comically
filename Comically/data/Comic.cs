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
                    if (volPaths.Length > 0)
                    {
                        string[] imgPaths = Directory.GetFiles(volPaths[0]);
                        if (imgPaths.Length > 0)
                        {
                            return Convert.ToBase64String(File.ReadAllBytes(imgPaths[0]));
                        }
                    }

                    return "";
                }
            }
        }

        private ComicMetadata metadata = null;

        public ComicMetadata Metadata
        {
            get
            {
                if (metadata == null)
                {
                    // find and read metadata.xml
                    string[] mdFiles = Directory.GetFiles(Path.Combine(ComicDirectory, "metadata.xml"));
                    if (mdFiles.Length > 0)
                    {
                        using (StreamReader r = new StreamReader(mdFiles[0]))
                        {
                            metadata = (ComicMetadata) new XmlSerializer(typeof(ComicMetadata)).Deserialize(r);
                        }
                    }
                    else
                    {
                        metadata = new ComicMetadata() {Title = "", Author = new Author() {Name = "Unknown"}, Summary = "", Tags = {}};
                    }
                }

                return metadata;
            }
        }

        public List<Chapter> Chapters { get; set; }
    }
}
