using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Medication
    {
        [Key]
        public int MedicationID { get; set; }
        public string AnimalType { get; set; }
        public string MedicationName { get; set; }
        public string Dosage { get; set; }
        public decimal Price { get; set; }
    }

}
