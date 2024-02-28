using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Vaccine
    {
        [Key]
        public int VaccineID { get; set; }
        public string AnimalType { get; set; }
        public string VaccineName { get; set; }
        public decimal Price { get; set; }
    }
}
