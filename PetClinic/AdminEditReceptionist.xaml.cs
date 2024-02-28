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
    /// Interaction logic for AdminEditReceptionist.xaml
    /// </summary>
    public partial class AdminEditReceptionist : Window
    {
        public Models.User User { get; set; }
        public AdminEditReceptionist(int userID)
        {
            InitializeComponent();
            try
            {
                User = Globals.DbContext.Users.FirstOrDefault(u => u.UserID == userID);
                if (User != null) BindUser(User);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading from the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BindUser(Models.User user)
        {
            Email.Text = user.Email;
            FirstName.Text = user.FirstName;
            LastName.Text = user.LastName;
            Username.Text = user.Username;
            Password.Password = user.PasswordHash;
            PasswordConfirm.Password = user.PasswordHash;
        }

        private void btnSave_Click(object sender, RoutedEventArgs args)
        {
            if (!ValidateForn()) return;

            if (User != null)
            {
                try
                {
                    User.Email = Email.Text;
                    User.FirstName = FirstName.Text;
                    User.LastName = LastName.Text;
                    User.Username = Username.Text;
                    User.PasswordHash = PasswordHelper.HashPassword(Password.Password);
                    User.IsActive = true;

                    Globals.DbContext.SaveChanges();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while saving to the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.DialogResult = false;
                }
                this.DialogResult = true;
            }
            this.Close();
        }

        private void btnCancelClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }

        private bool ValidateForn()
        {
            if (Email.Text.Trim().Length == 0 ||
               FirstName.Text.Trim().Length == 0 ||
               LastName.Text.Trim().Length == 0 ||
               Password.Password.Trim().Length == 0)
            {
                errorMessage.Text = "Missing inputs. All fields are mandatory";
                return false;
            }

            if (PasswordConfirm.Password != Password.Password)
            {
                errorMessage.Text = "Passwords don't match";
                return false;
            }
            return true;
        }
    }
}
