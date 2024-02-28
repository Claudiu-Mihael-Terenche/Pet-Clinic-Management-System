using PetClinic.Service;

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
    /// Interaction logic for AddAppointments.xaml
    /// </summary>
    public partial class ReceptionistAddAppointments : Window
    {
        List<string> appTypes = new List<string> { "Checkup", "Dental", "Vaccination", "Surgery" };

        public ReceptionistAddAppointments()
        {
            InitializeComponent();
            AppointmentType.ItemsSource = appTypes;

            var petsWithOwners = Globals.DbContext.Pets.Include("Owner").ToList();
            Pets.ItemsSource = petsWithOwners;
            var allDoctors = Globals.DbContext.Users.Where(u => u.RoleID == 2).ToList();
            Doctor.ItemsSource = allDoctors;            
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateForn()) return;

            Models.User selectedDoctor = Doctor.SelectedItem as Models.User;
            Models.Pet selectedPet = Pets.SelectedItem as Models.Pet;
            string appointmentType = AppointmentType.SelectedItem.ToString();

            Models.Appointment appointment = new Models.Appointment
            {
                AppointmentDate = AppointmentDate.SelectedDate.Value,
                StartTime = new TimeSpan(AppointmentTime.SelectedTime.Value.Hour, AppointmentTime.SelectedTime.Value.Minute, 0),
                DoctorID = selectedDoctor.UserID,
                PetID = selectedPet.PetID,
                Type = appointmentType,
                ReceptionistID = Globals.CurrentUser.UserID
            };
            Globals.DbContext.Appointments.Add(appointment);
            Globals.DbContext.SaveChanges();
            //send email
            if (ConfirmEmail.IsChecked.HasValue && ConfirmEmail.IsChecked.Value == true)
                SendEmail(appointment, selectedPet);
            //
            this.DialogResult = true;
            this.Close();
        }

        private void SendEmail(Models.Appointment appointment, Models.Pet selectedPet)
        {
            string senderEmail = "petclinic875@gmail.com";
            string recipientEmail = selectedPet.Owner.Email;//"ebarrellet@gmail.com";
            string subject = "Your have an appointment !";
            string body = "Dear Customer,\nYou have an appointment at our Pet Clinic for " + selectedPet.Name + " on the " + 
                         appointment.AppointmentDate.ToShortDateString() + " Time: " + appointment.StartTime +
                         ". \nAppointment Type: " + appointment.Type;

            EmailService.SendEmail(senderEmail, recipientEmail, subject, body);

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private void AppointmentType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private bool ValidateForn()
        {
            if (!AppointmentDate.SelectedDate.HasValue ||
               !AppointmentTime.SelectedTime.HasValue ||
               Pets.SelectedItem == null ||
               Doctor.SelectedItem == null ||
               AppointmentType.SelectedItem == null
               )
            {
                errorMessage.Text = "Missing inputs. All fields are mandatory";
                return false;
            }
            
            return true;
        }
    }
}
