using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _mvcproject_.Models
{
    public class Service
    {
        [Key]
        public int Service_Id { get; set; }
        [Required(ErrorMessage = "A Service Must Have A Name")]
        [MaxLength(50 , ErrorMessage = "That is a long name for service must be less than 50")]
        public string Service_Name { get; set; }
         [Required(ErrorMessage = " A service mst have a logo")]
        //[RegularExpression(@"^\/servicelogos\/", ErrorMessage ="That ca not be the path for your logo" )]
        public string ImgPath { get; set; }
    }
}
