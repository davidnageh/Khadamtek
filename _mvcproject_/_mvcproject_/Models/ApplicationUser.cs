using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _mvcproject_.Models
{
    public enum Gender { Female , Male}
    public class ApplicationUser : IdentityUser
    {
        [MaxLength(50, ErrorMessage ="first name too long")]
        [Display(Name ="First Name")]
        public string? FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "last name too long")]
        [Display(Name = "Last Name")]
        public string? LastName { get; set; }
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }
        //[Required(ErrorMessage = "National Id is required")]
        [RegularExpression(@"[0-9]{14}",ErrorMessage ="Enter a valid national id")]
        [Display(Name = "National ID")]
        public string? NationalId { get; set; }
        [RegularExpression(@"[0-9]{11}", ErrorMessage = "Enter a valid phone number")]
        [Display(Name = "Mobile Number")]
        public string? PhoneNumber { get; set; }
        public byte? IsAccepted { get; set; }

        public byte? discriminator { get; set; }
         public string? ProfilePhoto { get; set; }
        public string? NationalIdPhoto { get; set; }

        [ForeignKey("Service")]
        public int? Service_Id { get; set; }
        public double? latitude { get; set; }        
        public double? longitude { get; set; }  
        public double? PriceStart { get; set; }
        public double? PriceEnd { get; set; }



       // [ForeignKey("Service_Id")]
        public virtual Service? Service { get; set; }
    }
}

