using System.Net;
using Microsoft.VisualBasic;

namespace LivrariaTech.Exception.Exception;

public class ErrorOnValidationException : LivrariaTechException 
{   
    private readonly List<string> _erros;
	
    public ErrorOnValidationException(List<string> errorMessages) : base(string.Empty)
    {
        _erros = errorMessages;
    }

	public override List<string> GetErrorMessages()
	{
		return _erros;
	}

	public override HttpStatusCode GetStatusCode()
	{
		return HttpStatusCode.BadRequest;
	}
}