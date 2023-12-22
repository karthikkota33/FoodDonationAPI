using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDonationAPI.Repositories.IRepositories
{
    public interface IUserRepository
    {
        IdentityResult Register(ApplicationUser userModel);
    }
}
