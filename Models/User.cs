using System.ComponentModel.DataAnnotations;

namespace YourProject.Models
{
    public class User
    {
        [Key] // Explicitly marking the Id property as the primary key
        public int Id { get; set; }

        public string UserName { get; set; }
        public string Password { get; set; }
        // Add other properties as needed
    }
}
