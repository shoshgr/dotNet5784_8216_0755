namespace DO;

/// <summary>
/// Engineer Entity represents an Engineer with all its props
/// </summary>
/// <param name="engineer_id">unique id of the engineer  </param>
/// <param name="name">name of the Engineer</param>
/// <param name="email"> the engineer mail adress</param>
/// <param name="Engineer_degree">the Engineer degree </param>
/// <param name="task_name">the name of the current task</param>
/// <param name="cost_per_hour">  cost per hour </param>
public record Engineer
(
    int engineer_id,
    string name,
    string email,
    Level degree,
    int cost_per_hour,
    bool is_active = false
   
 );


