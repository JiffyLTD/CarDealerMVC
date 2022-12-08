using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarBrand
    {
        //атрибуты
        [Key]
        public int CarBrandId { get; set; }
        public string? CarBrandName { get; set; }
        //ссылка на внешнюю таблицу
        [Required]
        public CarModel CarModel { get; set; }
    }
}
