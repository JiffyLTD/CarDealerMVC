using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Cars;

namespace kursach3KursVers2._0.Repositories.BuyersRepositories
{
    public class BuyerRepository : IRepository<Buyer>
    {
        AppDbContext _appDbContext;
        public BuyerRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(Buyer item)
        {
            _appDbContext.buyers.Add(item);
        }

        public void Delete(int id)
        {
            Buyer buyer = _appDbContext.buyers.Find(id);
            if (buyer != null)
                _appDbContext.buyers.Remove(buyer);
        }
        public IEnumerable<Buyer> GetAll()
        {
            return _appDbContext.buyers;
        }

        public Buyer GetById(int id)
        {
            return _appDbContext.buyers.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(Buyer item)
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
