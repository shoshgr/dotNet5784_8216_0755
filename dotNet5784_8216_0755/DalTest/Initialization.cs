namespace DalTest;

using DalApi;
using DO;

static public class Initialization
{

    private static ITask? s_dalTask;
    private static IDependence? s_dalDependence;
    private static IEngineer? s_dalEngineer;
    private static readonly Random s_rand = new();
  
    private static void create_tasks()
    {
        string _description = "You haven't described the task yet";
        Level[] levels = { Level.Novice, Level.Proficient, Level.AdvancedBeginner, Level.Competent,Level.Expert };
       Level _level;
        
        for (int i = 0; i < 100;)
        { 
           DateTime date = DateTime.Now;
            DateTime _production_date = date.Add(TimeSpan.FromDays(-30)); 
            _level = levels[s_rand.Next(0, 4)];
            DateTime _estimated_end = date.Add(TimeSpan.FromDays(-5)); 
            Task new_task = new (0, _description, _level, _production_date, _estimated_end, false);
            s_dalTask!.Create(new_task);
            i++;
        }
    }
    private static void create_dependences()
    {
        int _next_task;
        int _prev_task;
        List<Task> tasks = s_dalTask!.ReadAll();
        foreach (var task in tasks)
        {
            if (tasks.FindIndex(_task => _task.task_id == task.task_id) == tasks.Count - 4)
                break;
            _prev_task = task.task_id;
            for (int i = 1; i < 4;)
            {
                _next_task = tasks[tasks.FindIndex(_task => _task.task_id == task.task_id) + i].task_id;
                Dependence new_Dependence = new(0, _next_task, _prev_task);
                s_dalDependence!.Create(new_Dependence);
                i++;
            }
        }
    }
    private static void create_engineers()
    {
        string[] Names = { " Eli Amar", " Yair Cohen", " Ariela Levin", " Dina Klein", " Shira Israelof","Sari Chefetz"
      ,"Shoshana Grilak","Bil Gates","Orna Levi","ayala man","Menachem Cohen","Antiochus Rasha","chaim cok","lizi grilak","tova rom",
      "Vladimir Zalenski","Vladi Putin","Joe Biden","shuly shor","miri lev","mor choen","Joe Biden","Shragi Sason","Bibi Netaniahu","shira monk",
      "alyu grilak","tamar tshiler","Aaliyah", "Abigail", "Amelia", "Anna", "Ava","Benjamin", "Blake", "Cameron", "Charles", "Christopher",
      "Daniel", "David", "Emily", "Emma", "Elizabeth","Gabriella", "Grace", "Hannah", "Isabella", "James",
      "John", "Joshua", "Madison", "Michael", "Olivia","Oliver", "Samuel", "Sarah", "Sophia", "William" };
        int _id;
        string _name;
        string _mail;
        int _cost;
        Level _degree;
        Level[] degrees = { Level.Novice, Level.Proficient, Level.AdvancedBeginner, Level.Competent, Level.Expert };
        foreach (var name in Names)
        {
            do
                _id = s_rand.Next(1000, 9999);
            while (s_dalEngineer!.Read(_id) != null);
            _name = name;
            _mail = name + "@gmail.com";
            _degree = degrees[s_rand.Next(0, 4)];
            _cost = s_rand.Next(50, 200) * ((int)_degree) + s_rand.Next(50, 200);
            Engineer new_engineer = new(_id, _name, _mail, _degree, _cost,true);
            s_dalEngineer!.Create(new_engineer);
        }
    }
    static readonly IEngineer? dalEngineer;
    public static void Do (IEngineer? dalEngineer, ITask? dalTask, IDependence? dalDependence)
    {
        s_dalTask = dalTask ?? throw new NullReferenceException(" DAL can not be null!");
        s_dalDependence = dalDependence ?? throw new NullReferenceException(" DAL can not be null!");
        s_dalEngineer = dalEngineer ?? throw new NullReferenceException(" DAL can not be null!");
        create_tasks();
        create_dependences();
        create_engineers();
    }
}
  

