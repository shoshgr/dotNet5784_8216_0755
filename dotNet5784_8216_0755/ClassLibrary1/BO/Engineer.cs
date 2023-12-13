namespace BO;
/// <summary>
/// Engineer Entity represents an Engineer with all its props
/// </summary>
/// <param name="engineer_id">unique id of the engineer  </param>
/// <param name="name">name of the Engineer</param>
/// <param name="email"> the engineer mail adress</param>
/// <param name="degree">the Engineer degree </param>
/// <param name="task">the Engineer current task </param>
/// <param name="cost_per_hour">  cost per hour </param>
public class Engineer
{
    public int engineer_id { get; init;}
    public required string name { get; set;}
    public required string email { get; set;}
    public Level degree { get; set;}
    public int cost_per_hour { get; set; }
    TaskInEngineer? task{ get; set; }   
}
