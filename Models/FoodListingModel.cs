namespace FoodDonationAPI.Models
{
    public class FoodListingModel
    {
        public int ID { get; set; }
        public string DonorName { get; set; }
        public string DonorType { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }
        public string Comments { get; set; }
        public DateTime PickUpDate { get; set; }

        public List<FoodDetails> FoodDetails { get; set; }
    }

    public class FoodDetails
    {
        public int OrderID { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }

        public DateTime ExpirationDate { get; set; }
        public string FoodType { get; set; } //whether is it Veg or Non-Veg
    }
}
