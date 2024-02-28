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
    /// Interaction logic for AddReceptionist.xaml
    /// </summary>
    public partial class AdminAddReceptionist : Window
    {
        public AdminAddReceptionist()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs args)
        {
            if (!ValidateForn()) return;

            Models.User user = new Models.User
            {
                Email = Email.Text,
                FirstName = FirstName.Text,
                LastName = LastName.Text,
                Username = Username.Text,
                PasswordHash = PasswordHelper.HashPassword(Password.Password),
                RoleID = 3,
                IsActive = true
            };
            Globals.DbContext.Users.Add(user);
            Globals.DbContext.SaveChanges();
            this.DialogResult = true;
            this.Close();
        }

        private void btnCancelClick(object sender, RoutedEventArgs args)
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
