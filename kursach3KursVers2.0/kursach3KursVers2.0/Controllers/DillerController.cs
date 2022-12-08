using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.Repositories.CarsRepositories;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace kursach3KursVers2._0.Controllers
{
    public class DillerController : Controller
    {
        SignInManager<User> _signInManager;
        UserManager<User> _userManager;
        IRepository<Diller> _dillerRepository;
        IRepository<UserRole> _roleRepository;
        IRepository<Seller> _sellerRepository;
        IRepository<User> _userRepository;
        IRepository<Car> _carRepository;


        public DillerController(IRepository<Diller> dillerRepository, UserManager<User> userManager,
            SignInManager<User> signInManager, IRepository<UserRole> roleRepository,
            IRepository<Seller> sellerRepository, IRepository<User> userRepository, IRepository<Car> carRepository)
        {
            _dillerRepository = dillerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleRepository = roleRepository;
            _sellerRepository = sellerRepository;
            _userRepository = userRepository;
            _carRepository = carRepository;
        }

        public async Task<IActionResult> AllDillers()
        {
            if (_signInManager.IsSignedIn(User))
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                UserRole role = _roleRepository.GetById(user.RoleId);//ищем роль пользователя
                DillerListViewModel allDillersViewModel = new DillerListViewModel
                {
                    AllDillers = _dillerRepository.GetAll(),
                    UserRole = role.RoleName
                };
                return View(allDillersViewModel);
            }
            else
            {
                DillerListViewModel allDillersViewModel = new DillerListViewModel
                {
                    AllDillers = _dillerRepository.GetAll()
                };
                return View(allDillersViewModel);
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult AddDiller()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddDiller(DillerListViewModel dillerListViewModel)
        {
            var userRole = _roleRepository.GetAll().Where(x => x.RoleName == "Сотрудник " + dillerListViewModel.CompanyName).ToList();
            if (userRole.FirstOrDefault() != null)
            {
                Diller diller = new Diller
                {
                    DillerName = dillerListViewModel.CompanyName,
                    DillerINN = dillerListViewModel.CompanyINN,
                    DillerKPP = dillerListViewModel.CompanyKPP,
                    DillerEmail = dillerListViewModel.CompanyEmail,
                    DillerLogoPath = dillerListViewModel.CompanyLogo,
                    DillerPhoneForHelp = dillerListViewModel.CompanyPhone,
                    DillerManagerPhone = dillerListViewModel.CompanyManagerPhone
                };
                _dillerRepository.Create(diller);
                _dillerRepository.Save();

                var user = _userRepository.GetAll().Where(x => x.RoleId == userRole.FirstOrDefault().RoleId).ToList();
                var newDillerId = _dillerRepository.GetAll().Max(x => x.DillerId);
                Seller seller = new Seller
                {
                    DillerId = newDillerId,
                    UserId = user.FirstOrDefault().Id
                };
                _sellerRepository.Create(seller);
                _sellerRepository.Save();
            }
            else
            {
                ModelState.AddModelError("AddDillerError", "Не удалось найти работника данной компании в списке зарегестрированных пользователей");
                return View(dillerListViewModel);
            }
            return RedirectToAction("AllDillers", "Diller");
        }
        [Authorize]
        public async Task<IActionResult> DeleteDiller(int id)
        {
            if(_carRepository.GetAll().Where(x => x.DillerId == id).Count() > 0 || _sellerRepository.GetAll().Where(x => x.DillerId == id).Count() > 0)
            {   
                return RedirectToAction("AllDillers", "Diller");
            }
            else
            {
                var sellerId = _sellerRepository.GetAll().FirstOrDefault(x => x.DillerId == id);
                if (sellerId != null)
                {
                    _sellerRepository.Delete(sellerId.SellerId);
                    _sellerRepository.Save();
                }
                _dillerRepository.Delete(id);
                _dillerRepository.Save();
            }
            return RedirectToAction("AllDillers", "Diller");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditDiller(int id)
        {
            var diller = _dillerRepository.GetById(id);
            DillerListViewModel model = new DillerListViewModel {
                CompanyName = diller.DillerName,
                CompanyINN = diller.DillerINN,
                CompanyKPP = diller.DillerKPP,
                CompanyEmail = diller.DillerEmail,
                CompanyLogo = diller.DillerLogoPath,
                CompanyPhone = diller.DillerPhoneForHelp,
                CompanyManagerPhone = diller.DillerManagerPhone,
                DillerId = diller.DillerId
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditDiller(DillerListViewModel model)
        {
            if (ModelState.IsValid)
            {
                Diller diller = _dillerRepository.GetById(model.DillerId);
                diller.DillerName = model.CompanyName;
                diller.DillerINN = model.CompanyINN;
                diller.DillerKPP = model.CompanyKPP;
                diller.DillerEmail = model.CompanyEmail;
                diller.DillerLogoPath = model.CompanyLogo;
                diller.DillerPhoneForHelp = model.CompanyPhone;
                diller.DillerManagerPhone = model.CompanyManagerPhone;

                _dillerRepository.Update(diller);
                _dillerRepository.Save();

                return RedirectToAction("AllDillers", "Diller");
            }
            return View(model);
        }
    }
}
