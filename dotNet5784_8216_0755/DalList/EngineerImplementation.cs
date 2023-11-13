namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

internal class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
        var engineer = DataSource.Engineers.FirstOrDefault(engineer => engineer.engineer_id == item.engineer_id);
        if (engineer!= null)
          throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        Engineer new_engineer = item with { is_active = true };
        DataSource.Engineers?.Add(new_engineer);
        return new_engineer.engineer_id;
    }

    public void Delete(int id)
    {
       var engineer = DataSource.Engineers.FirstOrDefault(engineer => engineer.engineer_id == id);
        if (engineer == null|| engineer.is_active == false)
            throw new DalDoesNotExistException("An engineer with this ID number does not exists");
        Engineer new_engineer = engineer with { is_active = false };
        DataSource.Engineers!.Remove(engineer);
        DataSource.Engineers.Add(new_engineer);
    }

    public Engineer? Read(int id)
    {
        var engineer = DataSource.Engineers!.FirstOrDefault(engineer => engineer.engineer_id == id);
        if (engineer == null || engineer.is_active == false)
            return null;
        return engineer;
    }
    public Engineer? Read(Func<Engineer, bool> filter)
    {
        return DataSource.Engineers!.FirstOrDefault(filter);
    }
    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        if (filter != null)
        {
            return from engineer in DataSource.Engineers
                   where filter(engineer) &&(engineer.is_active)
                   select engineer;
        }
        return from engineer in DataSource.Engineers
               where engineer.is_active
               select engineer;
    }

    public void Update(Engineer item)
    {
        var engineer = DataSource.Engineers!.FirstOrDefault(engineer => engineer.engineer_id == item.engineer_id);
        if (engineer == null)
            throw new DalDoesNotExistException("An engineer with this ID number does not exists");
        DataSource.Engineers!.Remove(engineer);
        DataSource.Engineers.Add(item);
    }
}
