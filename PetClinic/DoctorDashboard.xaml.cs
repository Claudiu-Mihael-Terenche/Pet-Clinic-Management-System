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
    /// Interaction logic for DoctorDashboard.xaml
    /// </summary>
    public partial class DoctorDashboard : Page
    {
        public DoctorDashboard()
        {
            InitializeComponent();
            RefreshGrid();
        }
        private bool isTabControlInitialized = false;
        private void RefreshGrid()
        {
            var appointments = Globals.DbContext.Appointments
                                                .Include("Pet.Owner")
                                                .Where(u => u.DoctorID == Globals.CurrentUser.UserID)
                                                .ToList();
            Appointments.ItemsSource = appointments;
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
                if (TabControl.SelectedItem == PetsTab)
                {
                    this.NavigationService.Navigate(new Uri("DoctorManagePets.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == MedicationTab)
                {
                    this.NavigationService.Navigate(new Uri("DoctorManageMedication.xaml", UriKind.Relative));
                }

            }
        }
        private void Logout(object sender, RoutedEventArgs e)
        {
            Globals.CurrentUser = null;
            // Open the login window
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }
    }
}
