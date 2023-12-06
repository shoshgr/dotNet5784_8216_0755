namespace Dal;
using DalApi;
sealed public class DalList : IDal
{
    public static IDal Instance { get; } = new DalList();
    private DalList() { }

    public ITask task => new TaskImplementation();
    //public ITask Task => throw new NotImplementedException();
    public IDependence dependence => new DependenceImplementation();
    //public IDependence Dependence => throw new NotImplementedException();
    public IEngineer engineer => new EngineerImplementation();
    //public IEngineer Engineer => throw new NotImplementedException();
}
