using Autofac;
using Autofac.Extensions.DependencyInjection;
using Introduction.Repository;
using Introduction.Repository.Common;
using Introduction.Service;
using Introduction.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>((container) =>
    {
        container.RegisterType<ClubRepository>().As<IClubRepository>().InstancePerLifetimeScope();
        container.RegisterType<ClubPresidentRepository>().As<IClubPresidentRepository>().InstancePerLifetimeScope();
        container.RegisterType<ClubService>().As<IClubService>().InstancePerLifetimeScope();
        container.RegisterType<ClubPresidentService>().As<IClubPresidentService>().InstancePerLifetimeScope();

    });





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
