using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;

namespace FoodDonationAPI.Services
{
    public interface IUserService
    {
        IdentityResult Register(ApplicationUser userModel);

        JwtSecurityToken GetUserToken(IConfiguration configurationRoot, ApplicationUser user);
    }
}
