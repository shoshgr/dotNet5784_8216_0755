namespace BlApi;
/// <summary>
/// The logical interface of milestone
/// </summary>
public interface IMilestone
{
    /// <summary>
    /// read a milestone 
    /// </summary>
    /// <param name="id">id of the milestone to read</param>
    /// <returns>logical milestone</returns>
    public BO.Milestone Read(int id);
    /// <summary>
    /// update a milestone
    /// </summary>
    /// <param name="id">id of the milestone to update</param>
    /// <returns>updated logical milestone</returns>
    public BO.Milestone Update(int id);
    /// <summary>
    /// create a project schedule
    /// </summary>
    public void CreateProject();
}
