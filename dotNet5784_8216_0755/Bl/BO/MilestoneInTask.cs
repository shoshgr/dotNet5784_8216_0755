

namespace BO;
/// <summary>
/// MilestoneInTask Entity represents an milestone of a task (name and id)
/// </summary>
/// <param name="task_id">the id of the task's milestone  </param>
/// <param name="nickname">the nickname of the task's milestone </param>
public class MilestoneInTask
{
    public required int id  { get; init; }
    public required string name { get; set; }
}
