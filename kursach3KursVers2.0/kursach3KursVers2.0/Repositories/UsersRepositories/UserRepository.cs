using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.UsersRepositories
{
    public class UserRepository : IRepository<User>

    {
        private AppDbContext _appDbContext;

        public UserRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public void Create(User item)
        {
            _appDbContext.Users.Add(item);
        }

        public void Delete(int id)
        {
            User user = _appDbContext.Users.Find(id);
            if (user != null)
                _appDbContext.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return _appDbContext.Users;
        }

        public User GetById(int id)
        {
            return _appDbContext.Users.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(User item)
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
