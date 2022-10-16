using Microsoft.EntityFrameworkCore;
using RealEstateAPI.Data;
using RealEstateAPI.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddDbContext<RealEstateDataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RealEstate"));
});

builder.Services.AddScoped<IRegionsRepository, RegionsRepository>();
builder.Services.AddScoped<IHousesRepository, HousesRepository>();
builder.Services.AddScoped<ILandscapesRepository, LandscapeRepository>();

var app = builder.Build();

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
