using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class AdministeredMedication
    {
        [Key]
        public int AdministeredMedicationID { get; set; }
        public int MedicationID { get; set; }
        public int PetID { get; set; }

        // Navigation properties
        public Medication Medication { get; set; }
        public Pet Pet { get; set; }
    }

}
