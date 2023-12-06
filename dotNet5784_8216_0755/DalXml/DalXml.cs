using DalApi;
using System.Diagnostics;

namespace Dal;

sealed public class DalXml : IDal
{
    public static IDal Instance { get; } = new DalXml();
    private DalXml() { }

    public ITask task => new TaskImplementation();
    public IDependence dependence => new DependenceImplementation();
    public IEngineer engineer => new EngineerImplementation();
}
