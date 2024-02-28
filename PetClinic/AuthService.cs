using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PetClinic
{
    public class AuthService
    {
        public bool ValidateLogin(string username, string password)
        {
            using (var context = new PetClinicDb())
            {
                var user = context.Users
                    .Include("Role") // Use the navigation property name as a string
                    .FirstOrDefault(u => u.Username == username);

                if (user != null && PasswordHelper.VerifyPassword(password, user.PasswordHash))
                {
                    // Check if the user is an admin
                    return user.Role.RoleName == "Admin";
                }
            }
            return false;
        }

    }

}
