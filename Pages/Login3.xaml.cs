using Exchange.Managers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for Login2.xaml
    /// </summary>
    public partial class Login3 : Page
    {
        private Page _returnPage;

        public Login3(string username, string password, Page returnPage)
        {
            InitializeComponent();

            // Bind values into hidden textboxes
            usernameHidden.Text = username;
            passwordHidden.Text = password;

            // Store return page reference (if any)
            _returnPage = returnPage;

            LoginInit();
        }

        private void LoginInit()
        {
            // Add any initialization logic here if needed
        }

        private async void OK_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = usernameHidden.Text;
                string password = passwordHidden.Text;
                int otp = int.Parse(OTPTextBox.Text);

                bool isLoginSuccessful = await LoginManager.LoginWithOTP(username, password, otp);

                if (!isLoginSuccessful)
                {
                    MessageBox.Show("Invalid Login Details");
                    return;
                }

                // Navigate back to return page if provided
                if (_returnPage != null)
                {
                    NavigationService.Navigate(_returnPage);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private NavigationService NavigationService
        {
            get { return NavigationService.GetNavigationService(this); }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
                NavigationService.GoBack();
        }
    }
}