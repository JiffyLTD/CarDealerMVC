using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.ViewModels
{
    public class UsersListViewModel
    {
        public IEnumerable<User> users { get; set; }
        public IEnumerable<UserRole> userRoles { get; set; }
    }
}
