using Microsoft.EntityFrameworkCore;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Users.Repositories
{
    public class ApplicationUsers: IApplicationUsers
    {
        private readonly ILogger<ApplicationUsers> logger;
        private readonly SampleContext context;

        public ApplicationUsers(ILogger<ApplicationUsers> logger,
            SampleContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<ApplicationUser?> GetByEmailForSigninAsync(string email)
        {
            return await this.context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Email == email);
        }
    }
}
