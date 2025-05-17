
using System.Net.Mime;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.Exception.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LivrariaTech.Api.Filters;

public class ExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        if(context.Exception is LivrariaTechException livrariaTechException){
            context.HttpContext.Response.StatusCode = (int)livrariaTechException.GetStatusCode();
            context.Result = new ObjectResult(new ResponseErrorMessageJson
            {
                Erros = livrariaTechException.GetErrorMessages()
            });
        }
        else
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Result = new ObjectResult(new ResponseErrorMessageJson
            {
                Erros = ["Erro desconhecido"]
            });
        }
    }
}