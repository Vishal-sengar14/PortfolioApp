using System.ComponentModel.DataAnnotations;

namespace PortfolioApp.ViewModels
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "Please enter your name."), MaxLength(100)]
        [Display(Name = "Full Name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your email."), EmailAddress, MaxLength(150)]
        public string Email { get; set; } = string.Empty;

        [Phone, MaxLength(20)]
        public string? Phone { get; set; }

        [Required(ErrorMessage = "Please enter a subject."), MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        [Required(ErrorMessage = "Please enter your message."), MinLength(10, ErrorMessage = "Message must be at least 10 characters.")]
        public string Message { get; set; } = string.Empty;

        /// <summary>Simple math captcha, e.g. "5 + 3". Value must equal CaptchaAnswer.</summary>
        [Required(ErrorMessage = "Please answer the captcha.")]
        public int CaptchaInput { get; set; }

        public int CaptchaFirstNumber { get; set; }
        public int CaptchaSecondNumber { get; set; }
    }
}
