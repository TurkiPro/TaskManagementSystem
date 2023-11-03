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
                return BadRequest(new TaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Due date is not valid, please enter in the date forma (dd/mm/yyyy" 
                });
            }
            if (taskRequest.Status > 3)
            {
                return BadRequest(new TaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid status" 
                });
            }

            var task = new Tasks 
            { 
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

            var taskResponse = new TaskResponse 
            { 
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

            var tasksResponse = getTasksResponse.Tasks.ConvertAll(o => new TaskResponse 
            { 
                Id = o.Id, 
                Name = o.Name, 
                Description = o.Description, 
                DueDate = Convert.ToString(o.DueDate.ToString("dd/MM/yyyy")), 
                Status = ((TaskStatusEnum)o.Status).ToString() 
            });

            return Ok(tasksResponse);
        }

        /// <summary>
        /// This method is used to get task details by task id
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTaskById/{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            if (taskId == 0)
            {
                return BadRequest(new DeleteTaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid Task id" 
                });
            }
            var getTasksResponse = await taskService.GetTaskById(taskId, UserID);

            if (!getTasksResponse.Success)
            {
                return UnprocessableEntity(getTasksResponse);
            }

            var tasksResponse = new TaskResponse 
            { 
                Id = getTasksResponse.Task.Id, 
                Name = getTasksResponse.Task.Name, 
                Description = getTasksResponse.Task.Description, 
                DueDate = Convert.ToString(getTasksResponse.Task.DueDate.ToString("dd/MM/yyyy")), 
                Status = ((TaskStatusEnum)getTasksResponse.Task.Status).ToString() 
            };

            return Ok(tasksResponse);
        }

        /// <summary>
        /// This method is used to delete task
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest(new DeleteTaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid Task id" 
                });
            }
            var deleteTaskResponse = await taskService.DeleteTask(id, UserID);
            if (!deleteTaskResponse.Success)
            {
                return UnprocessableEntity(deleteTaskResponse);
            }

            return Ok(deleteTaskResponse.TaskId);
        }

        /// <summary>
        /// This method used for update task detail
        /// </summary>
        /// <param name="taskRequest"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> Put(TaskRequest taskRequest)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors.Select(c => c.ErrorMessage)).ToList();
                if (errors.Any())
                {
                    return BadRequest(new TaskResponse
                    {
                        Error = $"{string.Join(",", errors)}",
                        ErrorCode = "S01"
                    });
                }
            }

            if (!DateTime.TryParse(taskRequest.DueDate, out DateTime dueDate))
            {
                return BadRequest(new TaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid due date, Please enter valid date format (dd/MM/yyyy)" 
                });
            }
            if (taskRequest.Status > 3)
            {
                return BadRequest(new TaskResponse 
                { 
                    Success = false, 
                    ErrorCode = "D01", 
                    Error = "Invalid status" 
                });
            }

            var task = new Tasks 
            { 
                Id = taskRequest.Id, 
                Name = taskRequest.Name, 
                Description = taskRequest.Description, 
                CreatedAt = DateTime.Now, 
                DueDate = dueDate, 
                Status = taskRequest.Status, 
                UserMasterId = UserID 
            };

            var saveTaskResponse = await taskService.CreateTask(task);

            if (!saveTaskResponse.Success)
            {
                return UnprocessableEntity(saveTaskResponse);
            }

            var taskResponse = new TaskResponse 
            { 
                Id = saveTaskResponse.Task.Id, 
                Name = saveTaskResponse.Task.Name, 
                Description = saveTaskResponse.Task.Description, 
                DueDate = Convert.ToString(saveTaskResponse.Task.DueDate.ToString("dd/MM/yyyy")), 
                Status = ((TaskStatusEnum)saveTaskResponse.Task.Status).ToString() 
            };

            return Ok(taskResponse);
        }
        #endregion
    }
}
