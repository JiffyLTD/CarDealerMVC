using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using System.Drawing;

namespace kursach3KursVers2._0.Controllers
{
    public class CarController : Controller
    {
        SignInManager<User> _signInManager;
        UserManager<User> _userManager;
        IRepository<Car> _carRepository;
        IRepository<CarBody> _carBodyRepository;
        IRepository<CarBrand> _carBrandRepository;
        IRepository<CarDescription> _carDescRepository;
        IRepository<CarEngine> _carEngineRepository;
        IRepository<CarLegalCharacteristics> _carLCRepository;
        IRepository<CarModel> _carModelRepository;
        IRepository<CarSpecification> _carSpecRepository;
        IRepository<CarTransmission> _carTransRepository;
        IRepository<UserRole> _roleRepository;
        IRepository<Seller> _sellerRepository;
        IRepository<Diller> _dillerRepository;
        AppDbContext _context;

        public CarController(SignInManager<User> signInManager, UserManager<User> userManager,
            IRepository<Car> carRepository, IRepository<CarBody> carBodyRepository,
            IRepository<CarBrand> carBrandRepository, IRepository<CarDescription> carDescRepository,
            IRepository<CarEngine> carEngineRepository, IRepository<CarLegalCharacteristics> carLCRepository,
            IRepository<CarModel> carModelRepository, IRepository<CarSpecification> carSpecRepository,
            IRepository<CarTransmission> carTransRepository, IRepository<UserRole> roleRepository, IRepository<Seller> sellerRepository,
            IRepository<Diller> dillerRepository, AppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _carRepository = carRepository;
            _carBodyRepository = carBodyRepository;
            _carBrandRepository = carBrandRepository;
            _carDescRepository = carDescRepository;
            _carEngineRepository = carEngineRepository;
            _carLCRepository = carLCRepository;
            _carModelRepository = carModelRepository;
            _carSpecRepository = carSpecRepository;
            _carTransRepository = carTransRepository;
            _roleRepository = roleRepository;
            _sellerRepository = sellerRepository;
            _dillerRepository = dillerRepository;
            _context = context;
        }

        public async Task<IActionResult> AllCars()
        {
            if (_signInManager.IsSignedIn(User))
            {
                User user = await _userManager.FindByNameAsync(User.Identity.Name);
                
                CarListViewModel allCarsViewModel = new CarListViewModel
                {
                    AllCars = _carRepository.GetAll(),
                    UserRole = _roleRepository.GetAll().First(x => x.RoleId == user.RoleId).RoleName,
                    CarSpec = _carSpecRepository.GetAll(),
                    CarBody = _carBodyRepository.GetAll(),
                    CarBrand = _carBrandRepository.GetAll(),
                    CarDescription = _carDescRepository.GetAll(),
                    CarEngine = _carEngineRepository.GetAll(),
                    CarLegalCharacteristics = _carLCRepository.GetAll(),
                    CarModel = _carModelRepository.GetAll(),
                    CarSpecification = _carSpecRepository.GetAll(),
                    CarTransmission = _carTransRepository.GetAll(),
                    contractOfSales = _context.contractsOfSales.ToList()
                };
                return View(allCarsViewModel);
            }
            else
            {
                CarListViewModel allCarsViewModel = new CarListViewModel
                {
                    AllCars = _carRepository.GetAll(),
                    CarSpec = _carSpecRepository.GetAll(),
                    CarBody = _carBodyRepository.GetAll(),
                    CarBrand = _carBrandRepository.GetAll(),
                    CarDescription = _carDescRepository.GetAll(),
                    CarEngine = _carEngineRepository.GetAll(),
                    CarLegalCharacteristics = _carLCRepository.GetAll(),
                    CarModel = _carModelRepository.GetAll(),
                    CarSpecification = _carSpecRepository.GetAll(),
                    CarTransmission = _carTransRepository.GetAll(),
                    contractOfSales = _context.contractsOfSales.ToList()
                };
                return View(allCarsViewModel);
            }
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddCar()
        {
            return View();
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddCar(CarAddViewModel carAddViewModel)
        {
            var check = _dillerRepository.GetAll().Where(x => x.DillerName == carAddViewModel.Brand).ToList();
            if (ModelState.IsValid && check.FirstOrDefault() != null)
            {
                CarBrand carBrand = new CarBrand
                {
                    CarBrandName = carAddViewModel.Brand
                };
                _carBrandRepository.Create(carBrand);
                _carBrandRepository.Save();

                CarModel carModel = new CarModel
                {
                    CarName = carAddViewModel.Model,
                    CarBrandId = _carBrandRepository.GetAll().Max(x => x.CarBrandId)
                };
                _carModelRepository.Create(carModel);
                _carModelRepository.Save();

                CarBody carBody = new CarBody
                {
                    CarBodyName = carAddViewModel.Body
                };
                _carBodyRepository.Create(carBody);
                _carBodyRepository.Save();

                CarEngine carEngine = new CarEngine
                {
                    CarEnginePower = carAddViewModel.Power,
                    CarEngineVolume = carAddViewModel.Volume
                };
                _carEngineRepository.Create(carEngine);
                _carEngineRepository.Save();

                CarTransmission carTransmission = new CarTransmission
                {
                    CarTransmissionName = carAddViewModel.Transmission
                };
                _carTransRepository.Create(carTransmission);
                _carTransRepository.Save();

                CarSpecification carSpecification = new CarSpecification
                {
                    CarModelId = _carModelRepository.GetAll().Max(x => x.CarModelId),
                    CarBodyId = _carBodyRepository.GetAll().Max(x => x.CarBodyId),
                    CarEngineId = _carEngineRepository.GetAll().Max(x => x.CarEngineId),
                    CarTransmissonId = _carTransRepository.GetAll().Max(x => x.CarTransmissionId),
                    CarMileage = carAddViewModel.CarMileage,
                    CarColor = carAddViewModel.Color
                };
                _carSpecRepository.Create(carSpecification);
                _carSpecRepository.Save();

                CarDescription carDescription = new CarDescription
                {
                    CarDesc = carAddViewModel.Desc,
                    CarPhotoPath = carAddViewModel.PhotoPath
                };
                _carDescRepository.Create(carDescription);
                _carDescRepository.Save();

                CarLegalCharacteristics carLegal = new CarLegalCharacteristics
                {
                    CarNumberOfOwners = carAddViewModel.NumberOfOwners,
                    CarYear = carAddViewModel.Year,
                    CarPrice = carAddViewModel.Price
                };
                _carLCRepository.Create(carLegal);
                _carLCRepository.Save();
               
                Car newCar = new Car
                {
                    CarSaleDate = DateTime.Now,
                    CarDescriptionId = _carDescRepository.GetAll().Max(x => x.CarDescriptionId),
                    CarLegalCharacteristicsId = _carLCRepository.GetAll().Max(x => x.CarLegalCharacteristicsId),
                    CarSpecificationId = _carSpecRepository.GetAll().Max(x => x.CarSpecificationId),
                    DillerId = check.FirstOrDefault().DillerId
                };
                _carRepository.Create(newCar);
                _carRepository.Save();
            }
            else
            {
                ModelState.AddModelError("AddCarError", "Дилер компании " + carAddViewModel.Brand + " не найден");
                return View(carAddViewModel);
            }
            return RedirectToAction("AllCars", "Car");
        }
        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {
            _carRepository.Delete(id);
            _carRepository.Save();

            _carLCRepository.Delete(id);
            _carLCRepository.Save();

            _carDescRepository.Delete(id);
            _carDescRepository.Save();

            _carSpecRepository.Delete(id);
            _carSpecRepository.Save();

            _carTransRepository.Delete(id);
            _carTransRepository.Save();

            _carEngineRepository.Delete(id);
            _carEngineRepository.Save();

            _carBodyRepository.Delete(id);
            _carBodyRepository.Save();

            _carModelRepository.Delete(id);
            _carModelRepository.Save();

            _carBrandRepository.Delete(id);
            _carBrandRepository.Save();
            return RedirectToAction("AllCars","Car");
        }
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EditCar(int id)
        {
            var thisCar = _carRepository.GetById(id);
            CarAddViewModel model = new CarAddViewModel 
            {
                CarId = id,
                Brand = _carBrandRepository.GetById(id).CarBrandName,
                Model = _carModelRepository.GetById(id).CarName,
                Body = _carBodyRepository.GetById(id).CarBodyName,
                Volume = _carEngineRepository.GetById(id).CarEngineVolume,
                Power = _carEngineRepository.GetById(id).CarEnginePower,
                CarMileage = _carSpecRepository.GetById(id).CarMileage,
                Color = _carSpecRepository.GetById(id).CarColor,
                Transmission = _carTransRepository.GetById(id).CarTransmissionName,
                Desc = _carDescRepository.GetById(id).CarDesc,
                PhotoPath = _carDescRepository.GetById(id).CarPhotoPath,
                NumberOfOwners = _carLCRepository.GetById(id).CarNumberOfOwners,
                Year = _carLCRepository.GetById(id).CarYear,
                Price = _carLCRepository.GetById(id).CarPrice,
            };
            return View(model);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> EditCar(CarAddViewModel editModel)
        {
            if (ModelState.IsValid)
            {
                Car thisCar = _carRepository.GetById(editModel.CarId);

                CarModel carModel = _carModelRepository.GetById(thisCar.CarId);
                carModel.CarName = editModel.Model;
                _carModelRepository.Update(carModel);
                _carModelRepository.Save();

                CarBody carBody = _carBodyRepository.GetById(thisCar.CarId);
                carBody.CarBodyName = editModel.Body;
                _carBodyRepository.Update(carBody);
                _carBodyRepository.Save();

                CarSpecification carSpecification = _carSpecRepository.GetById(thisCar.CarId);
                carSpecification.CarColor = editModel.Color;
                carSpecification.CarMileage = editModel.CarMileage;
                _carSpecRepository.Update(carSpecification);
                _carSpecRepository.Save();

                CarEngine carEngine = _carEngineRepository.GetById(thisCar.CarId);
                carEngine.CarEnginePower = editModel.Power;
                carEngine.CarEngineVolume = editModel.Volume;
                _carEngineRepository.Update(carEngine);
                _carEngineRepository.Save();

                CarTransmission carTransmission = _carTransRepository.GetById(thisCar.CarId);
                carTransmission.CarTransmissionName = editModel.Transmission;
                _carTransRepository.Update(carTransmission);
                _carTransRepository.Save();

                CarDescription carDescription = _carDescRepository.GetById(thisCar.CarId);
                carDescription.CarDesc = editModel.Desc;
                carDescription.CarPhotoPath = editModel.PhotoPath;
                _carDescRepository.Update(carDescription);
                _carDescRepository.Save();

                CarLegalCharacteristics carLegalCharacteristics = _carLCRepository.GetById(thisCar.CarId);
                carLegalCharacteristics.CarNumberOfOwners = editModel.NumberOfOwners;
                carLegalCharacteristics.CarYear = editModel.Year;
                carLegalCharacteristics.CarPrice = editModel.Price;
                _carLCRepository.Update(carLegalCharacteristics);
                _carLCRepository.Save();

                return RedirectToAction("AllCars","Car");
            }
            return View(editModel);  
        }
        [HttpGet]
        public async Task<IActionResult> ThisCar(int id)
        {
            var thisCar = _carRepository.GetById(id);
            CarAddViewModel model = new CarAddViewModel
            {
                CarId = id,
                Brand = _carBrandRepository.GetById(id).CarBrandName,
                Model = _carModelRepository.GetById(id).CarName,
                Body = _carBodyRepository.GetById(id).CarBodyName,
                Volume = _carEngineRepository.GetById(id).CarEngineVolume,
                Power = _carEngineRepository.GetById(id).CarEnginePower,
                CarMileage = _carSpecRepository.GetById(id).CarMileage,
                Color = _carSpecRepository.GetById(id).CarColor,
                Transmission = _carTransRepository.GetById(id).CarTransmissionName,
                Desc = _carDescRepository.GetById(id).CarDesc,
                PhotoPath = _carDescRepository.GetById(id).CarPhotoPath,
                NumberOfOwners = _carLCRepository.GetById(id).CarNumberOfOwners,
                Year = _carLCRepository.GetById(id).CarYear,
                Price = _carLCRepository.GetById(id).CarPrice,
                SaleDate = thisCar.CarSaleDate
            };
            return View(model);
        }
    }
}
