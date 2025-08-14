using Exchange.Managers;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wChooseLang.xaml
    /// </summary>
    public partial class wChooseLang : Page
    {
        public wChooseLang()
        {
            InitializeComponent();
        }

        private async void ImageButton_Clickar(object sender, RoutedEventArgs e)
        {
            TokenManager.SetLang("ar");
            loginpressed();
            //MessageBox.Show(TokenManager.Langofsoft);
        }

        private async void ImageButton_Click(object sender, RoutedEventArgs e)
        {
            TokenManager.SetLang("en");
            loginpressed();
            //MessageBox.Show(TokenManager.Langofsoft);
        }


        public async void loginpressed()
        {
            //SaveToken("apple2");
            //TokenManager.SetToken("Apple");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/auth");
            var content = new StringContent("{\r\n    \"ClientKey\": \""+Variable.apiClientKey+"\",\r\n    \"ClientSecret\": \"9Hp43hFWEb6X2U4+paGHd1wwyQm28y5BAfCrQsk/ZRs=\",\r\n    \"lng\": \"en_US\",\r\n    \"kioskID\": \"1543543\",\r\n    \"kioskIP\": \"64.233.161.147\"\r\n}\r\n", null, "application/json");
            request.Content = content;
            var responseBody = "";
            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request failed: {ex.Message}", "Error");
                return;
            }
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(LoadToken());


            // Read the response content as a string
            //var responseBody = await response.Content.ReadAsStringAsync();




            // Parse the JSON response using System.Text.Json
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                JsonElement dataElement = root.GetProperty("Data");

                // Extract the accessToken
                string accessToken = dataElement.GetProperty("accessToken").GetString();

                // Display the accessToken in a message box
                //Console.WriteLine($"Access Token: {accessToken}");
                //MessageBox.Show($"Access Token: {accessToken}");
                // RemoveToken(accessToken);
                // SaveToken(accessToken);
                //TokenManager.SetToken(accessToken);
                // MessageBox.Show(LoadToken());

            }



            //WelcomePage wl2 = new WelcomePage();
            //NavigationService.Navigate(wl2);

            //Login2 lg2 = new Login2();
            //NavigationService.Navigate(lg2);

            NavigationManager.NavigateToHome();
        }


        private static Dictionary<string, byte[]> EncryptedTokens = new Dictionary<string, byte[]>();

        public static void SaveToken(string token)
        {
            byte[] encryptedToken = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(token),
                null,
                DataProtectionScope.CurrentUser);
            EncryptedTokens.Add("AuthenticationToken", encryptedToken);
           
        }

        

        public static void RemoveToken(string token)
        {
            byte[] encryptedToken = ProtectedData.Protect(
                Encoding.UTF8.GetBytes(token),
                null,
                DataProtectionScope.CurrentUser);
            EncryptedTokens.Remove("AuthenticationToken");

        }

        public static string LoadToken()
        {
            if (!EncryptedTokens.ContainsKey("AuthenticationToken"))
            {
                return null;
            }

            byte[] encryptedToken = EncryptedTokens["AuthenticationToken"];

            if (encryptedToken == null)
            {
                return null;
            }

            try
            {
                byte[] decryptedToken = ProtectedData.Unprotect(
                    encryptedToken,
                    null,
                    DataProtectionScope.CurrentUser);
                return Encoding.UTF8.GetString(decryptedToken);
            }
            catch (CryptographicException)
            {
                // Handle decryption failure (e.g., invalid token)
                return null;
            }
        }       
    }
}
