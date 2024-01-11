using DalApi;
using DO;
using System.Xml.Serialization;

namespace Dal;

internal class EngineerImplementation : IEngineer
{
    string FILENAME = @"../xml/engineers.xml";
    public int Create(Engineer item)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.engineer_id == item.engineer_id);
        if (engineer != null && engineer.is_active == false)
            throw new DalAlreadyExistsNotActiveException("An engineer with this ID number already exists but is not active");
        if (engineer != null)
            throw new DalAlreadyExistsException("An engineer with this ID number already exists");
        Engineer new_engineer = item with { is_active = true };
        engineers?.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers!, "engineers");
        return new_engineer.engineer_id;
    }

    public void Delete(int id)
    {
        List<Engineer>? engineers = XMLTools.LoadListFromXMLSerializer<Engineer>("engineers");
        Engineer? engineer = engineers?.FirstOrDefault(engineer => engineer.engineer_id == id);
        if (engineer == null || engineer.is_active == false)
            throw new DalDoesNotExistException("An engineer with this ID number does not exists");
        Engineer new_engineer = engineer with { is_active = false };
        engineers!.Remove(engineer);
        engineers.Add(new_engineer);
        XMLTools.SaveListToXMLSerializer(engineers, "engineers");
    }

    public Engineer? Read(int id)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)serializer.Deserialize(reader);
        reader.Close();
        var engineer = engineers?.FirstOrDefault(engineer => engineer.engineer_id == id);
        if (engineer == null)
            return null;
        return engineer;
    }

    public Engineer? Read(Func<Engineer, bool> filter)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)serializer.Deserialize(reader);
        reader.Close();
        return engineers!.FirstOrDefault(filter);
    }

    public IEnumerable<Engineer?> ReadAll(Func<Engineer, bool>? filter = null)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)serializer.Deserialize(reader);
        reader.Close();
        if (filter != null)
        {
            return from engineer in engineers
                   where filter(engineer) 
                   select engineer;
        }
        return engineers!;
              
    }

    public void Update(Engineer item)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Engineer>));
        StreamReader reader = new StreamReader(FILENAME);
        List<Engineer>? engineers = (List<Engineer>?)serializer.Deserialize(reader);
        reader.Close();
        var engineer = engineers!.FirstOrDefault(engineer => engineer.engineer_id == item.engineer_id);
        if (engineer == null)
            throw new DalDoesNotExistException("An engineer with this ID number does not exists");
        engineers!.Remove(engineer);
        engineers.Add(item);
        StreamWriter writer = new StreamWriter(FILENAME);
        serializer.Serialize(writer, engineers);
        writer.Close();
    }
}
