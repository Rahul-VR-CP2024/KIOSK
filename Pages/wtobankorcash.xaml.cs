using Exchange.Managers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wtobankorcash.xaml
    /// </summary>
    public partial class wtobankorcash : Page
    {
        public wtobankorcash()
        {
            try
            {
                InitializeComponent();

                if (TokenManager.Langofsoft == "ar")
                {
                    tobp1.Text = "إلى حساب مصرفي";
                    tobp2.Text = "تحويل الأموال عبر الإنترنت إلى المستفيد الخاص بك.";
                    tocp1.Text = "تسليم نقدا";
                    tocp2.Text = "استلام النقدية من أقرب وكيل.";
                    backbtn.Content = "يرجع";
                    sendmontitle.Text = "إرسال الأموال";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }

        public static class BCManager
        {
            public static string fromwhereopenedwtbc { get; set; }

            public static string selectedoptionborc { get; set; }

            public static void SetTokenfromwhereopenedwtbc(string token)
            {
                fromwhereopenedwtbc = token;
            }

            public static void SetTokenselectedoptionborc(string token)
            {
                selectedoptionborc = token;
            }
        }

        private void transferbtnclick(object sender, RoutedEventArgs e)
        {

            try
            {
                BCManager.SetTokenselectedoptionborc("BT");

                if (BCManager.fromwhereopenedwtbc == "beneficiary")
                {
                    wBeneficiary welocmepage = new wBeneficiary();
                    NavigationService.Navigate(welocmepage);
                }

                if (BCManager.fromwhereopenedwtbc == "sendmoney")
                {
                    wSelectbeneficary wx = new wSelectbeneficary();
                    NavigationService.Navigate(wx);
                }
                if (BCManager.fromwhereopenedwtbc == "exchangerate")
                {
                    wSelectcountry mainpage = new wSelectcountry();
                    NavigationService.Navigate(mainpage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }

        private void cashbtnclick(object sender, RoutedEventArgs e)
        {
            try
            {
                BCManager.SetTokenselectedoptionborc("CP");


                if (BCManager.fromwhereopenedwtbc == "beneficiary")
                {
                    wBeneficiary welocmepage = new wBeneficiary();
                    NavigationService.Navigate(welocmepage);
                }

                if (BCManager.fromwhereopenedwtbc == "sendmoney")
                {

                    wSelectbeneficary wx = new wSelectbeneficary();
                    NavigationService.Navigate(wx);
                }

                if (BCManager.fromwhereopenedwtbc == "exchangerate")
                {

                    wSelectcountry mainpage = new wSelectcountry();
                    NavigationService.Navigate(mainpage);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationManager.NavigateToHome();

        }

    }
}
