using Dapper;
using FoodDonationAPI.Models;
using System.Data.SqlClient;
using System.Security.Claims;

namespace FoodDonationAPI.Common.Tables
{
    public class UsersClaimsTable
    {
        private readonly SqlConnection _sqlConnection;

        public UsersClaimsTable(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task<IList<Claim>> GetClaimsAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "SELECT * " +
                                   "FROM dbo.UserClaims " +
                                   "WHERE UserId = @UserId;";

            IEnumerable<UserClaim> userClaims = Task.Run(() => _sqlConnection.QueryAsync<UserClaim>(command, new
            {
                UserId = user.UserId
            }), cancellationToken).Result;

            return Task.FromResult<IList<Claim>>(userClaims.Select(e => new Claim(e.ClaimType, e.ClaimValue)).ToList());
        }

        public Task AddClaimsAsync(ApplicationUser user, IEnumerable<Claim> claims)
        {
            const string command = "INSERT INTO dbo.UserClaims " +
                                   "VALUES (@Id, @UserId, @ClaimType, @ClaimValue);";

            return _sqlConnection.ExecuteAsync(command, claims.Select(e => new
            {
                Id = Guid.NewGuid(),
                UserId = user.UserId,
                ClaimType = e.Type,
                ClaimValue = e.Value
            }));
        }

        public Task ReplaceClaimAsync(ApplicationUser user, Claim claim, Claim newClaim)
        {
            const string command = "UPDATE dbo.UserClaims " +
                                   "SET ClaimType = @NewClaimType, ClaimValue = @NewClaimValue " +
                                   "WHERE UserId = @UserId AND ClaimType = @ClaimType AND ClaimValue = @ClaimType;";

            return _sqlConnection.ExecuteAsync(command, new
            {
                NewClaimType = newClaim.Type,
                NewClaimValue = newClaim.Value,
                UserId = user.UserId,
                ClaimType = claim.Type,
                ClaimValue = claim.Value
            });
        }
    }
}
