using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AdministeredVaccine
    {
        [Key]
        public int AdministeredVaccineID { get; set; }
        public int VaccineID { get; set; }
        public int PetID { get; set; }

        // Navigation properties
        public Vaccine Vaccine { get; set; }
        public Pet Pet { get; set; }
    }

}
