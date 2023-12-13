

namespace BO;
/// <summary>
/// MilestoneInTask Entity represents an Milestone  name and id
/// </summary>
/// <param name="task_id">the id of the task Milestone  </param>
/// <param name="nickname">the nickname of the task Milestone </param>
public class MilestoneInTask
{
    public required int id  { get; init; }
    public required string name { get; set; }
}
