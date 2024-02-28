using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class PetOwner
    {
        [Key]
        public int OwnerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        // Navigation property
        public ICollection<Pet> Pets { get; set; }

        public override string ToString()
        {
            return $"{FirstName} {LastName}";
        }

        public string FullName
        {
            get { return $"{FirstName} {LastName}"; }
        }
    }
}
