using kursach3KursVers2._0.Models.Users;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarModel
    {
        [Key]
        public int CarModelId { get; set; }
        public string? CarName { get; set; }
        //внешний ключ
        [ForeignKey("CarBrand")]
        public int CarBrandId { get; set; }
        //ссылка на внешний ключ
        public CarBrand CarBrand { get; set; }
        [Required]
        public CarSpecification CarSpecification { get; set; }

    }
}
