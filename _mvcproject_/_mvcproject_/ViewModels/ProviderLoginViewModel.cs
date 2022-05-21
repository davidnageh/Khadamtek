using System.ComponentModel.DataAnnotations;

namespace _mvcproject_.Models
{
    public class ProviderLoginViewModel
    {
        [Required(ErrorMessage = "username is required")]
        [Display(Name = "User Name")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
    }
}
