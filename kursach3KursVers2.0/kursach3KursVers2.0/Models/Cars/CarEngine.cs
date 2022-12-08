using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarEngine
    {
        [Key]
        public int CarEngineId { get; set; }   
        public int CarEnginePower { get; set; }
        public double CarEngineVolume { get; set; }
        [Required]
        public CarSpecification CarSpecification { get; set; }
    }
}
