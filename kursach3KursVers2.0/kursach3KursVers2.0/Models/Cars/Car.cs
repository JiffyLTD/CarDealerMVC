using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Dillers;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Cars
{
    public class Car
    {
        [Key]
        public int CarId { get; set; }
        public DateTime CarSaleDate { get; set; }
        //внешний ключ
        [ForeignKey("CarDescription")]
        public int CarDescriptionId { get; set; }
        //ссылка на внешний ключ
        public CarDescription CarDescription { get; set; }
        //внешний ключ
        [ForeignKey("CarSpecification")]
        public int CarSpecificationId { get; set; }
        //ссылка на внешний ключ
        public CarSpecification CarSpecification { get; set; }
        //внешний ключ
        [ForeignKey("CarLegalCharacteristics")]
        public int CarLegalCharacteristicsId { get; set; }
        //ссылка на внешний ключ
        public CarLegalCharacteristics CarLegalCharacteristics { get; set; }
        [Required]
        public ContractOfSale ContractOfSale { get; set; }
        //внешний ключ
        [ForeignKey("Diller")]
        public int DillerId { get; set; }
        //ссылка на внешний ключ
        public Diller Diller { get; set; }
    }
}
