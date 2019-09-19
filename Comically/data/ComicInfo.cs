using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.data
{
    public class ComicInfo
    {
        public uint Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Summary { get; set; }
        public List<uint> TagIds { get; set; } = new List<uint>();

        /// <summary>
        /// Saves a .bin file with the data contained in this object.
        /// </summary>
        /// <param name="path">Path to save the file to</param>
        public void ToBinaryFile(string path)
        {
            using (BinaryWriter w = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                w.Write(Id);
                w.Write(Title);
                w.Write(Author);
                w.Write(Summary);
                
                w.Write(TagIds.Count);
                foreach (uint tag in TagIds)
                {
                    w.Write(tag);
                }

            }
        }

        /// <summary>
        /// Creates a ComicInfo object from data read from a .bin file.
        /// </summary>
        /// <param name="path">Path to the file to read from</param>
        /// <returns>ComicInfo object with the data</returns>
        public static ComicInfo FromBinary(string path)
        {
            ComicInfo c = new ComicInfo();
            using (BinaryReader r = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                c.Id = r.ReadUInt32();
                c.Title = r.ReadString();
                c.Author = r.ReadString();
                c.Summary = r.ReadString();

                for (int i = 0; i < r.ReadInt32(); i++)
                {
                    c.TagIds.Add(r.ReadUInt32());
                }
            }

            return c;
        }
    }
}
