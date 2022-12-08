using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Users
{
    public class UserPassport
    {
        //атрибуты
        [Key]
        public int PassportId { get; set; }
        public string? NumAndSeries { get; set; }
        public string? DateOfIssue { get; set; }
        public string? IssuedOfWhom { get; set; }
        //ссылка на внешний ключ
        [Required]
        public User User { get; set; }
    }
}
