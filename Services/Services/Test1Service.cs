using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories;
using FoodDonationAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using static System.Net.Mime.MediaTypeNames;

namespace FoodDonationAPI.Services.Services
{
    public class Test1Service : ITest1
    {
        private readonly ITest1Repository _ITestRepo;

        public Test1Service(ITest1Repository ITestRepo)
        {
            _ITestRepo = ITestRepo;
        }

        public string Testing()
        {
           return _ITestRepo.Testing();
        }

        public IEnumerable<UserDetailsModel> Register(string userName)
        {
            return _ITestRepo.Register(userName);
        }
    }
}
