using Exchange.Managers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
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

        private async void updateProfileButton_Click_old()
        {
            try
            {
                string email = usernameemailTextBox.Text;

                // Email validation
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                bool isValid = Regex.IsMatch(email, pattern);
                if (!isValid)
                {
                    MessageBox.Show("Invalid Email ID");
                    return;
                }

                // Collect user details
                string remID = LoginManager.Remiduser;
                string mobileNo = LoginManager.UserMOBILE;
                string civilId = LoginManager.civilidno;

                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Auth/Email_Updation"
                );

                request.Headers.Add("accept", "*/*");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // Create JSON payload with required fields
                var requestBody = new
                {
                    e_id = remID,
                    mobile_code = 965,
                    mobile_number = mobileNo,
                    name = "",             // Provide appropriate values below
                    first_name = "",
                    middle_name = "",
                    last_name = "",
                    date_of_birth = DateTime.MinValue, // Format: "YYYY-MM-DDT00:00:00.000Z"
                    gender = "",
                    country_code = "",
                    country = "",
                    state_code = "",
                    state = "",
                    city_id = "",
                    city = "",
                    address1 = "",
                    address2 = "",
                    nationality_code = "",
                    nationality = "",
                    country_of_birth_code = "",
                    country_of_birth = "",
                    email = email,
                    residency_type = "",
                    status = "",
                    status_description = "",
                    is_user_registered = true,
                    is_k_y_c_registered = true,
                    is_approved = true,
                    is_m_p_i_n_created = false,
                    app_member_code = 0,
                    member_code = Convert.ToInt32(remID),
                    salary = 0,
                    is_bio_metric_login_enabled = false,
                    profession = "",
                    expected_turnover1 = 0,
                    expected_turnover = 0,
                    expected_turnover_range = "",
                    expected_transaction_count_per_year = 0,
                    expected_transaction_count = 0,
                    expected_transaction_count_range = "",
                    mobile_number_with_out_code = mobileNo,
                    salutation = "",
                    employer = "",
                    place_of_birth = "",
                    economic_activity_code = "",
                    sub_economic_activity_code = "",
                    member_group_id = 0,
                    present_address2 = "",
                    risk_type_code = "",
                    economic_activity_name = "",
                    sub_economic_activity_name = "",
                    member_group = "",
                    risk_type_name = "",
                    salutation_name = "",
                    expiry_date = (DateTime?)null,
                    identity_type_code = "",
                    expected_transaction_count_per_month = 0,
                    income_source = "",
                    other_income = "",
                    account_number = ""
                };

                // Serialize request body to JSON
                string json = JsonSerializer.Serialize(requestBody);
                request.Content = new StringContent(json, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Read and parse response
                string responseText = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                bool success = root.TryGetProperty("success", out JsonElement successElem) &&
                               successElem.GetString()?.ToLower() == "true";
                string message = root.GetProperty("message").GetString();

                if (success)
                {
                    MessageBox.Show("Profile Updated Successfully");
                    NavigationManager.NavigateToHome();
                }
                else
                {
                    MessageBox.Show($"Failed to update profile: {message}");
                }
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        private async void updateProfileButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string email = usernameemailTextBox.Text;

                // Email validation
                string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
                if (!Regex.IsMatch(email, pattern))
                {
                    MessageBox.Show("Invalid Email ID");
                    return;
                }

                // Collect user details
                string remID = LoginManager.Remiduser; // MemberCode
                string apiUrl = $"https://" + Variable.apiipadd + "/api/Auth/Email_Updation?MemberCode="+remID+"&Email="+Uri.EscapeDataString(email);

                using var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, apiUrl);
                request.Headers.Add("accept", "*/*");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Read and parse response
                string responseText = await response.Content.ReadAsStringAsync();
                using var doc = JsonDocument.Parse(responseText);
                var root = doc.RootElement;

                bool success = root.TryGetProperty("success", out JsonElement successElem) &&
                               successElem.GetString()?.ToLower() == "true";
                string message = root.GetProperty("message").GetString();

                if (success)
                {
                    MessageBox.Show("Profile Updated Successfully");
                    NavigationManager.NavigateToHome();
                }
                else
                {
                    MessageBox.Show($"Failed to update profile: {message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private void Page_Load(object sender, RoutedEventArgs e)
        {
            usernameemailTextBox.Text = LoginManager.UserEMAILID;
        }
    }
}
