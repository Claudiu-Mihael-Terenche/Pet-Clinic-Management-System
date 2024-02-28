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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Application.Current.MainWindow.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text; // Replace with your actual TextBox for username
            string password = txtPassword.Password; // Replace with your actual PasswordBox for password

            using (var context = new PetClinicDb())
            {
                var user = context.Users.Include("Role")
                            .FirstOrDefault(u => u.Username == username && u.IsActive);

                if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    Globals.CurrentUser = user;

                    if (user.Role.RoleName == "Admin")
                    {
                        // User is an admin and credentials are valid, show the dashboard                        
                        //MainWindow mainWindow = new MainWindow(); // Create a new instance of the Dashboard window
                        //mainWindow.Show();
                        //this.Close(); // Close the login window
                        NavigationService.Navigate(new Uri("AdminDashboard.xaml", UriKind.Relative));
                    }
                    else if(user.Role.RoleName == "Doctor")
                    {
                        // User is not an admin, show an error message
                        NavigationService.Navigate(new Uri("DoctorDashboard.xaml", UriKind.Relative));
                    }
                    else
                    {
                        NavigationService.Navigate(new Uri("ReceptionistDashboard.xaml", UriKind.Relative));
                    }
                }
                else
                {
                    // Credentials are invalid, show an error message
                    MessageBox.Show("Invalid username or password.", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }


}
