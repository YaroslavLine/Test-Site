using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestSite.Core.Services;
using TestSite.Core.ViewModels;
using Umbraco.Web;
using Umbraco.Web.Mvc;

namespace TestSite.Core.Controllers
{
    public class NewsSurfaceController:SurfaceController
    {
        INewsService _newsService;
        public NewsSurfaceController(INewsService newsService)
        {
            _newsService = newsService;
        }
        public ActionResult RenderNewsSection()
        {
            var root = CurrentPage.Root();
            var news = _newsService.GetLastNews(root, 4);
            var allNewsItem = _newsService.GetNewsListItem(root);
            NewsSectionViewModel model = new NewsSectionViewModel { Content = news, NewsItem = allNewsItem };
            return PartialView("~/Views/Partials/Sections/LastNews.cshtml", model);
        }
    }
}
