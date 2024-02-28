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
    /// Interaction logic for ReceptionistDashboard.xaml
    /// </summary>
    /// 
  

    public partial class DoctorManagePets : Page
    {
        class GridRow
        {
            public Models.Pet Pet { get; set; }
            public string Species { get; set; }
        }

        public DoctorManagePets()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            var appointments = Globals.DbContext.Appointments
                                            .Include("Pet")                                           
                                            .Where(p => p.DoctorID == Globals.CurrentUser.UserID);

            var appointmentsWithDoctor = (from appointment in appointments
                                          where appointment.DoctorID == Globals.CurrentUser.UserID
                                          join species in Globals.DbContext.Species on appointment.Pet.SpeciesID equals species.SpeciesID
                                          select new GridRow
                                         {
                                             Pet = appointment.Pet,
                                             Species = species.TypeName
                                          })                                          
                                          .ToList();            
            Pets.ItemsSource = appointmentsWithDoctor;
        }

        private bool isTabControlInitialized = false;

        private void btnSeeDetails_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            var selectedRow = button.DataContext as GridRow;

            var petDetailsModal = new PetDetails(selectedRow.Pet.PetID);
            petDetailsModal.ShowDialog();
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
