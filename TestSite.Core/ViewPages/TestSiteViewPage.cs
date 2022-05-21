using TestSite.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Cache;
using Umbraco.Core.Services;
using Umbraco.Web.Mvc;
using Current = Umbraco.Web.Composing.Current;

namespace TestSite.Core.ViewPages
{
    public abstract class TestSiteViewPage<T> : UmbracoViewPage<T>
    {
        public readonly INewsService NewsService;
        public TestSiteViewPage() : this(
            Current.Factory.GetInstance<INewsService>(),
            Current.Factory.GetInstance<ServiceContext>(),
            Current.Factory.GetInstance<AppCaches>()
            )
        {
        }
        public TestSiteViewPage(INewsService newsService, ServiceContext services, AppCaches appCaches)
        {
            NewsService = newsService;
            Services = services;
            AppCaches = appCaches;
        }

    }
    public abstract class TestSiteViewPage : UmbracoViewPage
    {
        public readonly INewsService NewsService;
        public TestSiteViewPage() : this(
            Current.Factory.GetInstance<INewsService>(),
            Current.Factory.GetInstance<ServiceContext>(),
            Current.Factory.GetInstance<AppCaches>()
            )
        { }

        public TestSiteViewPage(INewsService newsService, ServiceContext services, AppCaches appCaches)
        {
            NewsService = newsService;
            Services = services;
            AppCaches = appCaches;
        }
    }
}