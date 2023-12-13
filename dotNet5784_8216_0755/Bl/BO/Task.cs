namespace BO;
/// <summary>
/// Task Entity represents a logical task with all its props
/// </summary>
/// <param name="task_id">unique id of task</param>
/// <param name="description">description of the task</param>
/// <param name="nickname">Short name of the task</param>
/// <param name="milestone">the task milestone</param>
/// <param name="production_date">production date of the task</param>
/// <param name="start_date"> the date of starting the task</param>
/// <param name="final_date">the final date of ending the task</param>
/// <param name="estimated_end"> the estimated date of ending the task</param>
/// <param name="actual_end">the actual date of ending the task</param>
/// <param name="product">description of the product</param>
/// <param name="remarks">remarks on the task</param>
/// <param name="engineer">the id of the task's engineer</param>
/// <param name="level">the task dificulty level </param>
/// <param name="tasks_list">tasks list that have dependence</param>
/// <param name="status">the task status</param>
public class Task {
    public int task_id { get;init;}
    public string? nickname { get;set;}
    public string? description { get;set;}
    public Level level { get;set;}
    public string? product { get;set;}
    public string? remarks { get; set; }
    public DateTime production_date { get; set; }
    public List<TaskInList>? tasks_list { get; set; }
    public DateTime estimated_end { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? final_date { get; set; }
    public DateTime? actual_end { get; set; }
    public Status? status { get; set; }
    public EngineerInTask? engineer { get; set; }
    public MilestoneInTask ?milestone { get; set; }
}
