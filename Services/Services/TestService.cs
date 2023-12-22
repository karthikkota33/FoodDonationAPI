using FoodDonationAPI.Repositories.IRepositories;

namespace FoodDonationAPI.Services.Services
{
    public class TestService : ITest
    {
        private readonly ITestRepository _testRepository;

        public TestService(ITestRepository testRepository)
        {
            _testRepository = testRepository;
        }
        public string GetGuid()
        {
            return _testRepository.Test();
        }
    }
}
