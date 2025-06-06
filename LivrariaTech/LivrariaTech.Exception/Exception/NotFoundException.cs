using System.Net;

namespace LivrariaTech.Exception.Exception;

public class NotFoundException : LivrariaTechException
{
    public NotFoundException(string message) : base(message) {}
    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.NotFound;
    }
}