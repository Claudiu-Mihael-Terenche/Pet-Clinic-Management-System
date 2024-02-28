using System;
using System.Linq;
using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for DoctorAddVaccine.xaml
    /// </summary>
    public partial class DoctorAddVaccine : Window
    {
        public DoctorAddVaccine()
        {
            InitializeComponent();
            Patients.ItemsSource = Globals.DbContext.Pets.Include("Owner").ToList();
            Vaccines.ItemsSource = Globals.DbContext.Vaccines.ToList();
        }

        private void btnSave_Click(object sender, RoutedEventArgs args)
        {
            try
            {
                var pet = Patients.SelectedItem as Models.Pet;
                var vaccine = Vaccines.SelectedItem as Models.Vaccine;

                var admVax = new Models.AdministeredVaccine
                {
                    PetID = pet.PetID,
                    VaccineID = vaccine.VaccineID
                };
                Globals.DbContext.AdministeredVaccines.Add(admVax);
                Globals.DbContext.SaveChanges();

                DialogResult = true;
            }
            catch (Exception ex)
            {
                DialogResult = false;
                MessageBox.Show($"An error occurred while saving the vaccine record: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs args)
        {
            this.DialogResult = false;
            this.Close();
        }

    }
}
