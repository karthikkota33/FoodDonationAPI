using FoodDonationAPI.Common;
using FoodDonationAPI.Models;
using FoodDonationAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FoodDonationAPI.Controllers
{
    [Route("api/FoodListing")]
    [ApiController]
    public class FoodListingController : ControllerBase
    {
        private readonly IFoodListingService _foodListing;

        public FoodListingController(IFoodListingService foodListing)
        {
            _foodListing = foodListing;
        }

        /// <summary>
        /// Saves food listing details when the user posts
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("SaveFoodListing")]
        public IActionResult SaveFoodListing(FoodListingModel saveFoodListingDetails)
        {
            string userID = User.GetUserId();
            int status = _foodListing.SaveFoodListing(saveFoodListingDetails, userID);
            if(status > 0) return Ok(true);
            else
            {
                return Ok(false);
            }
        }


        [Authorize]
        [HttpGet]
        [Route("GetFoodListings")]
        public IActionResult GetFoodListings()
        {
            string userID = User.GetUserId();
            IEnumerable<FoodListingModel> usrDetails = _foodListing.GetFoodListings(userID);
            return Ok(usrDetails);
        }

        /// <summary>
        /// Requests the food from the listing
        /// We need to update the requestor userId against the order in the respective table
        /// </summary>
        /// <param name="requestDetails"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("RequestFood")]
        public IActionResult RequestFood(FoodRequestModel requestDetails)
        {
            string userID = User.GetUserId();
            int status = _foodListing.RequestFood(requestDetails, userID);
            if (status > 0) return Ok(true);
            else return BadRequest(false);
        }
    }
}
