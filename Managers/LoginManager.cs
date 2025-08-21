using DocumentFormat.OpenXml.Spreadsheet;
using System.Net.Http;
using System.Reflection;
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

                var clientotp = new HttpClient();

                var requestotp = new HttpRequestMessage(HttpMethod.Post, "https://" + Variable.apiipadd + "/api/Auth/request-otp");

                var payloadotp = new
                {
                    mobile_Code = 965,
                    mobile_number = password?.Trim(),
                    id_number = username?.Trim()
                };

                // Serialize payload to JSON
                string jsonotp = JsonSerializer.Serialize(payloadotp);
                var contentotp = new StringContent(jsonotp, Encoding.UTF8, "application/json");
                requestotp.Content = contentotp;

                // Log request info

                // Send request
                var responseotp = await clientotp.SendAsync(requestotp);
                // Read response content
                string responseBodyotp = await responseotp.Content.ReadAsStringAsync();


                // Optional: Throw exception if not 2xx
                responseotp.EnsureSuccessStatusCode();

                using (JsonDocument docotp = JsonDocument.Parse(responseBodyotp))
                {

                    JsonElement roototp = docotp.RootElement;
                    // Try to get the "message" field
                    if (!roototp.TryGetProperty("success", out JsonElement messageElementotp))
                        return false;
                    string messageotp = messageElementotp.GetString();
                    // Check if the login was successful
                    if (messageotp != "true")
                        return false;


                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://" + Variable.apiipadd + "/api/Auth/verify-login-otp");

                    var payload = new
                    {
                        mobile_Code = 965,
                        mobile_number = password?.Trim(),
                        id_number = username?.Trim(),
                        o_t_p = 1234
                    };

                    // Serialize payload to JSON
                    string json = JsonSerializer.Serialize(payload);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    request.Content = content;

                    // Log request info

                    // Send request
                    var response = await client.SendAsync(request);
                    // Read response content
                    string responseBody = await response.Content.ReadAsStringAsync();


                    // Optional: Throw exception if not 2xx
                    response.EnsureSuccessStatusCode();

                    using (JsonDocument doc = JsonDocument.Parse(responseBody))
                    {
                        JsonElement root = doc.RootElement;
                        // Try to get the "message" field
                        if (!root.TryGetProperty("message", out JsonElement messageElement))
                            return false;
                        string message = messageElement.GetString();

                        // Check if the login was successful
                        if (message != "OTP Verified Successfully")
                            return false;

                        // Try to access "data.user"
                        if (!root.TryGetProperty("data", out JsonElement dataElement) ||
                            !dataElement.TryGetProperty("user", out JsonElement userElement))
                            return false;

                        string eId = userElement.GetProperty("e_id").GetString();
                        string mobileNumber = userElement.GetProperty("mobile_number").GetString();
                        string firstName = userElement.GetProperty("first_name").GetString();
                        string lastName = userElement.GetProperty("last_name").GetString();
                        string fullName = $"{firstName} {lastName}".Trim();
                        string dateOfBirth = userElement.GetProperty("date_of_birth").GetString();
                        string nationality = userElement.GetProperty("nationality").GetString();
                        string riskType = userElement.GetProperty("risk_type_name").GetString();
                        string email = userElement.GetProperty("email").GetString();
                        string civilno = userElement.GetProperty("id_number").GetString();
                        int memberCode = userElement.GetProperty("member_code").GetInt32();
                        string accessToken = dataElement.GetProperty("jwt_token").GetString();
                        TokenManager.SetToken(accessToken);
                        SetRemiduser(memberCode.ToString());
                        SetUserid(eId);
                        SetUserEMAILID(email);
                        SetUserMOBILE(mobileNumber);
                        SetUserFullname(fullName);
                        Setcivilidno(civilno);


                        return true;
                    }
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
            return !string.IsNullOrEmpty(TokenManager.Token);
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
