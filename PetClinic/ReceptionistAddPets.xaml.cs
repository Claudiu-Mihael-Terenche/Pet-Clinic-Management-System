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
    /// Interaction logic for AddPets.xaml
    /// </summary>
    public partial class ReceptionistAddPets : Window
    {
        List<string> genders = new List<string> { "Male", "Female" };

        public ReceptionistAddPets()
        {
            InitializeComponent();
            var species = Globals.DbContext.Species.ToList();
            Species.ItemsSource = species;
            Genders.ItemsSource = genders;
            Owners.ItemsSource = Globals.DbContext.PetOwners.ToList();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Species selectedSpecie = Species.SelectedItem as Models.Species;
                string gender = Genders.SelectedItem.ToString();
                Models.PetOwner selectedOwner = Owners.SelectedItem as Models.PetOwner;
                int.TryParse(Age.Text, out int age);

                var pet = new Models.Pet
                {
                    Age = age,
                    Gender = gender,
                    Name = Name.Text,
                    SpeciesID = selectedSpecie.SpeciesID,
                };

                if (selectedOwner != null)
                {
                    pet.OwnerID = selectedOwner.OwnerID;
                }
                else
                {
                    var newOwner = new Models.PetOwner
                    {
                        Email = OwnerEmail.Text,
                        FirstName = OwnerFirstName.Text,
                        LastName = OwnerLastName.Text,
                        Phone = OwnerPhone.Text,
                    };
                    Globals.DbContext.PetOwners.Add(newOwner);
                    Globals.DbContext.SaveChanges();
                    pet.OwnerID = newOwner.OwnerID;
                }
                Globals.DbContext.Pets.Add(pet);
                Globals.DbContext.SaveChanges();

                this.DialogResult = true;
            }
            catch(Exception ex)
            {
                this.DialogResult = false;
                MessageBox.Show($"An error occurred while saving the pet to the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
            this.Close();
        }
    }
}
