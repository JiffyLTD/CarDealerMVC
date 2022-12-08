using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.DillersRepositories
{
    public class DillerRepository : IRepository<Diller>
    {
        AppDbContext _appDbContext;
        public DillerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Diller item)
        {
            _appDbContext.dillers.Add(item);
        }

        public void Delete(int id)
        {
            Diller diller = _appDbContext.dillers.Find(id);
            if (diller != null)
                _appDbContext.dillers.Remove(diller);
        }
        public IEnumerable<Diller> GetAll()
        {
            return _appDbContext.dillers;
        }

        public Diller GetById(int id)
        {
            return _appDbContext.dillers.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Diller item)
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
