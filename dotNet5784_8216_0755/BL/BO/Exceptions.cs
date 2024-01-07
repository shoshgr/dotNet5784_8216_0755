namespace BO;

/// <summary>
/// Exception for entity object that called but does not exist
/// </summary>
[Serializable]
public class BlDoesNotExistException : Exception
{
    public BlDoesNotExistException(string? message) : base(message) { }
    public BlDoesNotExistException(string? message,Exception innerException)  : base(message, innerException) { }
}

/// <summary>
/// Exception for entity object that already exist
/// </summary>
[Serializable]
public class BlAlreadyExistsException : Exception
{
    public BlAlreadyExistsException(string? message) : base(message) { }
    public BlAlreadyExistsException(string? message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception for loading xml file
/// </summary>
/// 
[Serializable]
public class BlXMLFileLoadCreateException : Exception
{
    public BlXMLFileLoadCreateException(string? message) : base(message) { }
    public BlXMLFileLoadCreateException(string? message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception for an engineer that exist but is not active
/// </summary>
public class BlAlreadyExistsNotActiveException : Exception
{
    public BlAlreadyExistsNotActiveException(string? message) : base(message) { }
    public BlAlreadyExistsNotActiveException(string? message, Exception innerException) : base(message, innerException) { }
}

/// <summary>
/// Exception for invalid Value 
/// </summary>
[Serializable]
public class BlInvalidValueException : Exception
{
    public BlInvalidValueException(string? message) : base(message) { }
}
/// <summary>
/// Exception for object that can not be deleted
/// </summary>
[Serializable]
public class BlCannotDeleteException : Exception
{
    public BlCannotDeleteException(string? message) : base(message) { }
}

