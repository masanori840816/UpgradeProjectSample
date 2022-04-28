using Microsoft.EntityFrameworkCore;
using UpgradeProjectSample.Models;

namespace UpgradeProjectSample.Books;
public class Books: IBooks
{
    private readonly SampleContext context;
    private readonly ILogger<Books> logger;
    public Books(SampleContext context,
        ILogger<Books> logger)
    {
        this.context = context;
        this.logger = logger;
    }
    public async Task CreateAsync(Author author, Book newItem)
    {
        await this.context.Books.AddAsync(Book.Create(author, newItem));
        await this.context.SaveChangesAsync();
    }
    public async Task<Book?> GetTrackedBookAsync(int id)
    {
        return await this.context.Books
            .Include(b => b.Author)
            .FirstOrDefaultAsync(b => b.Id == id);
    }
}
