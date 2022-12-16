using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;

namespace kursach3KursVers2._0.ViewModels
{
    public class CarListViewModel
    {
        public IEnumerable<CarSpecification> CarSpec { get; set; }
        public string UserRole { get; set; }
        public IEnumerable<Car>? AllCars { get; set; }
        public IEnumerable<CarBody>? CarBody { get; set; }
        public IEnumerable<CarBrand>? CarBrand { get; set; }
        public IEnumerable<CarDescription>? CarDescription { get; set; }
        public IEnumerable<CarEngine>? CarEngine { get; set; }
        public IEnumerable<CarLegalCharacteristics>? CarLegalCharacteristics { get; set; }
        public IEnumerable<CarModel>? CarModel { get; set; }
        public IEnumerable<CarSpecification>? CarSpecification { get; set; }
        public IEnumerable<CarTransmission>? CarTransmission { get; set; }

        public List<ContractOfSale> contractOfSales { get; set; }
    }
}
