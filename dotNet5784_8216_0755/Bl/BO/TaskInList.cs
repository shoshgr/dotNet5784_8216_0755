namespace BO;

/// <summary>
/// TaskInList Entity represents a Task in list
/// </summary>
/// <param name="nickname">name of the task</param>
/// <param name="description"> the task's description </param>
/// <param name="id"> the task id </param>
/// <param name="status" > the task status</param>
public class TaskInList
{
    public int id { get; init; }
    public string? nickname { get; set; }
    public string ?description { get; set; }
    public Status status { get; set; }
}
