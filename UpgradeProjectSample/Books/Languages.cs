using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;

namespace UpgradeProjectSample.Books;

public class Languages(SampleContext context): ILanguages
{
    public async Task<List<Language>> GetAllAsync() =>
        await context.Languages.ToListAsync();
}
