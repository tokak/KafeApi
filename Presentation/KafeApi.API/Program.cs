using AspNetCoreRateLimit;
using FluentValidation;
using KafeApi.Application.Dtos.CafeInfoDtos;
using KafeApi.Application.Dtos.CategoryDtos;
using KafeApi.Application.Dtos.MenuItemDtos;
using KafeApi.Application.Dtos.OrderDtos;
using KafeApi.Application.Dtos.OrderItemDtos;
using KafeApi.Application.Dtos.ReviewDtos;
using KafeApi.Application.Dtos.TableDtos;
using KafeApi.Application.Helpers;
using KafeApi.Application.Interfaces;
using KafeApi.Application.Mapping;
using KafeApi.Application.Services.Abstract;
using KafeApi.Application.Services.Concrete;
using KafeApi.Persistence.Context;
using KafeApi.Persistence.Context.Identity;
using KafeApi.Persistence.Middlewares;
using KafeApi.Persistence.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<AppDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connection); // connection bo�sa hata verir
});

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
{
    var connection = builder.Configuration.GetConnectionString("DefaultConnection");
    options.UseSqlServer(connection); // connection bo�sa hata verir
});

builder.Services.AddIdentity<AppIdentityUser, AppIdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireDigit = false;
    opt.Password.RequiredLength = 6;
    opt.Password.RequireLowercase = false;
    opt.Password.RequireUppercase = false;
    opt.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppIdentityDbContext>().AddDefaultTokenProviders();


builder.Services.AddControllers();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuItemRepository, MenuItemRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IMenuItemServices, MenuItemServices>();
builder.Services.AddScoped<ICategoryServices, CategoryServices>();
builder.Services.AddScoped<ITableServices, TableService>();
builder.Services.AddScoped<IOrderItemServices, OrderItemServices>();
builder.Services.AddScoped<IOrderServices, OrderServices>();
builder.Services.AddScoped<IAuthServices, AuthServices>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICafeInfoServices, CafeInfoService>();
builder.Services.AddScoped<IReviewService, ReviewService>();

builder.Services.AddScoped<TokenHelpers>();


builder.Services.AddAutoMapper(typeof(GeneralMapping));

builder.Services.AddValidatorsFromAssemblyContaining<CreateCategoryDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCategoryDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateMenuItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateMenuItemDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateTableDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateTableDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderItemDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderItemDto>();


builder.Services.AddValidatorsFromAssemblyContaining<CreateOrderDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateOrderDto>();


builder.Services.AddValidatorsFromAssemblyContaining<CreateCafeInfoDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateCafeInfoDto>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateReviewDto>();
builder.Services.AddValidatorsFromAssemblyContaining<UpdateReviewDto>();



builder.Services.AddOpenApi();

//jwt yap�land�r�lmas�
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true, // Token'� olu�turan Issuer kontrol edilsin
        ValidateAudience = true, // Token'�n hedef kitlesi kontrol edilsin
        ValidateLifetime = true, // Token s�resi dolmu� mu kontrol edilsin
        ValidateIssuerSigningKey = true, // Token imzas� kontrol edilsin

        ValidIssuer = builder.Configuration["Jwt:Issuer"], // appsettings.json i�indeki "Issuer" de�eri
        ValidAudience = builder.Configuration["Jwt:Audience"], // appsettings.json i�indeki "Audience" de�eri
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),

        ClockSkew = TimeSpan.Zero // Token s�resi dolma tolerans� (varsay�lan 5 dakika, burada s�f�rland�),
    };
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHttpContextAccessor();

var columnOptions = new Collection<StandardColumn>
{
    StandardColumn.Message,
    StandardColumn.Level,
    StandardColumn.Exception,
    StandardColumn.TimeStamp
};


// Serilog yap�land�rmas� ba�lat�l�yor
Log.Logger = new LoggerConfiguration()

    // appsettings.json dosyas�ndaki "Serilog" b�l�m�nden ayarlar� okur
    .ReadFrom.Configuration(builder.Configuration)

    // LogContext i�indeki bilgileri loglara dahil eder (�rne�in kullan�c� ad�, request ID vs.)
    .Enrich.FromLogContext()
    // Logger'� olu�tur
    .CreateLogger();



builder.Services.AddSingleton<Serilog.ILogger>(Log.Logger);
builder.Host.UseSerilog();

builder.Services.AddHttpContextAccessor();


// Rate limiting i�lemleri i�in bellek tabanl� cache kullan�l�r.
// �stek say�lar� ve s�releri bellekte tutulur.
builder.Services.AddMemoryCache();

// appsettings.json i�indeki "IpRateLimiting" konfig�rasyonunun okunup kullan�lmas�n� sa�lar.
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

// Rate limiting yap�land�rmas�n�n nas�l �al��aca��n� belirten konfig�rasyon s�n�f� singleton olarak eklenir.
// Bu, rate limit kurallar�n�n y�klenmesini ve uygulanmas�n� sa�lar.
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Rate limit verileri (IP ve istek say�s� gibi) bellek i�inde saklanacak �ekilde servis eklenir.
// Da��t�k mimaride kullan�l�yorsa Redis veya ba�ka bir cache provider tercih edilebilir.
builder.Services.AddInMemoryRateLimiting();



var app = builder.Build();
app.MapScalarApiReference(
    opt =>
    {
        opt.Title = "Kafe Api v1";
        opt.Theme = ScalarTheme.BluePlanet;
        opt.DefaultHttpClient = new(ScalarTarget.Http, ScalarClient.Http11);
    });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseIpRateLimiting();



app.UseHttpsRedirection();


app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<SerilogMiddleware>();

app.MapControllers();

app.Run();
