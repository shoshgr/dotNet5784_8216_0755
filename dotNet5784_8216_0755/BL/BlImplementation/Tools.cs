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
    public static void validition(int id, string name)
    {
        if (id < 0)
            throw new BlInvalidValueException("ID must be positive");
        if (name == "")
            throw new BlInvalidValueException("nickname can not be null");
    }
    public static BO.Status calc_status(DO.Task task)
    {
        BO.Status status = (BO.Status)(task!.production_date == DateTime.MinValue ? 0
                            : task!.start_date == DateTime.MinValue ? 1
                            : task.actual_end == DateTime.MinValue ? 2
                            : 3);
        return status;
    }
   
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
            engineer = new EngineerMainDetails { id = (int)do_task.engineer!, name = _dal.engineer.Read((int)do_task.engineer)!.name },
            level = (BO.Level)do_task.level,
            tasks_list = (List<TaskInList>)(from dep in _dal!.dependence!.ReadAll()
                                            where (dep.next_task == do_task.task_id)
                                            let task = _dal.task.Read(dep.prev_task)
                                            select new TaskInList
                                            {
                                                id = do_task.task_id,
                                                nickname = task.nickname
                                            ,
                                                description = task.description,
                                                status = calc_status(task)
                                            }),
            status = calc_status(do_task)
        };
    }
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
    public static MilestoneInTask find_milestone(int id)
    {
        DO.Dependence milstone_dep = _dal.dependence.ReadAll(dep => dep.next_task == id).First(dep => _dal.task.Read(task => task.task_id == dep?.prev_task)!.milestone == true)!;
        DO.Task milestone = _dal.task.Read(task => task.task_id == milstone_dep.prev_task)!;
        return new MilestoneInTask
        {
            id = milestone.task_id,
            name = milestone.nickname!
        };
    }
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
