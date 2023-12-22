using FoodDonationAPI.Helpers;
using FoodDonationAPI.Models;
using FoodDonationAPI.Repositories.IRepositories;
using Microsoft.AspNetCore.Identity;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Web.Http.Results;

namespace FoodDonationAPI.Repositories
{
    public class Test1Repository : ITest1Repository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfigurationRoot _configurationRoot;

        public Test1Repository(UserManager<ApplicationUser> userManager, IConfiguration configurationRoot)
        {
            _userManager = userManager;
            _configurationRoot = (IConfigurationRoot)configurationRoot;
        }

        public string Testing()
        {
            return "Karthik";
        }

        public IEnumerable<UserDetailsModel> Register(string userName)
        {
            //SQLHelper sqlHelp = new SQLHelper(_configurationRoot);

            SqlParameter[] sqlParams = new SqlParameter[1];

            sqlParams[0] = new SqlParameter("@UserName", userName);

            DataTable dtUserDetails = SQLHelpers.ExecuteDataTable("sp_GetUsers", sqlParams);

            List<UserDetailsModel> lstUserDetails = FoodDonationAPI.Helpers.Helper.ConvertDataTable<UserDetailsModel>(dtUserDetails);
            return lstUserDetails;

            //return IdentityResult.Success;
            //var usr =  _userManager.FindByEmailAsync(userModel.Email);
            //if(usr != null)
            //{
            //    return IdentityResult.Success;
            //}
            //else
            //{
            //    return IdentityResult.Failed();
            //}
        }
    }
}
