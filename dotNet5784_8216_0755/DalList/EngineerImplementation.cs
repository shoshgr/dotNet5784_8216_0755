namespace Dal;

using DalApi;
using DO;
using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
       if(DataSource.Engineers!.Contains(item))
          throw new NotImplementedException();// צריך לזרוק שגיאה של :An engineer with this ID number already exists

        Engineer new_engineer = item with { is_active = true };
        DataSource.Engineers?.Add(new_engineer);
        return new_engineer.engineer_id;
    }

    public void Delete(int id)
    {
        int index = DataSource.Engineers!.FindIndex(engineer => engineer.engineer_id == id);
        if (index == -1)
            throw new NotImplementedException();
        if( DataSource.Engineers[index].is_active == false)
            throw new NotImplementedException();// אולי לא צריך להתיחס למקרה זה 
        Engineer new_engineer = DataSource.Engineers[index] with { is_active = true };
        DataSource.Engineers.RemoveAt(index);
        DataSource.Engineers.Add(new_engineer);
    }

    public Engineer? Read(int id)
    {
        int index = DataSource.Engineers!.FindIndex(engineer => engineer.engineer_id == id);
        if (index == -1|| DataSource.Engineers[index].is_active == false)
            return null;
        return DataSource.Engineers[index];
    }

    public List<Engineer> ReadAll()
    {
        List<Engineer> engineers = new List<Engineer>(DataSource.Engineers!.FindAll(engineer => engineer.is_active == true));
        return  engineers;
    }

    public void Update(Engineer item)
    {
        int index = DataSource.Engineers!.FindIndex(engineer => engineer.engineer_id == item.engineer_id);
        if (index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Engineers.RemoveAt(index);
        DataSource.Engineers.Add(item);
    }
}
