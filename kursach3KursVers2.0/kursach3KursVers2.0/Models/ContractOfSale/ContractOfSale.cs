using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Sellers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.ContractsOfSale
{
    public class ContractOfSale
    {
        [Key]
        public int ContractOfSaleId { get; set; }
        public DateTime DateTimeContractOfSale { get; set; }
        //внешний ключ
        [ForeignKey("Car")]
        public int CarId { get; set; }
        //ссылка на внешний ключ
        public Car Car { get; set; }
        //внешний ключ
        [ForeignKey("Seller")]
        public int SellerId { get; set; }
        //ссылка на внешний ключ
        public Seller Seller { get; set; }
        //внешний ключ
        [ForeignKey("Buyer")]
        public int BuyerId { get; set; }
        //ссылка на внешний ключ
        public Buyer Buyer { get; set; } 
    }
}
