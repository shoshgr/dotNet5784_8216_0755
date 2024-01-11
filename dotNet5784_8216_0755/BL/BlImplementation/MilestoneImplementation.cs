

using BlApi;
using BO;
using System.Collections.Generic;
using System.Runtime.Intrinsics.Arm;
namespace BlImplementation;

internal class MilestoneImplementation : IMilestone
{
    private DalApi.IDal _dal = Factory.Get;

   
   
    private Milestone convert_to_milestone(DO.Task milestone)
    {
        IEnumerable<TaskInList> list = from dep in _dal.dependence.ReadAll()!
                                       where (dep.next_task == milestone.task_id)
                                       let task = _dal.task.Read(dep.prev_task)
                                       select new TaskInList { id = milestone.task_id, nickname = task.nickname, description = task.description, status = Tools.calc_status(task) };
        return new Milestone
        {
            id = milestone.task_id,
            name = milestone.nickname,
            description = milestone.description,
            tasks_list = (List<TaskInList>)list,
            production_date = milestone.production_date,
            estimated_start = milestone.estimated_end,
            start_date = milestone.start_date,
            final_date = milestone.final_date,
            actual_end = milestone.actual_end,
            status = Tools.calc_status(milestone),
            remarks = milestone.remarks,
            progress_percentage = Tools.calc_ProgressRate(milestone.task_id)
        };
    }
    public void CreateProject()
    {
        throw new NotImplementedException();
    }

    public Milestone Read(int id)
    {
        DO.Task? milestone = _dal.task.Read(id);
        if (milestone == null)
            throw new NotImplementedException();
        if (!milestone.milestone)
            throw new NotImplementedException();
        IEnumerable<TaskInList> list = from dep in _dal.dependence.ReadAll()!
                                       where (dep.next_task == id)
                                       let task = _dal.task.Read(dep.prev_task)
                                       select new TaskInList { id = id, nickname = task.nickname, description = task.description, status = Tools.calc_status(task) };
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

