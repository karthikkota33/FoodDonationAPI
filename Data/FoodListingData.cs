using FoodDonationAPI.Helpers;
using FoodDonationAPI.Models;
using System.Data;
using System.Data.SqlClient;

namespace FoodDonationAPI.Data
{
    public static class FoodListingData
    {
        /// <summary>
        /// Saves the Food orders and the food items 
        /// First save the oders and insert in items data
        /// </summary>
        /// <param name="saveFoodListingDetails"></param>
        /// <returns></returns>
        public static int SaveFoodListingDetails(FoodListingModel saveFoodListingDetails, string userID)
        {
            //SQLHelper helper = new SQLHelper(_configurationRoot);
            Dictionary<string, object> dtValues = new Dictionary<string, object>();
            dtValues.Add("DonorType", saveFoodListingDetails.DonorType);
            dtValues.Add("Name", saveFoodListingDetails.DonorName);
            dtValues.Add("Location", saveFoodListingDetails.Location);
            dtValues.Add("PickUpDate", saveFoodListingDetails.PickUpDate);
            dtValues.Add("FoodStatus", "Available");
            dtValues.Add("Comments", saveFoodListingDetails.Comments);
            dtValues.Add("fd_CreatedBy", userID);
            dtValues.Add("fd_CreatedDate", DateTime.Now);
            dtValues.Add("fd_AddedTag", "Added the food order details");

            int orderID = SQLHelpers.ExecuteInsertCommandAndReturnIdentity("tblFoodOrderDetails", dtValues);
            int result = 0;
            if (orderID > 0)
            {
                foreach(FoodDetails item in saveFoodListingDetails.FoodDetails)
                {
                    Dictionary<string, object> dtFoodDetails = new Dictionary<string, object>();
                    dtFoodDetails.Add("OrderID", orderID);
                    dtFoodDetails.Add("ItemName", item.ItemName);
                    dtFoodDetails.Add("Quantity", item.Quantity);
                    dtFoodDetails.Add("ExpirationDate", item.ExpirationDate);
                    dtFoodDetails.Add("FoodType", item.FoodType);
                    dtFoodDetails.Add("fd_CreatedBy", userID);
                    dtFoodDetails.Add("fd_CreatedDate", DateTime.Now);
                    dtFoodDetails.Add("fd_AddedTag", "Food Items Added.");
                    result = SQLHelpers.ExecuteInsertCommandAndReturnIdentity("tblFoodItemDetails", dtFoodDetails);
                }
            }
            return result;
        }

        public static IEnumerable<FoodListingModel> GetFoodListings(string userID)
        {
            SqlParameter[] sqlparams = new SqlParameter[1];
            sqlparams[0] = new SqlParameter("@UserID", userID);
            DataSet dtResults = SQLHelpers.ExecuteDataSet("sp_GetFoodListings", sqlparams);
            List<FoodListingModel> lstOrderDetails = FoodDonationAPI.Helpers.Helper.ConvertDataTable<FoodListingModel>(dtResults.Tables[0]);
            List<FoodDetails> lstItemDetails = FoodDonationAPI.Helpers.Helper.ConvertDataTable<FoodDetails>(dtResults.Tables[1]);
            List<FoodListingModel> result = new List<FoodListingModel>();
            foreach(FoodListingModel item in lstOrderDetails)
            {
                FoodListingModel orderDetail = new FoodListingModel();
                //FoodDetails itemDetail = new FoodDetails();

                orderDetail.DonorName = item.DonorName;
                orderDetail.DonorType = item.DonorType;
                orderDetail.Comments = item.Comments;
                orderDetail.Location = item.Location;
                orderDetail.PickUpDate = item.PickUpDate;
                orderDetail.FoodDetails = lstItemDetails.Where(x => x.OrderID == item.ID).ToList<FoodDetails>();
                result.Add(orderDetail);
            }
            
            return result;
        }

        public static int RequestFood(FoodRequestModel requestDetails, string userID)
        {
            Dictionary<string, object> updateValues = new Dictionary<string, object>();
            Dictionary<string, object> whereConditions = new Dictionary<string, object>();
            updateValues.Add("RequestedBy", userID);
            updateValues.Add("FoodStatus", "Requested");
            whereConditions.Add("ID", requestDetails.OrderId);
            return SQLHelpers.ExecuteUpdateCommand("tblFoodOrderDetails", updateValues, whereConditions);
        }
    }
}
