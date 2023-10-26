
namespace DO;
/// <summary>
/// Task Entity represents a task with all its props
/// </summary>
/// 
/// <param name="task_id">unique ID of task  </param>
/// <param name="description">description of the task</param>
/// <param name="nickname">Short name of the task</param>
/// <param name="milestone">boolean flag, true if the task is active</param>
/// <param name="production_date">production date of the task</param>
/// <param name="start_date"> the date of starting the task</param>
/// <param name="final_date">the final date of ending the task</param>
/// <param name="estimated_end"> the estimated date of ending the task</param>
/// <param name="actual_end">the actual date of ending the task</param>
/// <param name="product">description of the product</param>
/// <param name="remarks">remarks on the task</param>
/// <param name="engineer_id">the id of the task's engineer</param>
/// <param name="level">the task dificulty level </param>

public record Task
{
    int task_id;
    string description;
    string? nickname=" ";
    bool milestone = false;
    DateTime? production_date=null;
    DateTime? start_date = null;
    DateTime? final_date = null;
    DateTime? estimated_end = null;
    DateTime? actual_end = null;
    string? product;
    string? remarks;
    int? engineer_id;
    Task_level level;
}
