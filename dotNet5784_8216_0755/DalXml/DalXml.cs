using DalApi;
namespace Dal;

sealed public class DalXml : IDal
{
    public ITask task => new TaskImplementation();
    public IDependence dependence => new DependenceImplementation();
    public IEngineer engineer => new EngineerImplementation();
}
