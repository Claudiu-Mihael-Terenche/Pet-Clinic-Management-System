using System.Windows;

namespace PetClinic
{
    /// <summary>
    /// Interaction logic for ConfirmDialog.xaml
    /// </summary>
    public partial class ConfirmDialog : Window
    {
        public string Message { get; set; }
        public bool IsConfirmed { get; private set; } = false;

        public ConfirmDialog(string message)
        {
            InitializeComponent();
            DataContext = this;
            Message = message;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            IsConfirmed = false;
            Close();
        }
    }
}
