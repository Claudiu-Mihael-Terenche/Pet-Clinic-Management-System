using PetClinic.Models;

using System;
using System.Data.Entity;


namespace PetClinic
{
    public class PetClinicDbInitializer : IDatabaseInitializer<PetClinicDb>
    {
        public void InitializeDatabase(PetClinicDb context)
        {
            //var adminRole = new Role { RoleID = 1, RoleName = "Admin" };


            //var adminUser = new User
            //{
            //    UserID = 1,
            //    Username = "admin",
            //    PasswordHash = PasswordHelper.HashPassword("123"), // Use a real hashing method here
            //    FirstName = "Admin",
            //    LastName = "User",
            //    Email = "admin@example.com",
            //    RoleID = adminRole.RoleID,
            //    IsActive = true
            //};

            //context.Users.Add(adminUser);

            //var result = context.SaveChanges();
            //Console.WriteLine($"Number of affected rows: {result}");

        }
    }
}
