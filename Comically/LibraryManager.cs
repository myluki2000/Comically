using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Comically.data;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Comically
{
    public static class LibraryManager
    {
        private const string LIBRARY_PATH = "E:\\Manga_Lib";
        private static readonly Dictionary<uint, Comic> comics = new Dictionary<uint, Comic>();

        public static void ScanLibrary()
        {
            List<Comic> comicsToAddToDb = new List<Comic>();
            foreach (string comicPath in Directory.GetDirectories(LIBRARY_PATH))
            {
                Comic comic = new Comic() { ComicDirectory = comicPath, Title = Path.GetFileName(comicPath) };

                // search db.bin
                string[] dbFiles = Directory.GetFiles(comicPath, "db.bin");
                if (dbFiles.Length > 0)
                {
                    if (dbFiles.Length > 1)
                    {
                        Console.Error.WriteLine("Found conflicting db.bin files in directory " + comicPath);
                    }
                    else
                    {
                        // read comic info from db.bin
                        ComicInfo ci =
                    }
                }
                else
                {
                    comicsToAddToDb.Add(comic);
                }
            }

            // add db.bin for comics without it
            uint highestId = comics.Count > 0 ? comics.Aggregate((x, y) => x.Key > y.Key ? x : y).Key : 0; // gets the highest existing key
            foreach (Comic comic in comicsToAddToDb)
            {
                highestId++;
                ComicInfo ci = new ComicInfo() {Id = highestId};
                comic.Id = highestId;
                comics.Add(comic.Id, comic);

                // Save ComicInfo
                ci.ToBinaryFile(Path.Combine(comic.ComicDirectory, "db.bin"));
            }
        }
        public static List<Comic> GetComics()
        {
            return comics.Values.ToList();
        }

        public static Comic GetComicById(uint id)
        {
            return comics[id];
        }
    }
}
