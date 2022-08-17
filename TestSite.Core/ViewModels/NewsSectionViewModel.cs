using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Umbraco.Core.Models.PublishedContent;

namespace TestSite.Core.ViewModels
{
    public class NewsSectionViewModel
    {
        public IEnumerable<IPublishedContent> Content { get; set; }
        public IPublishedContent NewsItem { get; set; }
    }
}
