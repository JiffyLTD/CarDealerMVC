using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Sellers;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Users
{
    public class User:IdentityUser
    {
        //атрибуты
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public DateTime RegisterDate { get; set; }
        //внешний ключ
        [ForeignKey("UserAddress")]
        public int AddressId { get; set; }
        //ссылка на таблицу
        public UserAddress UserAddress { get; set; }
        [ForeignKey("UserPassport")]
        public int PassportId { get; set; }
        public UserPassport UserPassport { get; set; }
        [ForeignKey("UserRole")]
        public int RoleId { get; set; }
        public UserRole UserRole { get; set; }
       [Required]
       public List<Buyer> Buyer { get; set; }
        [Required]
        public Seller Seller { get; set; }
    }
}
