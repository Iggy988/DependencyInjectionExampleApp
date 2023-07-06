using ServiceContracts;

namespace Services;

public class CitiesService : ICitiesService, IDisposable
{
    private List<string> _cities;

    //constructor
    public CitiesService()
    {
        //svaki put kad se kreira novi objekat od CitieService
        //constructor se izvrsava i kreira se novi Guid u _serviceInstanceId propertiju
        _serviceInstanceId = Guid.NewGuid();

        _cities = new List<string>()
        {
            "London",
            "New York",
            "Moscow",
            "Tokyo",
            "Rome",
        };
        //TODO: Add logic to open the db connection 
    }

    private Guid _serviceInstanceId;
    public Guid ServiceInstanceId { get
        {
            return _serviceInstanceId;
        } 
    }

    public List<string> GetCities()
    {
        return _cities;
    }

    public void Dispose()
    {
        //TODO: Add logic to close the db connection 
    }
}