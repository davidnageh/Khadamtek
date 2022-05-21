using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _mvcproject_.Models
{
    public class Order
    {
        [Key]
     // public string Order_Id { get; set; } 
     public int Id { get; set; }
        public string Client_Id { get; set; }
        public string Provider_Id { get; set; }
        public byte?Rating { get; set; }  
        [MaxLength(200,ErrorMessage ="please enter a complaint in less than 200 characters")]
        public string?Complaint { get; set; }
        public byte IsAccepted { get; set; }
        [MaxLength(200, ErrorMessage = "please enter a message in less than 200 characters")]
        public string?Message { get; set; }
        [Required(ErrorMessage ="You must specify a price")]
        public double price { get; set; }

        [ForeignKey("Client_Id")]
        public ApplicationUser User { get; set; }
        [ForeignKey("Provider_Id")]
        public ApplicationUser Provider { get; set; }
    }
}
