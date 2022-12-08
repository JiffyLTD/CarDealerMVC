using System.ComponentModel.DataAnnotations;

namespace kursach3KursVers2._0.ViewModels
{
    public class ProfileViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Patronymic { get; set; }
        public string? ProfilePhotoPath { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        [RegularExpression(@"([0-9]{11})", ErrorMessage = "Должно быть заполнено по форме '89998887766'")]
        public string? PhoneNumber { get; set; }       
        public DateTime RegisterDate { get; set; }
        [RegularExpression(@"([0-9]{4})+(\s{1})+([ 0-9]{6})", ErrorMessage = "Должно быть заполнено по форме 'сссс нннннн'")]
        public string? NumAndSeriesPassport { get; set; }
        public string? DateOfIssuePassport { get; set; }
        public string? IssuedByWhomPassport { get; set; }
        public string? UserRole { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
        public int? HouseNum { get; set; }
        public int? RoomNum { get; set; }
    }
}
