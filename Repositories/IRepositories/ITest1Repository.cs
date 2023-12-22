using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace FoodDonationAPI.Repositories.IRepositories
{
    public interface ITest1Repository
    {
        string Testing();

        IEnumerable<UserDetailsModel> Register(string userName);
    }
}
