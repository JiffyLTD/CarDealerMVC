using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace kursach3KursVers2._0.Controllers
{
    public class ProfileController : Controller
    {
        private readonly UserManager<User> _userManager;
        private IRepository<UserPassport> _userPassport;
        private IRepository<UserRole> _userRole;
        private IRepository<UserAddress> _userAddress;

        public ProfileController(UserManager<User> userManager,IRepository<UserPassport> userPassport,
            IRepository<UserRole> userRole, IRepository<UserAddress> userAddress)
        {
            _userManager = userManager;
            _userPassport = userPassport;
            _userRole = userRole;
            _userAddress = userAddress;
        }
        [Authorize]
        public async Task<ActionResult> UserProfile()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);//ищем пользователя
            UserPassport passport = _userPassport.GetById(user.PassportId);//ищем пасспорт пользователя
            UserAddress address = _userAddress.GetById(user.AddressId);//ищем адрес пользователя
            UserRole role = _userRole.GetById(user.RoleId);//ищем роль пользователя
            if (user != null)
            {
                ProfileViewModel model = new ProfileViewModel 
                { //создаем модель вывода данных 
                    FirstName  = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    ProfilePhotoPath = user.ProfilePhotoPath,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RegisterDate = user.RegisterDate,
                    NumAndSeriesPassport = passport.NumAndSeries,
                    DateOfIssuePassport = passport.DateOfIssue,
                    IssuedByWhomPassport = passport.IssuedOfWhom,
                    UserRole = role.RoleName,
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street,
                    HouseNum = address.HouseNum,
                    RoomNum = address.RoomNum
                };
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        [Authorize]
        public async Task<ActionResult> UserEdit()
        {
            User user = await _userManager.FindByNameAsync(User.Identity.Name);//ищем пользователя
            UserPassport passport = _userPassport.GetById(user.PassportId);//ищем пасспорт пользователя
            UserAddress address = _userAddress.GetById(user.AddressId);//ищем адрес пользователя
            UserRole role = _userRole.GetById(user.RoleId);//ищем роль пользователя

            if (user != null)
            {
                ProfileViewModel model = new ProfileViewModel
                { //создаем модель вывода данных 
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Patronymic = user.Patronymic,
                    ProfilePhotoPath = user.ProfilePhotoPath,
                    UserName = user.UserName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    RegisterDate = user.RegisterDate,
                    NumAndSeriesPassport = passport.NumAndSeries,
                    DateOfIssuePassport = passport.DateOfIssue,
                    IssuedByWhomPassport = passport.IssuedOfWhom,
                    UserRole = role.RoleName,
                    Country = address.Country,
                    City = address.City,
                    Street = address.Street,
                    HouseNum = address.HouseNum,
                    RoomNum = address.RoomNum
                };
                return View(model);
            }
            return RedirectToAction("Login", "Home");
        }
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> UserEdit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);//ищем пользователя
                UserPassport passport = _userPassport.GetById(user.PassportId);//ищем пасспорт пользователя
                UserAddress address = _userAddress.GetById(user.AddressId);//ищем адрес пользователя
                UserRole role = _userRole.GetById(user.RoleId);//ищем роль пользователя

                //userUpdate
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Patronymic = model.Patronymic;
                user.PhoneNumber = model.PhoneNumber;
                user.ProfilePhotoPath = model.ProfilePhotoPath;
                await _userManager.UpdateAsync(user);
                //userPassUpdate
                passport.NumAndSeries = model.NumAndSeriesPassport;
                passport.DateOfIssue = model.DateOfIssuePassport;
                passport.IssuedOfWhom = model.IssuedByWhomPassport;
                _userPassport.Update(passport);
                _userPassport.Save();
                //userAddressUpdate
                address.Country = model.Country;
                address.City = model.City;
                address.Street = model.Street;
                address.HouseNum = model.HouseNum;
                address.RoomNum = model.RoomNum;
                _userAddress.Update(address);
                _userAddress.Save();

                return RedirectToAction("UserProfile", "Profile");
            }
            return View(model);
        }
    }
}
