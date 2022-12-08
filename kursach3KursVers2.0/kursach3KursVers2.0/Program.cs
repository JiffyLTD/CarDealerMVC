using kursach3KursVers2._0.Data.DbContext;
using kursach3KursVers2._0.Interfaces;
using kursach3KursVers2._0.Models.Buyers;
using kursach3KursVers2._0.Models.Cars;
using kursach3KursVers2._0.Models.ContractsOfSale;
using kursach3KursVers2._0.Models.Dillers;
using kursach3KursVers2._0.Models.Sellers;
using kursach3KursVers2._0.Models.Users;
using kursach3KursVers2._0.Repositories.BuyersRepositories;
using kursach3KursVers2._0.Repositories.CarsRepositories;
using kursach3KursVers2._0.Repositories.ContractsOfSalesRepositories;
using kursach3KursVers2._0.Repositories.DillersRepositories;
using kursach3KursVers2._0.Repositories.SellersRepositories;
using kursach3KursVers2._0.Repositories.UsersRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options
    => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));//сервис для Entity
builder.Services.AddIdentity<User, IdentityRole>(opts =>
{
    opts.Password.RequiredLength = 5;   // минимальная длина
    opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
    opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
    opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
    opts.Password.RequireDigit = false; // требуются ли цифры
    opts.User.RequireUniqueEmail = true;//уникальность email
    opts.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ@.1234567890()";//разрешенные символы имени пользователя
}).AddEntityFrameworkStores<AppDbContext>();//Identity сервисы
builder.Services.AddTransient<IRepository<UserAddress>, UserAddressRepository>();
builder.Services.AddTransient<IRepository<UserPassport>, UserPassportRepository>();
builder.Services.AddTransient<IRepository<UserRole>, UserRoleRepository>();
builder.Services.AddTransient<IRepository<User>, UserRepository>();
builder.Services.AddTransient<IRepository<Car>, CarRepository>();
builder.Services.AddTransient<IRepository<CarBody>, CarBodyRepository>();
builder.Services.AddTransient<IRepository<CarBrand>, CarBrandRepository>();
builder.Services.AddTransient<IRepository<CarDescription>, CarDescriptionRepository>();
builder.Services.AddTransient<IRepository<CarEngine>, CarEngineRepository>();
builder.Services.AddTransient<IRepository<CarLegalCharacteristics>, CarLegalCharactRepository>();
builder.Services.AddTransient<IRepository<CarModel>, CarModelRepository>();
builder.Services.AddTransient<IRepository<CarSpecification>, CarSpecificRepository>();
builder.Services.AddTransient<IRepository<CarTransmission>, CarTransmissionRepository>();
builder.Services.AddTransient<IRepository<Diller>, DillerRepository>();
builder.Services.AddTransient<IRepository<Seller>, SellerRepository>();
builder.Services.AddTransient<IRepository<Buyer>, BuyerRepository>();
builder.Services.AddTransient<IRepository<ContractOfSale>, ContractOfSaleRepository>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();//подключаем аутентификацию
app.UseAuthorization();//подключаем авторизацию

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Main}");

app.Run();
