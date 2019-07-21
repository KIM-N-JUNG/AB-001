using System;
using System.Runtime.Serialization;

[Serializable]
internal class NotLoginException : System.Exception
{
    public NotLoginException()
    {
    }

    public NotLoginException(string message) : base(message)
    {
    }

    public NotLoginException(string message, System.Exception innerException) : base(message, innerException)
    {
    }

    protected NotLoginException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}