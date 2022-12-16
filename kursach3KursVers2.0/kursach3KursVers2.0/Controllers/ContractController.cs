using Aspose.Words;
using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing;

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
        private AppDbContext _context;

        public ContractController(IRepository<User> userRepository, IRepository<Diller> dillerRepository,
            IRepository<UserRole> roleRepository, IRepository<Car> carRepository, IRepository<CarBrand> carBrandRepository,
            IRepository<CarModel> carModelRepository, AppDbContext context)
        {
            _userRepository = userRepository;
            _dillerRepository = dillerRepository;
            _roleRepository = roleRepository;
            _carRepository = carRepository;
            _carBrandRepository = carBrandRepository;
            _carModelRepository = carModelRepository;
            _context = context;
        }

        [Authorize]
        [HttpGet]
        public async Task<IActionResult> NewContract(int id)
        {
            //ищем всех зарегестрированных клиентов
            List<User> users = _context.users.Where(x => x.UserRole.RoleName == "Клиент").Include(x => x.UserRole).ToList();

            NewContractViewModel newContractViewModel = new();


            //ищем авто
           var thisCar = _carRepository.GetById(id);
            var brandCar = _carBrandRepository.GetById(thisCar.CarId).CarBrandName;
            var modelCar = _carModelRepository.GetById(thisCar.CarId).CarName;
            //ищем дилера
           var thisDealer = _dillerRepository.GetById(thisCar.DillerId).DillerName;

            newContractViewModel.users = users;
            newContractViewModel.dealer = thisDealer;
            newContractViewModel.car = thisCar;
            newContractViewModel.CarName = brandCar + " " + modelCar;

            return View(newContractViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> NewContract(NewContractViewModel newContractViewModel)
        {
            Buyer buyer = new()
            {
                UserId = newContractViewModel.UserId
            };
            _context.buyers.Add(buyer);
            _context.SaveChanges();

            var sellerId = _context.sellers.Where(x => x.Diller.DillerName == newContractViewModel.dealer).FirstOrDefault();
            
            ContractOfSale contractOfSale = new()
            {
                BuyerId = buyer.BuyerId,
                SellerId = sellerId.SellerId,
                CarId = newContractViewModel.car.CarId,
                DateTimeContractOfSale = DateTime.Now
            };
            _context.contractsOfSales.Add(contractOfSale);
            _context.SaveChanges();

            return RedirectToAction("AllCars","Car");
        }
        public IActionResult DownloadDoc(int id)
        {
            var contract = _context.contractsOfSales.Where(x => x.CarId == id).Include(x => x.Seller).ThenInclude(x => x.User).
                                                                                Include(x => x.Seller).ThenInclude(x => x.Diller).
                                                                                Include(x => x.Seller).ThenInclude(x => x.User).
                                                                                Include(x => x.Buyer).ThenInclude(x => x.User).
                                                                                Include(x => x.Car).ThenInclude(x => x.CarLegalCharacteristics).
                                                                                Include(x => x.Car).ThenInclude(x => x.CarSpecification).ThenInclude(x => x.CarEngine).
                                                                                Include(x => x.Car).ThenInclude(x => x.CarSpecification).ThenInclude(x => x.CarBody).
                                                                                Include(x => x.Car).ThenInclude(x => x.CarSpecification).ThenInclude(x => x.CarModel).
                                                                                ThenInclude(x => x.CarBrand).FirstOrDefault();

            GetNewContract(contract);
            string file_path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dkpNewDoc" + contract.Buyer.User.FirstName + ".jpg");

            var doc = new Document();
            var builder = new DocumentBuilder(doc);

            builder.InsertImage(file_path);

            string file_pathSave = Path.Combine(AppDomain.CurrentDomain.BaseDirectory);
            doc.Save(file_pathSave + "dkpNewDoc" + contract.Buyer.User.FirstName + ".pdf");

            string pdfDoc = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dkpNewDoc" + contract.Buyer.User.FirstName + ".pdf");

            return PhysicalFile(pdfDoc, "application/pdf", "dkpNewDoc" + contract.Buyer.User.FirstName + ".pdf");
        }
        private void GetNewContract(ContractOfSale conctractOfSale)
        {
            string lastBuyer = conctractOfSale.Buyer.User.LastName; 
            string firstNameBuyer = conctractOfSale.Buyer.User.FirstName;
            string? patronimycBuyer = conctractOfSale.Buyer.User.Patronymic;

            string dateOfContract = DateTime.Now.ToLongDateString();

            string lastNameEmployee = conctractOfSale.Seller.User.LastName;
            string firstNameEmployee = conctractOfSale.Seller.User.FirstName;
            string? patronimycEmployee = conctractOfSale.Seller.User.Patronymic;
            string dealerName = conctractOfSale.Seller.Diller.DillerName;

            string carBrand = conctractOfSale.Car.CarSpecification.CarModel.CarBrand.CarBrandName;
            string carModel = conctractOfSale.Car.CarSpecification.CarModel.CarName;
            string carType = conctractOfSale.Car.CarSpecification.CarBody.CarBodyName;
            string carEnginePower = conctractOfSale.Car.CarSpecification.CarEngine.CarEnginePower.ToString();
            string carEngineVolume = Math.Round(conctractOfSale.Car.CarSpecification.CarEngine.CarEngineVolume,2).ToString();
            string carDate = conctractOfSale.Car.CarLegalCharacteristics.CarYear.ToString();
            string carColor = conctractOfSale.Car.CarSpecification.CarColor;
            string carMilliage = conctractOfSale.Car.CarSpecification.CarMileage.ToString();
            string carPrice = conctractOfSale.Car.CarLegalCharacteristics.CarPrice.ToString();

            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dkp.jpg");

            Image a = Image.FromFile(filePath);

            Graphics part2 = Graphics.FromImage(a);
             
            //дата договора
            part2.DrawString(dateOfContract,

           new System.Drawing.Font("Arial", 10),

           new SolidBrush(Color.Black), new RectangleF(1000, 30, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));

            //место составление договора
            part2.DrawString("Центр продажи поддержанных авто",

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(500, 30, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));

            //покупатель
            part2.DrawString(lastBuyer,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(100, 270, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(firstNameBuyer,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(250, 270, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(patronimycBuyer,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(400, 270, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            //продавец
            part2.DrawString(dealerName,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(100, 140, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(lastNameEmployee,

            new  System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(250, 140, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(firstNameEmployee,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(400, 140, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(patronimycEmployee,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(550, 140, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            //машина
            part2.DrawString(carBrand,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(100, 500, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(carModel,

            new System.Drawing.Font("Arial", 12),

            new SolidBrush(Color.Black), new RectangleF(250, 500, 0, 0),

            new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(carType,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(550, 550, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(carEnginePower,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(520, 590, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(carEngineVolume,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(650, 590, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(carDate,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(900, 540, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(carColor,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(900, 590, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));
            part2.DrawString(carMilliage,

           new System.Drawing.Font("Arial", 12),

           new SolidBrush(Color.Black), new RectangleF(100, 590, 0, 0),

           new StringFormat(StringFormatFlags.NoWrap));

            part2.DrawString(carPrice,

         new System.Drawing.Font("Arial", 12),

         new SolidBrush(Color.Black), new RectangleF(550, 930, 0, 0),

         new StringFormat(StringFormatFlags.NoWrap));
            string filePathNew = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "dkpNewDoc" + firstNameBuyer + ".jpg");
            a.Save(filePathNew, System.Drawing.Imaging.ImageFormat.Jpeg);
        }
    }
}
