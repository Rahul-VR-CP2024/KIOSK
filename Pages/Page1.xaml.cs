using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class Page1 : Page
    {
        public Page1(string username)
        {
            InitializeComponent();

            usernameLabel.Content = $"Hello, {username}!";
        }

        private void NavigateToPage2_Click(object sender, RoutedEventArgs e)
        {
            // Pass parameter to Page2
            string parameterValue = "Hello from Page 1!";
            NavigationService.Navigate(new Page2(parameterValue));
        }
    }
}
