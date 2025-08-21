using Exchange.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;


namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for Login2.xaml
    /// </summary>
    public partial class Login2 : Page
    {
        //private IdleTimer _idleTimer;
        private Page _returnPage;

        public Login2()
        {
            LoginInit();
        }

        public Login2(Page returnPage)
        {
            _returnPage = returnPage;
            LoginInit();
        }

        public void LoginInit()
        {
            InitializeComponent();
            usernameTextBox.Focus();

            if (TokenManager.Langofsoft == "ar")
            {
                logintitle.Text = "تسجيل الدخول";
                loginbtn.Content = "تسجيل الدخول";
                registerbtn.Content = "تسجيل";
                usernamelbl.Content = "اسم المستخدم";
                passwordlbl.Content = "كلمة المرور";
                backbtn.Content = "يرجع";
            }
        }

        private async void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string username = usernameTextBox.Text;
                string password = passwordBox.Password;

                bool isLoginSuccessful = await LoginManager.Login(username, password);

                if (!isLoginSuccessful)
                {
                    MessageBox.Show("Invalid Login Details ");
                    return;
                }

                if (isLoginSuccessful && _returnPage != null)
                {
                    NavigationService.Navigate(_returnPage);
                }
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private NavigationService NavigationService
        {
            get { return NavigationService.GetNavigationService(this); }
        }

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            await TokenManager.FetchAndSaveToken();

            WelcomePage welco = new WelcomePage();
            NavigationService.Navigate(welco);
        }        

        private void resetpasswordclick(object sender, RoutedEventArgs e)
        {
            wResetPassword welco = new wResetPassword();
            NavigationService.Navigate(welco);
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
