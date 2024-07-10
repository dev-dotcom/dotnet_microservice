using Hashkart.API.Services;
using Hashkart.Domain.Interfaces;
using Hashkart.Domain.Interfaces.IRepositories;
using Hashkart.Domain.Interfaces.IServices;
using Hashkart.Infrastructure.Helpers;
using Hashkart.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();


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
