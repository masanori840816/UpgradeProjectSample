using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UpgradeProjectSample.Models;
using UpgradeProjectSample.Users.Models;

namespace UpgradeProjectSample.Users
{
    public class ApplicationUserStore: IUserPasswordStore<ApplicationUser>
    {
        private readonly ILogger<ApplicationUserStore> logger;
        private readonly SampleContext context;
        public ApplicationUserStore(ILogger<ApplicationUserStore> logger,
            SampleContext context)
        {
            this.logger = logger;
            this.context = context;
        }
        public async Task<IdentityResult> CreateAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            // validation
            var validationError = user.Validate();
            if(string.IsNullOrEmpty(validationError) == false)
            {
                return IdentityResult.Failed(new IdentityError { Description = validationError });
            }
            using var transaction = await context.Database.BeginTransactionAsync();
            
            if(await context.ApplicationUsers
                .AnyAsync(u => u.Email == user.Email,
                cancellationToken))
            {
                return IdentityResult.Failed(new IdentityError
                {
                    Description = "Your e-mail address is already used"
                });
            }
            var newUser = new ApplicationUser();
            newUser.Update(user);
            await context.ApplicationUsers.AddAsync(newUser, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync();
            return IdentityResult.Success;
        }
        public async Task<IdentityResult> DeleteAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            ApplicationUser target = await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);
            if(target != null)
            {
                context.ApplicationUsers.Remove(target);
                await context.SaveChangesAsync(cancellationToken);
            }
            return IdentityResult.Success;
        }
        public void Dispose() { /* do nothing */ }
        public async Task<ApplicationUser> FindByIdAsync(string userId,
            CancellationToken cancellationToken)
        {
            if(int.TryParse(userId, out var id) == false)
            {
                return new ApplicationUser();
            }
            var result = await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == id,
                cancellationToken);
            return result ?? new ApplicationUser();
        }
        public async Task<ApplicationUser> FindByNameAsync(string normalizedUserName,
            CancellationToken cancellationToken)
        {
            var result = await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.UserName.ToUpper() == normalizedUserName,
                cancellationToken);
            return result ?? new ApplicationUser();
        }
        public async Task<string> GetNormalizedUserNameAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.NormalizedUserName);
        }
        public async Task<string> GetPasswordHashAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.PasswordHash);
        }
        public async Task<string> GetUserIdAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.Id.ToString());
        }
        public async Task<string> GetUserNameAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(user.UserName);
        }
        public async Task<bool> HasPasswordAsync(ApplicationUser user,
            CancellationToken cancellationToken)
        {
            return await Task.FromResult(true);
        }
        public async Task SetNormalizedUserNameAsync(ApplicationUser user,
            string normalizedName, CancellationToken cancellationToken)
        {
            // do nothing
            await Task.Run(() => {});
        }
        public async Task SetPasswordHashAsync(ApplicationUser user, string passwordHash,
            CancellationToken cancellationToken)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            ApplicationUser target = await context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);
            if(target != null)
            {
                target.PasswordHash = passwordHash;
                // validation
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }           
        }
        public async Task SetUserNameAsync(ApplicationUser user, string userName, CancellationToken cancellationToken)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            
            ApplicationUser target = await context.ApplicationUsers
                    .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);
            if(target != null)
            {
                target.UserName = userName;
                // validation
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
        }
        public async Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            var validationError = user.Validate();
            if(string.IsNullOrEmpty(validationError) == false)
            {
                return IdentityResult.Failed(new IdentityError { Description = validationError });
            }
            using var transaction = await context.Database.BeginTransactionAsync();

            ApplicationUser target = await context.ApplicationUsers
                .FirstOrDefaultAsync(u => u.Id == user.Id,
                cancellationToken);
            if(target == null)
            {
                return IdentityResult.Failed(new IdentityError { Description = "Target user was not found" });
            }
            // validation
            target.Update(user);
            await context.SaveChangesAsync(cancellationToken);
            transaction.Commit();
            return IdentityResult.Success;
        }
    }
}