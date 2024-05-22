using System.ComponentModel.DataAnnotations;

namespace SystemIdentity.Core.ViewModels


{
    public class RoleUpdateModel
    {
        public string Id { get; set; } = null!;

        [Required(ErrorMessage = "Role isim alanı boş bırakılamaz.")]
        [Display(Name = " Role İsmi ")]
        public string Name { get; set; } = null!;
    }
}
