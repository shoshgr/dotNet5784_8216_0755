namespace BlApi;
/// <summary>
/// The logical interface of task
/// </summary>
public interface ITask
{
    /// <summary>
    /// reads all the tasks with an option of filtering
    /// </summary>
    /// <param name="filter">Option to filter the tasks according to a parameter</param>
    /// <returns>collection of tasks</returns>
    public IEnumerable<BO.Task> ReadTasks(Func<BO.Task, bool>? filter = null);
    /// <summary>
    /// read a task 
    /// </summary>
    /// <param name="id">id of the task to read</param>
    /// <returns>a logical task</returns>
    public BO.Task Read(int id);
    /// <summary>
    /// create a new task
    /// </summary>
    /// <param name="engineer">the new task</param>
    public void Create(BO.Task task);
    /// <summary>
    /// delete a task
    /// </summary>
    /// <param name="id">id of the task to delete</param>
    public void Delete(int id);
    /// <summary>
    /// update a task
    /// </summary>
    /// <param name="engineer">the updated task</param>
    public void Update(BO.Task task);
}
