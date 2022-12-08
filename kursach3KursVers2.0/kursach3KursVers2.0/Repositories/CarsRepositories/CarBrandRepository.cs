using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarBrandRepository : IRepository<CarBrand>
    {
        AppDbContext _appDbContext;
        public CarBrandRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarBrand item)
        {
            _appDbContext.carBrands.Add(item);
        }

        public void Delete(int id)
        {
            CarBrand carBrand = _appDbContext.carBrands.Find(id);
            if (carBrand != null)
                _appDbContext.carBrands.Remove(carBrand);
        }
        public IEnumerable<CarBrand> GetAll()
        {
            return _appDbContext.carBrands;
        }

        public CarBrand GetById(int id)
        {
            return _appDbContext.carBrands.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarBrand item)
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
