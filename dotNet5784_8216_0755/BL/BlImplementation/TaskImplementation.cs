using BlApi;
using BO;
using DO;

namespace BlImplementation;

internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;

    /// <summary>
    /// create a task
    /// </summary>
    /// <param name="task">task to create</param>
    /// <returns>id of the new task</returns>
    public int Create(BO.Task task)
    {
        try
        {
            Tools.validition(task.task_id, task.nickname!);
            int id_task = _dal.task.Create(new DO.Task(task.task_id, task.description!, (DO.Level)task.level, task.production_date,
                task.estimated_start, false, task.start_date, task.final_date, task.actual_end,
                   task.nickname, task.product, task.remarks, task.engineer?.id));
            Tools.createTaskDependnce(task.tasks_list!, task.task_id);
            return id_task;
        }
        catch (BlDoesNotExistException e)
        {
            throw e;
        }
        catch (BlInvalidValueException e)
        {
            throw e;
        }

    }

    /// <summary>
    /// delete a task
    /// </summary>
    /// <param name="id">id of task to delete</param>
    /// <exception cref="BlCannotDeleteException">trying to delete a task that cannot be deleted</exception>
    /// <exception cref="BlDoesNotExistException">trying to delete a task that does not exist</exception>
    public void Delete(int id)
    {
        try
        {
            var task = _dal.task?.Read(id);
            //if (task == null )
            //    throw new BlDoesNotExistException($"the task with id : {id} does not exist");
            IEnumerable<DO.Dependence> dependencies = _dal.dependence!.ReadAll(dep => dep.prev_task == id)!;

            if (dependencies.Any())
                throw new BlCannotDeleteException($"task with id:{id} has dependency so it can not be deleted ");
            else
                _dal.task!.Delete(id);

        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"the task with id : {id} does not exist", ex);
        }
    }

    /// <summary>
    /// read a task
    /// </summary>
    /// <param name="id">id of task to read</param>
    /// <returns>task</returns>
    /// <exception cref="BlDoesNotExistException">trying to read a task that does not exist</exception>
    public BO.Task Read(int id)
    {

        DO.Task? do_task = _dal.task.Read(id);
        if (do_task == null)
            throw new BlDoesNotExistException($" task with id:{id} does not exist");
        BO.Task bo_task = Tools.convert_to_bo(do_task);
        return bo_task;

    }

    /// <summary>
    /// read all tasks (by filter-option)
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>all tasks</returns>
    public IEnumerable<BO.Task> ReadTasks(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task> tasks = from task in _dal.task.ReadAll()
                                     let bo_task = Tools.convert_to_bo(task)
                                     select bo_task;
        if (filter != null)
        {
            return from task in tasks
                   where filter(task)
                   select task;
        }
        return tasks;
    }

    /// <summary>
    /// read all tasks (by filter-option)
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>list of main details of tasks</returns>
    public IEnumerable<BO.TaskInList> ReadMainDetailsTasks(Func<DO.Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from task in _dal.task.ReadAll()!
                   where filter(task)
                   select new BO.TaskInList()
                   {
                       id = task.task_id,
                       nickname = task.nickname,
                       description = task.description,
                       status = Tools.calc_status(task)
                   };
        }
        return from task in _dal.task.ReadAll()!
               select new BO.TaskInList()
               {
                   id = task.task_id,
                   nickname = task.nickname,
                   description = task.description,
                   status = Tools.calc_status(task)
               };
    }

    /// <summary>
    /// update a task
    /// </summary>
    /// <param name="task">updated task</param>
    /// <exception cref="BlDoesNotExistException">trying to update a tsak that does not exist</exception>
    public void Update(BO.Task task)
    {
        try
        {
            _dal.task!.Update(new DO.Task(task.task_id, task.description!, (DO.Level)task.level, task.production_date,
                    task.estimated_start, false, task.start_date, task.final_date, task.actual_end,
                       task.nickname, task.product, task.remarks, task.engineer?.id));
        }
        catch (DO.DalDoesNotExistException ex)
        {
            throw new BlDoesNotExistException($"the task with id : {task.task_id} does not exist", ex);
        }
    }
}
