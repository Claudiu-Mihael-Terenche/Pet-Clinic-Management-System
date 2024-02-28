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
    /// Interaction logic for ManageDoctors.xaml
    /// </summary>
    public partial class AdminManageDoctors : Page
    {
        private bool isTabControlInitialized = false;

        public AdminManageDoctors()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void btnAddDoctor_Click(object sender, RoutedEventArgs e)
        {
            AdminAddDoctor addDoctorWindow = new AdminAddDoctor();
            bool? result = addDoctorWindow.ShowDialog();
            if (result == true)
            {
                // Refresh grid
                RefreshGrid();
            }
        }

        private void btnDeleteDoctor_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var doctor = button.DataContext as Models.User; 

            var confirmDialog = new ConfirmDialog("Do you want to Delete this doctor? ");

            // Show the dialog window
            confirmDialog.ShowDialog();

            // Check the value of IsConfirmed to determine the user's response
            if (confirmDialog.IsConfirmed)
            {
                var userToDelete = Globals.DbContext.Users.FirstOrDefault(u => u.UserID == doctor.UserID); // Replace 'userId' with the ID of the user you want to delete

                if (userToDelete != null)
                {                   
                    Globals.DbContext.Users.Remove(userToDelete);                    
                    Globals.DbContext.SaveChanges();
                    RefreshGrid();
                }                
            }
        }

        private void btnEditDoctor_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var doctor = button.DataContext as Models.User;

            AdminEditDoctor editDoctorWindow = new AdminEditDoctor(doctor.UserID);
            bool? result = editDoctorWindow.ShowDialog();
            if (result == true)
            {
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            var doctors = Globals.DbContext.Users.Where(u => u.RoleID == 2).ToList();
            Doctors.ItemsSource = doctors;
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
                if (TabControl.SelectedItem == HomeTab)
                {
                    this.NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == ReceptionistsTab)
                {
                    this.NavigationService.Navigate(new Uri("AdminManageReceptionists.xaml", UriKind.Relative));
                }

            }
        }
    }
}
