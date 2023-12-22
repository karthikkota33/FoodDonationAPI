using Dapper;
using FoodDonationAPI.Models;
using System.Data.SqlClient;

namespace FoodDonationAPI.Common.Tables
{
    public class UsersRolesTable
    {
        private readonly SqlConnection _sqlConnection;

        public UsersRolesTable(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task AddToRoleAsync(ApplicationUser user, Guid roleId)
        {
            const string command = "INSERT INTO dbo.UserRoles " +
                                   "VALUES (@UserId, @RoleId);";

            return _sqlConnection.ExecuteAsync(command, new
            {
                UserId = user.UserId,
                RoleId = roleId
            });
        }

        public Task RemoveFromRoleAsync(ApplicationUser user, Guid roleId)
        {
            const string command = "DELETE " +
                                   "FROM dbo.UserRoles " +
                                   "WHERE UserId = @UserId AND RoleId = @RoleId;";

            return _sqlConnection.ExecuteAsync(command, new
            {
                UserId = user.UserId,
                RoleId = roleId
            });
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "SELECT r.Name " +
                                   "FROM dbo.Roles as r " +
                                   "INNER JOIN dbo.UserRoles AS ur ON ur.RoleId = r.Id " +
                                   "WHERE ur.UserId = @UserId;";

            IEnumerable<string> userRoles = Task.Run(() => _sqlConnection.QueryAsync<string>(command, new
            {
                UserId = user.UserId
            }), cancellationToken).Result;

            return Task.FromResult<IList<string>>(userRoles.ToList());
        }
    }
}
