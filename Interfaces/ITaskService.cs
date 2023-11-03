using TaskManagementSystem.Entities;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Interfaces
{
    public interface ITaskService
    {
        Task<CreateTaskResponse> CreateTask(Tasks task);
        Task<GetTasksResponse> GetTasks(int userId);
        Task<GetTaskByIdResponse> GetTaskById(int taskId, int userId);
        Task<DeleteTaskResponse> DeleteTask(int taskId, int userId);

    }
}
