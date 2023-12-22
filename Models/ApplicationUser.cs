using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace FoodDonationAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
       //public int  UserId { get; set; }
        //public Guid ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string UserName { get; set; }
        //public string NormalizedUserName { get; set; }
        //public string Email { get; set; }
        //public string NormalizedEmail { get; set; }
        //public bool EmailConfirmed { get; set; }
        //public string PasswordHash { get; set; }
        //public string PhoneNumber { get; set; }
        //public bool PhoneNumberConfirmed { get; set; }
        ////public string PhotoUrl { get; set; }
        ////public string Address { get; set; }
        //public string? ConcurrencyStamp { get; set; }
        //public string SecurityStamp { get; set; }
        ////public DateTime? RegistrationDate { get; set; }
        ////public DateTime? LastLoginDate { get; set; }
        //public bool LockoutEnabled { get; set; }
        //public DateTime? LockoutEnd { get; set; }
        //public bool TwoFactorEnabled { get; set; }
        //public int AccessFailedCount { get; set; }
        //public int OrganisationId { get; set; }
        //public string DisplayName { get; set; }
        //public Nullable<int> CreatedBy { get; set; }
        public Nullable<System.DateTime> CreatedDate { get; set; }
        public Nullable<int> ModifiedBy { get; set; }
        public Nullable<System.DateTime> ModifiedDate { get; set; }
        //public Nullable<int> ClientInvitationId { get; set; }
        //Changing Status from 'bool' to 'int' to maintain different status levels 
        public Nullable<int> Status { get; set; }
        public Nullable<bool> IsNGO { get; set; }

        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        //public string AuthenticatorSecretKey { get; set; }
        //public string MobileSecurityPIN { get; set; }
        //public bool IsQRVerified { get; set; }

        //public DateTime? LockoutEndDateTimeUtc { get; set; }

    }
}
