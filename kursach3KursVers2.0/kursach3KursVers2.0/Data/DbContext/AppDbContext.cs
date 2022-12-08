using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;
using kursach3KursVers2._0.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace kursach3KursVers2._0.Data.DbContext
{
    public class AppDbContext:IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        //users
       public Microsoft.EntityFrameworkCore.DbSet<UserPassport> userPassports { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<UserAddress> userAddresses { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<UserRole> userRoles { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<User> users { get; set; }
        //cars
       public Microsoft.EntityFrameworkCore.DbSet<Car> cars { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarBody> carBodies { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarBrand> carBrands { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarModel> carModels { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarDescription> carDescriptions { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarEngine> carEngines { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarLegalCharacteristics> carLegalCharacteristics { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarSpecification> carSpecifications { get; set; }
       public Microsoft.EntityFrameworkCore.DbSet<CarTransmission> carTransmissions { get; set; }
        //buyers
       public Microsoft.EntityFrameworkCore.DbSet<Buyer> buyers { get; set; }
        //sellers
       public Microsoft.EntityFrameworkCore.DbSet<Seller> sellers { get; set; }
        //dillers
       public Microsoft.EntityFrameworkCore.DbSet<Diller> dillers { get; set; }
        //contracts 
       public Microsoft.EntityFrameworkCore.DbSet<ContractOfSale> contractsOfSales { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // equivalent of modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
                // and modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }
            base.OnModelCreating(builder);
        }
    }
}
