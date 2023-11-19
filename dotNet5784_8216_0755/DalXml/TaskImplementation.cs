

using DalApi;
using DO;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Dal;
using DalApi;
using DO;
using System.Data.Common;

internal class TaskImplementation : ITask
{
    string FILENAME = "tasks.xml";
    public int Create(Task item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        int new_id = Config.NextTaskId;
        Task new_item = item with { task_id = new_id };
        tasks?.Add(new_item);
        StreamWriter writer = new StreamWriter(FILENAME);
        serializer.Serialize(writer, tasks);
        writer.Close();
        return new_id;
    }

    public void Delete(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        Task? task = tasks?.FirstOrDefault(task => task.task_id == id);
        if (task == null)
            throw new DalDoesNotExistException("A task with this ID number does not exists");
        tasks!.Remove(task);
        StreamWriter writer = new StreamWriter(FILENAME);
        serializer.Serialize(writer, tasks);
        writer.Close();
    }

    public DO.Task? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        var task = tasks!.FirstOrDefault(task => task.task_id == id);
        if (task == null)
            return null;
        return task;
    }

    public DO.Task? Read(Func<DO.Task, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        return tasks!.FirstOrDefault(filter);
    }

    public IEnumerable<DO.Task?> ReadAll(Func<DO.Task, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        if (filter != null)
        {
            return from task in tasks
                   where filter(task)
                   select task;
        }
        return from task in tasks
               select task;
    }

    public void Update(DO.Task item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Task>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Task>? tasks = (List<Task>?)serializer.Deserialize(reader);
        reader.Close();
        var task = tasks!.FirstOrDefault(task => task.task_id == item.task_id);
        if (task == null)
            throw new DalDoesNotExistException("A task with this ID number does not exists");
        tasks!.Remove(task);
        tasks.Add(item);
        StreamWriter writer = new StreamWriter(FILENAME);
        serializer.Serialize(writer, tasks);
        writer.Close();
    }
}
