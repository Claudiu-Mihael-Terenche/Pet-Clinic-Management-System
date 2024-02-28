using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Navigation;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for ManageReceptionists.xaml
    /// </summary>
    public partial class AdminManageReceptionists : Page
    {
        private bool isTabControlInitialized = false;

        public AdminManageReceptionists()
        {
            InitializeComponent();
            RefreshGrid();
        }       

        private void Logout(object sender, RoutedEventArgs e)
        {
            Globals.CurrentUser = null;
            // Open the login window
            NavigationService.Navigate(new Uri("Login.xaml", UriKind.Relative));
        }

        private void RefreshGrid()
        {
            var users = Globals.DbContext.Users.Where(u => u.RoleID == 3).ToList();
            Receptionists.ItemsSource = users;
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

            // Check if the newly selected tab is the one you're interested in
            if (TabControl.SelectedItem == HomeTab)
            {
                this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
            }
            else if (TabControl.SelectedItem == DoctorsTab)
            {
                this.NavigationService.Navigate(new Uri("AdminManageDoctors.xaml", UriKind.Relative));
            }

        }
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var doctor = button.DataContext as Models.User;

            var editDoctorWindow = new AdminEditReceptionist(doctor.UserID);
            bool? result = editDoctorWindow.ShowDialog();
            if (result == true)
            {
                RefreshGrid();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var receptionist = button.DataContext as Models.User;

            var confirmDialog = new ConfirmDialog("Do you want to Delete this receptionist ? ");

            // Show the dialog window
            confirmDialog.ShowDialog();

            // Check the value of IsConfirmed to determine the user's response
            if (confirmDialog.IsConfirmed)
            {
                var userToDelete = Globals.DbContext.Users.FirstOrDefault(u => u.UserID == receptionist.UserID); 

                if (userToDelete != null)
                {
                    Globals.DbContext.Users.Remove(userToDelete);
                    Globals.DbContext.SaveChanges();
                    RefreshGrid();
                }
            }
        }

        private void AddNewReceptionist_Click(object sender, RoutedEventArgs e)
        {
            AdminAddReceptionist addWindow = new AdminAddReceptionist();
            bool? result = addWindow.ShowDialog();
            if (result == true)
            {
                // Refresh grid
                RefreshGrid();
            }
        }
    }
}

