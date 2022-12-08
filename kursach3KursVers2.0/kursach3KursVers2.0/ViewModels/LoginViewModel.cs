using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace kursach3KursVers2._0.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Проверьте заполненность логина или пароля")]
        [Display(Name = "UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Проверьте заполненность логина или пароля")]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }
        [Display(Name = "Запомнить?")]
        public bool RememberMe { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
