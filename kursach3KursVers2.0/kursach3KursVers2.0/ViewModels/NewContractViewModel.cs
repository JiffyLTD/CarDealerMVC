using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.ViewModels
{
    public class NewContractViewModel
    {
        public List<User> users { get; set; }  
        public string dealer { get; set; }   
        public Car car { get; set; }   
        public string CarName { get; set; }   
        public string UserId { get; set; }
    }
}
