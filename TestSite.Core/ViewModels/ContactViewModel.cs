using System.ComponentModel.DataAnnotations;

namespace TestSite.Core.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please enter your email")]
        [EmailAddress(ErrorMessage = "Email adress is incorrect")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please enter your company")]
        public string Company { get; set; }
        [Required(ErrorMessage = "Please enter your message")]
        [MaxLength(250, ErrorMessage = "Message must be no longer than 250 characters")]
        public string Message { get; set; }
        public int ContactFormId { get; set; }
        public string Culture { get; set; }
    }
}
