
using DalApi;
using DO;
using System.Xml.Linq;
namespace Dal;

internal class DependenceImplementation : IDependence
{
    string FILENAME = "dependence.xml";

    XElement xml = XElement.Load("dependence.xml");
    public int Create(Dependence item)
    {
        
        int new_id = Config.NextDependenceId;
        XElement dependence = new XElement("dependence",
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
      
      
        XElement? dependence = xml.Descendants("dependence").FirstOrDefault(d => (int)d.Attribute("id") == id);
        if (dependence == null)
        {
            throw new DalDoesNotExistException("A dependence with this ID number does not exist");
        }
        dependence.Remove();
        xml.Save(FILENAME);
       
    }

    public Dependence? Read(int id)
    {

      
        XElement? dependence = xml.Descendants("dependence").FirstOrDefault(d => (int)d.Attribute("id") == id);
        if (dependence == null)
            return null;

        return dependence.ToEntity<Dependence>();
    }

    public Dependence? Read(Func<Dependence, bool> filter)
    {
        XElement? dependence = xml.Descendants("dependence").FirstOrDefault(x => filter(x.ToEntity<Dependence>()));
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
        XElement? dependence = xml.Descendants("dependence").FirstOrDefault(d => (int)d.Attribute("id") == item.id);
        if (dependence == null)
            throw new DalDoesNotExistException("A dependence with this ID number does not exists");
        dependence.Remove();
        xml.Add(dependence);
        xml.Save(FILENAME);
    }
}
