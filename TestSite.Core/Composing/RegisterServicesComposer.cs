using TestSite.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Composing;

namespace TestSite.Core.Composing
{
    public class RegisterServicesComposer : IUserComposer
    {
        public void Compose(Composition composition)
        {
            composition.Register<ISmtpService, SmtpService>(Lifetime.Singleton);
            composition.Register<INewsService, NewsService>(Lifetime.Request);
        }
    }
}
