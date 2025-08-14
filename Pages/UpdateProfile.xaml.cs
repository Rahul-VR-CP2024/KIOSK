using Exchange.Managers;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for UpdateProfile.xaml
    /// </summary>
    public partial class UpdateProfile : Page
    {
        public UpdateProfile()
        {
            InitializeComponent();
        }

        private void mobile_GotFocus(object sender, RoutedEventArgs e)
        {
            //if (mobileTextBox.Text == "Mobile Number")
            //{
            //    mobileTextBox.Text = "";
            //    mobileTextBox.Opacity = 1;
            //    mobileTextBox.Foreground = new SolidColorBrush(Colors.White);
            //}

        }

        private void mobile_LostFocus(object sender, RoutedEventArgs e)
        {
            //if (string.IsNullOrWhiteSpace(mobileTextBox.Text))
            //{
            //    mobileTextBox.Text = "Mobile Number";
            //    mobileTextBox.Opacity = 0.5;
            //    mobileTextBox.Foreground = new SolidColorBrush(Colors.Gray);
            //}
        }

        private async void updateProfileButton_Click(object sender, RoutedEventArgs e)
        {

            string email = usernameemailTextBox.Text;

            // Regular expression pattern for email validation
            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

            // Check if the email matches the pattern
            bool isValid = Regex.IsMatch(email, pattern);

            if (!isValid) {
                MessageBox.Show("Invalid Email ID");
                return;
            }

            string reminid = LoginManager.Remiduser;
            string mobileno = LoginManager.UserMOBILE;
            string civilid = LoginManager.civilidno;

            //MessageBox.Show(reminid + " " + mobileno + " " + civilid + " " + email);
                

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxRemitter/Remitter/PostRemitterUpdate");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("3"), "appID");
            content.Add(new StringContent("1"), "moduleID");
            content.Add(new StringContent("KIOSK"), "channelCode");
            content.Add(new StringContent(reminid), "RemID");
            content.Add(new StringContent(mobileno), "MobileNo");
            content.Add(new StringContent(civilid), "IdentityNo");
            content.Add(new StringContent(email), "EmailID");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            MessageBox.Show("Profile Updated Successfully");
            NavigationManager.NavigateToHome();

        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private void Page_Load(object sender, RoutedEventArgs e)
        {
            usernameemailTextBox.Text = LoginManager.UserEMAILID;
            //mobileTextBox.Text = LoginManager.UserMOBILE;
        }
    }
}
