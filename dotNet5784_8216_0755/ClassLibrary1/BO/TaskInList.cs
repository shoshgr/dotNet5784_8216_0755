

namespace BO;
/// <summary>
/// TaskInList Entity represents an TaskInList with all its props
/// </summary>
/// <param name="nickname">name of the task in list</param>
/// <param name="description"> the Milestone Description </param>
/// <param name="id"> the task id </param>
/// <param name="status" > the task status</param>
public class TaskInList
{
    public int id { get; init; }
    public string? nickname { get; set; }
    public string ?description { get; set; }
    public Status status { get; set; }
}
