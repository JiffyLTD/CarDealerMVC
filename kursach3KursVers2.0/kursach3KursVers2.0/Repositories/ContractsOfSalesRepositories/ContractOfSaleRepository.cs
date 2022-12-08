using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.ContractsOfSale;

namespace kursach3KursVers2._0.Repositories.ContractsOfSalesRepositories
{
    public class ContractOfSaleRepository : IRepository<ContractOfSale>
    {
        AppDbContext _appDbContext;
        public ContractOfSaleRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void Create(ContractOfSale item)
        {
            _appDbContext.contractsOfSales.Add(item);
        }

        public void Delete(int id)
        {
            ContractOfSale contract = _appDbContext.contractsOfSales.Find(id);
            if (contract != null)
                _appDbContext.contractsOfSales.Remove(contract);
        }
        public IEnumerable<ContractOfSale> GetAll()
        {
            return _appDbContext.contractsOfSales;
        }

        public ContractOfSale GetById(int id)
        {
            return _appDbContext.contractsOfSales.Find(id);
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }

        public void Update(ContractOfSale item)
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
