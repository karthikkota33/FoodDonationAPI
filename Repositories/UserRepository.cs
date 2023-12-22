using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Web.Http.Results;

namespace FoodDonationAPI.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public IdentityResult Register(ApplicationUser userModel)
        {
            ////IdentityResult? identityResult;
            var userExists = _userManager.FindByEmailAsync(userModel.Email);
            if (userExists != null) return IdentityResult.Success;
            return IdentityResult.Failed();
        }
    }
}
