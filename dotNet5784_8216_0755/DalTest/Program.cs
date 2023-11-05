using DalApi;
using Dal;
namespace DalTest
{
    internal class Program
    {
        try{
             private static ITask? s_dalTask = new TaskImplementation(); //stage 1
        private static IEngineer? s_dalEngineer = new EngineerImplementation(); //stage 1
        private static IDependence? s_dalDependence = new DependenceImplementation(); //stage 1
        static void Main(string[] args)
        {
            Initialization.Do(s_dalEngineer, s_dalTask, s_dalDependence);
        }
       }
    catch (Exception e)
   {
   
    }
}