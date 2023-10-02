namespace Application.Exceptions;

public class NoAvailableSeatsException : Exception
{
    public NoAvailableSeatsException() : base()
    { }
    
    public NoAvailableSeatsException(string message) : base(message)
    { }
    
    public NoAvailableSeatsException(string message, Exception e) : base(message, e)
    { }
}
