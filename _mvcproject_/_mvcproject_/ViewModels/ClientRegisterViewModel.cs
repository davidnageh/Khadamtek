using System.ComponentModel.DataAnnotations;

namespace _mvcproject_.Models
{
    public class ClientRegisterViewModel
    {
        [Required(ErrorMessage = "First name is required")]
        [MaxLength(50, ErrorMessage = "first name too long")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "last name is required")]
        [MaxLength(50, ErrorMessage = "last name too long")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "gender is required")]
        [EnumDataType(typeof(Gender))]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "phone is required")]
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Mobile Number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [RegularExpression(@"[0-9a-zA-Z_]+@[a-zA-z]+.com", ErrorMessage = "Enter a valid email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Service")]
        public int Service_id { get; set; }

        
        [Required(ErrorMessage = "national ID is required")]
        [RegularExpression(@"[0-9]{14}", ErrorMessage = "Enter a valid national id")]
        [Display(Name = "National Id")]
        public string NationalId { get; set; }
        [Display(Name = "National ID Image")]

        [Required(ErrorMessage = "Must Specify a price range")]
        public double? PriceStart { get; set; }
        [Required(ErrorMessage = "Must Specify a price range")]
        public double? PriceEnd { get; set; }
        public IFormFile nationalidimage { get; set; }
        [Display(Name = "Previous Work Image")]
        public List<IFormFile> workimage { get; set; }

        public double? latitude { get; set; }

        public double? longitude { get; set; }
    }
}
