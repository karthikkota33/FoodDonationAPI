using System.ComponentModel.DataAnnotations;

namespace FoodDonationAPI.Models
{
    public class LoginModel
    {
        [Required(AllowEmptyStrings =false, ErrorMessage = "Please enter your email address")]
        //[RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Please provide a valid email address")]
        public string Email { set; get; }

        [Required(AllowEmptyStrings =false, ErrorMessage = "Please specify a password")]
        [DataType(DataType.Password)]
        public string Password { set; get; }
    }
}
