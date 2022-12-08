using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarSpecificRepository : IRepository<CarSpecification>
    {
        AppDbContext _appDbContext;
        public CarSpecificRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarSpecification item)
        {
            _appDbContext.carSpecifications.Add(item);
        }

        public void Delete(int id)
        {
            CarSpecification carSpecification = _appDbContext.carSpecifications.Find(id);
            if (carSpecification != null)
                _appDbContext.carSpecifications.Remove(carSpecification);
        }
        public IEnumerable<CarSpecification> GetAll()
        {
            return _appDbContext.carSpecifications;
        }

        public CarSpecification GetById(int id)
        {
            return _appDbContext.carSpecifications.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarSpecification item)
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
