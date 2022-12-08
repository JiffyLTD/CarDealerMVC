using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Sellers;
using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.Models.Dillers
{
    public class Diller
    {
        [Key]
        public int DillerId { get; set; }
        public string? DillerName { get; set; }
        public string? DillerLogoPath { get; set; }
        public string? DillerINN { get; set; }
        public string? DillerKPP { get; set; }
        public string? DillerEmail { get; set; }
        public string? DillerPhoneForHelp{ get; set; }
        public string? DillerManagerPhone { get; set; }
        public Seller Seller { get; set; }
        public List<Car> Car { get; set; }
    }
}
