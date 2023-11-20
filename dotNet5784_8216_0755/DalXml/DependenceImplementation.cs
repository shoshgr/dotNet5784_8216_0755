
using DalApi;
using DO;
using System.Drawing;
using System.Xml.Linq;
namespace Dal;

internal class DependenceImplementation : IDependence
{
    string FILENAME = @"../xml/dependences.xml";
    XElement xml = XElement.Load("../xml/dependences.xml");

    public int Create(Dependence item)
    {
        int new_id = Config.NextDependenceId;
        XElement dependence = new XElement("Dependence",
            new XElement("next_task", item.next_task),
            new XElement("prev_task", item.prev_task),
            new XElement("id",new_id )   
        );
        xml.Add(dependence);
        xml.Save(FILENAME);
        return new_id;
      
    }

    public void Delete(int id)
    {
        XElement? dependence = xml.Descendants("Dependence").FirstOrDefault(d => (int)d.Element("id") ==  id);
        if (dependence == null)
        {
            throw new DalDoesNotExistException("A dependence with this ID number does not exist");
        }
        dependence.Remove();
        xml.Save(FILENAME);
       
    }

    public Dependence? Read(int id)
    {
        XElement? dependence = xml.Elements("Dependence").FirstOrDefault(d => (int)d.Element("id") == id);
        if (dependence == null)
            return null;

        return dependence.ToEntity<Dependence>();
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        XElement? dependence = xml.Descendants("Dependence").FirstOrDefault(x => filter(x.ToEntity<Dependence>()));
        if (dependence == null)
            return null;
        return dependence.ToEntity<Dependence>();
    }

    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter is null)
            return xml.Descendants("Dependence").Select(el => el.ToEntity<Dependence>());
        return xml.Descendants("Dependence").Where(el => filter(el?.ToEntity<Dependence>())).Select(el => el.ToEntity<Dependence>());         
    }


    public void Update(Dependence item)
    {
        XElement? dependence = xml.Descendants("Dependence").FirstOrDefault(d => (int)d.Element("id") == item.id);
        if (dependence == null)
            throw new DalDoesNotExistException("A dependence with this ID number does not exists");
        dependence.Remove();
        XElement _item = new XElement("Dependence",
              new XElement("next_task", item.next_task),
              new XElement("prev_task", item.prev_task),
              new XElement("id", item.id)
          );
        xml.Add(_item);
        xml.Save(FILENAME);
    }
}
