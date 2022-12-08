using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Sellers
{
    public class Seller
    {
        [Key]
        public int SellerId { get; set; }
        //внешний ключ
        [ForeignKey("Diller")]
        public int DillerId { get; set; }
        //ссылка на внешний ключ
        public Diller Diller { get; set; }
        //внешний ключ
        [ForeignKey("User")]
        public string UserId { get; set; }
        //ссылка на внешний ключ
        public User User { get; set; }

        public List<ContractOfSale> contracts { get; set; }
    }
}
