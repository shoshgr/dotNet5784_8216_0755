namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public ITask task => throw new NotImplementedException();

    public IDependence dependence => throw new NotImplementedException();

    public IEngineer engineer => throw new NotImplementedException();
    public ITask Task => new TaskImplementation();
    public IDependence Dependence => new DependenceImplementation();
    public IEngineer Engineer => new EngineerImplementation();
}
