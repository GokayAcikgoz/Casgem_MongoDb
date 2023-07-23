using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<EstateStoreDatabaseSetting>(
    builder.Configuration.GetSection(nameof(EstateStoreDatabaseSetting)));

builder.Services.AddSingleton<IEstateStoreDatabaseSetting>(sp =>
    sp.GetRequiredService<IOptions<EstateStoreDatabaseSetting>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("EstateStoreDatabaseSetting:ConnectionString")));

builder.Services.AddScoped<IEstateService, EstateService>();

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
