namespace BO;
public class Task {
    public int task_id { get;init;}
    public string? nickname { get;set;}
    public string? description { get;set;}
    public Level level { get;set;}
    public DateTime production_date { get; set; }
    public List<TaskInList>? tasks_list { get; set; }
    public DateTime estimated_end { get; set; }
    public DateTime? start_date { get; set; }
    public DateTime? final_date { get; set; }
    public DateTime? actual_end { get; set; }
    public Status? status { get; set; }
    public int? engineer { get; set; }
    public MilestoneInTask milestone { get; set; }

}
