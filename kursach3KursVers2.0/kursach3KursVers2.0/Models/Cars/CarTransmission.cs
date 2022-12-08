using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarTransmission
    {
        [Key]
        public int CarTransmissionId { get; set; }  
        public string? CarTransmissionName { get; set; }
        [Required]
        CarSpecification CarSpecification { get; set; }
    }
}
