namespace TaskManagementSystem.Entities
{
    public partial class UserMaster
    {
        public UserMaster() 
        { 
            Tasks = new HashSet<Tasks>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }

        public virtual ICollection<Tasks> Tasks { get; set; }
    }
}
