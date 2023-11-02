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
                ErrorCode = "T01"
            };
        }
    }
}
