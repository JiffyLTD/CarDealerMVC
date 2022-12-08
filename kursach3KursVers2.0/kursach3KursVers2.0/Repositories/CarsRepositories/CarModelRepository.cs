using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarModelRepository : IRepository<CarModel>
    {
        AppDbContext _appDbContext;
        public CarModelRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarModel item)
        {
            _appDbContext.carModels.Add(item);
        }

        public void Delete(int id)
        {
            CarModel carModel = _appDbContext.carModels.Find(id);
            if (carModel != null)
                _appDbContext.carModels.Remove(carModel);
        }
        public IEnumerable<CarModel> GetAll()
        {
            return _appDbContext.carModels;
        }

        public CarModel GetById(int id)
        {
            return _appDbContext.carModels.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarModel item)
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
