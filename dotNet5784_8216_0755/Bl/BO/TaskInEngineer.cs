

namespace BO;
/// <summary>
/// TaskInEngineer Entity represents an engineer's current task (name and id)
/// </summary>
/// <param name="task_id">the id of the task that the engineer is working on </param>
/// <param name="nickname">the nickname of the task that the engineer is working on </param>
public class TaskInEngineer
{
    public required int task_id { get; init; }
    public required string nickname { get; set; }
}
