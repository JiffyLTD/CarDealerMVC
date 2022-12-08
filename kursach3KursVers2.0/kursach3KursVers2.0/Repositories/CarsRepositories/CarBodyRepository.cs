using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarBodyRepository : IRepository<CarBody>
    {
        AppDbContext _appDbContext;
        public CarBodyRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarBody item)
        {
            _appDbContext.carBodies.Add(item);
        }

        public void Delete(int id)
        {
            CarBody carBody = _appDbContext.carBodies.Find(id);
            if (carBody != null)
                _appDbContext.carBodies.Remove(carBody);
        }
        public IEnumerable<CarBody> GetAll()
        {
            return _appDbContext.carBodies;
        }

        public CarBody GetById(int id)
        {
            return _appDbContext.carBodies.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarBody item)
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
