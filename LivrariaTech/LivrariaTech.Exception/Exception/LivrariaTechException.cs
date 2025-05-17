using Microsoft.VisualBasic;
using System.Net;

namespace LivrariaTech.Exception.Exception;

public abstract class LivrariaTechException : SystemException
{   
    public LivrariaTechException(string message) : base(message) {}
    public abstract List<string> GetErrorMessages();
    public abstract HttpStatusCode GetStatusCode();
}