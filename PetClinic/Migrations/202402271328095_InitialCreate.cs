namespace PetClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AdministeredMedications",
                c => new
                    {
                        AdministeredMedicationID = c.Int(nullable: false, identity: true),
                        MedicationID = c.Int(nullable: false),
                        PetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministeredMedicationID)
                .ForeignKey("dbo.Medications", t => t.MedicationID, cascadeDelete: true)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: true)
                .Index(t => t.MedicationID)
                .Index(t => t.PetID);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        MedicationID = c.Int(nullable: false, identity: true),
                        AnimalType = c.String(),
                        MedicationName = c.String(),
                        Dosage = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.MedicationID);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Gender = c.String(),
                        OwnerID = c.Int(nullable: false),
                        SpeciesID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PetID)
                .ForeignKey("dbo.PetOwners", t => t.OwnerID, cascadeDelete: true)
                .ForeignKey("dbo.Species", t => t.SpeciesID, cascadeDelete: true)
                .Index(t => t.OwnerID)
                .Index(t => t.SpeciesID);
            
            CreateTable(
                "dbo.PetOwners",
                c => new
                    {
                        OwnerID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.OwnerID);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpeciesID = c.Int(nullable: false, identity: true),
                        TypeName = c.String(),
                    })
                .PrimaryKey(t => t.SpeciesID);
            
            CreateTable(
                "dbo.AdministeredVaccines",
                c => new
                    {
                        AdministeredVaccineID = c.Int(nullable: false, identity: true),
                        VaccineID = c.Int(nullable: false),
                        PetID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AdministeredVaccineID)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: true)
                .ForeignKey("dbo.Vaccines", t => t.VaccineID, cascadeDelete: true)
                .Index(t => t.VaccineID)
                .Index(t => t.PetID);
            
            CreateTable(
                "dbo.Vaccines",
                c => new
                    {
                        VaccineID = c.Int(nullable: false, identity: true),
                        AnimalType = c.String(),
                        VaccineName = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.VaccineID);
            
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentID = c.Int(nullable: false, identity: true),
                        PetID = c.Int(nullable: false),
                        DoctorID = c.Int(nullable: false),
                        ReceptionistID = c.Int(nullable: false),
                        Type = c.String(),
                        AppointmentDate = c.DateTime(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        Reason = c.String(),
                        Doctor_UserID = c.Int(),
                        Receptionist_UserID = c.Int(),
                    })
                .PrimaryKey(t => t.AppointmentID)
                .ForeignKey("dbo.Users", t => t.Doctor_UserID)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.Receptionist_UserID)
                .Index(t => t.PetID)
                .Index(t => t.Doctor_UserID)
                .Index(t => t.Receptionist_UserID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        PasswordHash = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        RoleID = c.Int(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID, cascadeDelete: true)
                .Index(t => t.RoleID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        RoleName = c.String(),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.MedicalRecords",
                c => new
                    {
                        RecordID = c.Int(nullable: false, identity: true),
                        PetID = c.Int(nullable: false),
                        Allergies = c.String(),
                        Condition = c.String(),
                    })
                .PrimaryKey(t => t.RecordID)
                .ForeignKey("dbo.Pets", t => t.PetID, cascadeDelete: true)
                .Index(t => t.PetID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MedicalRecords", "PetID", "dbo.Pets");
            DropForeignKey("dbo.Appointments", "Receptionist_UserID", "dbo.Users");
            DropForeignKey("dbo.Appointments", "PetID", "dbo.Pets");
            DropForeignKey("dbo.Appointments", "Doctor_UserID", "dbo.Users");
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.AdministeredVaccines", "VaccineID", "dbo.Vaccines");
            DropForeignKey("dbo.AdministeredVaccines", "PetID", "dbo.Pets");
            DropForeignKey("dbo.AdministeredMedications", "PetID", "dbo.Pets");
            DropForeignKey("dbo.Pets", "SpeciesID", "dbo.Species");
            DropForeignKey("dbo.Pets", "OwnerID", "dbo.PetOwners");
            DropForeignKey("dbo.AdministeredMedications", "MedicationID", "dbo.Medications");
            DropIndex("dbo.MedicalRecords", new[] { "PetID" });
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Appointments", new[] { "Receptionist_UserID" });
            DropIndex("dbo.Appointments", new[] { "Doctor_UserID" });
            DropIndex("dbo.Appointments", new[] { "PetID" });
            DropIndex("dbo.AdministeredVaccines", new[] { "PetID" });
            DropIndex("dbo.AdministeredVaccines", new[] { "VaccineID" });
            DropIndex("dbo.Pets", new[] { "SpeciesID" });
            DropIndex("dbo.Pets", new[] { "OwnerID" });
            DropIndex("dbo.AdministeredMedications", new[] { "PetID" });
            DropIndex("dbo.AdministeredMedications", new[] { "MedicationID" });
            DropTable("dbo.MedicalRecords");
            DropTable("dbo.Roles");
            DropTable("dbo.Users");
            DropTable("dbo.Appointments");
            DropTable("dbo.Vaccines");
            DropTable("dbo.AdministeredVaccines");
            DropTable("dbo.Species");
            DropTable("dbo.PetOwners");
            DropTable("dbo.Pets");
            DropTable("dbo.Medications");
            DropTable("dbo.AdministeredMedications");
        }
    }
}
