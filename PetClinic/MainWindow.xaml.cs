using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Main shell window for the whole app
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();            
            MainFrame.Navigate(new Login());
        }
    }
}
