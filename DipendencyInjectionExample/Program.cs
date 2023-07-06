using Autofac;
using Autofac.Extensions.DependencyInjection;
using ServiceContracts;
using Services;

var builder = WebApplication.CreateBuilder(args);

//unosimo Autofac
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());   

builder.Services.AddControllersWithViews();

/* 
builder.Services.Add(new ServiceDescriptor(
    typeof(ICitiesService),
    typeof(CitiesService),
    ServiceLifetime.Scoped
));*/

// krace metode za dodavanje di
//builder.Services.AddTransient<ICitiesService, CitiesService>();
//builder.Services.AddScoped<ICitiesService, CitiesService>();
//builder.Services.AddSingleton<ICitiesService, CitiesService>();

//koristenje Autofak service liftimea
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    //equal to AddTransient
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerDependency();

    //equal to AddScoped
    containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().InstancePerLifetimeScope();

    //equal to AddSingleton
    //containerBuilder.RegisterType<CitiesService>().As<ICitiesService>().SingleInstance();

});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();
app.MapControllers();

app.Run();
