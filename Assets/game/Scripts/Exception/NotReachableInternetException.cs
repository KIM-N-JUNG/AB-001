using System;
using System.Runtime.Serialization;

[Serializable]
internal class NotReachableInternetException : System.Exception
{
    public NotReachableInternetException()
    {
    }

    public NotReachableInternetException(string message) : base(message)
    {
    }

    public NotReachableInternetException(string message, System.Exception innerException) : base(message, innerException)
    {
    }

    protected NotReachableInternetException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}