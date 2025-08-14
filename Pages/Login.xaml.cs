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

            // Create an instance of the LoginViewModel and set it as the DataContext
            //var viewModel = new LoginViewModel();
            //DataContext = viewModel;
           // DataContext = new LoginViewModel(NavigationService?.NavigationService?.Frame);

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            // Replace this with your actual login logic
            if (IsValidLogin(username, password))
            {
                MessageBox.Show("Login successful!");
                // Navigate to the next window or perform other actions

                // Navigate to Page1.xaml after successful login
                //NavigationService.Navigate(new Uri("Pages/Page1.xaml", UriKind.Relative));

                // Pass parameters to Page1.xaml after successful login
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
