using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Users;

namespace kursach3KursVers2._0.Repositories.UsersRepositories
{
    public class UserAddressRepository : IRepository<UserAddress>
    {
        private AppDbContext _appDbContext;

        public UserAddressRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(UserAddress item)
        {
            _appDbContext.userAddresses.Add(item);
        }

        public void Delete(int id)
        {
            UserAddress userAddress = _appDbContext.userAddresses.Find(id);
            if (userAddress != null)
                _appDbContext.userAddresses.Remove(userAddress);
        }
        public IEnumerable<UserAddress> GetAll()
        {
            return _appDbContext.userAddresses;
        }

        public UserAddress GetById(int id)
        {
            return _appDbContext.userAddresses.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(UserAddress item)
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
