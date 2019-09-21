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
                Comic comic = new Comic() { ComicDirectory = comicPath };

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
                        comic.ComicInfo = ComicInfo.FromBinary(Path.Combine(comicPath, "db.bin"));

                        // safety check for comic id
                        if (comic.ComicInfo.Id == 0)
                        {
                            Console.WriteLine("Broken id for comic '" + comic.ComicInfo.Title + "'");
                        }

                        // add comic to comics dictionary
                        if (comics.ContainsKey(comic.ComicInfo.Id))
                        {
                            Console.Error.WriteLine("Multiple comics with the same id exist. Offender: '" + comic.ComicInfo.Title + "' ");
                            continue;
                        }

                        comics.Add(comic.ComicInfo.Id, comic);
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
                ComicInfo ci = new ComicInfo()
                {
                    Id = highestId,
                    Title = Path.GetFileName(comic.ComicDirectory)
                };

                comic.ComicInfo = ci;

                comics.Add(comic.ComicInfo.Id, comic);

                // Save ComicInfo
                ci.ToBinaryFile(Path.Combine(comic.ComicDirectory, "db.bin"));
            }
        }

        public static void SetComicInfo(uint comicId, ComicInfo newInfo)
        {
            Comic c = GetComicById(comicId);
            c.ComicInfo = newInfo;
            newInfo.ToBinaryFile(Path.Combine(LIBRARY_PATH, c.ComicDirectory, "db.bin"));
        }

        public static List<Comic> GetComics()
        {
            return comics.Values.ToList();
        }

        public static Comic GetComicById(uint comicId)
        {
            return comics[comicId];
        }
    }
}
