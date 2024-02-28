using System;
using System.Data.Entity;
using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            try
            {
                // Invoke the database initializer here
                Database.SetInitializer(new PetClinicDbInitializer());
                // Ensure the context is used once to trigger the initializer, if necessary                
                Globals.DbContext = new PetClinicDb();
                Globals.DbContext.Database.Initialize(force: true);

            }
            catch (Exception ex)
            {
                // Log the exception or display a message
                MessageBox.Show($"An error occurred while initializing the database: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
