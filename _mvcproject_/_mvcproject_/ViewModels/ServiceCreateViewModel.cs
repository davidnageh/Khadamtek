using System.ComponentModel.DataAnnotations;

namespace _mvcproject_.ViewModels
{
    public class ServiceCreateViewModel
    {
        [Required(ErrorMessage = "A Service Must Have A Name")]
        [MaxLength(50, ErrorMessage = "That is a long name for service must be less than 50")]
        public string Service_Name { get; set; }
        [Required(ErrorMessage = " A service mst have a logo")]
        //[RegularExpression(@"^\/servicelogos\/", ErrorMessage ="That ca not be the path for your logo" )]
        public IFormFile Image { get; set; }
    }
}
