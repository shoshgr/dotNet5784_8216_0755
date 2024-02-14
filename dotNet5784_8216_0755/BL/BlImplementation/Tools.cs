using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlImplementation;

static class Tools
{

    public static DalApi.IDal _dal = Factory.Get;

    /// <summary>
    /// validition of task detailes
    /// </summary>
    /// <param name="id">task id</param>
    /// <param name="name"> task name</param>
    /// <exception cref="BlInvalidValueException">not valid value entered</exception>
    public static void validition(int id, string name)
    {
        if (id < 0)
            throw new BlInvalidValueException("ID must be positive");
        if (name == "")
            throw new BlInvalidValueException("nickname can not be null");
    }

    /// <summary>
    /// calculate task status
    /// </summary>
    /// <param name="task"></param>
    /// <returns>the task's status</returns>
    public static BO.Status calc_status(DO.Task task)
    {
        BO.Status status = (BO.Status)(task!.production_date == DateTime.MinValue ? 0
                            : task!.start_date == DateTime.MinValue ? 1
                            : task.actual_end == DateTime.MinValue ? 2
                            : 3);
        return status;
    }
   
    /// <summary>
    /// create all the task dependeces
    /// </summary>
    /// <param name="tasks">the dependent tasks of the task</param>
    /// <param name="id">the id of the task</param>
    /// <exception cref="BlDoesNotExistException">task does not exist</exception>
    public static void createTaskDependnce(List<TaskInList> tasks, int id)
    {
        if (tasks.Count == 0)
            return;
        IEnumerable<DO.Task>? is_exist = _dal!.task!.ReadAll(task => task.task_id == tasks.First().id)!;
        if (!is_exist.Any())
            throw new BlDoesNotExistException($"the task with id : {tasks.First().id} does not exist");
        _dal.dependence.Create(new DO.Dependence(0, id, tasks.First()!.id));
        tasks.RemoveAt(0);
        createTaskDependnce(tasks, id);
    }

    /// <summary>
    /// convert DO task to BO task
    /// </summary>
    /// <param name="do_task">DO task</param>
    /// <returns>BO task</returns>
    public static BO.Task convert_to_bo(DO.Task do_task)
    {
        return new BO.Task
        {
            task_id = do_task.task_id,
            description = do_task.description,
            nickname = do_task.nickname,
            milestone = find_milestone(do_task.task_id),
            production_date = do_task.production_date,
            start_date = do_task.start_date,
            final_date = do_task.final_date,
            estimated_start = do_task.estimated_end,
            actual_end = do_task.actual_end,
            product = do_task.product,
            remarks = do_task.remarks,
            engineer = do_task.engineer == null ? null : new EngineerMainDetails { id = (int)do_task.engineer!, name = _dal.engineer.Read((int)do_task.engineer)!.name },
            level = (BO.Level)do_task.level,
            tasks_list = (List<TaskInList>)(from dep in _dal!.dependence!.ReadAll()
                                            where (dep.next_task == do_task.task_id)
                                            let task = _dal.task.Read(dep.prev_task)
                                            select task == null ?null: new TaskInList
                                            {
                                                id = do_task.task_id,
                                                nickname = task.nickname,
                                                description = task.description,
                                                status = calc_status(task)
                                            }).ToList(),
            status = calc_status(do_task)
        };
    }

    /// <summary>
    /// engineer detailes validation
    /// </summary>
    /// <param name="engineer">engineer</param>
    /// <exception cref="BlInvalidValueException">not valid value entered</exception>
    public static void engineer_validition(BO.Engineer engineer)
    {
        string pattern = @"^[a-zA-Z0-9._-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
        var regex = new Regex(pattern);
        if (!regex.IsMatch(engineer.email))
            throw new BlInvalidValueException("invalid email");
        if (engineer.cost_per_hour < 0)
            throw new BlInvalidValueException("cost_per_hour must be positive");
        validition(engineer.engineer_id, engineer.name);
    }

    /// <summary>
    /// find task's milestone
    /// </summary>
    /// <param name="id">id of the task</param>
    /// <returns>milestone</returns>
    public static MilestoneInTask find_milestone(int id)
    {
        MilestoneInTask milestoneInTask = (from d in _dal.dependence.ReadAll()
                                           let next_task = d.next_task
                                           where next_task == id && (_dal.task.Read(d.prev_task) != null) && (_dal.task.Read(d.prev_task)!.milestone == true)
                                           select new MilestoneInTask { id = d.prev_task, name = _dal.task.Read(d.prev_task)!.nickname! }).FirstOrDefault()!;
        return milestoneInTask;
    }

    /// <summary>
    /// calculate task progress rate
    /// </summary>
    /// <param name="id">id of task</param>
    /// <returns>progress rate</returns>
    public static float calc_ProgressRate(int id)
    {
        IEnumerable<DO.Dependence> prev_dependences = _dal.dependence.ReadAll((DO.Dependence do_dependence) => do_dependence.next_task == id)!;
        if (!prev_dependences.Any())
            return 100;
        int count_tasks = prev_dependences.Count();
        int count_completed_tasks = prev_dependences.Aggregate(0, (count, next) => count += (_dal.task.Read((DO.Task task) => task.task_id == id)!).actual_end == DateTime.MinValue ? 1 : 0);
        return (float)count_completed_tasks / count_tasks;
    }
   
}
