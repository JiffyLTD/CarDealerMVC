using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarDescriptionRepository : IRepository<CarDescription>
    {
        AppDbContext _appDbContext;
        public CarDescriptionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarDescription item)
        {
            _appDbContext.carDescriptions.Add(item);
        }

        public void Delete(int id)
        {
            CarDescription carDescription = _appDbContext.carDescriptions.Find(id);
            if (carDescription != null)
                _appDbContext.carDescriptions.Remove(carDescription);
        }
        public IEnumerable<CarDescription> GetAll()
        {
            return _appDbContext.carDescriptions;
        }

        public CarDescription GetById(int id)
        {
            return _appDbContext.carDescriptions.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarDescription item)
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
