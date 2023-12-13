namespace BO;
/// <summary>
/// Milestone Entity represents an Milestone with all its props
/// </summary>
/// <param name="id">unique id of the Milestone  </param>
/// <param name="name">name of the Milestone</param>
/// <param name="description"> the Milestone Description </param>
/// <param name="tasks_list"> tasks list that have dependence  </param>
/// <param name="production_date">production date of the Milestone</param>
/// <param name="start_date"> the date of starting the Milestone</param>
/// <param name="final_date">the final date of ending the Milestone</param>
/// <param name="estimated_end"> the estimated date of ending the Milestone</param>
/// <param name="actual_end">the actual date of ending the Milestone</param>
/// <param name="status">Milestone status </param>
/// <param name="remarks">remarks on the Milestone</param>
/// <param name="nickname">the nickname of the task that the engineer is working on </param>
/// <param name="progress_percentage"> the progress percentage of work </param>
public class Milestone
{
    public int id { get; init; }
    public string? Name { get; set; }
    public string? description { get; set; }
    public List<TaskInList> ?tasks_list { get; set; }
    public DateTime production_date { get; set; }
    public DateTime estimated_end { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? final_date { get; set;}
    public DateTime? actual_end { get; set;}
    public Status? status { get; set;}
    public string? remarks { get; set;}
    public float ? progress_percentage { get; set; }      
}
