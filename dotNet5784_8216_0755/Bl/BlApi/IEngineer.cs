namespace BlApi;
/// <summary>
/// The logical interface of engineer
/// </summary>
public interface IEngineer
{
    /// <summary>
    /// reads all the engineers with an option of filtering
    /// </summary>
    /// <param name="filter">Option to filter the engineers according to a parameter</param>
    /// <returns>collection of engineers</returns>
    public IEnumerable<BO.Engineer> ReadEngineers(Func<BO.Engineer, bool>? filter = null); 
    /// <summary>
    /// read an Engineer 
    /// </summary>
    /// <param name="id">id of the engineer to read</param>
    /// <returns>a logical engineer</returns>
    public BO.Engineer Read(int id);
    /// <summary>
    /// create a new engineer
    /// </summary>
    /// <param name="engineer">the new engineer</param>
    public int Create(BO.Engineer engineer);
    /// <summary>
    /// delete an engineer
    /// </summary>
    /// <param name="id">id of the engineer to delete</param>
    public void Delete(int id);
    /// <summary>
    /// update an engineer
    /// </summary>
    /// <param name="engineer">the updated engineer</param>
    public void Update(BO.Engineer engineer);
    /// <summary>
    /// read all the main details of the engineers
    /// </summary>
    /// <returns> colction type EngineerMainDetails</returns>
    public IEnumerable<BO.EngineerMainDetails> ReadMainDetailsEngineers();

}
