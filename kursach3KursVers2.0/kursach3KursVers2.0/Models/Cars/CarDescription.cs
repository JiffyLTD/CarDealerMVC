using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Cars
{
    public class CarDescription
    {
        [Key]
        public int CarDescriptionId { get; set; }
        public string? CarDesc { get; set; }
        public string? CarPhotoPath { get; set; }
        [Required]
        public Car Car { get; set; }

    }
}
