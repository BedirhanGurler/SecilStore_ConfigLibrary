using Microsoft.EntityFrameworkCore;
using SecilStore.Business.Abstract;
using SecilStore.Business.Concrete;
using SecilStore.Common.Validations.Abstract;
using SecilStore.Common.Validations.Concrete;
using SecilStore.Data.Context;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddTransient(typeof(IConfigItemService<>), typeof(ConfigItemService<>));
builder.Services.AddScoped<IModelStateResponseService, ModelStateResponseManager>();

builder.Services.AddDbContext<ConfigDbContext>(options =>
{
    options.UseSqlServer(configuration.GetConnectionString("connstr")!, sqlOptions =>
    {
        sqlOptions.UseNetTopologySuite(); // Enable NetTopologySuite support
    });
});
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
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
