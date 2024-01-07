
using BlApi;
using BO;
using DO;
using System.Text.RegularExpressions;
namespace BlImplementation;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;
   
    private void validition(BO.Engineer engineer)
    {
        string pattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])""|" + @"([-a-z0-9!#$%&'+/=?^_`{|}~]|(?<!\.)\.))(?<!\.)" + @"@[a-z0-9][\w\.-][a-z0-9]\.[a-z][a-z\.]*[a-z]$";
        var regex = new Regex(pattern, RegexOptions.IgnoreCase);
        if (!regex.IsMatch(engineer.email))
            throw new BlInvalidValueException("invalid email");
        if (engineer.engineer_id < 0)
            throw new BlInvalidValueException("engineer_id must be positive"); 
        if (engineer.name == "")
            throw new BlInvalidValueException("name can not be null"); 
        if (engineer.cost_per_hour < 0)
            throw new BlInvalidValueException("cost_per_hour must be positive"); 
    }
    public int Create(BO.Engineer engineer)
    {
        
        try
        {
            validition(engineer);
            int id_engineer = _dal.engineer.Create(new DO.Engineer(engineer.engineer_id, engineer.name, engineer.email, (DO.Level)engineer.degree, engineer.cost_per_hour));
            return id_engineer;
        }
        catch (DalAlreadyExistsNotActiveException e)
        {
            throw new BlAlreadyExistsNotActiveException($"engineer with id: {engineer.engineer_id} is exist but not active",e);
        }
        catch (DalAlreadyExistsException e)
        {
            throw new BlAlreadyExistsException($"engineer with id: {engineer.engineer_id} is already exist ",e);
        }
    }


    public void Delete(int id)
    {
        try
        {
           
            IEnumerable<DO.Task> tasks = _dal.task!.ReadAll()!;
            IEnumerable<int> taskId = from task in tasks
                                      where (task.engineer == id)
                                      select task.task_id;
            if (taskId.Any())
                throw new NotImplementedException();
            else
                _dal.engineer!.Delete(id);
            ///לא לאפשר מחיקת מהנדס שסיים עכשיו משימה 

        }
        catch (DalDoesNotExistException e)
        {
            throw new BlDoesNotExistException($"engineer with id: {id} does not exist ", e);
        }
        

    }

    public BO.Engineer Read(int id)
    {
       
            DO.Engineer? do_engineer = _dal.engineer.Read(id);
            if(do_engineer==null)
                throw new BlDoesNotExistException($"engineer with id: {id} does not exist ");
            DO.Task? task = _dal.task.Read(task => task.engineer == do_engineer.engineer_id);
            BO.Engineer bo_engineer = new BO.Engineer()
                                      {
                                          engineer_id = do_engineer.engineer_id,
                                          name = do_engineer.name,
                                          email = do_engineer.email,
                                          degree = (BO.Level)do_engineer.degree,
                                          cost_per_hour = do_engineer.cost_per_hour,
                                          is_active= do_engineer.is_active,
                                          task = (task != null) ? new TaskInEngineer() { task_id = task.task_id, nickname = task.nickname! } : null
                                      };
            return bo_engineer; 
        
       
    }

    public IEnumerable<BO.Engineer> ReadEngineers(Func<BO.Engineer, bool>? filter = null)
    {
       // IEnumerable<DO.Engineer> do_engineers = _dal.engineer.ReadAll()!;
        IEnumerable<BO.Engineer> bo_engineers = from engineer in _dal.engineer.ReadAll()!
                                                let task = _dal.task.Read(task => task.engineer == engineer.engineer_id)
                                      select new BO.Engineer()
                                      {
                                          engineer_id = engineer.engineer_id,
                                          name = engineer.name,
                                          email = engineer.email,
                                          degree = (BO.Level)engineer.degree,
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
          
            //DO.Engineer? is_exist = _dal.engineer.Read(engineer.engineer_id);
            //if (is_exist == null)
            //    throw new NotImplementedException();
            _dal.engineer.Update(new DO.Engineer(engineer.engineer_id, engineer.name, engineer.email, (DO.Level)engineer.degree, engineer.cost_per_hour,engineer.is_active));
        }
        catch (DalDoesNotExistException e)
        {
            throw new BlDoesNotExistException($"engineer with id: {engineer.engineer_id} does not exist ", e);
        }
    }
}
