using Exchange.Managers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wResetPassword.xaml
    /// </summary>
    public partial class wResetPassword : Page
    {

        string resetstagemanager = "1";
        public wResetPassword()
        {
            InitializeComponent();
            resetstagemanager = "1";
            //securityquestionpanel.Visibility = Visibility.Hidden;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private async void proceedtosecurityquestion(object sender, RoutedEventArgs e)
        {

            if(usernametxt.Text == "")
            {
                MessageBox.Show("Kindly Enter Username to Proceed");
                return;
            }
           

            if (resetstagemanager == "1")
            {
                enterusernamearea.Visibility = Visibility.Collapsed;
                runtheloadersource();
                //MessageBox.Show("" + resetstagemanager);

            }


             if (resetstagemanager == "2")
            {
                securityquestionpanel.Visibility = Visibility.Visible;

                ResetPass selectedProduct = (ResetPass)questionsList.SelectedItem;
                //MessageBox.Show("" + selectedProduct.ResetCode + " " + selectedProduct.ResetName);

            }
            return;

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxuser/user/forgot");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new StringContent("{\n    \"username\":\""+ usernametxt.Text + "\"\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());

            string responseString = await response.Content.ReadAsStringAsync();
            dynamic responseObject = JsonConvert.DeserializeObject(responseString);
            // Assuming you have Newtonsoft.Json installed

            //if (responseObject.IsSuccess)
            {
                // Clear any previous questions from the list
                questionsList.Items.Clear();

                // Extract and display questions
                foreach (var question in responseObject.Data.SecurityQns)
                {
                    string questionId = question.QuestionID.ToString();
                    string questionText = question.Question;
                    questionsList.Items.Add($"{questionId}: {questionText}");
                }

                // Optionally, display a success message or provide instructions for answering the questions
                MessageBox.Show("Security questions retrieved successfully. Please answer the questions below to reset your password.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                resetstagemanager = "2";
            }
           // else
            {
             //   MessageBox.Show(responseObject.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }

        private void Page_Load(object sender, RoutedEventArgs e)
        {

        }

        private async void runtheloadersource()
        {

          
            try
            {
                var client = new HttpClient();


                var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/sxuser/user/forgot");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
               
                var content = new StringContent("{\n    \"username\":\"" + usernametxt.Text + "\"\n}", null, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                        resetstagemanager = "2";
                    }
                }

                //runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (questionsList.Items.Count > 0)
            {
                //  productcombo.SelectedIndex = 0;

            }
        }

        private void UpdateComboBoxsource(JsonElement root)
        {
            questionsList.ItemsSource = null; 
            questionsList.Items.Clear();

            List<ResetPass> products = new List<ResetPass>();


            
            //if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            //{
            //    foreach (var item in dataElement.EnumerateArray())
            //    {
            //        if (item.TryGetProperty("Productname", out JsonElement productNameElement) &&
            //            item.TryGetProperty("ProductCode", out JsonElement productCodeElement))

            //        //DisbursalModeCode
            //        {

            //            //   MessageBox.Show(productCodeElement.GetString());
            //            //   MessageBox.Show(productNameElement.GetString());

            //            products.Add(new ResetPass
            //            {
            //                ResetCode = productNameElement.GetString(),
            //                ResetName = productCodeElement.GetString()
            //            });
            //        }
            //    }
            //}

            // Check if "IsSuccess" is true and "Data" exists
            if (root.TryGetProperty("IsSuccess", out JsonElement isSuccessElement) &&
                isSuccessElement.ValueKind == JsonValueKind.True &&
                root.TryGetProperty("Data", out JsonElement dataElement) &&
                dataElement.ValueKind == JsonValueKind.Object)
            {
                // Check for "SecurityQns" array inside "Data"
                if (dataElement.TryGetProperty("SecurityQns", out JsonElement securityQnsElement) &&
                    securityQnsElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in securityQnsElement.EnumerateArray())
                    {
                        if (item.TryGetProperty("QuestionID", out JsonElement questionIdElement) &&
                            item.TryGetProperty("Question", out JsonElement questionElement))
                        {
                            products.Add(new ResetPass
                            {
                                ResetCode = questionIdElement.GetInt32().ToString(),
                                ResetName = questionElement.GetString()
                            });
                        }
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }

            // Bind the ComboBox to the products list
            questionsList.ItemsSource = products;

            // Assuming products is a collection and has at least one item
            if (products.Count > 0)
            {
                questionsList.SelectedItem = products[0];
            }
            questionsList.DisplayMemberPath = "ResetName";
        }

        public class ResetPass
        {
            public string ResetCode { get; set; }
            public string ResetName { get; set; }
        }

        private void questionsListselectionchanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
