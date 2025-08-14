using Exchange.Common;
using Exchange.Managers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;

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
            runtheloader();
            runtheloadersource();

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
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxBeneficiary/Beneficiary/Get");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6Ijg2ZWYyMmU2LTUxNDItNGNmMi04M2UzLTc3NTNmYTI2OGRhMSIsImlhdCI6IjA4LzE1LzIwMjQgMDA6MDA6QU0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyMzY2OTIzMiwiZXhwIjoxNzIzNjcxMDMyLCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.Fe16sIjMdkoCbtasqRBXh7fQaU57yiygF6YlfMVvSgs");
            var content = new StringContent("{\n    \"remID\":"+ LoginManager.Remiduser + ",\n    \"disbMode\":\"\",\n    \"beneSLNO\":"+ SelectedBeneficiaryManager.BENE_SLNO+ ",\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n}", null, "application/json");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            // Read the response content as a string
            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse the JSON response using System.Text.Json
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                //JsonElement dataElement = root.GetProperty("Message");

                // Extract the accessToken
                //string Message = root.GetProperty("Message").GetString();


                // Navigate to the 'Data' object
                JsonElement dataElement = root.GetProperty("Data");

                // Extract the accessToken
                //RemitterID
                //string remid = dataElement.GetProperty("UserId").ToString();
                //string remid = dataElement.GetProperty("BENE_FNAME").ToString() + " " + dataElement.GetProperty("BENE_MNAME").ToString() + " " + dataElement.GetProperty("BENE_LNAME").ToString();
//                nameofreciver.Text = remid;

                //request.Content = content;

                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();

                
                // LoginManager.SetRemiduser(remid);

                // Display the accessToken in a message box
                //Console.WriteLine($"Access Token: {accessToken}");
                //MessageBox.Show($"Message: {Message}");

                //MessageBox.Show(Message + " RemID : " + remid);

                //if (Message == "Login Successfully")
                //{
                //    wMainPage wmainpage = new wMainPage();
                //    NavigationService.Navigate(wmainpage);
                //}


                // RemoveToken(accessToken);
                // SaveToken(accessToken);
                //TokenManager.SetToken(accessToken);
                // MessageBox.Show(LoadToken());

            }


           

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {
                    //Countries.Add(new Country
                    //{
                    //    CountryName = dataElement.GetProperty("BENNAME").GetString(),
                    //    Amt = $"{dataElement.GetProperty("COREDISB").GetString()} {dataElement.GetProperty("BENE_CURRENCY").GetString()}",
                    //    Bene = $"{dataElement.GetProperty("BENE_SALUTE").GetString()} {dataElement.GetProperty("BENNAME").GetString()}",
                    //    Date = "", // You need to specify how to get the date from the JSON response
                    //    TID = dataElement.GetProperty("BENE_SLNO").ToString(), // You need to specify how to get the TID from the JSON response BENE_SLNO
                    //    BANK = dataElement.GetProperty("BENE_BANK").GetString(),
                    //    stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                    //    FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                    //    //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                    //});

                    
                    string remid = dataElement.GetProperty("BENE_SALUTE").ToString() + dataElement.GetProperty("BENE_FNAME").ToString() + " " + dataElement.GetProperty("BENE_MNAME").ToString() + " " + dataElement.GetProperty("BENE_LNAME").ToString();

                    TransferManagers1.SetBENE_FNAME(dataElement.GetProperty("BENE_FNAME").ToString());
                    TransferManagers1.SetBENE_MNAME(dataElement.GetProperty("BENE_MNAME").ToString());
                    TransferManagers1.SetBENE_LNAME(dataElement.GetProperty("BENE_LNAME").ToString());






                    currencymoneytoTextBlock.Text = dataElement.GetProperty("BENE_CURR").ToString();
                    ral.Content =  "0 " + dataElement.GetProperty("BENE_CURR").ToString();

                    nameofreciver.Text = remid;
                    productcode = dataElement.GetProperty("BENE_PROD").ToString();
                    TransferManagers1.SetProductCode(productcode);
                    CurrencyCode = dataElement.GetProperty("BENE_CURR").ToString();
                    CountryCode = dataElement.GetProperty("BENE_CNTRY").ToString();
                    //BENE_PROD
                    Paymentmethod.Text = "";
                    disbtypecode = dataElement.GetProperty("DISBTYPE").ToString();
                    if (dataElement.GetProperty("DISBTYPE").ToString() == "CP") { 

                    //BT OR CP BANK TRANSFER OR CASH PAYMENT
                    //BCManager.selectedoptionborc;
                    Paymentmethod.Text = "CASH PAYMENT";
                    }
                    if (dataElement.GetProperty("DISBTYPE").ToString() == "BT")
                    {
                        
                        Paymentmethod.Text = "BANK TRANSFER";
                    }

                    deliverymethod = dataElement.GetProperty("COREDISB").ToString();

                }
            }


            var client3 = new HttpClient();
            var request3 = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxgeneral/DefaultProduct/Disbmodes?ProductCode="+ productcode + "&MobDisbcode="+ disbtypecode + "");
            request3.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6ImVjY2QwNDM0LWE4NjctNDk5Zi1hNTI1LTZkMTI0NjlmMTkyYiIsImlhdCI6IjA4LzE1LzIwMjQgMDE6NDg6QU0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyMzY3NTczMiwiZXhwIjoxNzIzNjc3NTMyLCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.GGiaVTGdmMkyupZuWx7VbxfoeEe4LAxnoqGJ1D1DKdQ");
            using var response3 = await client3.SendAsync(request3);
            response3.EnsureSuccessStatusCode();
            //Console.WriteLine(await response3.Content.ReadAsStringAsync());

            using (var responseStream = await response3.Content.ReadAsStreamAsync())
            {
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {


                    //string remid = dataElement.GetProperty("BENE_SALUTE").ToString() + dataElement.GetProperty("BENE_FNAME").ToString() + " " + dataElement.GetProperty("BENE_MNAME").ToString() + " " + dataElement.GetProperty("BENE_LNAME").ToString();


                    //currencymoneytoTextBlock.Text = dataElement.GetProperty("BENE_CURR").ToString();


                    //nameofreciver.Text = remid;
                    //productcode = dataElement.GetProperty("BENE_PROD").ToString();
                    ////BENE_PROD
                    //Paymentmethod.Text = "";
                    //disbtypecode = dataElement.GetProperty("DISBTYPE").ToString();
                    //if (dataElement.GetProperty("DISBTYPE").ToString() == "CP")
                    //{

                    //    //BT OR CP BANK TRANSFER OR CASH PAYMENT
                    //    //BCManager.selectedoptionborc;
                    //    Paymentmethod.Text = "CASH PAYMENT";
                    //}
                    //if (dataElement.GetProperty("DISBTYPE").ToString() == "BT")
                    //{

                    //    Paymentmethod.Text = "BANK TRANSFER";
                    //}
                    DM.Text = dataElement.GetProperty("DisbName").ToString();

                    //deliverymethod = dataElement.GetProperty("COREDISB").ToString();

                }
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

                    REFRESHCURRENCYMETHOD("yes");
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

        //Purpose of Transfer
        private async void runtheloader()
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
        private async void runtheloadersource()
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
            // Clear existing items (optional)
            sourcecombo.Items.Clear();
            

            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("NAME", out JsonElement purpNameElement))
                    {
                        sourcecombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
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
        private async void REFRESHCURRENCYMETHOD(string buttonclick)
        {

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxremittance/ControlValue");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //MessageBox.Show("productcode " + productcode + "CurrencyCode " + CurrencyCode + "CountryCode " + CountryCode + "disbtypecode " + deliverymethod + " " );
            //var content = new StringContent("{\r" +
            //    "\n  \"ProductCode\": \""+ productcode + "\"," +
            //    "\r\n  \"CurrencyCode\": \""+CurrencyCode+"\"," +
            //    "\r\n  \"CountryCode\": \""+ CountryCode + "\"," +
            //    "\r\n  \"DisbursalCode\": \""+ deliverymethod + "\"," +
            //    "\r\n  \"Amount\": " + amounttosendTextbox.Text + "," +

            //    "\r\n  \"ReceiverCityId\": \"\"," +
            //    "\r\n  \"PayerId\": \"\"," +
            //    "\r\n  \"BankCode\": \"\"," +
            //    "\r\n  \"PayingAgentId\": \"\"," +
            //    "\r\n  \"ReceiverTownId\": \"\"," +
            //    "\r\n  \"RateType\": \"FC\"\r\n}"
            //    , null, "application/json");


            //var content = new StringContent("{\r\n  \"ProductCode\": \""+ productcode + "\",\r\n  \"CurrencyCode\": \""+CurrencyCode+"\",\r\n  \"CountryCode\": \""+ CountryCode + "\",\r\n  \"DisbursalCode\": \"2\",\r\n  \"Amount\": " + amounttosendTextbox.Text + ",\r\n  \"ReceiverCityId\": \"\",\r\n  \"PayerId\": \"\",\r\n  \"BankCode\": \"\",\r\n  \"PayingAgentId\": \"\",\r\n  \"ReceiverTownId\": \"\",\r\n  \"RateType\": \"FC\"\r\n}", null, "application/json");


            //var content = new StringContent("{\r\n  \"ProductCode\": \""+ productcode + "\",\r\n  \"CurrencyCode\": \"" + CurrencyCode + "\",\r\n  \"CountryCode\": \"" + CountryCode + "\",\r\n  \"DisbursalCode\": \"" + deliverymethod + "\",\r\n  \"Amount\": " + amounttosendTextbox.Text + ",\r\n  \"ReceiverCityId\": \"\",\r\n  \"PayerId\": \"\",\r\n  \"BankCode\": \"\",\r\n  \"PayingAgentId\": \"\",\r\n  \"ReceiverTownId\": \"\",\r\n  \"RateType\": \"FC\"\r\n}", null, "application/json");
            var finalamount = "";

            if(fcorlcswitch == "FC")
            {
                finalamount = amounttosendTextbox.Text;
            }

            if (fcorlcswitch == "LC")
            {
                finalamount = kdamount.Text;
            }



            var content = new StringContent("{\r\n  \"ProductCode\": \"" + productcode + "\"," +
                "\r\n  \"CurrencyCode\": \"" + CurrencyCode + "\"," +
                "\r\n  \"CountryCode\": \"" + CountryCode + "\"," +
                "\r\n  \"DisbursalCode\": \"" + deliverymethod + "\"," +
                "\r\n  \"Amount\": " + finalamount + "," +
                "\r\n  \"ReceiverCityId\": \"\"," +
                "\r\n  \"PayerId\": \"\"," +
                "\r\n  \"BankCode\": \"\"," +
                "\r\n  \"PayingAgentId\": \"\"," +
                "\r\n  \"ReceiverTownId\": \"\"," +
                "\r\n  \"RateType\": \""+ fcorlcswitch + "\"\r\n}", null, "application/json");

            string jsonContent = content.ReadAsStringAsync().Result; // Replace with content.Encoding if known
            //MessageBox.Show(jsonContent);


            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());

            var contentString = await request.Content.ReadAsStringAsync();
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/v1/sxremittance/ControlValue\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxremittance/ControlValue\n" + DateTime.Now + "\n" + responseString);



            if (buttonclick == "yes")
            {
                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        // UpdateComboBoxsource(doc.RootElement);
                        updateadmounts(doc.RootElement);

                        checklimits();
                        //wPaymentmethod mainpage = new wPaymentmethod();
                        //NavigationService.Navigate(mainpage);

                    }
                }
            }
            else
            {
                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        // UpdateComboBoxsource(doc.RootElement);
                        updateadmounts(doc.RootElement);

                    }
                }
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

            fcorlcswitch = "FC";
            curencyRefreshTimer?.Cancel();
            curencyRefreshTimer = new DisposableTimer(() => DoSomethingAfter3Seconds(), 3);
        }

        private void DoSomethingAfter3Seconds()
        {
           REFRESHCURRENCYMETHOD("no");
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
        private void lcamounttosendTextbox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (isUpdating) return;

            fcorlcswitch = "LC";
            curencyRefreshTimer?.Cancel();
            curencyRefreshTimer = new DisposableTimer(() => DoSomethingAfter3Seconds(), 3);
        }

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
