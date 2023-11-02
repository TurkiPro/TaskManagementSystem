namespace TaskManagementSystem.Entities
{
    public partial class Tasks
    {
        public int Id { get; set; }
        public int UserMasterId { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }
        public short Status { get; set; }
        public virtual UserMaster UserMaster { get; set; }
    }
}
