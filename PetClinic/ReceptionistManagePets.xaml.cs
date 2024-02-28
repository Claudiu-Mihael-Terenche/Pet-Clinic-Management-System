using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for ReceptionistManagePets.xaml
    /// </summary>
    public partial class ReceptionistManagePets : Page
    {
        private bool isTabControlInitialized = false;
        public ReceptionistManagePets()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new ReceptionistAddPets();
            bool? result = addWindow.ShowDialog();
            if (result == true)
            {               
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var pet = button.DataContext as Models.Pet;          
            var confirmDialog = new ConfirmDialog("Do you want to Delete this patient ? ");

            // Show the dialog window
            confirmDialog.ShowDialog();
           
            if (confirmDialog.IsConfirmed)
            {
                var toDelete = Globals.DbContext.Pets.FirstOrDefault(p => p.PetID == pet.PetID);

                if (toDelete != null)
                {
                    Globals.DbContext.Pets.Remove(toDelete);
                    Globals.DbContext.SaveChanges();
                    RefreshGrid();
                }
            }
        }

        private void RefreshGrid()
        {
            var pets = Globals.DbContext.Pets
                                        .Include("Owner")
                                        .Include("Species")
                                        .ToList();
            
            Pets.ItemsSource = pets;

        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Globals.CurrentUser = null;
            // Open the login window
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
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
                if (TabControl.SelectedItem == BillingTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManageBilling.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == HomeTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistDashboard.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == AppointmentsTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManageAppointments.xaml", UriKind.Relative));
                }
            }
        }
    }
}
