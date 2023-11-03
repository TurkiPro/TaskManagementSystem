using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Helpers;
using TaskManagementSystem.Interfaces;

namespace TaskManagementSystem.Services
{
    public class TokenService : ITokenService
    {
        private readonly TasksDbContext tasksDbContext;

        public TokenService(TasksDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        public async Task<Tuple<string, string>> GenerateTokensAsync(int userId)
        {
            var accessToken = await TokenHelper.GenerateAccessToken(userId);
            var refreshToken = await TokenHelper.GenerateRefreshToken();

            var userRecord = await tasksDbContext.UserMaster.FirstOrDefaultAsync(e => e.Id == userId);

            if (userRecord == null)
            {
                return null;
            }

            await tasksDbContext.SaveChangesAsync();

            var token = new Tuple<string, string>(accessToken, refreshToken);

            return token;
        }
    }
}
