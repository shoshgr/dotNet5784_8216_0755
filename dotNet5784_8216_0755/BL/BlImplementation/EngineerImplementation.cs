
using BlApi;
using BO;
using DO;
using System.Text.RegularExpressions;
namespace BlImplementation;


internal class EngineerImplementation : IEngineer
{
    private DalApi.IDal _dal = Factory.Get;

    /// <summary>
    /// create new engineer
    /// </summary>
    /// <param name="engineer"></param>
    /// <returns>id of the new engineer</returns>
    /// <exception cref="BlAlreadyExistsNotActiveException">
    ///            exception for creating an engineer that already exist but not not active</exception>
    /// <exception cref="BlAlreadyExistsException">exception for creating an engineer that already exist</exception>
    public int Create(BO.Engineer engineer)
    {        
        try
        {
            Tools.engineer_validition(engineer);
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
        catch(BlInvalidValueException e)
        {
            throw e;
        }
    }

    /// <summary>
    /// delete an engineer
    /// </summary>
    /// <param name="id">id of engineer to delete</param>
    /// <exception cref="BlCannotDeleteException">trying to delete engineer that have a task</exception>
    /// <exception cref="BlDoesNotExistException">trying to delete engineer that does not exist</exception>
    public void Delete(int id)
    {
        try
        {
           
            IEnumerable<DO.Task> tasks = _dal.task!.ReadAll()!;
            IEnumerable<int> taskId = from task in tasks
                                      where (task.engineer == id)
                                      select task.task_id;
            if (taskId.Any())
                throw new BlCannotDeleteException("can not delete engineer that has task ");
            else
                _dal.engineer!.Delete(id);
        }
        catch (DalDoesNotExistException e)
        {
            throw new BlDoesNotExistException($"engineer with id: {id} does not exist ", e);
        }
        

    }

    /// <summary>
    /// read an engineer by id
    /// </summary>
    /// <param name="id">id of engineer to read</param>
    /// <returns>engineer</returns>
    /// <exception cref="BlDoesNotExistException">trying to read an engineer that does not exist</exception>
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

    /// <summary>
    /// read all engineers (by filter-option)
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>list of engineers</returns>
    public IEnumerable<BO.Engineer> ReadEngineers(Func<BO.Engineer, bool>? filter = null)
    {
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
                   where filter(engineer)
                   select engineer;
        }
        return bo_engineers;

    }

    /// <summary>
    /// read all engineers (by filter-option)
    /// </summary>
    /// <param name="filter"></param>
    /// <returns>list of main details of engineers</returns>
    public IEnumerable<BO.EngineerMainDetails> ReadMainDetailsEngineers(Func<DO.Engineer, bool>? filter = null)
    {
       
        if (filter != null)
        {
            return from engineer in _dal.engineer.ReadAll()!
                   where filter(engineer)
                   let task = _dal.task.Read(task => task.engineer == engineer.engineer_id)
                   select new BO.EngineerMainDetails()
                   {

                       name = engineer.name,
                       id = engineer.engineer_id

                   };
        }

        return from engineer in _dal.engineer.ReadAll()!
               let task = _dal.task.Read(task => task.engineer == engineer.engineer_id)
               select new BO.EngineerMainDetails()
               {

                   name = engineer.name,
                   id = engineer.engineer_id

               };     
    }

    /// <summary>
    /// update an engineer
    /// </summary>
    /// <param name="engineer">updated engineer</param>
    /// <exception cref="BlDoesNotExistException">trying to update an engineer that does not exist</exception>
    public void Update(BO.Engineer engineer)
    {
        try
        {          
            _dal.engineer.Update(new DO.Engineer(engineer.engineer_id, engineer.name, engineer.email, (DO.Level)engineer.degree, engineer.cost_per_hour,engineer.is_active));
        }
        catch (DalDoesNotExistException e)
        {
            throw new BlDoesNotExistException($"engineer with id: {engineer.engineer_id} does not exist ", e);
        }
    }
}
