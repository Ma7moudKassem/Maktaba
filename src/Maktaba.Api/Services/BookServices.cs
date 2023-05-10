namespace Maktaba.Api;

//[Authorize]
public class BookServices : MaktabaBase
{
    private readonly IBookRepository _repository;
    private readonly IJwtTokenValidationService _jwtService;
    public BookServices(
        IBookRepository repository,
        IJwtTokenValidationService jwtService)
    {
        _repository = repository;
        _jwtService = jwtService;
    }
    public async override Task<StatusMessage> AddBook(BookMessage request, 
        ServerCallContext context)
    {
        Book book = new()
        {
            Title = request.Title,
            Price = request.Price,
            Category = new()
            {
                Name = request.CategoryName
            },
            Description = request.Description,
        };

        await _repository.AddAsync(book, default);

        return new StatusMessage()
        {
            Message = "Success",
            Status = ReadingStatus.Success,
        };
    }

    public override async Task<StatusMessage> UpdateBook(BookToUpdateMessage request, 
        ServerCallContext context)
    {
        Book? book = await _repository.GetByIdAsync(new Guid(request.Id), default);

        if (book is null)
            throw new EntityNotFoundException(nameof(Book));

        book.Price = request.Price;
        book.Title = request.Title;
        book.Description = request.Description;
        book.Category.Name = request.Category.Name;
        book.CategoryId = new Guid(request.CategoryId);

        await _repository.UpdateAsync(book, default);
        return new StatusMessage()
        {
            Message = "Data Updated Succeful",
            Status = ReadingStatus.Success,
        };
    }

    public override async Task<BookMessage> GetBook(UUID request, ServerCallContext context)
    {
        Book? book = await _repository.GetByIdAsync(new Guid(request.Id), default) ??
            throw new EntityNotFoundException(nameof(Book));

        return new BookMessage
        {
            Title = book.Title,
            CategoryName = book.Category.Name,
            Description = book.Description,
            Price = book.Price,
        };
    }

    [AllowAnonymous]
    public override async Task<TokenResponce> GenerateToken(
        TokenRequest request, ServerCallContext context)
    {
        CredentialModel credentialModel = new()
        {
            UserName = request.UserName,
            Password = request.Password
        };

        var result = await _jwtService.GenerateTokenModelAsync(credentialModel);

        if (result.Success)
        {
            return new TokenResponce()
            {
                Success = true,
                Token = result.Token,
                Expiration = Timestamp.FromDateTime(result.Expiration),
            };
        }
        else
        {
            return new TokenResponce()
            {
                Success = false,
                Token = string.Empty,
            };
        }
    }

    public override async Task<Empty> AddReadingStream(IAsyncStreamReader<BookMessage> requestStream, ServerCallContext context)
    {
        while (await requestStream.MoveNext())
        {
            var msg = requestStream.Current;
        }

        return new Empty();
    }
}