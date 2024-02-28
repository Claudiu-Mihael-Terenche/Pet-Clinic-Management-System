using System.Linq;
using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for PetDetails.xaml
    /// </summary>
    public partial class PetDetails : Window
    {
        public Models.Pet Pet { get; set; }
        public PetDetails(int petId)
        {
            InitializeComponent();
            var pet = Globals.DbContext.Pets.Include("Owner")
                                            .Include("Species")
                                            .FirstOrDefault(p => p.PetID == petId);

            PetSummary.Text = $"Name: {pet.Name}\n" +
                              $"Age: {pet.Age}\nSpecies: {pet.Species.TypeName}\n" +
                              $"Owner: {pet.Owner.FullName}\nGender: {pet.Gender}";

            var vaccines = Globals.DbContext.AdministeredVaccines.Include("Vaccine")
                                                                 .Where(av => av.PetID == petId)
                                                                 .ToList();

            var meds = Globals.DbContext.AdministeredMedications.Include("Medication")
                                                                 .Where(m => m.PetID == petId)
                                                                 .ToList(); ;
            Vaccines.Text = "Vaccines:\n\n";
            foreach(var vax in vaccines)
            {
                Vaccines.Text += $"{vax.Vaccine.VaccineName}, ";
            }

            Meds.Text = "Meds:\n\n";
            foreach (var med in meds)
            {
                Meds.Text += $"{med.Medication.MedicationName}, ";
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs args)
        {
            this.Close();
        }
    }
}
