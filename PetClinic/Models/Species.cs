
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Species
    {
        [Key]
        public int SpeciesID { get; set; }
        public string TypeName { get; set; }

        // Navigation property
        public ICollection<Pet> Pets { get; set; }
    }
}
