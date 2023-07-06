using Microsoft.AspNetCore.Mvc;
using Services;
using ServiceContracts;
using Autofac;

namespace DipendencyInjectionExample.Controllers;

// controller class ne treba da zavisi od dependency class
public class HomeController : Controller
{
    //private readonly CitiesService _citiesService;
    // kreiramo 3 razlicita objekta ICitiesService
    private readonly ICitiesService _citiesService1;
    private readonly ICitiesService _citiesService2;
    private readonly ICitiesService _citiesService3;

    private readonly ILifetimeScope _lifetimeScope;

    //private readonly IServiceScopeFactory _serviceScopeFactory;

    //constructor
    public HomeController(ICitiesService citiesService1, ICitiesService citiesService2,
        ICitiesService citiesService3, IServiceScopeFactory serviceScopeFactory,
        ILifetimeScope lifetimeScope)
    {
        //create object of CitiesService class
        //_citiesService = new CitiesService(); -> nikad ne treba da unosimo dependency class direktno
        _citiesService1 = citiesService1;
        _citiesService2 = citiesService2;
        _citiesService3 = citiesService3;

        //_lifetimeScope = serviceScopeFactory;

        _lifetimeScope = lifetimeScope;

    }

    [Route("/")]
    //kad zelimo da service bude dostupan samo za odredjenu metodu,
    //dodajemo ga kao parametar i stavljamo [FromServices]
    public IActionResult Index(/*[FromServices] ICitiesService _citiesService*/)
    {
        //transient service lifetime - dobicemo 3 razlita Guida
        //scoped service lifetime - dobicemo 3 ista Guida zato, ali prilikom novog requesta dobijamo razlicita Guida 
        //singleton service lifetime - uvijek dobijamo 3 ista Guida 
        List<string> cities = _citiesService1.GetCities();
        ViewBag.InstanceId_CiriesService_1 = _citiesService1.ServiceInstanceId;
        ViewBag.InstanceId_CiriesService_2 = _citiesService2.ServiceInstanceId;
        ViewBag.InstanceId_CiriesService_3 = _citiesService3.ServiceInstanceId;



        // kreiramo scope sa automatskom disposable metodom
        //using (IServiceScope scope = _lifetimeScope.CreateScope())
        using (ILifetimeScope scope = _lifetimeScope.BeginLifetimeScope())
        {//begin of scope
            //Inject CitiesService
            //ICitiesService citiesService = scope.ServiceProvider.GetRequiredService<ICitiesService>(); //inject service u scope umjesto globalno
            //using AutoFac
            ICitiesService citiesService = scope.Resolve<ICitiesService>(); //inject service u scope umjesto globalno
            //DB work

            ViewBag.InstanceId_CitiesService_InScope = citiesService.ServiceInstanceId;
        }//end of this scope, it auto calls CitiesService.Dispose()

        //proslijedjujemo model object u view -> @model
        return View(cities);
    }
}
