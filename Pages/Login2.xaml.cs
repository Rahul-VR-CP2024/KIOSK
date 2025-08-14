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

            //_idleTimer = new IdleTimer(NavigationService);
            //_idleTimer.Start();

            //// Reset the timer on user interaction
            //PreviewMouseDown += (sender, e) => _idleTimer.Reset();
            //PreviewKeyDown += (sender, e) => _idleTimer.Reset();
            //PreviewMouseMove += (sender, e) => _idleTimer.Reset();



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


                //MessageBox.Show(TokenManager.Token);

                //"username": "Halani",
                //"password": "Halani@123",

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
                    //MessageBox.Show("Invalid username or password.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Login failed");
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

        private async void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            await TokenManager.FetchAndSaveToken();
            //wRegister wmainpage = new wRegister();
            //NavigationService.Navigate(wmainpage);

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
