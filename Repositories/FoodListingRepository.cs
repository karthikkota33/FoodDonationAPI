using FoodDonationAPI.Data;
using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;

namespace FoodDonationAPI.Repositories
{
    public class FoodListingRepository : IFoodListingRepository
    {
        private readonly IConfigurationRoot _configurationRoot;

        public FoodListingRepository(IConfiguration configurationRoot)
        {
            _configurationRoot = (IConfigurationRoot)configurationRoot;
        }
        public int SaveFoodListing(FoodListingModel saveFoodListingDetails, string userID)
        {
            //FoodListingData data = FoodListingData(_configurationRoot);
            return FoodListingData.SaveFoodListingDetails(saveFoodListingDetails, userID);
        }

        public IEnumerable<FoodListingModel> GetFoodListings(string userID)
        {
            return FoodListingData.GetFoodListings(userID);
        }

        public int RequestFood(FoodRequestModel requestDetails, string userID)
        {
            return FoodListingData.RequestFood(requestDetails, userID);
        }
    }
}
