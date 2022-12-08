using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Должно быть заполнено по форме 'name@example.com'")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить")]
        [RegularExpression(@"[A-Za-z0-9._%+-]{5,50}", ErrorMessage = "Длина логина должна быть от 5 до 50 символов, разрешенные символы 'A-Za-z0-9._%+-'")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить")]
        [StringLength(50,MinimumLength =5,ErrorMessage ="Длина пароля должна быть от 5 до 50 символов")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Необходимо заполнить")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        [Display(Name = "Подтвердить пароль")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }
}
