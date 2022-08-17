using System.Web;
using System.Web.Mvc;
using TestSite.Core.ViewModels;
using Umbraco.Web;
using Umbraco.Web.Mvc;
using TestSite.Core.Services;
using Umbraco.Core.Models.PublishedContent;
using System.Net;
using Newtonsoft.Json;
using TestSite.Core.Models;
using System.Configuration;
using Newtonsoft.Json.Linq;

namespace TestSite.Core.Controllers
{
    public class ContactSurfaceController : SurfaceController
    {
        private readonly ISmtpService _smtp;
        IVariationContextAccessor _variationContextAccessor;

        public ContactSurfaceController(ISmtpService smtp, IVariationContextAccessor variationContextAccessor)
        {
            _smtp = smtp;
            _variationContextAccessor = variationContextAccessor;
        }

        [HttpGet]
        public ActionResult RenderForm() => PartialView("~/Views/Partials/Contact/ContactForm.cshtml", new ContactViewModel { ContactFormId = CurrentPage.Id });
        [HttpPost]
        public ActionResult RenderForm(ContactViewModel model) => PartialView("~/Views/Partials/Contact/ContactForm.cshtml", model);
        public ActionResult SubmitForm(ContactViewModel model)
        {
            bool sent = false;
            string response = Request["g-recaptcha-response"];
            string secretKey = ConfigurationManager.AppSettings["serverKey"];
            string endpoint = ConfigurationManager.AppSettings["reCaptchaEndpoint"];
            WebClient client = new WebClient();
            string result = client.DownloadString($"{endpoint}?secret={secretKey}&response={response}");
            ReCapthaResult capthaResult = JsonConvert.DeserializeObject<ReCapthaResult>(result);

            if (capthaResult.Success && ModelState.IsValid)
            {
                sent = _smtp.SendEmail(model);
            }

            _variationContextAccessor.VariationContext = new VariationContext(model.Culture);

            IPublishedContent contactPage = UmbracoContext.Content.GetById(false, model.ContactFormId);
            //var element = contactPage.Value<IEnumerable<IPublishedElement>>("contentElements").FirstOrDefault(x => x.ContentType.Alias == "sectionFormControls");
            var successMessage = contactPage.Value<IHtmlString>("successMessage");
            var errorMessage = contactPage.Value<IHtmlString>("errorMessage");


            JObject obj = (JObject)contactPage.Value("mainContent");
            foreach (var item in obj.GetValue("sections")[0]["rows"][0]["areas"][0]["controls"])
            {
                if (item["value"]["dtgeContentTypeAlias"].ToString() == "contactFormSection")
                {
                    successMessage = new HtmlString(item["value"]["value"]["successMessage"].ToString());
                    errorMessage = new HtmlString(item["value"]["value"]["errorMessage"].ToString());
                }
            }



            var element = contactPage.Value("errorMessage");

            return PartialView("~/Views/Partials/Contact/Result.cshtml", sent ? successMessage : errorMessage);
        }
    }
}
