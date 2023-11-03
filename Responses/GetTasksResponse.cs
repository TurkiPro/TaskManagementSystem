using TaskManagementSystem.Entities;

namespace TaskManagementSystem.Responses
{
    public class GetTasksResponse : BaseResponse
    {
        public List<Tasks> Tasks { get; set; }
    }

    public class GetTaskByIdResponse : BaseResponse 
    { 
        public Tasks Task { get; set; }
    }
}
