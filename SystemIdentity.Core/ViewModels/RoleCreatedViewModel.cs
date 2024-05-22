using System.ComponentModel.DataAnnotations;

namespace SystemIdentity.Core.ViewModels
{

    public class RoleCreatedViewModel
    {
        [Required(ErrorMessage = "Role ismi boş bırakılamnaz boş bırakılamaz.")]
        [Display(Name = " Role ismi ")]
        public string Name { get; set; }
    }
}
