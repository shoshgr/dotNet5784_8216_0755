namespace Dal;

using DalApi;
using DO;
//using System.Collections.Generic;

public class EngineerImplementation : IEngineer
{
    public int Create(Engineer item)
    {
       if(DataSource.Engineers.Contains(item))
          throw new NotImplementedException();// צריך לזרוק שגיאה של :An engineer with this ID number already exists
        DataSource.Engineers.Add(item);
        return item.engineer_id;
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();

    }

    public Engineer? Read(int id)
    {
        int index = DataSource.Engineers.FindIndex(engineer => engineer.engineer_id == id);

        if (index == -1)
            return null;
        return DataSource.Engineers[index];
    }

    public List<Engineer> ReadAll()
    {
        
        return new List<Engineer>(DataSource.Engineers);
    }

    public void Update(Engineer item)
    {
        int index = DataSource.Engineers.FindIndex(engineer => engineer.engineer_id == item.engineer_id);
        if (index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Engineers.RemoveAt(index);
        DataSource.Engineers.Add(item);
    }
}
