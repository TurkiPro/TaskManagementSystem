using Microsoft.EntityFrameworkCore;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Services
{
    public class TaskService : ITaskService
    {
        private readonly TasksDbContext tasksDbContext;

        public TaskService(TasksDbContext tasksDbContext)
        {
            this.tasksDbContext = tasksDbContext;
        }

        //Create new task service
        public async Task<CreateTaskResponse> CreateTask(Tasks task)
        {
            if (task.Id == 0)
            {
                await tasksDbContext.Tasks.AddAsync(task);
            }
            else
            {
                var taskRecord = await tasksDbContext.Tasks.FindAsync(task.Id);

                taskRecord.Name = task.Name;
                taskRecord.Description = task.Description;
                taskRecord.DueDate = task.DueDate;
                taskRecord.Status = task.Status;
            }

            var createResponse = await tasksDbContext.SaveChangesAsync();

            if (createResponse >= 0)
            {
                return new CreateTaskResponse
                {
                    Success = true,
                    Task = task
                };
            }
            return new CreateTaskResponse
            {
                Success = false,
                Error = "Unable to create task",
                ErrorCode = "T05"
            };
        }

        //Get all tasks for a specific user service
        public async Task<GetTasksResponse> GetTasks(int userId)
        {
            var tasks = await tasksDbContext.Tasks.Where(o => o.UserMasterId == userId).ToListAsync();

            return new GetTasksResponse { Success = true, Tasks = tasks };
        }

        //Get a specific task by ID
        public async Task<GetTaskByIdResponse> GetTaskById(int taskId, int userId)
        {
            var task = await tasksDbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return new GetTaskByIdResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }

            if (task.UserMasterId != userId)
            {
                return new GetTaskByIdResponse
                {
                    Success = false,
                    Error = "You don't have access to this task",
                    ErrorCode = "T02"
                };
            }

            return new GetTaskByIdResponse { Success = true, Task = task };
        }

        //Delete a task using Task ID and user ID
        public async Task<DeleteTaskResponse> DeleteTask(int taskId, int userId)
        {
            var task = await tasksDbContext.Tasks.FindAsync(taskId);

            if (task == null)
            {
                return new DeleteTaskResponse
                {
                    Success = false,
                    Error = "Task not found",
                    ErrorCode = "T01"
                };
            }

            if (task.UserMasterId != userId)
            {
                return new DeleteTaskResponse
                {
                    Success = false,
                    Error = "You don't have access to delete this task",
                    ErrorCode = "T02"
                };
            }

            tasksDbContext.Tasks.Remove(task);

            var saveResponse = await tasksDbContext.SaveChangesAsync();

            if (saveResponse >= 0)
            {
                return new DeleteTaskResponse
                {
                    Success = true,
                    TaskId = task.Id
                };
            }

            return new DeleteTaskResponse
            {
                Success = false,
                Error = "Unable to delete task",
                ErrorCode = "T03"
            };
        }
    }
}
