using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDonationAPI.Services
{
    public interface ITest1
    {
        string Testing();

        IEnumerable<UserDetailsModel> Register(string userName);
    }
}
