using System.Collections.Generic;
using Comically.data;

namespace Comically
{
    public sealed class MainModule : Nancy.NancyModule
    {
        public MainModule()
        {
            Get("/", p => View["index.cshtml", LibraryManager.GetComics()]);

            Get("/info/{id}", p =>
            {
                Comic c = LibraryManager.GetComicById(uint.Parse(p["id"]));
                return View["info.cshtml", c];
            });

            Get("/editmeta/{id}", p =>
            {
                Comic c = LibraryManager.GetComicById(uint.Parse(p["id"]));
                return View["editmeta.cshtml", c];
            });

            Get("/setmeta/{id}", p =>
            {
                Comic c = LibraryManager.GetComicById(uint.Parse(p["id"]));
                if (Request.Query["title"] != null) c.ComicInfo.Title = Request.Query["title"];
                if (Request.Query["author"] != null) c.ComicInfo.Author = Request.Query["author"];
                if (Request.Query["summary"] != null) c.ComicInfo.Summary = Request.Query["summary"];
                // TODO: Implement tag editing

                LibraryManager.WriteComicInfo(c);

                return View["setmeta.cshtml"];
            });
        }
    }
}
