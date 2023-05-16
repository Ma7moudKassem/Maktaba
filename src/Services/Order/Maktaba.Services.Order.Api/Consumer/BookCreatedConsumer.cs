using Maktaba.Services.Catalog.Domain;

namespace Maktaba.Services.Order.Api.Consumer
{
    public class BookCreatedConsumer : IConsumer<BookDto>
    {
        private readonly OrderDbContext _context;
        public BookCreatedConsumer(OrderDbContext context)
        {

        }

        public Task Consume(ConsumeContext<BookDto> context)
        {
            throw new NotImplementedException();
        }
    }
}