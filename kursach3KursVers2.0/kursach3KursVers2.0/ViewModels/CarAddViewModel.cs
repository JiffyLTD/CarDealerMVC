using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.ViewModels
{
    public class CarAddViewModel
    {
        public int CarId { get; set; }
        [Required(ErrorMessage ="Должно быть заполнено")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string Model { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string Body { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public double Volume { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public int Power { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public int? CarMileage { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string Color { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string Transmission { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string Desc { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public string PhotoPath { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public int NumberOfOwners { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public int Year { get; set; }
        [Required(ErrorMessage = "Должно быть заполнено")]
        public double Price { get; set; }
        public DateTime? SaleDate { get; set; }
    }
}
