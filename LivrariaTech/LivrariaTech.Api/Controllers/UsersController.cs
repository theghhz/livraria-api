using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.UseCases.Users.Register;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTech.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{   
    private readonly RegisterUserUseCase _registerUserUseCase;

    public UserController(RegisterUserUseCase registerUserUseCase) => _registerUserUseCase = registerUserUseCase;

    [HttpPost]
    [ProducesResponseType(typeof(ResponseRegisteresUserJson), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ResponseErrorMessageJson), StatusCodes.Status400BadRequest)]
    public IActionResult Register(RequestUserJson request)
    {
        var response = _registerUserUseCase.Execute(request);

        return Created(string.Empty, response);
    }
}

