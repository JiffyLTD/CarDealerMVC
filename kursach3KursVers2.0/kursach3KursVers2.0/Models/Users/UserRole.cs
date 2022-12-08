using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Users
{
    public class UserRole
    {
        //атрибуты
        [Key]
        public int RoleId { get; set; }
        public string? RoleName { get; set; }
        //ссылка на внешний ключ
        [Required]
        public User User { get; set; }
    }
}
