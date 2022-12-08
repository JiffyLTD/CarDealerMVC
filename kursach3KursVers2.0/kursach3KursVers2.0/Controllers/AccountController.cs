using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace kursach3KursVers2._0.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private IRepository<UserPassport> _userPassportRepository;
        private IRepository<UserAddress> _userAddressRepository;
        private IRepository<UserRole> _userRoleRepository;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, 
                                 IRepository<UserPassport> userPassportRepository, IRepository<UserAddress> userAddressRepository,
                                 IRepository<UserRole> userRoleRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userPassportRepository = userPassportRepository;
            _userAddressRepository = userAddressRepository;
            _userRoleRepository = userRoleRepository;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                UserPassport userPassport = new UserPassport();//создаем новый паспорт
                UserAddress userAddress = new UserAddress();//создаем новый адрес
                UserRole userRole;
                if (_userRoleRepository.GetAll().ToList().Count() <= 0)
                {
                    userRole = new UserRole { RoleName = "Администратор" };//создаем новую роль
                    _userRoleRepository.Create(userRole);//добавляем в БД новую роль
                    _userRoleRepository.Save();//сохраянем новую роль
                }
                else
                {
                    userRole = new UserRole { RoleName = "Клиент" };//создаем новую роль
                    _userRoleRepository.Create(userRole);//добавляем в БД новую роль
                    _userRoleRepository.Save();//сохраянем новую роль
                }
                _userAddressRepository.Create(userAddress);//добавляем в БД новый адрес
                _userAddressRepository.Save();//сохраянем новый адрес
                _userPassportRepository.Create(userPassport);//добавляем в БД новый паспорт
                _userPassportRepository.Save();//сохраянем новый паспорт

                var newRoleId = _userRoleRepository.GetAll().Max(x => x.RoleId);//ищем ID новой роли для нового пользователя
                var newAddressId = _userAddressRepository.GetAll().Max(x => x.AddressId);//ищем ID нового адреса для нового пользователя
                var newPassportId = _userPassportRepository.GetAll().Max(x => x.PassportId);//ищем ID нового пасспорта для нового пользователя

                User user = new User {         //создаем нового пользователя
                    UserName = model.UserName,
                    Email = model.Email, 
                    RegisterDate = DateTime.Now, 
                    RoleId = newRoleId,
                    PassportId = newPassportId,
                    AddressId = newAddressId,
                    ProfilePhotoPath = "kot.png"
                };
                var result = await _userManager.CreateAsync(user, model.Password);//добавляем нового пользователя в БД

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);//устанавливаем куки
                    return RedirectToAction("Main", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("RegisterError", error.Description);  //ошибка регистрации
                    }
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);//проверка параметров входа
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Main", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("LoginError", "Неправильный логин и (или) пароль");//вывод ошибки входа
                }
            }
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();//Удаляем куки
            return RedirectToAction("Main", "Home");
        }
    }
}
