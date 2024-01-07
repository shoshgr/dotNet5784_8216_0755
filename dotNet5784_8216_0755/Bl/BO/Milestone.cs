namespace BO;
/// <summary>
/// Milestone Entity represents a logical milestone with all its props
/// </summary>
/// <param name="id">unique id of the milestone</param>
/// <param name="name">name of the milestone</param>
/// <param name="description"> the milestone description </param>
/// <param name="tasks_list"> tasks list that have dependence  </param>
/// <param name="production_date">production date of the milestone</param>
/// <param name="start_date"> the date of starting the milestone</param>
/// <param name="final_date">the final date of ending the milestone</param>
/// <param name="estimated_end"> the estimated date of ending the milestone</param>
/// <param name="actual_end">the actual date of ending the milestone</param>
/// <param name="status">the milestone's status </param>
/// <param name="remarks">remarks on the milestone</param>
/// <param name="progress_percentage"> the progress percentage of work of the milestone </param>
public class Milestone
{
    public int id { get; init; }
    public string? name { get; set; }
    public string? description { get; set; }
    public List<TaskInList> ?tasks_list { get; set; }
    public DateTime production_date { get; set; }
    public DateTime estimated_start { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? final_date { get; set;}
    public DateTime? actual_end { get; set;}
    public Status? status { get; set;}
    public string? remarks { get; set;}
    public float ? progress_percentage { get; set; }      
}
