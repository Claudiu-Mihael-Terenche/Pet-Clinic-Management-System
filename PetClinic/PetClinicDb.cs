using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using PetClinic.Models;

namespace PetClinic
{
    public class PetClinicDb : DbContext
    {
        public PetClinicDb() : base("name=PetClinicDbConnectionString")
        {
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Species> Species { get; set; }
        public DbSet<PetOwner> PetOwners { get; set; }
        public DbSet<Pet> Pets { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<MedicalRecord> MedicalRecords { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Medication> Medications { get; set; }
        public DbSet<AdministeredVaccine> AdministeredVaccines { get; set; }
        public DbSet<AdministeredMedication> AdministeredMedications { get; set; }
    }
}