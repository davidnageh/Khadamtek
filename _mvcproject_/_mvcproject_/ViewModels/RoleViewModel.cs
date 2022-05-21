using System.ComponentModel.DataAnnotations;

namespace _mvcproject_.ViewModels
{
    public class RoleViewModel
    {
        [Required(ErrorMessage ="Role Name Is required")]
        [Display(Name = "Role Name ")]
        public string RoleName { get; set; }
    }
}
