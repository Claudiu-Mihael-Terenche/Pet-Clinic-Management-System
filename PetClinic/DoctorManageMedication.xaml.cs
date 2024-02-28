using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for DoctorManageMedication.xaml
    /// </summary>
    public partial class DoctorManageMedication : Page
    {
        public DoctorManageMedication()
        {
            InitializeComponent();
            LoadData();
        }
        private bool isTabControlInitialized = false;

        private void LoadData()
        {
            var vaccines = Globals.DbContext.Vaccines.ToList();
            Vaccines.ItemsSource = vaccines;

            var medications = Globals.DbContext.Medications.ToList();
            Medications.ItemsSource = medications;

        }
        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (!e.Source.Equals(TabControl)) return;

            // Avoid running the logic on the initial selection change event.
            if (!isTabControlInitialized)
            {
                isTabControlInitialized = true; // Update the flag
                return; // Exit the method to avoid executing the rest of the code below
            }

            // Ensure the event is for the TabControl to avoid handling other selection changed events
            if (e.Source is TabControl)
            {
                // Check if the newly selected tab is the one you're interested in
                if (TabControl.SelectedItem == HomeTab)
                {
                    this.NavigationService.Navigate(new Uri("DoctorDashboard.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == PetsTab)
                {
                    this.NavigationService.Navigate(new Uri("DoctorManagePets.xaml", UriKind.Relative));
                }

            }
        }

        private void btnDeleteVaccine_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var vaccine = button.DataContext as Models.Vaccine;
            var confirmDialog = new ConfirmDialog("Do you want to Delete this vaccine ? ");

            // Show the dialog window
            confirmDialog.ShowDialog();

            // Check the value of IsConfirmed to determine the user's response
            if (confirmDialog.IsConfirmed)
            {
                var toDelete = Globals.DbContext.Vaccines.FirstOrDefault(v => v.VaccineID == vaccine.VaccineID);

                if (toDelete != null)
                {
                    Globals.DbContext.Vaccines.Remove(toDelete);
                    Globals.DbContext.SaveChanges();
                    LoadData();
                }
            }
        }

        private void btnDeleteMedication_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var medication = button.DataContext as Models.Medication;
            var confirmDialog = new ConfirmDialog("Do you want to Delete this medication ? ");

            // Show the dialog window
            confirmDialog.ShowDialog();

            // Check the value of IsConfirmed to determine the user's response
            if (confirmDialog.IsConfirmed)
            {
                var toDelete = Globals.DbContext.Medications.FirstOrDefault(m => m.MedicationID == medication.MedicationID);

                if (toDelete != null)
                {
                    Globals.DbContext.Medications.Remove(toDelete);
                    Globals.DbContext.SaveChanges();
                    LoadData();
                }
            }
        }

        private void Logout(object sender, RoutedEventArgs e)
        {
            Globals.CurrentUser = null;
            // Open the login window
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }

        private void AddVaccine(object sender, RoutedEventArgs e)
        {
            DoctorAddVaccine AddVaccine = new DoctorAddVaccine();
            bool? result = AddVaccine.ShowDialog();
            if (result == true)
            {
                // Refresh grid
                LoadData();
            }
        }

        private void AddMedication(object sender, RoutedEventArgs e)
        {
            DoctorAddMedication AddMedication = new DoctorAddMedication();
            bool? result = AddMedication.ShowDialog();
            if (result == true)
            {
                // Refresh grid
                LoadData();
            }
        }
    }
}
