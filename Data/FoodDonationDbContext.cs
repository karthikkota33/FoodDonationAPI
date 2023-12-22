using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace FoodDonationAPI.Data
{
    public class FoodDonationDbContext : IdentityDbContext<ApplicationUser>
    {
        public FoodDonationDbContext(DbContextOptions<FoodDonationDbContext> options) : base(options)
        {
                
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
