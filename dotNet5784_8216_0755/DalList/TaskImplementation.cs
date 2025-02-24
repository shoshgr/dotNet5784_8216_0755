﻿namespace Dal;
using DalApi;
using DO;
using System.Xml;

internal class TaskImplementation : ITask
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
        Task? task = DataSource.Tasks?.FirstOrDefault(task => task.task_id == id);
        if (task == null)
            throw new DalDoesNotExistException("A task with this ID number does not exists");  
        DataSource.Tasks!.Remove(task);
    }

    public Task? Read(int id)
    {
        var task = DataSource.Tasks!.FirstOrDefault(task => task.task_id == id); 
        if (task == null)
            return null;
        return task;
    }
    public Task? Read(Func<Task, bool> filter)
    {
        return DataSource.Tasks!.FirstOrDefault(filter);
    }

    public IEnumerable<Task?> ReadAll(Func<Task, bool>? filter = null)
    {
        if (filter != null)
        {
            return from task in DataSource.Tasks
                   where filter(task) 
                   select task;
        }
        return from task in DataSource.Tasks
               select task;
    }
    public void Update(Task item)
    {
        var task = DataSource.Tasks!.FirstOrDefault(task => task.task_id == item.task_id);
        if(task == null)
            throw new DalDoesNotExistException("A task with this ID number does not exists"); 
        DataSource.Tasks!.Remove(task);
        DataSource.Tasks.Add(item);
    }

}
