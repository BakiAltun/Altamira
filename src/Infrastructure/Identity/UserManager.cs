using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Vimo.ApplicationCore.Entities.UserAggregate;
using Vimo.ApplicationCore.Exceptions;
using Vimo.ApplicationCore.Interfaces.Data;
using Vimo.ApplicationCore.Specifications;

namespace Vimo.Infrastructure.Identity
{
    public class UserManager : IUserManager
    {
        private readonly IAsyncRepository<User> _userRepository;

        public const string SecretKey = "hmKv}t)bZvo+99;QJAIwppSF+LI7";
        public UserManager(IAsyncRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<string> Authenticate(string username, string password)
        {
            var user = await _userRepository.FirstOrDefaultAsync(new UserSpecification(username, password));

            if (user == null)
                throw new NotFoundException();


            // Authentication(Yetkilendirme) başarılı ise JWT token üretilir.
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username.ToString()),
                    new Claim(ClaimTypes.Email, user.Email.ToString()),
                 }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);


        }
    }

    public interface IUserManager
    {
        Task<string> Authenticate(string username, string password);
    }
}