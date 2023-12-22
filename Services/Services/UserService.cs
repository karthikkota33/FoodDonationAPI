using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FoodDonationAPI.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public IdentityResult Register(ApplicationUser userModel)
        {
            return _userRepository.Register(userModel);
        }

        public JwtSecurityToken GetUserToken(IConfiguration configurationRoot, ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Email, user.Email),
                        new Claim(JwtRegisteredClaimNames.Name, user.UserName.ToString()),
                        new Claim(JwtRegisteredClaimNames.NameId, user.Id.ToString()),
                        //Field [Status] indicates the current status of the User account. (ACTIVE/INACTIVE/LOCKED)
                        new Claim("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/AccountStatus",user.Status.ToString())
                        
            };

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configurationRoot["JwtSecurityToken:Key"]));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: configurationRoot["JwtSecurityToken:Issuer"],
                //audience: configurationRoot["JwtSecurityToken:Audience"],
                claims: claims,
                //Setting Token expiration time to 8 Hours
                expires: DateTime.UtcNow.AddMinutes(480),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
    }
}
