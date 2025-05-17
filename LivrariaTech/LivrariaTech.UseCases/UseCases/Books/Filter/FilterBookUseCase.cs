
using LivrariaTech.Comunication.Requests;
using LivrariaTech.Comunication.Responses;
using LivrariaTech.Infrastructure;

namespace LivrariaTech.UseCases.UseCases.Books.Filter;

public class FilterBookUseCase
{   
    private readonly LivrariaTechDbContext _dbContext;
    
    public FilterBookUseCase(LivrariaTechDbContext dbContext) => _dbContext = dbContext;
    
    private const int PAGE_SIZE = 10;
    public ResponseBooksJson Execute(RequestFilterBooksJson request)
    {
        var query  = _dbContext.Books.AsQueryable();

        if (string.IsNullOrWhiteSpace(request.Title) == false)
        {
            query = query.Where(book => book.Title.Contains(request.Title));
        }
        
        var books = query
            .OrderBy(title => title.Title)
            .ThenBy(book => book.Author)
            .Skip(PAGE_SIZE * (request.PageNumber - 1))
            .Take(PAGE_SIZE)
            .ToList();
        
        var totalCount = 0;
        if (string.IsNullOrWhiteSpace(request.Title))
        {
            totalCount = _dbContext.Books.Count();
        }
        else
        {
            totalCount = _dbContext.Books.Count(book => book.Title.Contains(request.Title));
        }
        return new ResponseBooksJson
        {   
            Pagination = new ResponsePaginationJson
            {
                PageNumber = request.PageNumber,
                TotalCount = totalCount
            },
            Books = books.Select(book => new ResponseBookJson
            {
                Id = book.Id,
                Title = book.Title,
                Author = book.Author,
            }).ToList()
        };
    }
    
}