using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class User
    {
        [Key]     
        public int UserID { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int RoleID { get; set; }
        public bool IsActive { get; set; }

        // Navigation properties
        public Role Role { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public string FullName 
        { 
            get { return $"{FirstName} {LastName}";  }  
        }
    }
}
