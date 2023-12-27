namespace BlImplementation;
using BlApi;
using BO;

using System.Text.RegularExpressions;

internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
   
    private void validition(BO.Engineer engineer)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])""|" + @"([-a-z0-9!#$%&'+/=?^_`{|}~]|(?<!\.)\.))(?<!\.)" + @"@[a-z0-9][\w\.-][a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        if (!regex.IsMatch(engineer.email))
            throw new NotImplementedException();// חריגה לעשות 
        if (engineer.engineer_id < 0)
            throw new NotImplementedException();// חריגה לעשות 
        if (engineer.name == "")
            throw new NotImplementedException();// חריגה לעשות 
        if (engineer.cost_per_hour < 0)
            throw new NotImplementedException();// חריגה לעשות 
    }
    public int Create(BO.Engineer engineer)
    {
        
        try
        {
            validition(engineer);
            int id_engineer = _dal.engineer.Create(new DO.Engineer(engineer.engineer_id, engineer.name, engineer.email, engineer.degree, engineer.cost_per_hour));
            return id_engineer;
        }
        catch (Exception e)
        {
            throw new NotImplementedException();
        }
    }


    public void Delete(int id)
    {
        try
        {
            var engineer = _dal.engineer?.Read(id);
            if (engineer == null || engineer.is_active == false)
                throw new NotImplementedException();
            IEnumerable<DO.Task> tasks = _dal.task!.ReadAll()!;
            IEnumerable<int> taskId = from task in tasks
                                      where (task.engineer == id)
                                      select task.task_id;
            if (taskId.Any())
                throw new NotImplementedException();
            else
                _dal.engineer!.Delete(engineer.engineer_id);
            ///לא לאפשר מחיקת מהנדס שסיים עכשיו משימה 

        }
        catch (Exception e)
        {
            throw new NotImplementedException();
        }
     
    }

    public BO.Engineer Read(int id)
    {
        try
        {
            DO.Engineer? do_engineer = _dal.engineer.Read(id);
            if(do_engineer==null)
                throw new NotImplementedException();
            DO.Task? task = _dal.task.Read(task => task.engineer == do_engineer.engineer_id);
            BO.Engineer bo_engineer = new BO.Engineer()
                                      {
                                          engineer_id = do_engineer.engineer_id,
                                          name = do_engineer.name,
                                          email = do_engineer.email,
                                          degree = do_engineer.degree,
                                          cost_per_hour = do_engineer.cost_per_hour,
                                          is_active= do_engineer.is_active,
                                          task = (task != null) ? new TaskInEngineer() { task_id = task.task_id, nickname = task.nickname! } : null
                                      };
            return bo_engineer; 
        }
        catch(Exception e) {
            throw new NotImplementedException();
        }
       
    }

    public IEnumerable<BO.Engineer> ReadEngineers(Func<BO.Engineer, bool>? filter = null)
    {
        IEnumerable<DO.Engineer> do_engineers = _dal.engineer.ReadAll()!;
        IEnumerable<BO.Engineer> bo_engineers = from engineer in do_engineers
                                                let task = _dal.task.Read(task => task.engineer == engineer.engineer_id)
                                      select new BO.Engineer()
                                      {
                                          engineer_id = engineer.engineer_id,
                                          name = engineer.name,
                                          email = engineer.email,
                                          degree = engineer.degree,
                                          cost_per_hour = engineer.cost_per_hour,
                                          is_active= engineer.is_active,
                                          task = (task != null) ? new TaskInEngineer() { task_id = task.task_id, nickname = task.nickname! } : null
                                      };
        if (filter != null)
        {
            return from engineer in bo_engineers
                   where filter(engineer)&&(engineer.is_active)
                   select engineer;
        }
        return bo_engineers;

    }

    public void Update(BO.Engineer engineer)
    {
        try
        {
            validition(engineer);
          

            _dal.engineer.Update(new DO.Engineer(engineer.engineer_id, engineer.name, engineer.email, engineer.degree, engineer.cost_per_hour,engineer.is_active));
        }
        catch (Exception ex)
        {

            throw new NotImplementedException();
        }
    }
}
