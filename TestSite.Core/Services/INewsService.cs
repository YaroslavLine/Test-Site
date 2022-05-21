using System.Collections.Generic;
using System.Web;
using TestSite.Core.ViewModels;
using Umbraco.Core.Models.PublishedContent;

namespace TestSite.Core.Services
{
    public interface INewsService
    {
        IPublishedContent GetNewsListItem(IPublishedContent siteRoot);
        IEnumerable<IPublishedContent> GetLastNews(IPublishedContent siteRoot, int takeCount = int.MaxValue);
        NewsResultSet GetLastNewsPage(IPublishedContent currentContentItem, HttpRequestBase request, int itemsPerPage = 3);
    }
}
