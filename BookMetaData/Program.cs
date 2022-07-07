using BookMetaDataApiDomain.Interface;
using BookMetaDataPrimaryDomain.Service;
using BookMetaDataSecondaryDomain.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.Add(new ServiceDescriptor(typeof(IPrimaryGetBookMetaDataService), typeof(PrimaryGetBookMetaDataService), ServiceLifetime.Transient));
builder.Services.Add(new ServiceDescriptor(typeof(ISecondaryGetBookMetaDataService), typeof(RedisGetBookMetaDataServiceImp), ServiceLifetime.Transient));


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
