using AutoMapper;
using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Add dbContext
builder.Services.AddDbContext(builder.Configuration.GetConnectionString(Utils.CONNECTION_ROOT_NAME) ?? throw new ApplicationException("Conexión a base de datos no encontrada"));
// add app settings
builder.Services.AddSettings(builder.Configuration, out AppSettings appSettings);
// add JWT app authentication
builder.Services.AddAuthentication(builder.Configuration, appSettings);
// Add data acces
builder.Services.AddDataAcces();
// Add service providers
builder.Services.AddServices();
// Add validation rules
builder.Services.AddBllValidationRulesLayer();
// Add AutoMapper
builder.Services.AddSingleton(new MapperConfiguration(mc => mc.AddProfile(typeof(AutoMapperConfiguration))).CreateMapper());
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(builder =>
	{
		builder.AllowAnyOrigin()
			   .AllowAnyMethod()
			   .AllowAnyHeader();
	});
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseCors();
	app.UseSwagger();
	app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
