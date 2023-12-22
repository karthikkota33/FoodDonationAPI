using FoodDonationAPI.Repositories.IRepositories;

namespace FoodDonationAPI.Repositories
{
    public class TestRepository : ITestRepository
    {
        public string Test()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
