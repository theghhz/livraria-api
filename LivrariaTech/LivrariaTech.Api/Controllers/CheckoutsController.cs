using LivrariaTech.Api.Services;
using LivrariaTech.UseCases.UseCases.Checkouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTech.Api.Controllers;

[Route("[controller]")]
[ApiController]
[Authorize]
public class CheckoutsController : ControllerBase
{   
    private readonly RegisterBookCheckoutUseCase _registerBookCheckoutUseCase;
    private readonly LoggedUserService _loggedUserService;
    
    public CheckoutsController(RegisterBookCheckoutUseCase registerBookCheckoutUseCase) => _registerBookCheckoutUseCase = registerBookCheckoutUseCase;
    
    [HttpPost]
    [Route("{bookId}")]
    public IActionResult BookCheckOut(Guid bookId)
    {
        _registerBookCheckoutUseCase.Execute(bookId);
        
        return NoContent();
    }
}