using System.ComponentModel.DataAnnotations;

namespace SystemIdentity.Core.ViewModels
{

    public class PasswordChangeModel
    {
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Şifre alanı boş bırakılamaz.")]
        [Display(Name = " YeniŞifre ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string OldPassword { get; set; } = null!;

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Yeni Şifre alanı boş bırakılamaz.")]
        [Display(Name = " YeniŞifre ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordNew { get; set; } = null!;

        [DataType(DataType.Password)]
        [Compare(nameof(PasswordNew),ErrorMessage ="Şifre Aynı Değildir.")]
        [Required(ErrorMessage = "Yeni Şifre alanı boş bırakılamaz.")]
        [Display(Name = " Yeni Şifre Tekrar :  ")]
        [MinLength(6, ErrorMessage = "Şifreniz en az 6 karakter olabilir")]
        public string PasswordNewConfirm { get; set; } = null!;
    }
}
