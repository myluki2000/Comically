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
        }
    }
}
