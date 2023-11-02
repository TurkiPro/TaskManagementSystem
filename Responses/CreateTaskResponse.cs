using TaskManagementSystem.Entities;

namespace TaskManagementSystem.Responses
{
    public class CreateTaskResponse : BaseResponse
    {
        public Tasks Task { get; set; }
    }
}
