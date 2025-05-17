
using System.Net;

namespace LivrariaTech.Exception.Exception;

public class InvalidLoginException : LivrariaTechException
{   
    public InvalidLoginException(string message) : base("Email e/ou senha invalidos") {}
    public override List<string> GetErrorMessages()
    {
        return [Message];
    }

    public override HttpStatusCode GetStatusCode()
    {
        return HttpStatusCode.Unauthorized;
    }
}