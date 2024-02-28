using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class MedicalRecord
    {
        [Key]
        public int RecordID { get; set; }
        public int PetID { get; set; }
        public string Allergies { get; set; }
        public string Condition { get; set; }

        // Navigation property
        public Pet Pet { get; set; }
    }

}
