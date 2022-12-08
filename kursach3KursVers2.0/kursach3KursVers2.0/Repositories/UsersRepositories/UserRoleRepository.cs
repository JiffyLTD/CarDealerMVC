using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.UsersRepositories
{
    public class UserRoleRepository : IRepository<UserRole>
    {
        private AppDbContext _appDbContext;

        public UserRoleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(UserRole item)
        {
            _appDbContext.userRoles.Add(item);
        }

        public void Delete(int id)
        {
            UserRole userRole = _appDbContext.userRoles.Find(id);
            if (userRole != null)
                _appDbContext.userRoles.Remove(userRole);
        }
        public IEnumerable<UserRole> GetAll()
        {
            return _appDbContext.userRoles;
        }

        public UserRole GetById(int id)
        {
            return _appDbContext.userRoles.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(UserRole item)
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
