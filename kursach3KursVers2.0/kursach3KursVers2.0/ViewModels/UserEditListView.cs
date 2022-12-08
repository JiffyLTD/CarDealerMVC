using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.ViewModels
{
    public class UserEditListView
    {
        [Required(ErrorMessage = "Необходимо заполнить")]
        public string? UserRole { get; set; }
        public string? UserId { get; set; } 
    }
}
