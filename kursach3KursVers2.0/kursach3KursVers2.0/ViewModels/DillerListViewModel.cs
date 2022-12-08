using kursach3KursVers2._0.Models.Dillers;
using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.ViewModels
{
    public class DillerListViewModel
    {
        [Required(ErrorMessage = "Необходимо заполнить")]
        public string? CompanyName { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        public string? CompanyLogo { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        [RegularExpression(@"([0-9]{10})", ErrorMessage = "Должно быть заполнено по форме 'инниннинни'")]
        public string? CompanyINN { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        [RegularExpression(@"([0-9]{9})", ErrorMessage = "Должно быть заполнено по форме 'кппкппкпп'")]
        public string? CompanyKPP { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage = "Должно быть заполнено по форме 'name@example.com'")]
        public string? CompanyEmail{ get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        public string? CompanyPhone { get; set; }
        [Required(ErrorMessage = "Необходимо заполнить")]
        public string? CompanyManagerPhone { get; set; }


        public string? UserRole { get; set; }
        public int DillerId { get; set; }
        public IEnumerable<Diller>? AllDillers { get; set; }
    }
}
