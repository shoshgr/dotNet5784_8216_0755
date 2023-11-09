namespace DalApi;

public interface IDal
{
    ITask task { get; }
    IDependence dependence { get; }
    IEngineer engineer { get; }
}
