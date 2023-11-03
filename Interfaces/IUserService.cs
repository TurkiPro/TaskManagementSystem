using TaskManagementSystem.Requests;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Interfaces
{
    public interface IUserService
    {
        Task<TokenResponse> LoginAsync(LoginRequest loginRequest);
        Task<SignupResponse> SignupAsync(SignupRequest signupRequest);
        Task<UserResponse> GetInfoAsync(int userId);
    }
}
