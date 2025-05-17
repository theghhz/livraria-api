using LivrariaTech.Comunication.Requests;
using LivrariaTech.Domain.Entities;
using LivrariaTech.Exception.Exception;
using LivrariaTech.Infrastructure;
using LivrariaTech.UseCases.UseCases.Checkouts.Interfaces;

namespace LivrariaTech.UseCases.UseCases.Checkouts;

public class RegisterBookCheckoutUseCase
{   
    private readonly LivrariaTechDbContext _dbContext;
    private readonly IUserService _loggedUserService;
    public RegisterBookCheckoutUseCase(LivrariaTechDbContext dbContext, IUserService loggedUserService) => (_dbContext, _loggedUserService) = (dbContext, loggedUserService);
    
    private const int MAX_LOAN_DAYS = 7;
    public void Execute(Guid bookId)
    {   
        var userId = _loggedUserService.GetLoggedUserId();
        
        Validade(_dbContext, bookId);
        
        _dbContext.Checkouts.Add(new Domain.Entities.Checkout
        {   
            UserId = userId,
            BookId = bookId,
            ExpectedReturnDate = DateTime.Now.AddDays(MAX_LOAN_DAYS)
        });
        
        _dbContext.SaveChanges();
    }
    private void Validade(LivrariaTechDbContext dbContext , Guid bookId)
    {
        var book = dbContext.Books.FirstOrDefault(book => book.Id == bookId);

        if (book is null)
        {
            throw new NotFoundException("Book not found");
        }

        var amoutBookNotReturned = dbContext.Checkouts.Count(checkout => checkout.BookId == bookId && checkout.ReturnDate == null);

        if (amoutBookNotReturned == book.Amount)
        {
            throw new ConflictException("There is no book available for loan");
        }
    }
}