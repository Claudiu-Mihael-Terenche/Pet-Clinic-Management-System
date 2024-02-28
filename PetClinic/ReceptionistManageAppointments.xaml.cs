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
    /// Interaction logic for ReceptionistManageAppointments.xaml
    /// </summary>
     /// 

    internal class GridRow
    {
        public Models.Appointment Appointment { get; set; }
        public Models.User Doctor { get; set; }
    }
    public partial class ReceptionistManageAppointments : Page
    {
        private bool isTabControlInitialized = false;
        public ReceptionistManageAppointments()
        {
            InitializeComponent();
            RefreshGrid();
        }


        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new ReceptionistAddAppointments();
            bool? result = addWindow.ShowDialog();
            if (result == true)
            {
                // Refresh grid
                RefreshGrid();
            }
        }

        private void RefreshGrid()
        {
            var appointments = Globals.DbContext.Appointments                                               
                                               .Include("Pet.Owner").ToList(); 

            var appointmentsWithDoctor = (from appointment in appointments
                                          join doctor in Globals.DbContext.Users on appointment.DoctorID equals doctor.UserID
                                          select new GridRow
                                          {
                                              Appointment = appointment,
                                              Doctor = doctor
                                          }).ToList();

            Appointments.ItemsSource = appointmentsWithDoctor;
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var rowData = button.DataContext as GridRow;
            var appointment = rowData.Appointment;

            var confirmDialog = new ConfirmDialog("Do you want to Delete this appointment ? ");

            // Show the dialog window
            confirmDialog.ShowDialog();

            // Check the value of IsConfirmed to determine the user's response
            if (confirmDialog.IsConfirmed)
            {
                var toDelete = Globals.DbContext.Appointments.FirstOrDefault(a => a.AppointmentID == appointment.AppointmentID); 

                if (toDelete != null)
                {
                    Globals.DbContext.Appointments.Remove(toDelete);
                    Globals.DbContext.SaveChanges();
                    RefreshGrid();
                }
            }
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
                else if (TabControl.SelectedItem == HomeTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistDashboard.xaml", UriKind.Relative));
                }
                else if (TabControl.SelectedItem == BillingTab)
                {
                    this.NavigationService.Navigate(new Uri("ReceptionistManageBilling.xaml", UriKind.Relative));
                }
            }
        }
    }
}
