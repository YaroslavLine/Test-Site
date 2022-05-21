using System.Collections.Generic;
using Umbraco.Core.Models.PublishedContent;

namespace TestSite.Core.ViewModels
{
    public class NewsResultSet
    {
        public IEnumerable<IPublishedContent> Results { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
        public double TotalPages { get; set; }
    }
}
