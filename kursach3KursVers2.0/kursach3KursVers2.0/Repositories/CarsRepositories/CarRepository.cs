using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarRepository : IRepository<Car>
    {
        AppDbContext _appDbContext;
        public CarRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Car item)
        {
            _appDbContext.cars.Add(item);
        }

        public void Delete(int id)
        {
            Car car = _appDbContext.cars.Find(id);
            if (car != null)
                _appDbContext.cars.Remove(car);
        }
        public IEnumerable<Car> GetAll()
        {
            return _appDbContext.cars;
        }

        public Car GetById(int id)
        {
            return _appDbContext.cars.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Car item)
        {
            _appDbContext.Entry(item).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _appDbContext.Dispose();
                }
            }
            disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
