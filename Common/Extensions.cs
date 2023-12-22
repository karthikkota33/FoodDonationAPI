using System.Security.Claims;

namespace FoodDonationAPI.Common
{
    public static class Extensions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated
                ? user.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier).Value
                : string.Empty;
        }

        public static string GetUserEmail(this ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated
                ? user.Claims.FirstOrDefault(e => e.Type == ClaimTypes.Email).Value
                : string.Empty;
        }
        public static IEnumerable<string> GetUserRoles(this ClaimsPrincipal user)
        {
            return user.Identity.IsAuthenticated
                ? user.Claims.Where(e => e.Type == ClaimTypes.Role).Select(x => x.Value).ToList()
                : null;
        }
    }
}
