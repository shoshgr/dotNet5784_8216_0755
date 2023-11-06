namespace Dal;
using DalApi;
using DO;
//using System.Collections.Generic;

public class TaskImplementation : ITask
{
    public int Create(Task item)
    {
        int new_id = DataSource.Config.Next_task_id;
        Task new_item = item with { task_id = new_id };
        DataSource.Tasks?.Add(new_item);
        return new_id;
    }

    public void Delete(int id)
    {
        int index = DataSource.Tasks!.FindIndex(task => task.task_id == id);
        if (index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Tasks.RemoveAt(index);
    }

    public Task? Read(int id)
    {
       int index = DataSource.Tasks!.FindIndex(task => task.task_id == id);
               
        if (index == -1)
            return null;
        return DataSource.Tasks[index];
    }

    public List<Task> ReadAll()
    {
        return new List<Task>(DataSource.Tasks);
    }
    public void Update(Task item)
    {
        int index = DataSource.Tasks!.FindIndex(task => task.task_id == item.task_id);
        if(index == -1)
            throw new NotImplementedException(); // צריך להוסיף חריגה שהמשתמש אינו קיים 
        DataSource.Tasks.RemoveAt(index);
        DataSource.Tasks.Add(item);
    }

}
