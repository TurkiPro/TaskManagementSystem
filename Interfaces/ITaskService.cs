using TaskManagementSystem.Entities;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskService
    {
        Task<CreateTaskResponse> CreateTask(Tasks task);
    }
}
