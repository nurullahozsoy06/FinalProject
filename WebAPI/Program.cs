using Autofac;
using Autofac.Extensions.DependencyInjection;
using Business.Abstract;
using Business.Concrete;
using Business.DependencyResolvers.Autofac;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()); // Autofac'ı kullanarak bağımlılıkları yönetmek istediğimizi belirtir

builder.Host.ConfigureContainer<ContainerBuilder>(options => // Autofac'ın modül yapısını kullanarak bağımlılıkları kaydettiğimiz yer
{
    options.RegisterModule(new AutofacBusinessModule()); // AutofacBusinessModule'de tanımladığımız bağımlılıkları kaydeder
});
// Add services to the container.
// autofac,ninject,castlewindsor,structuremap,lightinject,dryinject --> ioc container
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
