namespace DO;

/// <summary>
/// Exception for entity object that called but does not exist
/// </summary>
[Serializable]
public class DalDoesNotExistException : Exception
{
    public DalDoesNotExistException(string? message) : base(message) { }
}

/// <summary>
/// Exception for entity object that already exist
/// </summary>
[Serializable]
public class DalAlreadyExistsException : Exception
{
    public DalAlreadyExistsException(string? message) : base(message) { }
}
/// <summary>
/// Exception for load xml file
/// </summary>
[Serializable]
public class DalXMLFileLoadCreateException : Exception
{
    public DalXMLFileLoadCreateException(string? message) : base(message) { }
}

public class DalAlreadyExistsNotActiveException : Exception
{
    public DalAlreadyExistsNotActiveException(string? message) : base(message) { }
}
