using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.Repositories.UsersRepositories;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace kursach3KursVers2._0.Controllers
{
    public class UsersListController : Controller
    {
        private UserManager<User> _userManager;
        private IRepository<UserRole> _userRoleRepository;
        private IRepository<UserPassport> _userPassportRepository;
        private IRepository<UserAddress> _userAddressRepository;
        private IRepository<User> _userRepository;
        private IRepository<Seller> _sellerRepository;
        private IRepository<Diller> _dillerRepository;
        private IRepository<Car> _carRepository;

        public UsersListController(UserManager<User> userManager, IRepository<UserRole> userRoleRepository,
            IRepository<UserPassport> userPassportRepository, IRepository<UserAddress> userAddressRepository,
            IRepository<User> userRepository, IRepository<Seller> sellerRepository, IRepository<Diller> dillerRepository,
            IRepository<Car> carRepository)
        {
            _userManager = userManager;
            _userRoleRepository = userRoleRepository;
            _userPassportRepository = userPassportRepository;
            _userAddressRepository = userAddressRepository;
            _userRepository = userRepository;
            _sellerRepository = sellerRepository;
            _dillerRepository = dillerRepository;
            _carRepository = carRepository;
        }
        [Authorize]
        public IActionResult UsersList()
        {
            var usersList = _userRepository.GetAll().ToList();
            var usersRoles = _userRoleRepository.GetAll().ToList();
            UsersListViewModel usersListModel = new UsersListViewModel
            {
                users = usersList,
                userRoles = usersRoles
            };
            return View(usersListModel);
        }
        [Authorize]
        public async Task<IActionResult> DeleteUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);

            Seller seller = _sellerRepository.GetAll().Where(x => x.UserId == user.Id).First();
            if(seller != null)
            {
                _sellerRepository.Delete(seller.SellerId);
                _sellerRepository.Save();

                var carList = _carRepository.GetAll().Where(x => x.DillerId == seller.DillerId);
                if(carList.Count() > 0)
                {
                    foreach(var car in carList)
                    {
                        _carRepository.Delete(car.CarId);
                    }
                }

                Diller diller = _dillerRepository.GetById(seller.DillerId);
                if(diller != null)
                {
                    _dillerRepository.Delete(diller.DillerId);
                    _dillerRepository.Save();
                }
               
            }
         
            var result = await _userManager.DeleteAsync(user);
            _userAddressRepository.Delete(user.AddressId);
            _userAddressRepository.Save();
            _userRoleRepository.Delete(user.RoleId);
            _userRoleRepository.Save();
            _userPassportRepository.Delete(user.PassportId);
            _userPassportRepository.Save();
            if (result.Succeeded)
            {
                return RedirectToAction("UsersList", "UsersList");
            }
            else
            {
                foreach(var error in result.Errors)
                {
                    ModelState.AddModelError("DeleteUserError", error.ToString());
                }
            }
            return RedirectToAction("UsersList","UsersList");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditUser(string id)
        {
            User user = await _userManager.FindByIdAsync(id);
            UserRole userRole = _userRoleRepository.GetById(user.RoleId);
            UserEditListView model = new UserEditListView
            {
                UserRole = userRole.RoleName,
                UserId = user.Id
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditUser(UserEditListView model)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByIdAsync(model.UserId);
                UserRole userRole = _userRoleRepository.GetById(user.RoleId);
                userRole.RoleName = model.UserRole;
                _userRoleRepository.Update(userRole);
                _userRoleRepository.Save();
                return RedirectToAction("UsersList", "UsersList");
            }
            return View(model);
        }
    }
}
