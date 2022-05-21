using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _mvcproject_.Models
{
    public class ProviderWorks
    {

       public  string Provider_Id { get; set; }

        public  string ImgPath { get; set; }

        [ForeignKey("Provider_Id")]
        public virtual ApplicationUser provider { get; set; }

    }
}
