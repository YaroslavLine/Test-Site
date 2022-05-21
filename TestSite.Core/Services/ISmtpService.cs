using ClientDependency.Core.Logging;
using TestSite.Core.ViewModels;

namespace TestSite.Core.Services
{
    public interface ISmtpService
    {
        bool SendEmail(ContactViewModel model);
    }
}
