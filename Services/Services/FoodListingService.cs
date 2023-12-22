using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories.IRepositories;

namespace FoodDonationAPI.Services.Services
{
    public class FoodListingService : IFoodListingService
    {
        private readonly IFoodListingRepository _foodRepository;

        public FoodListingService(IFoodListingRepository foodRepository)
        {
            _foodRepository = foodRepository;
        }
        public int SaveFoodListing(FoodListingModel saveFoodListingDetails, string userID)
        {
            return _foodRepository.SaveFoodListing(saveFoodListingDetails, userID);
        }

        public IEnumerable<FoodListingModel> GetFoodListings(string userID)
        {
            return _foodRepository.GetFoodListings(userID);
        }

        public int RequestFood(FoodRequestModel requestDetails, string userID)
        {
            return _foodRepository.RequestFood(requestDetails,userID);
        }
    }
}
