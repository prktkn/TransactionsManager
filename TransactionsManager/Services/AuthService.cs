using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using TransactionsManager.DAL.Models;

namespace TransactionsManager.Services
{
    public class AuthService : IAuthService
    {
        private readonly DbContext _dbContext;
        public AuthService(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> Register(User user)
        {
            var existingUser = await _dbContext.Set<User>().FirstOrDefaultAsync(x => x.Login == user.Login);
            if (existingUser != null) return false;

            await _dbContext.Set<User>().AddAsync(new User { Login = user.Login, Password = user.Password });
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetUser(User user)
        {
            var currentUser = await _dbContext.Set<User>()
                .FirstOrDefaultAsync(x => x.Login == user.Login && x.Password == user.Password);
            return currentUser;
        }

        public string GenerateToken(string login)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, login.ToString())
            };

            var jwt = new JwtSecurityToken(
                   AuthOptions.ISSUER,
                   AuthOptions.AUDIENCE,
                   claims,
                   expires: DateTime.UtcNow.AddDays(1),
                   signingCredentials: new SigningCredentials(
                       AuthOptions.GetSymmetricSecurityKey(),
                       SecurityAlgorithms.HmacSha256)
                   );
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            return encodedJwt;
        }


    }
}
