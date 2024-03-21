using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Strawberry.Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Strawberry.Server.Manager.Controllers
{
    [Route("authentication/[action]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public DatabaseContext Db { get; }

        public AuthenticationController(DatabaseContext db)
        {
            this.Db = db;
        }

        [HttpPost]
        public async Task<object> Login(
            [FromForm] string userid, 
            [FromForm] string passwd)
        {
            try
            {
                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, userid));

                if (await this.Db.Settings.AnyAsync(x => x.AdminId == userid && x.AdminPw == passwd))
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
                }
                else if (await this.Db.Managers.AnyAsync(x => x.UserId == userid && x.Passwd == passwd))
                {
                    var role = await this.Db.Managers
                        .Where(x => x.UserId == userid && x.Passwd == passwd)
                        .Select(x => x.Role)
                        .FirstOrDefaultAsync();
                    claims.Add(new Claim(ClaimTypes.Role, role));
                }
                else
                {
                    throw new Exception("아이디 또는 비밀번호가 일치하지 않습니다.");
                }

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var authProperties = new AuthenticationProperties
                {
                    //AllowRefresh = <bool>,
                    // Refreshing the authentication session should be allowed.

                    //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                    // The time at which the authentication ticket expires. A 
                    // value set here overrides the ExpireTimeSpan option of 
                    // CookieAuthenticationOptions set with AddCookie.

                    //IsPersistent = true,
                    // Whether the authentication session is persisted across 
                    // multiple requests. When used with cookies, controls
                    // whether the cookie's lifetime is absolute (matching the
                    // lifetime of the authentication ticket) or session-based.

                    //IssuedUtc = <DateTimeOffset>,
                    // The time at which the authentication ticket was issued.

                    //RedirectUri = <string>
                    // The full path or absolute URI to be used as an http 
                    // redirect response value.
                };

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity),
                    authProperties);

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }

        [HttpPost]
        public object Logout()
        {
            try
            {
                this.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

                return new { };
            }
            catch (Exception ex)
            {
                return new { ex.Message };
            }
        }
    }
}
