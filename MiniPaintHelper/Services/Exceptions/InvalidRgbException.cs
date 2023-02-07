namespace Services.Exceptions;

public class InvalidRgbException : Exception
{ 
    public InvalidRgbException(string message)
        : base(message)
    {
    }
}