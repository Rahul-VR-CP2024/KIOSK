using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Login()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Replace this with your actual login logic
            if (IsValidLogin(username, password))
            {
                Page1 page1 = new Page1(username);
                NavigationService.Navigate(page1);
            }
            else
            {
                MessageBox.Show("Invalid username or password.");
            }
        }

        private bool IsValidLogin(string username, string password)
        {
            // Replace this with your actual validation logic
            return username == "user" && password == "password";
        }

        private NavigationService NavigationService
        {
            get { return NavigationService.GetNavigationService(this); }
        }

    }
}
