using PetClinic.Models;

namespace PetClinic
{
    internal class Globals
    {        
        public static PetClinicDb DbContext { get; set; }

        public static User CurrentUser { get; set; }
    }
}

