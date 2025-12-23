using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Authentificate(string username, string password)
        {
            var users = DBExample.GetDB().Users.ToList();
            var userTest = users.FirstOrDefault(u => u.Fio.Equals(username) && 
            Convert.
            ToBase64String(
                SHA256.HashData(
                    Encoding.UTF8.GetBytes(password)
                    )
                ).Equals(u.Password));
            var user = await DBExample.GetDB().Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.Fio.Equals(username) && Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password))).Equals(u.Password));
            if (user is null)
                return Unauthorized();

            var identity = new ClaimsIdentity([new (ClaimsIdentity.DefaultRoleClaimType, user.Role.Title), new (ClaimsIdentity.DefaultRoleClaimType, user.Role.Title)], "Token", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var now = DateTime.UtcNow;

            return Ok(new JwtSecurityTokenHandler().WriteToken(new JwtSecurityToken
                (
                    issuer: Config.ISSUER,
                    //audience: Config.AUDIENCE,
                    claims: identity.Claims,
                    notBefore: now,
                    expires: now.AddMinutes(Config.LIFE_TIME),
                    signingCredentials: new(Config.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
                )));
        }
    }
}
