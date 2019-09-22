using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Comically.models
{
    public class CardModel
    {
        public string LinkTarget { get; set; }
        public string Image { get; set; }
        public string Title { get; set; }

        /// <inheritdoc />
        public CardModel(string title, string image, string linkTarget)
        {
            LinkTarget = linkTarget;
            Image = image;
            Title = title;
        }
    }
}
