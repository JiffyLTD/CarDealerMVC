using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;

namespace kursach3KursVers2._0.Repositories.SellersRepositories
{
    public class SellerRepository : IRepository<Seller>
    {
        AppDbContext _appDbContext;
        public SellerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Seller item)
        {
            _appDbContext.sellers.Add(item);
        }

        public void Delete(int id)
        {
            Seller seller = _appDbContext.sellers.Find(id);
            if (seller != null)
                _appDbContext.sellers.Remove(seller);
        }
        public IEnumerable<Seller> GetAll()
        {
            return _appDbContext.sellers;
        }

        public Seller GetById(int id)
        {
            return _appDbContext.sellers.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Seller item)
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
