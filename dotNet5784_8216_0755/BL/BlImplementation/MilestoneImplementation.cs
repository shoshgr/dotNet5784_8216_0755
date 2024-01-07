
namespace BlImplementation;
using BlApi;
using BO;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;
 
 
    private BO.Status calc_status(DO.Task task)
    {
        BO.Status status = (BO.Status)(task!.production_date == DateTime.MinValue ? 0
                            : task!.start_date == DateTime.MinValue ? 1
                            : task.actual_end == DateTime.MinValue ? 2
                            : 3);
        return status;
    }
    private float calc_ProgressRate(int id)
    {
        IEnumerable<DO.Dependence> prev_dependences = _dal.dependence.ReadAll((DO.Dependence do_dependence) => do_dependence.next_task == id)!;
        if (!prev_dependences.Any())
            return 100;
        int count_tasks = prev_dependences.Count();
        int count_completed_tasks = prev_dependences.Aggregate(0, (count, next) => count += (_dal.task.Read((DO.Task task) => task.task_id == id)!).actual_end==DateTime.MinValue ? 1 : 0);
        return (float)count_completed_tasks / count_tasks;
    }
    private Milestone convert_to_milestone(DO.Task milestone)
    {
        IEnumerable<TaskInList> list = from dep in _dal.dependence.ReadAll()!
                                       where (dep.next_task == milestone.task_id)
                                       let task = _dal.task.Read(dep.prev_task)
                                       select new TaskInList { id = milestone.task_id, nickname = task.nickname, description = task.description, status = calc_status(task) };
        return new Milestone
        {
            id = milestone.task_id,
            name = milestone.nickname,
            description = milestone.description,
            tasks_list = (List<TaskInList>)list,
            production_date = milestone.production_date,
            estimated_start = milestone.estimated_start,
            start_date = milestone.start_date,
            final_date = milestone.final_date,
            actual_end = milestone.actual_end,
            status = calc_status(milestone),
            remarks = milestone.remarks,
            progress_percentage = calc_ProgressRate(milestone.task_id)
        };
    }
    public void CreateProject()
    {
        throw new NotImplementedException();
    }

    public Milestone Read(int id)
    {
        DO.Task ? milestone=_dal.task.Read(id);
        if(milestone == null)
            throw new NotImplementedException();
        if(!milestone.milestone)
            throw new NotImplementedException();
        IEnumerable<TaskInList> list = from dep in _dal.dependence.ReadAll()!
                                       where (dep.next_task == id)
                                       let task = _dal.task.Read(dep.prev_task)
                                       select new TaskInList { id = id, nickname = task.nickname, description = task.description, status = calc_status(task) };
        return convert_to_milestone(milestone);

    }

    public Milestone Update(int id)
    {
        DO.Task? milestone = _dal.task.Read(id);
        if (milestone == null)
            throw new NotImplementedException();
        _dal.task.Update(milestone);
        return convert_to_milestone(milestone);
    }
}

