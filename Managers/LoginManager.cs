using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;

namespace Exchange.Managers
{
    public static class LoginManager
    {
        public static string Remiduser { get; set; }

        public static string Userid { get; set; }

        public static string UserEMAILID { get; set; }

        public static string UserMOBILE { get; set; }

        public static string UserFullname { get; set; }

        public static string civilidno { get; set; }
        public static string RemCode{ get; set; }

        public static void SetRemiduser(string token)
        {
            Remiduser = token;
        }
        public static void SetUserid(string token)
        {
            Userid = token;
        }

        public static void SetUserEMAILID(string token)
        {
            UserEMAILID = token;
        }

        public static void SetUserMOBILE(string token)
        {
            UserMOBILE = token;
        }

        public static void SetUserFullname(string token)
        {
            UserFullname = token;
        }
        public static void Setcivilidno(string token)
        {
            civilidno = token;
        }

        internal static async Task<bool> Login(string username, string password)
        {
            try
            {
                // bool tokenGenerated = await TokenManager.FetchAndSaveToken();
                //if (!tokenGenerated)
                //{
                //    return false;
                //}

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://" + Variable.apiipadd + "/api/Auth/verify-login-otp");

                // Optional: Authorization header
                // request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // Create payload using an anonymous object
                var payload = new
                {
                    mobile_Code = 987,
                    mobile_number = password?.Trim(),
                    id_number = username?.Trim()
                };

                // Serialize payload to JSON
                string json = JsonSerializer.Serialize(payload);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;

                // Log request info
                MessageBox.Show("Request Body:\n" + json);
                MessageBox.Show("Request URL:\n" + request.RequestUri.ToString());

                // Send request
                var response = await client.SendAsync(request);

                // Read response content
                string responseBody = await response.Content.ReadAsStringAsync();

                // Log response
               // MessageBox.Show("Status: " + (int)response.StatusCode + "\n\nResponse Body:\n" + responseBody);

                // Optional: Throw exception if not 2xx
                response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
                //MessageBox.Show(await response.Content.ReadAsStringAsync());



                // Read the response content as a string
                // var responseBody = await response.Content.ReadAsStringAsync();

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = doc.RootElement;
                    MessageBox.Show(root.ToString());
                    // Try to get the "message" field
                    if (!root.TryGetProperty("message", out JsonElement messageElement))
                        return false;

                    string message = messageElement.GetString();
                    MessageBox.Show(message);
                    // Check if the login was successful
                    if (message != "Login Successful")
                        return false;

                    // Try to access "data.user"
                    if (!root.TryGetProperty("data", out JsonElement dataElement) ||
                        !dataElement.TryGetProperty("user", out JsonElement userElement))
                        return false;

                    // Safely extract needed fields from "user"
                    string eId = userElement.GetProperty("e_id").GetString();
                    string mobileNumber = userElement.GetProperty("mobile_number").GetString();
                    string firstName = userElement.GetProperty("first_name").GetString();
                    string lastName = userElement.GetProperty("last_name").GetString();
                    string fullName = $"{firstName} {lastName}".Trim();
                    string dateOfBirth = userElement.GetProperty("date_of_birth").GetString();
                    string nationality = userElement.GetProperty("nationality").GetString();
                    string riskType = userElement.GetProperty("risk_type_name").GetString();
                    int memberCode = userElement.GetProperty("member_code").GetInt32();
                    MessageBox.Show(mobileNumber);
                    // You can now assign these to your session, service, or display
                    // SetUserEId(eId);
                    // SetUserMobile(mobileNumber);
                    // SetUserFullName(fullName);
                    // SetUserDOB(dateOfBirth);
                    // SetUserNationality(nationality);
                    // SetUserRiskType(riskType);
                    // SetMemberCode(memberCode);

                    // Optional: Navigate to the main page after login
                    // wMainPage wmainpage = new wMainPage();
                    // NavigationService.Navigate(wmainpage);

                    return true;
                }


            }
            catch (Exception ex)
            {
                return false;
            }
        }

        internal static void Logout()
        {
            Clear();
            TokenManager.Clear();
        }
        public static bool IsLoggedIn()
        {
            return !string.IsNullOrEmpty(TokenManager.Token) && !string.IsNullOrEmpty(UserEMAILID);
        }
        private static void Clear()
        {
            Remiduser = "";
            Userid = "";
            UserEMAILID = "";
            UserMOBILE = "";
            UserFullname = "";
            civilidno = "";
        }
    }
}
