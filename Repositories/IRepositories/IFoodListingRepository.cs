using FoodDonationAPI.Models;

namespace FoodDonationAPI.Repositories.IRepositories
{
    public interface IFoodListingRepository
    {
        int SaveFoodListing(FoodListingModel saveFoodListingDetails, string userID);

        IEnumerable<FoodListingModel> GetFoodListings(string userID);

        int RequestFood(FoodRequestModel requestDetails, string userID);
    }
}
