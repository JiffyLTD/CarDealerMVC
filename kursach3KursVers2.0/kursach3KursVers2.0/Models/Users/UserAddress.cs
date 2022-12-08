using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Users
{
    public class UserAddress
    {
        //атрибуты
        [Key]
        public int AddressId { get; set; } 
        public string? Country { get; set; } 
        public string? City { get; set; } 
        public string? Street { get; set; } 
        public int? HouseNum { get; set; } 
        public int? RoomNum { get; set; }
        //ссылка на внешний ключ
        [Required]
        public User User { get; set; } 
    }
}
