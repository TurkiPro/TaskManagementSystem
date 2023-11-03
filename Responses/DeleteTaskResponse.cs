using System.Text.Json.Serialization;

namespace TaskManagementSystem.Responses
{
    public class DeleteTaskResponse : BaseResponse
    {
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int TaskId { get; set; }
    }
}
