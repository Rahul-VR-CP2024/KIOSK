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
            InitializeComponent();

            //MessageBox.Show("" + BCManager.fromwhereopenedwtbc);
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


        //transferbtnclick


        private void transferbtnclick(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            //wTransferpay wtpay = new wTransferpay();
            //NavigationService.Navigate(wtpay);

            BCManager.SetTokenselectedoptionborc("BT");
            //wSelectbeneficary wx = new wSelectbeneficary();
            //NavigationService.Navigate(wx);


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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            NavigationManager.NavigateToHome();

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

        private void cashbtnclick(object sender, RoutedEventArgs e)
        {

            BCManager.SetTokenselectedoptionborc("CP");
            //BCManager.SetTokenfromwhereopenedwtbc("sendmoney");
            //BCManager.SetTokenfromwhereopenedwtbc("beneficiary");
            


            if(BCManager.fromwhereopenedwtbc == "beneficiary")
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
    }
}
