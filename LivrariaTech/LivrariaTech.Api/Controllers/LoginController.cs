using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.UseCases.Login.DoLogin;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTech.Api.Controllers;

[Route("[Controller]")]
[ApiController]
public class LoginController : ControllerBase
{   
    private readonly DoLoginUseCase _doLoginUseCase;
    
    public LoginController(DoLoginUseCase doLoginUseCase)
    {
        _doLoginUseCase = doLoginUseCase;
    }
    
    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteresUserJson), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status401Unauthorized)]
    public IActionResult DoLogin(RequestLoginJson request){

        var responde = _doLoginUseCase.Execute(request);

        return Ok(responde);
    }
}