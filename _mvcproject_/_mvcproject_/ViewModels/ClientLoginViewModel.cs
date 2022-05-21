using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;

namespace _mvcproject_.Models
{
    public class ClientLoginViewModel
    {
        [Required(ErrorMessage = "username is required")]
        [Display(Name ="User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}
