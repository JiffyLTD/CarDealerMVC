using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace kursach3KursVers2._0.Controllers
{
    public class ContractController : Controller
    {
        private IRepository<User> _userRepository;
        private IRepository<Diller> _dillerRepository;
        private IRepository<UserRole> _roleRepository;
        private IRepository<Car> _carRepository;
        private IRepository<CarBrand> _carBrandRepository;
        private IRepository<CarModel> _carModelRepository;

        public ContractController(IRepository<User> userRepository, IRepository<Diller> dillerRepository,
            IRepository<UserRole> roleRepository, IRepository<Car> carRepository, IRepository<CarBrand> carBrandRepository,
            IRepository<CarModel> carModelRepository)
        {
            _userRepository = userRepository;
            _dillerRepository = dillerRepository;
            _roleRepository = roleRepository;
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _carModelRepository = carModelRepository;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> NewContract(int id)
        {
            //ищем всех зарегестрированных клиентов
            var roleList = _roleRepository.GetAll().Where(x => x.RoleName == "Клиент").ToList();
            var userList = _userRepository.GetAll().ToList();
            List<User> listUserForViewModel = new();
            NewContractViewModel newContractViewModel = new();

            
            for(int i = 0;i < roleList.Count(); i++)
            {
                for(int j = 0; j < userList.Count(); j++)
                {
                    if (userList[j].RoleId == roleList[i].RoleId)
                    {
                        listUserForViewModel.Add(userList[j]);
                    }
                }
            }
            //ищем авто
           var thisCar = _carRepository.GetById(id);
            var brandCar = _carBrandRepository.GetById(thisCar.CarId).CarBrandName;
            var modelCar = _carModelRepository.GetById(thisCar.CarId).CarName;
            //ищем дилера
           var thisDealer = _dillerRepository.GetById(thisCar.DillerId);

            newContractViewModel.users = listUserForViewModel;
            newContractViewModel.dealer = thisDealer;
            newContractViewModel.car = thisCar;
            newContractViewModel.CarName = brandCar + " " + modelCar;

            return View(newContractViewModel);
        }
    }
}
