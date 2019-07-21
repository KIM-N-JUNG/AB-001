using System;
using System.Runtime.Serialization;

[Serializable]
internal class NotReachableSceneException : System.Exception
{
    public NotReachableSceneException()
    {
    }

    public NotReachableSceneException(string message) : base(message)
    {
    }

    public NotReachableSceneException(string message, System.Exception innerException) : base(message, innerException)
    {
    }

    protected NotReachableSceneException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}