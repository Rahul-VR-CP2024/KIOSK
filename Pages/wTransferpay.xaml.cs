using Exchange.Common;
using Exchange.Managers;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using static Exchange.Pages.wViewBenficiaryDetails;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wTransferpay.xaml
    /// </summary>
    public partial class wTransferpay : Page
    {
        //get bene id
        //Get the bene name
        //get the bank id
        //get currency code
        public wTransferpay()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                transferdtllbl.Text = "تفاصيل الحوالة";
                receiverlbl.Content = "المستلم";
                sendmoneytolbl.Content = "ارسال الاموال الى ";
                sendmoneyfrom.Content = "ارسال الاموال من  ";
                deliverymethodlbl.Content = "طريقة التسليم";
                paymentmethodlbl.Content = "طريقة الدفع او السداد:";
                purposelbl.Content = "الغرض من التحويل";
                sourcelbl.Content = "مصدر الدخل";
                talbl.Content = "مبلغ التحويل";
                tflbl.Content = "عمولة التحويل";
                oclbl.Content = "رسوم أخرى";
                promodislbl.Content = "الخصم الترويجى";
                totallbl.Content = "إجمالي المستحق";
                monavaillbl.Content = "المبلغ سيكون جاهز";
                receiveamtlbl.Content = "لمبلغ المدفوع من العميل";
            }


            loadbenedetails();
            //runtheloader();
            //runtheloadersource();
            runtheloader(1, 1);
            runtheloadersource(1);
            //nameofreciver.Text = "Test";
            Unloaded += OnPageUnloaded;
        }

        string deliverymethod = "";

        string productcode = "";
        string disbtypecode = "";
        string CurrencyCode = "";
        string CountryCode = "";
        private DisposableTimer curencyRefreshTimer;

        public async void loadbenedetails()
        {
            try
            {
                using var client = new HttpClient();

                var url = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-by-id?eId={SelectedBeneficiaryManager.BENE_SLNO}";
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenManager.Token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    JsonElement root = doc.RootElement;

                    if (root.TryGetProperty("data", out JsonElement dataObj) &&
                        dataObj.TryGetProperty("beneficiary_by_id", out JsonElement beneficiary))
                    {
                        
                        string firstName = beneficiary.TryGetProperty("beneficiary_first_name", out var f) ? f.GetString() : "";
                        string middleName = beneficiary.TryGetProperty("beneficiary_middle_name", out var m) ? m.GetString() : "";
                        string lastName = beneficiary.TryGetProperty("beneficiary_last_name", out var l) ? l.GetString() : "";

                        nameofreciver.Text = $"{firstName} {middleName} {lastName}".Trim();

                        // Save to managers
                        TransferManagers1.SetBENE_FNAME(firstName);
                        TransferManagers1.SetBENE_MNAME(middleName);
                        TransferManagers1.SetBENE_LNAME(lastName);

                        // Mobile
                        if (beneficiary.TryGetProperty("beneficiary_mobile", out var mobileElement))
                            BeneficiaryDetailsManager.SetBENE_MOBILE(mobileElement.GetString());

                        // Currency
                        if (beneficiary.TryGetProperty("beneficiary_category_code", out var currencyElement))
                        {
                            currencymoneytoTextBlock.Text = currencyElement.GetString();
                            ral.Content = $"0 {currencyElement.GetString()}";
                        }

                        // Product Code
                        if (beneficiary.TryGetProperty("product_code", out var productElement))
                        {
                            productcode = productElement.ToString();
                            TransferManagers1.SetProductCode(productcode);
                        }

                        // Country
                        if (beneficiary.TryGetProperty("beneficiary_country_code", out var countryElement))
                        {
                            CountryCode = countryElement.GetString();
                        }
                        if (beneficiary.TryGetProperty("currency_code", out var CurrencyElement))
                        {
                            CurrencyCode = CurrencyElement.GetString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        //Payment_Click
        private void Payment_Click(object sender, RoutedEventArgs e)
        {
            //checklimits();
            //return;
            //REFRESHCURRENCYMETHOD();
            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            //wPaymentmethod mainpage = new wPaymentmethod();
            //NavigationService.Navigate(mainpage);

            //kdamount
            //if (amounttosendTextbox.Text != "" || amounttosendTextbox.Text != null || amounttosendTextbox.Text != "0" || amounttosendTextbox.Text != "0.000" || amounttosendTextbox.Text != "0.00" || kdamount.Text != "" || kdamount.Text != null || kdamount.Text != "0" || kdamount.Text != "0.000" || kdamount.Text != "0.00") {

            if (amounttosendTextbox.Text != "" && amounttosendTextbox.Text != null && amounttosendTextbox.Text != "0" && amounttosendTextbox.Text != "0.000" && amounttosendTextbox.Text != "0.00" && kdamount.Text != "" && kdamount.Text != null && kdamount.Text != "0" && kdamount.Text != "0.000" && kdamount.Text != "0.00")
            {

                    //REFRESHCURRENCYMETHOD("yes");
            }
            else
            {
                MessageBox.Show("Kindly Enter Amount");
            }
            

        }

        //BACK BUTTON
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            NavigationManager.NavigateToHome();

        }
        private async void runtheloader(int ProductCode, int CountryCode)
        {
            try
            {
                var client = new HttpClient();

                // Build GET URL with query parameters
                string url = $"https://{Variable.apiipadd}/api/Transaction/get-purpose-combo-list?ProductCode=" + ProductCode + "&CountryCode=" + CountryCode;

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("accept", "text/plain");

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var doc = JsonDocument.Parse(responseStream))
                {
                    var root = doc.RootElement;

                    if (root.TryGetProperty("data", out JsonElement dataElement) &&
                        dataElement.TryGetProperty("purpose_list", out JsonElement purposeList))
                    {
                        purposecombo.Items.Clear();

                        foreach (var purpose in purposeList.EnumerateArray())
                        {
                            var item = new PurposeItem
                            {
                                Code = purpose.GetProperty("code").GetString(),
                                Name = purpose.GetProperty("name").GetString(),
                                IsDefault = purpose.GetProperty("is_default").GetBoolean()
                            };

                            purposecombo.Items.Add(item);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Auto-select default item if available, otherwise first
            if (purposecombo.Items.Count > 0)
            {
                var defaultItem = purposecombo.Items.Cast<PurposeItem>()
                    .FirstOrDefault(p => p.IsDefault);

                purposecombo.SelectedItem = defaultItem ?? purposecombo.Items[0];
            }
        }
        public class PurposeItem
        {
            public string Code { get; set; }
            public string Name { get; set; }
            public bool IsDefault { get; set; }

            public override string ToString()
            {
                return Name; // Display only the name in the combo box
            }
        }


        private async void runtheloadersource(int ProductCode)
        {
            try
            {
                var client = new HttpClient();

                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "https://" + Variable.apiipadd + "/api/Transaction/get-income-source-combo-list?ProductCode=" + ProductCode
                );

              
                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        JsonElement root = doc.RootElement;

                        if (root.TryGetProperty("data", out JsonElement data) &&
                            data.TryGetProperty("income_source_list", out JsonElement incomeList))
                        {
                            UpdateComboBoxsource(incomeList);
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            if (sourcecombo.Items.Count > 0)
            {
                sourcecombo.SelectedIndex = 0;
            }
        }


        //Purpose of Transfer
        private async void runtheloader_old()
        {
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxmaster/POT");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                request.Content = content;

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBox(doc.RootElement);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (purposecombo.Items.Count > 0)
            {
                purposecombo.SelectedIndex = 0;
            }
        }

        //Source of Income
        private async void runtheloadersource_old()
        {
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxmaster/SOI");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

               // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                request.Content = content;

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (sourcecombo.Items.Count > 0)
            {
                sourcecombo.SelectedIndex = 0;
                
            }
        }

        //XXXXXXXXXX DOES NOTHING
        private async void Button_Click(object sender, RoutedEventArgs e) // Assuming a button click triggers the action
        {
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
               // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxmaster/POT");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                request.Content = content;

                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBox(doc.RootElement);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }
        }

        //Purpose combo update
        private void UpdateComboBox(JsonElement root)
        {
            // Clear existing items (optional)
            purposecombo.Items.Clear();

            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("PURPNAME", out JsonElement purpNameElement))
                    {
                        purposecombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }



        //Source of Income Combo update
        private void UpdateComboBoxsource(JsonElement root)
        {
            
            sourcecombo.Items.Clear();

            // root itself is already an array (not an object with "Data")
            if (root.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in root.EnumerateArray())
                {
                    if (item.TryGetProperty("name", out JsonElement nameElement)) 
                    {
                        sourcecombo.Items.Add(nameElement.GetString());
                    }
                }
            }
            else
            {
                Console.WriteLine("Invalid JSON response: expected array at root.");
            }
        }


        //Update amounts fields
        private void updateadmounts(JsonElement root)
        {
            


            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("SessionID", out JsonElement SessionIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(SessionIDNameElement.GetString());
                        TransferManagers1.SetSessionid(SessionIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("CurrencyCode", out JsonElement IDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetCurrencyCode(IDNameElement.ToString());
                    }

                    if (item.TryGetProperty("CountryCode", out JsonElement CountryCodeIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetCountryCode(CountryCodeIDNameElement.ToString());
                    }
                    if (item.TryGetProperty("Rate", out JsonElement RateIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetRate(RateIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("Commison", out JsonElement CommisonIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetCommison(CommisonIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("DiscoutPercentage", out JsonElement DiscoutPercentageIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetDiscoutPercentage(DiscoutPercentageIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("Operator", out JsonElement OperatorIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetOperator(OperatorIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("LCAmt", out JsonElement LCAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetLCAmt(LCAmtIDNameElement.ToString());
                    }


                    if (item.TryGetProperty("NetAmt", out JsonElement NetAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetNetAmt(NetAmtIDNameElement.ToString());
                    }


                    if (item.TryGetProperty("FCAmt", out JsonElement FCAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetFCAmt(FCAmtIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("VatAmt", out JsonElement VatAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetVatAmt(VatAmtIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("VatPec", out JsonElement VatPecIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetVatPec(VatPecIDNameElement.ToString());
                    }




                    if (item.TryGetProperty("DiscoutValue", out JsonElement DiscoutValueIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        TransferManagers1.SetDiscoutValue(DiscoutValueIDNameElement.ToString());
                    }







                    if (item.TryGetProperty("LCAmt", out JsonElement LCAmtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                        isUpdating = true;
                        kdamount.Text = LCAmtNameElement.ToString();
                        isUpdating = false;

                        tal.Content = LCAmtNameElement.ToString() + " KWD";
                    }

                    if (item.TryGetProperty("Commison", out JsonElement CommisonNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                        //kdamount.Text = LCAmtNameElement.ToString();
                        tfl.Content = CommisonNameElement.ToString() + " KWD";

                    }

                    if (item.TryGetProperty("OtherCharge", out JsonElement OtherChargesElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                        //kdamount.Text = LCAmtNameElement.ToString();
                        ocl.Content = OtherChargesElement.ToString() + " KWD";

                    }


                    if (item.TryGetProperty("NetAmt", out JsonElement NetAmtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                       
                        totl.Content = NetAmtNameElement.ToString() + " KWD";
                    }

                    if (item.TryGetProperty("FCAmt", out JsonElement FCamtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());
                        isUpdating = true;
                        amounttosendTextbox.Text = FCamtNameElement.ToString();
                        isUpdating = false;

                        ral.Content = FCamtNameElement.ToString() + " " + CurrencyCode;
                    }


                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }



        bool isUpdating = false;
        //Get Rate and charge
        private async void REFRESHCURRENCY(object sender, RoutedEventArgs e)
        {
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxremittance/ControlValue");
            //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token); 
            //var content = new StringContent("{\r\n  \"ProductCode\": \"401\",\r\n  \"CurrencyCode\": \"INR\",\r\n  \"CountryCode\": \"IN\",\r\n  \"DisbursalCode\": \"CS\",\r\n  \"Amount\": "+ amounttosendTextbox .Text+ ",\r\n  \"ReceiverCityId\": \"\",\r\n  \"PayerId\": \"\",\r\n  \"BankCode\": \"\",\r\n  \"PayingAgentId\": \"\",\r\n  \"ReceiverTownId\": \"\",\r\n  \"RateType\": \"FC\"\r\n}", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());

           

            //// Parse the JSON response with JsonDocument
            //using (var responseStream = await response.Content.ReadAsStreamAsync())
            //{
            //    using (var doc = JsonDocument.Parse(responseStream))
            //    {
            //       // UpdateComboBoxsource(doc.RootElement);
            //        updateadmounts(doc.RootElement);

            //    }
            //}

        }

        string fcorlcswitch = "FC";
        //ControlValue
        private async void RefreshCurrencyMethod(string buttonClick)
        {
            try
            {
                string source_amount = "";
                string destination_amount = "";

                if (fcorlcswitch == "FC")
                {
                    destination_amount = amounttosendTextbox.Text;
                }
                else if (fcorlcswitch == "LC")
                {
                    source_amount = kdamount.Text;
                }

                using var client = new HttpClient();

                // Build API URL with PascalCase param names (exactly as backend expects)
                var url = $"http://{Variable.apiipadd}/api/Transaction/calculate-amount" +
                          $"?DestinationCountryCode={CountryCode}" +
                          $"&DestinationCurrencyCode={CurrencyCode}" +
                          $"&SourceCountryCode=KW" +
                          $"&SourceCurrencyCode=KWD" +
                          $"&SourceAmount={source_amount}" +
                          $"&DestinationAmount={destination_amount}" +
                          $"&ProductCode={productcode}" +
                          $"&TransferModeCode={deliverymethod}" +
                          $"&PaymentMode={Paymentmethod.Text}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Authorization =
                    new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenManager.Token);
                request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                using var response = await client.SendAsync(request);
                string responseString = await response.Content.ReadAsStringAsync();

                Console.WriteLine("Response: " + responseString);

                using var doc = JsonDocument.Parse(responseString);
                var root = doc.RootElement;

                if (root.TryGetProperty("success", out JsonElement successProp) &&
                    successProp.GetString()?.ToLower() == "true")
                {
                    if (root.TryGetProperty("data", out JsonElement data) &&
                        data.TryGetProperty("amount", out JsonElement amount))
                    {
                        // Update amounts correctly
                        if (fcorlcswitch == "FC" &&
                            amount.TryGetProperty("net_pay_amount", out JsonElement srcAmt))
                        {
                            kdamount.Text = srcAmt.GetDecimal().ToString("N3");
                        }

                        if (fcorlcswitch == "LC" &&
                            amount.TryGetProperty("net_receive_amount", out JsonElement destAmt))
                        {
                            amounttosendTextbox.Text = destAmt.GetDecimal().ToString("N3");
                        }

                        // Labels
                        tal.Content = amount.GetProperty("pay_amount").GetDecimal().ToString("N3") + " KWD";
                        tfl.Content = amount.GetProperty("commission").GetDecimal().ToString("N3") + " KWD";
                        totl.Content = amount.GetProperty("net_pay_amount").GetDecimal().ToString("N3") + " KWD";
                        ral.Content = amount.GetProperty("net_receive_amount").GetDecimal().ToString("N0")
                                      + " " + CurrencyCode;
                    }

                    if (buttonClick == "yes")
                    {
                        checklimits();
                    }
                }
                else
                {
                    string errorMsg = root.TryGetProperty("message", out JsonElement msgEl)
                        ? msgEl.GetString()
                        : "Unknown error from API.";
                    MessageBox.Show(errorMsg, "API Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error calling calculate-amount API: " + ex.Message,
                                "System Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }









        public async void checklimits()
        {




            var fcfinalamount = amounttosendTextbox.Text;

            var lcfinalamount = kdamount.Text;
            


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxremittance/LimitValue");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6ImY0ZTI2Y2FkLWJiNzEtNGJjZi1iNTcwLTcwNDE5NzgzNDM0MyIsImlhdCI6IjEwLzIxLzIwMjQgMTM6MjE6UE0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyOTUwNjA3NCwiZXhwIjoxNzI5NTA3ODc0LCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.5sBI7RhvcIPZoWOl-fwIgiMC34cb4eKu52SGCH3rHgY");
            var content = new StringContent("{\r\n  \"ProductCode\": \""+productcode+"\",\r\n  \"CurrencyCode\": \"" + CurrencyCode + "\",\r\n  \"CountryCode\": \"" + CountryCode + "\",\r\n  \"DisbursalCode\": \"" + deliverymethod + "\",\r\n  \"LCAmount\": "+lcfinalamount+",\r\n  \"FCAmount\": "+ fcfinalamount + ",\r\n  \"RemID\": "+ LoginManager.Remiduser +",\r\n  \"BenSlNo\": "+ SelectedBeneficiaryManager.BENE_SLNO+ "\r\n}", null, "application/json");

            //var content = new StringContent("{\r\n  \"ProductCode\": \"" + productcode + "\"," +
            //    "\r\n  \"CurrencyCode\": \"" + CurrencyCode + "\"," +
            //    "\r\n  \"CountryCode\": \"" + CountryCode + "\"," +
            //    "\r\n  \"DisbursalCode\": \"" + deliverymethod + "\"," +
            //    "\r\n  \"Amount\": " + finalamount + "," +
            //    "\r\n  \"ReceiverCityId\": \"\"," +
            //    "\r\n  \"PayerId\": \"\"," +
            //    "\r\n  \"BankCode\": \"\"," +
            //    "\r\n  \"PayingAgentId\": \"\"," +
            //    "\r\n  \"ReceiverTownId\": \"\"," +
            //    "\r\n  \"RateType\": \"" + fcorlcswitch + "\"\r\n}", null, "application/json");



            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            var contentString = await request.Content.ReadAsStringAsync();
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/v1/sxremittance/LimitValue\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxremittance/LimitValue\n" + DateTime.Now + "\n" + responseString);


            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {
                    


                    string respmsg = dataElement.GetProperty("RESPONSEMSG").ToString();

                    //MessageBox.Show(respmsg);
                    //respmsg = "CONTACT SER";
                    if (respmsg != "SUCCESS")
                    {
                        MessageBox.Show(respmsg);
                        return;
                    }


                    if(respmsg == "SUCCESS")
                    {

                        //MessageBox.Show(""+validatetransation());
                        string result = await validatetransation();
                       

                        if(result == "True") {
                            wPaymentmethod mainpage = new wPaymentmethod();
                            NavigationService.Navigate(mainpage);
                        } else
                        {
                           // MessageBox.Show(" " + result);
                        }
                        //return;
                        
                    }

                    

                }
            }


        }


        public async Task<string> validatetransation()
        {

            string paymentmodestr = "2";

            if (POSTTOBRANCHDONE.kn6 == "Approved")
            {
                //POSTTOBRANCHDONE.Setkn6("Approved");
                paymentmodestr = "2";
            }
            else if (POSTTOBRANCHDONE.kn6 == "Time out")
            {
                //POSTTOBRANCHDONE.Setkn6("Time out");
                paymentmodestr = "5";
            }
            else if (POSTTOBRANCHDONE.kn6 == "Declined")
            {
                // POSTTOBRANCHDONE.Setkn6("Declined");
                paymentmodestr = "4";
            }
            else if (POSTTOBRANCHDONE.kn6 == "Incorrect PIN")
            {
                // POSTTOBRANCHDONE.Setkn6("Incorrect PIN");
                paymentmodestr = "5";
            }
            else
            {
                // POSTTOBRANCHDONE.Setkn6("Declined");
                paymentmodestr = "5";
            }

            paymentmodestr = "11";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/sxRemittance/Remittance/ValidateTransaction");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("3"), "AppID");
            content.Add(new StringContent("3"), "ModuleID");
            content.Add(new StringContent("kiosk"), "ChannelCode");
            content.Add(new StringContent(TransferManagers1.FCAmt), "FCAmount"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.Rate), "Rate"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.Operator), "RateOperator"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.LCAmt), "LCAmount"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.Commison), "Commission"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.DiscoutValue), "Discount"); //CONTROL API
            content.Add(new StringContent(TransferManagers1.NetAmt), "NetAmount"); //CONTROL API
            content.Add(new StringContent(LoginManager.Remiduser), "RemID");
            content.Add(new StringContent("BSN"), "SOICode");
            content.Add(new StringContent("034"), "PurposeCode");
            content.Add(new StringContent(""), "GiftCode");
            //content.Add(new StringContent("11"), "PaymentMode");
            content.Add(new StringContent(paymentmodestr), "PaymentMode");
            content.Add(new StringContent(SelectedBeneficiaryManager.BENE_SLNO.ToString()), "BenCardSlNo");
            content.Add(new StringContent("0"), "TaxAmount");
            content.Add(new StringContent("0"), "TaxPerc");
            content.Add(new StringContent(""), "BeneRemark");
            content.Add(new StringContent("0"), "TaxPerc");
            content.Add(new StringContent(""), "BeneRemark");
            content.Add(new StringContent("" + TransferManagers1.Sessionid + ""), "SESSIONID");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());


            var responseBody = await response.Content.ReadAsStringAsync();
            string issucc = "";
            // Parse the JSON response using System.Text.Json
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                //JsonElement dataElement = root.GetProperty("Message");

                // Extract the accessToken
                 issucc = root.GetProperty("IsSuccess").ToString();

                string Message = root.GetProperty("Message").GetString();


                if(issucc != "True")
                {
                    MessageBox.Show(" " + Message);
                }

                //MessageBox.Show(" " + issucc + " " + Message);



            }
            //MessageBox.Show("" + await response.Content.ReadAsStringAsync());


            // Get form data string representation in a controlled manner
            // string formDataString = "";
            // foreach (var formData in content)
            // {
            // Access the key using ContentHeaders.ContentDisposition
            //string key = formData.ContentHeaders?.ContentDisposition?.ParameterValue("name");
            // MessageBox.Show(""+formData);
            //// Handle missing key gracefully (use formData.Headers.ContentType.MediaType instead)
            //if (key == null)
            //{
            //    key = formData.Headers.ContentType.MediaType;
            //}

            //string value = await formData.ReadAsStringAsync();
            //formDataString += $"{key}: {value}\n";
            // }


            var contentString = "";
            foreach (var part in content)
            {
                //contentString += part.Headers.ToString() + "\n" + await part.ReadAsStringAsync() + "\n";
                if (part.Headers.ContentDisposition != null)
                {
                    var name = part.Headers.ContentDisposition.Name?.Trim('"'); // Trims the quotes around the name
                    var value = await part.ReadAsStringAsync(); // Reads the value of the part
                    //contentString += $"Name: {name}, Value: {value}\n";
                    contentString += $"{name}: {value}\n";
                }
            }
            // MessageBox.Show(contentString);

            //WORKING V1
            //var contentString = await content.ReadAsStringAsync();


            // Get response content as a string
            string responseString = await response.Content.ReadAsStringAsync();

            //MessageBox.Show(contentString);

            // Display content and response in separate RichMessageBoxes
            //RichMessageBox.Show(formDataString + "Request Data");
            RichMessageBox.Show("Request Data to api/v1/sxRemittance/Remittance/ValidateTransaction\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxRemittance/Remittance/ValidateTransaction\n" + DateTime.Now + "\n" + responseString);

            //RichMessageBox.Show("" + content);
            // RichMessageBox.Show("" + response.Content.ReadAsStringAsync());
            //return;


            return issucc;

        }


        private void amounttosendTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdating) return;

            fcorlcswitch = "FC"; // user entered Foreign Currency (INR etc.)

            curencyRefreshTimer?.Cancel();
            curencyRefreshTimer = new DisposableTimer(() =>
            {
                Dispatcher.Invoke(() => RefreshCurrencyMethod("no"));
            }, 3);
        }

        private void lcamounttosendTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdating) return;

            fcorlcswitch = "LC"; // user entered Local Currency (KWD)

            curencyRefreshTimer?.Cancel();
            curencyRefreshTimer = new DisposableTimer(() =>
            {
                Dispatcher.Invoke(() => RefreshCurrencyMethod("no"));
            }, 3);
        }





        private void DoSomethingAfter3Seconds()
        {
        //   REFRESHCURRENCYMETHOD("no");
           // Your code to execute after 3 seconds of no text change
        }



        public static class TransferManagers1
        {
            public static string Sessionid { get; set; }
            public static string CurrencyCode { get; set; }
            public static string CountryCode { get; set; }
            public static string Rate { get; set; }
            public static string Commison { get; set; }
            public static string DiscoutPercentage { get; set; }
            public static string DiscoutValue { get; set; }
            public static string Operator { get; set; }
            public static string LCAmt { get; set; }
            public static string NetAmt { get; set; }
            public static string FCAmt { get; set; }
            public static string VatAmt { get; set; }
            public static string VatPec { get; set; }

            public static string ProductCode { get; set; }

            public static string BENE_FNAME { get; set; }
            public static string BENE_MNAME { get; set; }
            public static string BENE_LNAME { get; set; }


            public static void SetProductCode(string token)
            {
                ProductCode = token;
            }

            public static void SetBENE_FNAME(string token)
            {
                BENE_FNAME = token;
            }

            public static void SetBENE_MNAME(string token)
            {
                BENE_MNAME = token;
            }

            public static void SetBENE_LNAME(string token)
            {
                BENE_LNAME = token;
            }

            public static void SetSessionid(string token)
            {
                Sessionid = token;
            }

            public static void SetCurrencyCode(string token)
            {
                CurrencyCode = token;
            }
            public static void SetCountryCode(string token)
            {
                CountryCode = token;
            }
            public static void SetRate(string token)
            {
                Rate = token;
            }
            public static void SetCommison(string token)
            {
                Commison = token;
            }
            public static void SetDiscoutPercentage(string token)
            {
                DiscoutPercentage = token;
            }

            public static void SetDiscoutValue(string token)
            {
                DiscoutValue = token;
            }
            public static void SetOperator(string token)
            {
                Operator = token;
            }
            public static void SetLCAmt(string token)
            {
                LCAmt = token;
            }
            public static void SetNetAmt(string token)
            {
                NetAmt = token;
            }
            public static void SetFCAmt(string token)
            {
                FCAmt = token;
            }

            public static void SetVatAmt(string token)
            {
                VatAmt = token;
            }

            public static void SetVatPec(string token)
            {
                VatPec = token;
            }
        }


        //LC AMOUNT
        //private void lcamounttosendTextbox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    if (isUpdating) return;

        //    fcorlcswitch = "LC";
        //    curencyRefreshTimer?.Cancel();
        //    curencyRefreshTimer = new DisposableTimer(() => DoSomethingAfter3Seconds(), 3);
        //}

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }
        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            curencyRefreshTimer?.Cancel();
        }
    }
}
