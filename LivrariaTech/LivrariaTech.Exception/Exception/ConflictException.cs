using System.Net;

namespace LivrariaTech.Exception.Exception;

public class ConflictException : LivrariaTechException
{
    public ConflictException(string message) : base(message) {}
    public override List<string> GetErrorMessages()
    {
        return [Message];
    }
    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Conflict;
    }
}