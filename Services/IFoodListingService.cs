using FoodDonationAPI.Models;

namespace FoodDonationAPI.Services
{
    public interface IFoodListingService
    {
        int SaveFoodListing(FoodListingModel saveFoodListingDetails, string userID);

        IEnumerable<FoodListingModel> GetFoodListings(string userID);

        int RequestFood(FoodRequestModel requestDetails, string userID);
    }
}
