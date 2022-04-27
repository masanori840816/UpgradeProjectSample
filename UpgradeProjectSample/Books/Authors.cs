using UpgradeProjectSample.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace UpgradeProjectSample.Books
{
    public class Authors: IAuthors
    {
        private readonly SampleContext context;
        private readonly ILogger<Authors> logger;
        public Authors(SampleContext context,
            ILogger<Authors> logger)
        {
            this.context = context;
            this.logger = logger;
        }
        public async Task<Author> GetOrCreateAsync(string name)
        {
            var exited = await this.context.Authors
                .FirstOrDefaultAsync(a => a.Name == name);
            if(exited != null)
            {
                return exited;
            }
            var newAuthor = Author.Create(name);
            await this.context.Authors.AddAsync(newAuthor);
            await this.context.SaveChangesAsync();
            return newAuthor;
        }
        public async Task<List<Author>> GetByNameAsync(string name)
        {
            return await this.context.Authors
                .Where(a => a.Name.Contains(name))
                .ToListAsync();
        }
        public async Task<Author> GetTrackedAuthorAsync(int id)
        {
            return await this.context.Authors
                .FirstOrDefaultAsync(a => a.Id == id);
        }
    }
}
