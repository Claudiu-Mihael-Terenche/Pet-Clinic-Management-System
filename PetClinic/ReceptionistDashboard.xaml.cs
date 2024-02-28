using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for ReceptionistDashboard.xaml
    /// </summary>
    public partial class ReceptionistDashboard : Page
    {
        private bool isTabControlInitialized = false;

        public ReceptionistDashboard()
        {
            InitializeComponent();
            BindGrid();
        }

        private void BindGrid()
        {
            var todaysApp = Globals.DbContext.Appointments
                                                .Include("Pet.Owner")
                                                .Where(a => a.AppointmentDate == DateTime.Today)
                                                .ToList();            

            Appointments.ItemsSource = todaysApp;
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
                 if (TabControl.SelectedItem == PetsTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManagePets.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == AppointmentsTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManageAppointments.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == BillingTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManageBilling.xaml", UriKind.Relative));
                }
            }
        }
    }
}
