using System.Net.Http;
using System.Text.Json;
using System.Windows;

namespace Exchange.Managers
{
    public static class TokenManager
    {
        internal static string Token { get; set; }

        public static string Langofsoft { get; set; }

        private static void SetToken(string token)
        {
            Token = token;
        }

        public static void SetLang(string token)
        {
            Langofsoft = token;
        }
        internal static async Task<bool> FetchAndSaveToken()
        {
            if (!string.IsNullOrEmpty(Token))
            {
                return true;
            }

            //SaveToken("apple2");
            //TokenManager.SetToken("Apple");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/auth");
            var content = new StringContent("{\r\n    \"ClientKey\": \"" + Variable.apiClientKey + "\",\r\n    \"ClientSecret\": \"9Hp43hFWEb6X2U4+paGHd1wwyQm28y5BAfCrQsk/ZRs=\",\r\n    \"lng\": \"en_US\",\r\n    \"kioskID\": \"1543543\",\r\n    \"kioskIP\": \"64.233.161.147\"\r\n}\r\n", null, "application/json");
            request.Content = content;
            var responseBody = "";
            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                await Task.Delay(1000);
                responseBody = await response.Content.ReadAsStringAsync();
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request failed: {ex.Message}", "Error");
                return false;
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
                SetToken(accessToken);
                // MessageBox.Show(LoadToken());

            }

            return true;

            //WelcomePage wl2 = new WelcomePage();
            //NavigationService.Navigate(wl2);

            //Login2 lg2 = new Login2();
            //NavigationService.Navigate(lg2);

            //wMainPage wmainpage = new wMainPage();
            //NavigationService.Navigate(wmainpage);
        }
        public static void Clear()
        {
            Token = "";
        }
    }
}
