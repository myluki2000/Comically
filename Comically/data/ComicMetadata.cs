using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.data
{
    [Serializable]
    public class ComicMetadata
    {
        public string Title { get; set; }
        public Author Author { get; set; }
        public string Summary { get; set; }
        public List<string> Tags { get; set; }
    }
}
