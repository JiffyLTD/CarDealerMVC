using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.CarsRepositories
{
    public class CarTransmissionRepository : IRepository<CarTransmission>
    {
        AppDbContext _appDbContext;
        public CarTransmissionRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(CarTransmission item)
        {
            _appDbContext.carTransmissions.Add(item);
        }

        public void Delete(int id)
        {
            CarTransmission carTransmission = _appDbContext.carTransmissions.Find(id);
            if (carTransmission != null)
                _appDbContext.carTransmissions.Remove(carTransmission);
        }
        public IEnumerable<CarTransmission> GetAll()
        {
            return _appDbContext.carTransmissions;
        }

        public CarTransmission GetById(int id)
        {
            return _appDbContext.carTransmissions.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(CarTransmission item)
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
