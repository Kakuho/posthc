using System;

public class BandNotFoundException : Exception{
    public BandNotFoundException()
    {
    }

    public BandNotFoundException(string message): 
        base(message)
    {
    }

    public BandNotFoundException(string message, Exception inner):
        base(message, inner)
    {
    }
}
