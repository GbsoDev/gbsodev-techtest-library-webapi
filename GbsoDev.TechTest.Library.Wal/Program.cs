using AutoMapper;
using GbsoDev.TechTest.Library.Bll;
using GbsoDev.TechTest.Library.El;
using GbsoDev.TechTest.Library.Mol;
using GbsoDev.TechTest.Library.Wal;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add app settings
builder.Services.AddSettings(builder.Configuration, out AppSettings appSettings);
// Add dbContext
builder.Services.AddDbContext(appSettings.GetConnectionString(Utils.CONNECTION_ROOT_NAME) ?? throw new ApplicationException("Conexión a base de datos no encontrada"));
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
	foreach (var corPolicy in appSettings.AllowCors)
	{
		options.AddPolicy(corPolicy.Origin, builder =>
		{
			var policy = builder.WithOrigins(corPolicy.Origin)
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials();

			if(corPolicy.Methods?.Any() ?? false)
			{
				policy.WithMethods(corPolicy.Methods);
			}
		});
	}
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

app.UseRouting();

app.UseCors(builder =>
{
	builder.AllowAnyOrigin()
		   .AllowAnyMethod()
		   .AllowAnyHeader();
});

foreach (var corPolicy in appSettings.AllowCors)
{
	app.UseCors(corPolicy.Name);
}

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});

app.MapControllers();

app.Run();
