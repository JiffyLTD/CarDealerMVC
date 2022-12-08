using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Buyers
{
    public class Buyer
    {
        [Key]
        public int BuyerId { get; set; }
        //внешний ключ
        [ForeignKey("User")]
        public string UserId { get; set; }
        //ссылка на внешний ключ
        public User User { get; set; }
        public List<ContractOfSale> ContractOfSale { get; set; }
    }
}
