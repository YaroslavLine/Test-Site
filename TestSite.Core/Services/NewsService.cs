using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TestSite.Core.Helpers;
using TestSite.Core.ViewModels;
using Umbraco.Core.Models.PublishedContent;
using Umbraco.Web;

namespace TestSite.Core.Services
{
    public class NewsService : INewsService
    {
        public IPublishedContent GetNewsListItem(IPublishedContent siteRoot)
        {
            return siteRoot.Descendants().Where(x => x.ContentType.Alias == "newsList").FirstOrDefault();
        }

        public IEnumerable<IPublishedContent> GetLastNews(IPublishedContent siteRoot, int takeCount = int.MaxValue)
        {
            var newsList = GetNewsListItem(siteRoot);
            var latestNews = newsList.Descendants()
                .Where(x => x.ContentType.Alias == "newsItem" && x.IsVisible())
                .OrderByDescending(n => n.Value<DateTime>("newsDate"))
                .Take(takeCount);

            return latestNews;
        }
        public NewsResultSet GetLastNewsPage(IPublishedContent currentContentItem, HttpRequestBase request, int itemsPerPage = 3)
        {
            var siteRoot = currentContentItem.Root();
            var newsListItem = GetNewsListItem(siteRoot);
            var newsList = newsListItem.Descendants()
                .Where(x => x.ContentType.Alias == "newsItem" && x.IsVisible())
                .OrderByDescending(n => n.Value<DateTime>("newsDate"));

            int pageNumber = QueryStringHelper.GetIntFromQueryString(request, "page", 1);
            int pageSize = QueryStringHelper.GetIntFromQueryString(request, "size", itemsPerPage);
            int totalNewsItems = newsList.Count();
            double totalPages = totalNewsItems > 0 ? Math.Ceiling((double)totalNewsItems / pageSize) : 1;
            var newsPage = newsList.Skip((pageNumber - 1) * pageSize).Take(pageSize);

            return new NewsResultSet
            {
                TotalPages = totalPages,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Results = newsPage.ToList()
            };
        }
    }
}
