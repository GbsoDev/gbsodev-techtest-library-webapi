using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
	// Add dbContext
	builder.Services.AddDbContext(builder.Configuration.GetConnectionString(Utils.CONNECTION_ROOT_NAME) ?? throw new ApplicationException("Conexión a base de datos no encontrada"));
	// Add data acces
	builder.Services.AddDataAcces();
	// Add service providers
	builder.Services.AddServices();
	// Add validation rules
	builder.Services.AddBllValidationRulesLayer();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
