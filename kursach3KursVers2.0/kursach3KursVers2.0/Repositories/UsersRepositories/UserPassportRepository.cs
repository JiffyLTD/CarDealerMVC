using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.UsersRepositories
{
    public class UserPassportRepository : IRepository<UserPassport>
    {
        private AppDbContext _appDbContext;

        public UserPassportRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(UserPassport item)
        {
            _appDbContext.userPassports.Add(item);
        }

        public void Delete(int id)
        {
            UserPassport userPassport = _appDbContext.userPassports.Find(id);
            if (userPassport != null)
                _appDbContext.userPassports.Remove(userPassport);
        }

        public IEnumerable<UserPassport> GetAll()
        {
            return _appDbContext.userPassports;
        }

        public UserPassport GetById(int id)
        {
            return _appDbContext.userPassports.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(UserPassport item)
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
