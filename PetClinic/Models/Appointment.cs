using System;
using System.ComponentModel.DataAnnotations;

namespace PetClinic.Models
{
    public class Appointment
    {
        [Key]
        public int AppointmentID { get; set; }
        public int PetID { get; set; }
        public int DoctorID { get; set; } // Assuming Doctor is a User with a specific role
        public int ReceptionistID { get; set; } // Assuming Receptionist is also a User
        public string Type { get; set; }
        public DateTime AppointmentDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public string Reason { get; set; }

        // Navigation properties
        public Pet Pet { get; set; }
        public User Doctor { get; set; }
        public User Receptionist { get; set; }
    }

}
