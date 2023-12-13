
namespace BO;
/// <summary>
/// EngineerInTask Entity represents a task current engineer name and id
/// </summary>
/// <param name="id">the id of the engineer that working on this task </param>
/// <param name="name">the name of the engineer that working on this task  </param>
public class EngineerInTask
{
    public required string name { get; set; }
    public required int id  { get; init; }

}
