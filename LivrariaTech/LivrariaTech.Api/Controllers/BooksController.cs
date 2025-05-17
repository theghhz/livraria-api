using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.UseCases.UseCases.Books.Filter;
using Microsoft.AspNetCore.Mvc;

namespace LivrariaTech.Api.Controllers;

[Route("[controller]")]
[ApiController]
public class BooksController : ControllerBase {
    
    private readonly FilterBookUseCase _filterBookUseCase;
    
    public BooksController(FilterBookUseCase filterBookUseCase) => _filterBookUseCase = filterBookUseCase;
    
    [HttpGet("Filter")]
    [ProducesResponseType(typeof(ResponseBooksJson), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Filter(int pageNumber , string? title) {

        var result = _filterBookUseCase.Execute(new RequestFilterBooksJson {
            PageNumber = pageNumber,
            Title = title
        });

        if(result.Books.Count > 0) {
            return Ok(result);
        }
        
        return NoContent();
    }
}