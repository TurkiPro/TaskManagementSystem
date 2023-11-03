using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Requests;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Services
{
    public class UserService : IUserService
    {
        private readonly TasksDbContext tasksDbContext;

        public UserService(TasksDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        public async Task<UserResponse> GetInfoAsync(int userId)
        {
            var user = await tasksDbContext.UserMaster.FindAsync(userId);

            if (user == null)
            {
                return new UserResponse
                {
                    Success = false,
                    Error = "No user found",
                    ErrorCode = "I001"
                };
            }

            return new UserResponse
            {
                Success = true,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                CreationDate = user.CreatedAt
            };
        }

        public Task<TokenResponse> LoginAsync(LoginRequest loginRequest)
        {
            throw new NotImplementedException();
        }

        public Task<SignupResponse> SignupAsync(SignupRequest signupRequest)
        {
            throw new NotImplementedException();
        }
    }

    
}
