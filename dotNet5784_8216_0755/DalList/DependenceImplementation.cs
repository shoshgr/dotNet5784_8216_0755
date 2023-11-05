namespace Dal;
using DalApi;
using DO;
//using System.Collections.Generic;

public class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {
        int new_id = DataSource.Config.Next_dependence_id;
        Dependence new_item = item with { id = new_id };
        DataSource.Dependences.Add(new_item);
        return new_id;
    }

    public void Delete(int id)
    {
        int index = DataSource.Dependences.FindIndex(dependence => dependence.id == id);
        if (index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Dependences.RemoveAt(index);
    }

    public Dependence? Read(int id)
    {
        int index = DataSource.Dependences.FindIndex(dependence => dependence.id == id);
        if (index == -1)
            return null;
        return DataSource.Dependences[index];
    }

    public List<Dependence> ReadAll()
    {
        return new List<Dependence>(DataSource.Dependences);
       
    }

    public void Update(Dependence item)
    {
        int index = DataSource.Dependences.FindIndex(dependence => dependence.id == item.id);
        if (index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Dependences.RemoveAt(index);
        DataSource.Dependences.Add(item);
    }
}
