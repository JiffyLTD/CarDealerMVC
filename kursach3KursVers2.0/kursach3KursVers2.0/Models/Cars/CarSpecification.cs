using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarSpecification
    {
        [Key]
        public int CarSpecificationId { get; set; }
        public int? CarMileage { get; set; }
        public string? CarColor { get; set; }
        //внешний ключ
        [ForeignKey("CarModel")]
        public int CarModelId { get; set; }
        //ссылка на внешний ключ
        public CarModel CarModel { get; set; }
        //внешний ключ
        [ForeignKey("CarBody")]
        public int CarBodyId { get; set; }
        //ссылка на внешний ключ
        public CarBody CarBody { get; set; }
        //внешний ключ
        [ForeignKey("CarEngine")]
        public int CarEngineId { get; set; }
        //ссылка на внешний ключ
        public CarEngine CarEngine { get; set; }
        //внешний ключ
        [ForeignKey("CarTransmission")]
        public int CarTransmissonId { get; set; }
        //ссылка на внешний ключ
        public CarTransmission CarTransmission { get; set; }
        [Required]
        public Car Car { get; set; }    
    }
}
