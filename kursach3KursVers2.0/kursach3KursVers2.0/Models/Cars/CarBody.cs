using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarBody
    {
        [Key]
        public int CarBodyId { get; set; } 
        public string? CarBodyName { get; set; }
        [Required]
        public CarSpecification CarSpecification { get; set; }
    }
}
