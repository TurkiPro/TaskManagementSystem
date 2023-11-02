using System.ComponentModel.DataAnnotations;

namespace TaskManagementSystem.Requests
{
    public class TaskRequest
    {

        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? DueDate { get; set; }
        [Required]

        public short Status { get; set; }
    }
}
