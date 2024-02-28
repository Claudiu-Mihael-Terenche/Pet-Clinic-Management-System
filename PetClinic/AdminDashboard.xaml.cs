using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class AdminDashboard : Page
    {
        private bool isTabControlInitialized = false;
        public AdminDashboard()
        {
            InitializeComponent();
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
                if (TabControl.SelectedItem == DoctorsTab)
                {
                    this.NavigationService.Navigate(new Uri("AdminManageDoctors.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == ReceptionistsTab)
                {
                    this.NavigationService.Navigate(new Uri("AdminManageReceptionists.xaml", UriKind.Relative));
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
