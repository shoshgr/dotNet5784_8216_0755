namespace DO;

/// <summary>
/// dependence Entity represents the  dependence between tasks with all its props
/// </summary>
/// <param name="id">unique ID of the engineer  </param>
/// <param name="next_task">name of the Engineer</param>
/// <param name="prev_task"> the engineer mail adress</param>

public record Dependence
(
    int id,
    int next_task,
    int prev_task
)
{
    public Dependence() : this(0,0,0) { }
}
