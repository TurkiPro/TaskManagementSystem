using Microsoft.AspNetCore.Mvc;
using TaskManagementSystem.Entities;
using TaskManagementSystem.Helpers;
using TaskManagementSystem.Interfaces;
using TaskManagementSystem.Requests;
using TaskManagementSystem.Responses;

namespace TaskManagementSystem.Controllers
{
    [Route("api/controller")]
    [ApiController]
    public class TasksController : BaseApiController
    {
        #region Variables
        private readonly ITaskService taskService;
        #endregion

        #region Constructor
        public TasksController(ITaskService taskService)
        {
            this.taskService = taskService;
        }
        #endregion

        #region Methods

        ///<summary>
        ///This method is used to create a new task by a user
        ///</summary>
        ///<param name="taskRequest"></param>"
        ///<returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(TaskRequest taskRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new TaskResponse
                    {
                        Error = $"{string.Join(", ", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            if (!DateTime.TryParse(taskRequest.DueDate, out DateTime dueDate))
            {
                return BadRequest(new TaskResponse { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Due date is not valid, please enter in the date forma (dd/mm/yyyy" 
                });
            }
            if (taskRequest.Status > 3)
            {
                return BadRequest(new TaskResponse { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid status" 
                });
            }

            int UserID = 0;
            var task = new Tasks { 
                Id = taskRequest.Id, 
                Name = taskRequest.Name, 
                Description = taskRequest.Description, 
                CreatedAt = DateTime.Now, 
                DueDate = dueDate, 
                Status = taskRequest.Status, 
                UserMasterId = UserID 
            };

            var createTaskResponse = await taskService.CreateTask(task);

            if (!createTaskResponse.Success)
            {
                return UnprocessableEntity(createTaskResponse);
            }

            var taskResponse = new TaskResponse { 
                Id = createTaskResponse.Task.Id, 
                Name = createTaskResponse.Task.Name, 
                Description = createTaskResponse.Task.Description, 
                DueDate = Convert.ToString(createTaskResponse.Task.DueDate.ToString("dd/MM/yyyy")), 
                Status = ((TaskStatusEnum)createTaskResponse.Task.Status).ToString() 
            };

            return Ok(taskResponse);
        }

        ///<summary>
        ///This method gets all the tasks for a logged in user
        ///</summary>
        ///<return></return>
        [HttpGet]
        [Route(nameof(GetAllTasks))]
        public async Task<IActionResult> GetAllTasks()
        {
            var getTasksResponse = await taskService.GetTasks(UserID);

            if (!getTasksResponse.Success)
            {
                return UnprocessableEntity(getTasksResponse);
            }

            var tasksResponse = getTasksResponse.Tasks.ConvertAll(o => new TaskResponse { Id = o.Id, Name = o.Name, Description = o.Description, DueDate = Convert.ToString(o.DueDate.ToString("dd/MM/yyyy")), Status = ((TaskStatusEnum)o.Status).ToString() });

            return Ok(tasksResponse);
        }
        #endregion
    }
}
