using FoodDonationAPI.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.SqlClient;
using Dapper;

namespace FoodDonationAPI.Common.Tables
{
    public class UsersTable
    {
        private SqlConnection _sqlConnection;

        public UsersTable(SqlConnection sqlConnection)
        {
            _sqlConnection = sqlConnection;
        }

        public Task<IdentityResult> CreateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "INSERT INTO dbo.[User]([UserId],[FirstName],[LastName],[UserName],[NormalizedUserName],[Email],[NormalizedEmail],[EmailConfirmed],[PasswordHash],[PhoneNumber],[PhoneNumberConfirmed],[PhotoUrl],[Address],[ConcurrencyStamp],[SecurityStamp],[RegistrationDate],[LastLoginDate],[LockoutEnabled],[LockoutEndDateTimeUtc],[TwoFactorEnabled],[AccessFailedCount],[Title],[SecondName],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate],[OrganisationId],[DisplayName],[Status]) " +
                                   "VALUES (@UserId, @FirstName, @LastName, @UserName, @NormalizedUserName, @Email, @NormalizedEmail, @EmailConfirmed, " +
                                           "@PasswordHash, @PhoneNumber, @PhoneNumberConfirmed, @PhotoUrl, @Address, @ConcurrencyStamp, @SecurityStamp, " +
                                           "@RegistrationDate, @LastLoginDate, @LockoutEnabled, @LockoutEndDateTimeUtc, @TwoFactorEnabled, @AccessFailedCount, " +
                                           "@Title,@SecondName,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,@OrganisationId,@DisplayName,@Status );";

            int rowsInserted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
            {
                user.UserId,
                user.FirstName,
                user.LastName,
                user.UserName,
                user.NormalizedUserName,
                user.Email,
                user.NormalizedEmail,
                user.EmailConfirmed,
                user.PasswordHash,
                user.PhoneNumber,
                user.PhoneNumberConfirmed,
                //user.PhotoUrl,
                //user.Address,
                user.ConcurrencyStamp,
                user.SecurityStamp,
                //user.RegistrationDate,
                //user.LastLoginDate,
                user.LockoutEnabled,
                user.LockoutEndDateTimeUtc,
                user.TwoFactorEnabled,
                user.AccessFailedCount,
                //user.Title,
                //user.SecondName,
                //user.CreatedBy,
                user.CreatedDate,
                user.ModifiedBy,
                user.ModifiedDate,
                //user.OrganisationId,
                //user.DisplayName,
                //Added Status field
                user.Status

            }), cancellationToken).Result;

            return Task.FromResult(rowsInserted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = string.Empty,
                Description = $"The user with email {user.Email} could not be inserted in the dbo.[User] table."
            }));
        }

        public Task<IdentityResult> DeleteAsync(ApplicationUser user, CancellationToken cancellationToken)
        {
            const string command = "DELETE " +
                                   "FROM dbo.[User] " +
                                   "WHERE UserId = @UserId;";

            int rowsDeleted = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
            {
                user.UserId
            }), cancellationToken).Result;

            return Task.FromResult(rowsDeleted.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
            {
                Code = string.Empty,
                Description = $"The user with email {user.Email} could not be deleted from the dbo.[User] table."
            }));
        }

        public Task<ApplicationUser?> FindByIdAsync(Guid userId)
        {
            const string command = "SELECT * " +
                                   "FROM dbo.[User] " +
                                   "WHERE UserId = @UserId;";

            return _sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
            {
                UserId = userId
            });
        }

        public Task<ApplicationUser?> FindByNameAsync(string normalizedUserName)
        {
            const string command = "SELECT * " +
                                   "FROM dbo.[User] " +
                                   "WHERE NormalizedUserName = @NormalizedUserName;";

            return _sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
            {
                NormalizedUserName = normalizedUserName
            });
        }

        public Task<ApplicationUser?> FindByEmailAsync(string normalizedEmail)
        {
            const string command = "SELECT * " +
                                   "FROM dbo.[User] " +
                                   "WHERE NormalizedEmail = @NormalizedEmail;";

            return _sqlConnection.QuerySingleOrDefaultAsync<ApplicationUser>(command, new
            {
                NormalizedEmail = normalizedEmail
            });
        }

        public Task<IdentityResult> UpdateAsync(ApplicationUser user, CancellationToken cancellationToken)
        {


            if (user.Email == "ben@flipview.co.nz")
            {
                const string command = "UPDATE dbo.[User] " +
                      "SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, NormalizedUserName = @NormalizedUserName, Email = @Email, NormalizedEmail = @NormalizedEmail, " +
                          "EmailConfirmed = @EmailConfirmed, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, PhotoUrl = @PhotoUrl, Address = @Address, " +
                          "ConcurrencyStamp = @ConcurrencyStamp, SecurityStamp = @SecurityStamp, RegistrationDate = @RegistrationDate, LastLoginDate = @LastLoginDate, LockoutEnabled = @LockoutEnabled, LockoutEndDateTimeUtc = @LockoutEndDateTimeUtc, " +
                          "TwoFactorEnabled = @TwoFactorEnabled, AccessFailedCount = @AccessFailedCount, " +
                          "Title=@Title, SecondName=@SecondName, CreatedBy=@CreatedBy, CreatedDate=@CreatedDate, ModifiedBy=@ModifiedBy, ModifiedDate=@ModifiedDate, DisplayName=@DisplayName ,AuthenticatorSecretKey=@AuthenticatorSecretKey, " +
                      "MobileSecurityPIN=@MobileSecurityPIN, Status=@Status, IsQRVerified=@IsQRVerified " +
                      "WHERE UserId = @UserId;";

                int rowsUpdated = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
                {
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.NormalizedUserName,
                    user.Email,
                    user.NormalizedEmail,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    //user.PhotoUrl,
                    //user.Address,
                    user.ConcurrencyStamp,
                    user.SecurityStamp,
                    //user.RegistrationDate,
                    //user.LastLoginDate,
                    user.LockoutEnabled,
                    user.LockoutEndDateTimeUtc,
                    user.TwoFactorEnabled,
                    user.AccessFailedCount,
                    //user.Title,
                    //user.SecondName,
                    //user.CreatedBy,
                    user.CreatedDate,
                    user.ModifiedBy,
                    user.ModifiedDate,
                    //user.DisplayName,
                    //user.AuthenticatorSecretKey,
                    //user.MobileSecurityPIN,
                    user.Status,
                    //user.IsQRVerified,
                    user.UserId

                }), cancellationToken).Result;

                return Task.FromResult(rowsUpdated.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The user with email {user.Email} could not be updated in the dbo.[User] table."
                }));
            }
            else
            {
                const string command = "UPDATE dbo.[User] " +
                      "SET FirstName = @FirstName, LastName = @LastName, UserName = @UserName, NormalizedUserName = @NormalizedUserName, Email = @Email, NormalizedEmail = @NormalizedEmail, " +
                          "EmailConfirmed = @EmailConfirmed, PasswordHash = @PasswordHash, PhoneNumber = @PhoneNumber, PhoneNumberConfirmed = @PhoneNumberConfirmed, PhotoUrl = @PhotoUrl, Address = @Address, " +
                          "ConcurrencyStamp = @ConcurrencyStamp, SecurityStamp = @SecurityStamp, RegistrationDate = @RegistrationDate, LastLoginDate = @LastLoginDate, LockoutEnabled = @LockoutEnabled, LockoutEndDateTimeUtc = @LockoutEndDateTimeUtc, " +
                          "TwoFactorEnabled = @TwoFactorEnabled, AccessFailedCount = @AccessFailedCount, " +
                          "Title=@Title, SecondName=@SecondName, CreatedBy=@CreatedBy, CreatedDate=@CreatedDate, ModifiedBy=@ModifiedBy, ModifiedDate=@ModifiedDate, OrganisationId=@OrganisationId, DisplayName=@DisplayName ,AuthenticatorSecretKey=@AuthenticatorSecretKey, " +
                      "MobileSecurityPIN=@MobileSecurityPIN, Status=@Status, IsQRVerified=@IsQRVerified " +
                      "WHERE UserId = @UserId;";

                int rowsUpdated = Task.Run(() => _sqlConnection.ExecuteAsync(command, new
                {
                    user.FirstName,
                    user.LastName,
                    user.UserName,
                    user.NormalizedUserName,
                    user.Email,
                    user.NormalizedEmail,
                    user.EmailConfirmed,
                    user.PasswordHash,
                    user.PhoneNumber,
                    user.PhoneNumberConfirmed,
                    //user.PhotoUrl,
                    //user.Address,
                    user.ConcurrencyStamp,
                    user.SecurityStamp,
                    //user.RegistrationDate,
                    //user.LastLoginDate,
                    user.LockoutEnabled,
                    user.LockoutEndDateTimeUtc,
                    user.TwoFactorEnabled,
                    user.AccessFailedCount,
                    //user.Title,
                    //user.SecondName,
                    //user.CreatedBy,
                    user.CreatedDate,
                    user.ModifiedBy,
                    user.ModifiedDate,
                    //user.OrganisationId,
                    //user.DisplayName,
                    //user.AuthenticatorSecretKey,
                    //user.MobileSecurityPIN,
                    user.Status,
                    //user.IsQRVerified,
                    user.UserId

                }), cancellationToken).Result;

                return Task.FromResult(rowsUpdated.Equals(1) ? IdentityResult.Success : IdentityResult.Failed(new IdentityError
                {
                    Code = string.Empty,
                    Description = $"The user with email {user.Email} could not be updated in the dbo.[User] table."
                }));
            }

        }

        public Task<IEnumerable<ApplicationUser>> GetAllUsers()
        {
            const string command = "SELECT * " +
                                   "FROM dbo.[User];";

            return _sqlConnection.QueryAsync<ApplicationUser>(command);
        }

        public void Dispose()
        {
            if (_sqlConnection == null)
            {
                return;
            }

            _sqlConnection.Dispose();
            _sqlConnection = null;
        }
    }
}
