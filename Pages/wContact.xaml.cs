using Exchange.Managers;
using System.Windows;
using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wContact.xaml
    /// </summary>
    public partial class wContact : Page
    {
        public wContact()
        {
            InitializeComponent();

            
            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                contuslbl.Text = "اتصل بنا";
                usernameTextBox.Text = "اسم";
                phonenolbl.Text = "الهاتف";
                emaillbl.Text = "بريد إلكتروني";
                subjelbl.Text = "موضوع";
                submitlbl.Content = "يُقدِّم";
                messagelbl.Text = "رسالة";

            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationManager.NavigateToHome();

        }
    }
}
