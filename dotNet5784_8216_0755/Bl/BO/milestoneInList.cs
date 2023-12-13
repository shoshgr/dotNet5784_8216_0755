namespace BO;
/// <summary>
/// MilestoneInList Entity represents a milestone in list
/// </summary>
/// <param name="name">name of the milestone</param>
/// <param name="description">the milestone description </param>
/// <param name="progress_percentage"> the progress percentage of the milestone  </param>
/// <param name="production_date">production date of the milestone</param>
/// <param name="status" > the milestone status</param>
public class MilestoneInList
{
    public string ?name { get; set; }
    public string ?description { get; set; }
    public float? progress_percentage { get; set; }
    public DateTime production_date { get; set; }
    public Status? status { get; set; }
}
