using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarLegalCharacteristics
    {
        [Key]
        public int CarLegalCharacteristicsId {get;set;}
        public int CarNumberOfOwners { get;set;}
        public int CarYear { get; set; }
        public double CarPrice { get; set; }
        [Required]
        public Car Car { get; set; }
    }
}
