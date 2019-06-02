using System;
using System.Runtime.Serialization;

[Serializable]
internal class DatabaseConnectionException : System.Exception
{
    public DatabaseConnectionException()
    {
    }

    public DatabaseConnectionException(string message) : base(message)
    {
    }

    public DatabaseConnectionException(string message, System.Exception innerException) : base(message, innerException)
    {
    }

    protected DatabaseConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}