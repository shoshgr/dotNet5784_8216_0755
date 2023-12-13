namespace DO;

/// <summary>
/// Engineer Entity represents an Engineer with all its props
/// </summary>
/// <param name="engineer_id">unique id of the engineer  </param>
/// <param name="name">name of the Engineer</param>
/// <param name="email"> the engineer mail adress</param>
/// <param name="degree">the Engineer degree </param>
/// <param name="is_active">check if the engnieer is active</param>
/// <param name="cost_per_hour">  cost per hour </param>
public record Engineer
(
    int engineer_id,
    string name,
    string email,
    Level degree,
    int cost_per_hour,
    bool is_active = false
)
{
    public Engineer() : this(0,"","", 0, 0) { }
}


