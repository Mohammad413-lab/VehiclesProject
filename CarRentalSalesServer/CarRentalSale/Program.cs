using System.Text;
using CarRentalSale.interfacee;
using CarRentalSale.services;
using CarRentalSale.validationservices;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ApplicationName = typeof(Program).Assembly.FullName,
    WebRootPath = "wwwroot"

});


builder.WebHost.UseUrls("http://localhost:5050", "https://localhost:5051");


builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication("moh")
    .AddJwtBearer("moh", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]))
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
options.AddPolicy("AllowFrontend",
policy =>
{
policy.WithOrigins("http://127.0.0.1:5500")
.AllowAnyHeader()
.AllowAnyMethod();
});
});

builder.Services.AddScoped<IValidationService, ValidationService>();
builder.Services.AddScoped<IUsersServices, UsersService>();
builder.Services.AddScoped<ICarsServices, CarsServices>();
builder.Services.AddScoped<ISalesOrderService, SalesOrderServices>();
builder.Services.AddScoped<IRentalsOrderService, RentalsOrderServices>();

var app = builder.Build();
app.UseCors("AllowFrontend");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();
app.Run();


// https://localhost:5051/swagger/index.html

