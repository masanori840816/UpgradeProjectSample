using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UpgradeProjectSample.Books
{
    public class Languages: ILanguages
    {
        private readonly SampleContext context;
        public Languages(SampleContext context)
        {
            this.context = context;
        }
        public async Task<List<Language>> GetAllAsync()
        {
            return await this.context.Languages
                .ToListAsync();
        }
    }
}
