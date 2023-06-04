using System.Security.AccessControl;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebApp
{
    [Route("api")]
    public class LoginController : Controller
    {
        private readonly IAccountDatabase _db;

        public LoginController(IAccountDatabase db)
        {
            _db = db;
        }

        [HttpPost("sign-in")]
        public async Task<IActionResult> Login(string userName)
        {
            var account = await _db.FindByUserNameAsync(userName);
            if (account != null)
            {
                //TODO 1: Generate auth cookie for user 'userName' with external id
                var externalId = account.ExternalId;
                var authCookie = GenerateAuthCookie(userName, externalId);
                Response.Cookies.Append("AuthCookie", authCookie);

                return Ok();
            }
            //TODO 2: return 404 if user not found

            return NotFound();
        }

        private string GenerateAuthCookie(string userName, string externalId)
        {
            return "cookie";
        }
    }
}