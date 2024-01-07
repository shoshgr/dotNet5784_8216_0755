

using BlApi;
using BO;

namespace BlImplementation;



internal class TaskImplementation : ITask
{
    private DalApi.IDal _dal = Factory.Get;
    private BO.Status calc_status(DO.Task task)
    {
        BO.Status status = (BO.Status)(task!.production_date == DateTime.MinValue? 0
                            : task!.start_date == DateTime.MinValue ? 1
                            : task.actual_end == DateTime.MinValue ? 2
                            : 3);
        return status;
    }
    private MilestoneInTask find_milestone(int id)
    {
        DO.Dependence milstone_dep = _dal.dependence.ReadAll(dep => dep.next_task == id).First(dep => _dal.task.Read(task => task.task_id == dep?.prev_task)!.milestone == true)!;
        DO.Task milestone = _dal.task.Read(task => task.task_id == milstone_dep.prev_task)!;
        return new MilestoneInTask
        {
            id = milestone.task_id,
            name = milestone.nickname!
        };
    }
   
    private BO.Task convert_to_bo(DO.Task do_task)
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
            engineer = new EngineerInTask { id = (int)do_task.engineer!, name = _dal.engineer.Read((int)do_task.engineer)!.name },
            level = (BO.Level)do_task.level,
            tasks_list = (List<TaskInList>)(from dep in _dal!.dependence!.ReadAll()
                                           where(dep.next_task == do_task.task_id)
                                           let task = _dal.task.Read(dep.prev_task)
                                           select new TaskInList { id = do_task.task_id, nickname = task.nickname
                                           , description = task.description, status = calc_status(task) }),
            status = calc_status(do_task)
        };
    }
    private void validition(BO.Task task)
    {
        if (task.task_id < 0)
            throw new BlInvalidValueException("ID must be positive");
        if (task.nickname == "")
            throw new BlInvalidValueException("nickname can not be null");
    }
    private void createTaskDependnce(List<TaskInList> tasks,int id) {
        if (tasks.Count == 0)
            return;
        IEnumerable<DO.Task> ?is_exist = _dal!.task!.ReadAll(task =>task.task_id == tasks.First().id)!;
        if (!is_exist.Any())
            throw new BlDoesNotExistException($"the task with id : {tasks.First().id} does not exist");
       
        _dal.dependence.Create(new DO.Dependence(0, id, tasks.First()!.id));
        tasks.RemoveAt(0);
        createTaskDependnce(tasks,id);
    }

    public int Create(BO.Task task)
    {
        try
        {
            validition(task);
            int id_task = _dal.task.Create(new DO.Task(task.task_id,task.description!, (DO.Level)task.level, task.production_date,
                task.estimated_start,false, task.start_date, task.final_date, task.actual_end,
                   task.nickname, task.product, task.remarks, task.engineer?.id));
            createTaskDependnce(task.tasks_list!, task.task_id);
            return id_task;
        }
        catch (BlDoesNotExistException e)
        {
            throw e;
        }
        catch(BlInvalidValueException e)
        {
            throw e;
        }
      
    }
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
            throw new BlDoesNotExistException($"the task with id : {id} does not exist",ex);
        }
    }
    public BO.Task Read(int id)
    {
       
            DO.Task? do_task = _dal.task.Read(id);
            if (do_task == null)
                throw new BlDoesNotExistException($" task with id:{id} does not exist");
            BO.Task bo_task = convert_to_bo(do_task);
            return bo_task;
      
    }

    public IEnumerable<BO.Task> ReadTasks(Func<BO.Task, bool>? filter = null)
    {
        IEnumerable<BO.Task> tasks = from task in _dal.task.ReadAll()
                                     let bo_task = convert_to_bo(task)
                                     select bo_task;
        if (filter != null)
        {
            return from task in tasks
                    where filter(task)
                    select task;
        }
        return tasks;
    }

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
