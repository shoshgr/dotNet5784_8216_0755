

namespace BlImplementation;
using BlApi;
using BO;
using System;
using System.Collections.Generic;

internal class TaskImplementation : ITask
{
    public void Create(Task task)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public Task Read(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Task> ReadTasks(Func<Task, bool>? filter = null)
    {
        throw new NotImplementedException();
    }

    public void Update(Task task)
    {
        throw new NotImplementedException();
    }
}
