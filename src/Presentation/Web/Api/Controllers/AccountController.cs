using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Vimo.Infrastructure.Identity;

namespace Vimo.Web.Api.Controllers
{
    [AllowAnonymous]
    public class AccountController : BaseController
    {
        private readonly IUserManager _userManager;

        public AccountController(IUserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<string> SignIn(string username, string password)
        {
            var token = await _userManager.Authenticate(username, password);
            return token;
        }
    }
}