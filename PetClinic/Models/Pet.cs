using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PetClinic.Models
{
    public class Pet
    {
        [Key]
        public int PetID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        // Foreign key for Owner
        public int OwnerID { get; set; }
        [ForeignKey("OwnerID")]
        public PetOwner Owner { get; set; }

        // Assuming you have a Species entity
        public int SpeciesID { get; set; }
        [ForeignKey("SpeciesID")]
        public Species Species { get; set; }

        public override string ToString()
        {
            return $"{Name} | {Owner.FirstName} {Owner.LastName}";
        }       
    }
}
