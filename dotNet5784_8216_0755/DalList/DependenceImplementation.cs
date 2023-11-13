namespace Dal;
using DalApi;
using DO;


internal class DependenceImplementation : IDependence
{
    public int Create(Dependence item)
    {

        int new_id = DataSource.Config.Next_dependence_id;
        Dependence new_item = item with { id = new_id };
        DataSource.Dependences?.Add(new_item);
        return new_id;
    }

    public void Delete(int id)
    {
        var dependence = DataSource.Dependences!.FirstOrDefault(d => d.id == id);
        if (dependence == null)
            throw new DalDoesNotExistException("A dependence with this ID number does not exists");
        DataSource.Dependences!.Remove(dependence);
    }

    public Dependence? Read(int id)
    {
        var dependence = DataSource.Dependences!.FirstOrDefault(dependence => dependence.id == id);
        if (dependence == null)
            return null;
        return dependence;
    }
    public Dependence? Read(Func<Dependence, bool> filter)
    {
        return DataSource.Dependences!.FirstOrDefault(filter);
    }
    public IEnumerable<Dependence?> ReadAll(Func<Dependence, bool>? filter = null)
    {
        if (filter != null)
        {
            return from dependence in DataSource.Dependences
                   where filter(dependence)
                   select dependence;
        }
        return from dependence in DataSource.Dependences
               select dependence;
    }

    public void Update(Dependence item)
    {
        var dependence = DataSource.Dependences!.FirstOrDefault(dependence => dependence.id == item.id);
        if (dependence == null)
            throw new DalDoesNotExistException("A dependence with this ID number does not exists");
        DataSource.Dependences!.Remove(dependence);
        DataSource.Dependences.Add(item);
    }
}
