namespace TaskManagementSystem.Responses
{
    public class TaskResponse : BaseResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DueDate { get; set; }
        public string? Status { get; set; }

    }
}
