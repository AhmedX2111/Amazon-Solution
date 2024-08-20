using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Amazon.Users
{
    public class CustomSignInManager : SignInManager<IdentityUser>
    {
        public CustomSignInManager(UserManager<IdentityUser > userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<IdentityUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<IdentityUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<IdentityUser> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
        }

        public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            //var user = await UserManager.FindByNameAsync(userName);
            //if (user != null && !user.IsEnabled)
            //{
            //    return SignInResult.NotAllowed; // Prevent login
            //}

            return await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
    }
}
