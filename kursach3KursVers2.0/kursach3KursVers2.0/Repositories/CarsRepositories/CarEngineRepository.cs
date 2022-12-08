using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarEngineRepository : IRepository<CarEngine>
    {
        AppDbContext _appDbContext;
        public CarEngineRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarEngine item)
        {
            _appDbContext.carEngines.Add(item);
        }

        public void Delete(int id)
        {
            CarEngine carEngine = _appDbContext.carEngines.Find(id);
            if (carEngine != null)
                _appDbContext.carEngines.Remove(carEngine);
        }
        public IEnumerable<CarEngine> GetAll()
        {
            return _appDbContext.carEngines;
        }

        public CarEngine GetById(int id)
        {
            return _appDbContext.carEngines.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarEngine item)
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
