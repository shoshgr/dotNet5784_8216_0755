

namespace BO;
/// <summary>
/// MilestoneInList Entity represents an MilestoneInList with all its props
/// </summary>
/// <param name="name">name of the Milestone in list</param>
/// <param name="description"> the Milestone Description </param>
/// <param name="progress_percentage"> the progress percentage of milestone  </param>
/// <param name="production_date">production date of the Milestone</param>
/// <param name="status" > the Milestone status</param>
public class MilestoneInList
{
    public string ?name { get; set; }
    public string ?description { get; set; }
    public float? progress_percentage { get; set; }
    public DateTime production_date { get; set; }
    public Status? status { get; set; }
}
