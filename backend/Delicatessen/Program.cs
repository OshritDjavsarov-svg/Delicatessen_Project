using BLL.BLLInterfaces;
using BLL.BLLServises;
using DAL.DALInterfaces;
using DAL.DALServises;
using DAL.models;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// חיבור לדאטה
builder.Services.AddDbContext<DelicatessenProjectContext>
    (a => a.UseSqlServer("Server=OshritDjavsarov;Database=DelicatessenProject;Trusted_Connection=True; TrustServerCertificate=True"));

// מיפוי
//builder.Services.AddAutoMapper(x => x.GetType());
builder.Services.AddAutoMapper(typeof(DTOs.MappingProfiles).Assembly);

// הזרקת תלויות - מוצרים
builder.Services.AddScoped<IProductBLLServise, ProductBLLServise>();
builder.Services.AddScoped<IProductDALServise, ProductDALServise>();

// הזרקת תלויות - משתמשים
builder.Services.AddScoped<IUserBLLService, UserBLLServise>();
builder.Services.AddScoped<IUserDALServise, UserDALServise>();

// הזרקת תלויות - עגלת קניות
builder.Services.AddScoped<ICartBLLService, CartBLLService>();
builder.Services.AddScoped<ICartDALService, CartDALService>();

// חיבור לאנגולר
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});


var app = builder.Build();
app.UseCors();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
