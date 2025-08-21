using Exchange.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{

    public partial class wMainPage : Page
    {
        public wMainPage()
        {
            InitializeComponent();

            LanguageInit();

            if (!LoginManager.IsLoggedIn())
            {
                logoutbtn.Visibility = Visibility.Hidden;
            }
        }

        private void Sendmoney_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BCManager.SetTokenfromwhereopenedwtbc("sendmoney");

                wtobankorcash wx = new wtobankorcash();

                if (LoginManager.IsLoggedIn())
                {
                    NavigationService.Navigate(wx);
                    return;
                }
                Login2 login2 = new Login2(wx);

                NavigationService.Navigate(login2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                LoginManager.Logout();

                logoutbtn.Visibility = Visibility.Hidden;

                RefreshWelcomeText();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void LangButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TokenManager.Langofsoft == "ar")
                {
                    TokenManager.SetLang("en");
                }
                else
                {
                    TokenManager.SetLang("ar");
                }

                LanguageInit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }          
        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                wContact welocmepage = new wContact();
                NavigationService.Navigate(welocmepage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void Exchange_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                BCManager.SetTokenfromwhereopenedwtbc("exchangerate");

                wExchangecalculator wx = new wExchangecalculator();

                NavigationService.Navigate(wx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }          
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateProfile wx = new UpdateProfile();
                if (LoginManager.IsLoggedIn())
                {
                    NavigationService.Navigate(wx);
                    return;
                }
                Login2 login2 = new Login2(wx);
                NavigationService.Navigate(login2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }     
        }

        private void LanguageInit()
        {
            try
            {
                if (TokenManager.Langofsoft == "ar")
                {
                    sendmoneybtn.Text = "إرسال الأموال";
                    exchangeratebtn.Text = "سعر الصرف";
                    contbtn.Text = "اتصل بنا";
                    updtprofibtn.Text = "تحديث الملف الشخصي";
                    logoutbtn.Content = "خروج";
                    langbtn.Content = "English";
                }
                else
                {
                    sendmoneybtn.Text = "SEND MONEY";
                    exchangeratebtn.Text = "EXCHANGE RATE";
                    contbtn.Text = "CONTACT US";
                    updtprofibtn.Text = "UPDATE PROFILE";
                    logoutbtn.Content = "Logout";
                    langbtn.Content = "عربي";
                }
                RefreshWelcomeText();
            }       
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void RefreshWelcomeText()
        {
            try
            {
                if (TokenManager.Langofsoft == "ar")
                {
                    welcomtext.Text = "! " + LoginManager.UserFullname + " مرحباً";
                    langbtn.Content = "English";
                }
                else
                {
                    welcomtext.Text = "Welcome " + LoginManager.UserFullname + " !";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }
    }
}
