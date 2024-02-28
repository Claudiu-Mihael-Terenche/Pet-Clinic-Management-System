using System;
using System.Linq;
using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for DoctorAddMedication.xaml
    /// </summary>
    public partial class DoctorAddMedication : Window
    {
        public DoctorAddMedication()
        {
            InitializeComponent();
            Patients.ItemsSource = Globals.DbContext.Pets.Include("Owner").ToList();
            MedicationTypes.ItemsSource = Globals.DbContext.Medications.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                var pet = Patients.SelectedItem as Models.Pet;
                var medication = MedicationTypes.SelectedItem as Models.Medication;

                var admMed = new Models.AdministeredMedication
                {
                    PetID = pet.PetID,
                    MedicationID = medication.MedicationID
                };
                Globals.DbContext.AdministeredMedications.Add(admMed);
                Globals.DbContext.SaveChanges();

                DialogResult = true;
            }
            catch (Exception ex)
            {
                DialogResult = false;
                MessageBox.Show($"An error occurred while saving the prescription: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void btnCancelClick(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
