using Exchange.Common;
using Exchange.Knet;
using Exchange.Managers;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using System.Xml.Linq;
using static Exchange.Pages.wtobankorcash;
using static Exchange.Pages.wTransferpay;
using static Exchange.Pages.wViewBenficiaryDetails;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wPaymentmethod.xaml
    /// </summary>
    public partial class wPaymentmethod : Page
    {
        private DispatcherTimer timer;
        string statusofknet = "y";
        private eSocketClient eSocketClient;
        static private string TestTerminalId = Variable.kioskidno;

        public wPaymentmethod()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                paymentmethodlbl.Text = "طريقة الدفع او السداد:";

            }

            // Example usage:
            //RichMessageBox.Show("This is a rich text error message.");

            //MessageBox.Show(TransferManagers1.Sessionid);

            //Validate
            //Post BO
            //Post to Branch
            Unloaded += OnPageUnloaded;
        }

        

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            //WelcomePage welocmepage = new WelcomePage();
            //NavigationService.Navigate(welocmepage);

            NavigationManager.NavigateToHome();
        }

        //
        private void transferbtnclick(object sender, RoutedEventArgs e)
        {
            int getcurrentvalue = 0;
            try
            {
                //OG METHODS
                //POSTTOBRANCHDONE.Setkt3("APPROVE");
                knetbutton.IsEnabled = false;
                //TO TEST WITHOUT KNET
                //validatetransation();
                //OG METHODS
                //return;
                //TO TEST WITHOUT KNET

                //knetbutton.IsEnabled = true;
                //wThankyou mainpage = new wThankyou();
                //NavigationService.Navigate(mainpage);


                statusofknet = "y";
                getcurrentvalue = GetInvoiceNumber(getcurrentvalue);
                // turning this to true will display a lot of information that might hide the command menu
                Utility.DEBUG_MODE = false;


                //Spin off reader thread, this thread continually processes responses and
                //puts them into a eSocketClient.ReceivedMessages
                //Thread client_thread = new Thread(client.TcpRead);
                //client_thread.Name = "TcpReaderThread";
                //Utility.Log("Starting reading thread..");
                //client_thread.Start();

                eSocketClient = new eSocketClient();
                eSocketClient.StartReading();

                RequestPaymentCustom(getcurrentvalue.ToString());

                // Create a DispatcherTimer and set its interval to 3 seconds
                timer = new DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(3);

                // Set the Tick event handler
                timer.Tick += Timer_Tick;

                //if()

                //string a = "y";



                // Start the timer

                timer.Start();

            }
            catch (Exception ex) 
            {
                MessageBox.Show("Transaction failed");
                RichMessageBox.Show($"TransferToBank-Transaction:{getcurrentvalue}-Error-{ex.Message}\n {JsonConvert.SerializeObject(ex)}");
                NavigationManager.NavigateToHome();
            }
        }

        private int GetInvoiceNumber(int getcurrentvalue)
        {
            string filePath = "KNETINVOICE.txt"; // Replace with the actual path to your text file
            try
            {
                // Read the existing value from the text file
                int currentValue = 0;
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        int.TryParse(reader.ReadLine(), out currentValue);
                    }
                }

                // Increment the value
                currentValue++;

                // Write the incremented value back to the text file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(currentValue);
                }
                getcurrentvalue = currentValue;
                // MessageBox.Show($"Value incremented to: {currentValue}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

            return getcurrentvalue;
        }

        public async void validatetransation()
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

            //MessageBox.Show(paymentmodestr + POSTTOBRANCHDONE.kn6);

            

            if(paymentmodestr != "2")
            {
                wThankyou page = new wThankyou();
                NavigationService.Navigate(page);


                //JsonElement dataElement = root.GetProperty("Data");
                //MessageBox.Show("" + dataElement.GetProperty("TxnReferenceNo").GetRawText());
                POSTTOBRANCHDONE.Setseltxnno("0");
                return;
            }

            paymentmodestr = "11";

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxRemittance/Remittance/ValidateTransaction");
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
            content.Add(new StringContent(""+ TransferManagers1.Sessionid + ""), "SESSIONID");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());


            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse the JSON response using System.Text.Json
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                //JsonElement dataElement = root.GetProperty("Message");

                // Extract the accessToken
                string issucc = root.GetProperty("IsSuccess").ToString();

                string Message = root.GetProperty("Message").GetString();


               // MessageBox.Show(" "+ issucc + " " + Message);
                



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
            posttobo();

        }

        string TranID;
        public async void posttobo()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxTransaction/Transaction");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new StringContent("{" +
                "\n    \"TransactionID\": 0," +
                "\n    \"BENE_REMID\": "+ LoginManager.Remiduser + "," +
                "\n    \"BENE_SLNO\": "+ SelectedBeneficiaryManager.BENE_SLNO.ToString() + "," +
                "\n    \"BENE_NAME\": \""+TransferManagers1.BENE_FNAME + " " + TransferManagers1.BENE_MNAME + " "+ TransferManagers1.BENE_LNAME +"\"," +
                "\n    \"LCCurrency\": \"KWD\"," +
                "\n    \"LCAmount\": "+ TransferManagers1.LCAmt + "," +
                "\n    \"FCCurrency\": \""+TransferManagers1.CurrencyCode+"\"," +
                "\n    \"FCAmount\": "+ TransferManagers1.FCAmt+ "," +
                "\n    \"Rate\": "+ TransferManagers1.Rate + "," +
                "\n    \"Commission\": "+TransferManagers1.Commison+"," +
                "\n    \"Discount\": "+TransferManagers1.DiscoutValue+"," +
                "\n    \"SOICode\": \"BSN\"," +
                "\n    \"SOIName\": \"\"," +
                "\n    \"POTCode\": \"034\"," +
                "\n    \"POTName\": \"\"," +
                "\n    \"TaxPerc\": 0," +
                "\n    \"TaxAmount\": 0," +
                "\n    \"PaymentType\": 11," +
                "\n    \"TransStatus\": 1," +
                "\n    \"RemittanceType\": \""+ BCManager.selectedoptionborc + "\"," +
                "\n    \"CreatedOn\": \"\"," +
                "\n    \"ModifiedOn\": \"\"," +
                "\n    \"NetAmt\": "+ TransferManagers1.NetAmt +"," +
                "\n    \"BENE_SALUTE\": \"MR.\"," +
                "\n    \"BENE_GENDER\": \"0\"," +
                "\n    \"BENE_FNAME\": \""+TransferManagers1.BENE_FNAME+"\"," +
                "\n    \"BENE_MNAME\": \""+TransferManagers1.BENE_MNAME+"\"," +
                "\n    \"BENE_LNAME\": \""+TransferManagers1.BENE_LNAME+"\"," +
                "\n    \"BENE_ADDRESS1\": \"\"," +
                "\n    \"BENE_ADDRESS2\": \"\"," +
                "\n    \"BENE_CITY\": \"\"," +
                "\n    \"BENE_STATE\": \"\"," +
                "\n    \"BENE_CNTRY\": \""+TransferManagers1.CountryCode+"\"," +
                "\n    \"BENE_NATION\": \""+TransferManagers1.CountryCode+"\"," +
                "\n    \"BENE_ZIP\": \"\"," +
                "\n    \"BENE_PHONE\": \"\"," +
                "\n    \"BENE_MOBILE\": \""+ BeneficiaryDetailsManager.BENE_MOBILE+ "\"," +
                "\n    \"BENE_FAX\": \"\"," +
                "\n    \"BENE_EMAIL\": \"\"," +
                "\n    \"BENE_PROD\": \""+TransferManagers1.ProductCode+"\"," +
                "\n    \"BENE_CURR\": \""+TransferManagers1.CurrencyCode+"\"," +
                "\n    \"BENE_ACCNO\": \"\"," +
                "\n    \"BENE_ACCTYPE\": \"\"," +
                "\n    \"BENE_BANKID\": \"\"," +
                "\n    \"BENE_BANK\": \"\"," +
                "\n    \"BENE_BRANCHID\": \"\"," +
                "\n    \"BENE_BRANCH\": \"\"," +
                "\n    \"BENE_BRADD1\": \"\"," +
                "\n    \"BENE_BRADD2\": \"\"," +
                "\n    \"BENE_BBRCITY\": \"\"," +
                "\n    \"BENE_BBRSTATE\": \"\"," +
                "\n    \"BENE_BBRCNTRY\": \"\"," +
                "\n    \"BENE_BBRZIP\": \"\"," +
                "\n    \"BENE_BIDTYP\": \"\"," +
                "\n    \"BENE_BIDREF\": \"\"," +
                "\n    \"BENE_REMRKS\": \"\"," +
                "\n    \"BENE_STATUS\": \"1\"," +
                "\n    \"BENE_BRIFSCCODE\": \"\"," +
                "\n    \"BENE_SWIFTCODE\": \"\"," +
                "\n    \"BENE_SORTCODE\": \"\"," +
                "\n    \"BENE_ROUTCODE\": \"\"," +
                "\n    \"BENE_IBANCODE\": \"\"," +
                "\n    \"BENE_BBANKTYPE\": \"\"," +
                "\n    \"BENE_BANKCODE\": \"\"," +
                "\n    \"BENE_DISB\": \"\"," +
                "\n    \"BENE_EMIRATE\": \"\"," +
                "\n    \"BENE_AIRPORT\": \"\"," +
                "\n    \"BENE_MOBILECODE\": \"\"," +
                "\n    \"BENE_DOB\": \"\"," +
                "\n    \"BENE_CNIC\": \"\"," +
                "\n    \"BENE_TAXID\": \"\"," +
                "\n    \"BENE_LNDMRK\": \"\"," +
                "\n    \"BENE_OUTBR\": \"\"," +
                "\n    \"BENE_OUTBRCODE\": \"\"," +
                "\n    \"BENE_TYPE\": \"\"," +
                "\n    \"BENE_REL\": \"\"," +
                "\n    \"BENE_BRNDIST\": \"\"," +
                "\n    \"VATCODE\": \"\"," +
                "\n    \"Operator\": \"\"," +
                "\n    \"SessionID\":\""+ TransferManagers1.Sessionid + "\"," +
                "\n    \"CurrentTime\": \"\"\n}", null, "application/json");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());

            var contentString = await request.Content.ReadAsStringAsync();
            //foreach (var part in content)
            //{
            //    //contentString += part.Headers.ToString() + "\n" + await part.ReadAsStringAsync() + "\n";
            //    if (part.Headers.ContentDisposition != null)
            //    {
            //        var name = part.Headers.ContentDisposition.Name?.Trim('"'); // Trims the quotes around the name
            //        var value = await part.ReadAsStringAsync(); // Reads the value of the part
            //        //contentString += $"Name: {name}, Value: {value}\n";
            //        contentString += $"{name}: {value}\n";
            //    }
            //}
            // MessageBox.Show(contentString);

            //WORKING V1
            //var contentString = await content.ReadAsStringAsync();


            // Get response content as a string
            string responseString = await response.Content.ReadAsStringAsync();

            //MessageBox.Show(contentString);

            // Display content and response in separate RichMessageBoxes
            //RichMessageBox.Show(formDataString + "Request Data");
            RichMessageBox.Show("Request Data to api/v1/sxTransaction/Transaction\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxTransaction/Transaction\n" + DateTime.Now + "\n" + responseString);

            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());



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
                string Message = root.GetProperty("Message").GetString();


                // Navigate to the 'Data' object
                JsonElement dataElement = root.GetProperty("Data");

                // Extract the accessToken
                //RemitterID
                //string remid = dataElement.GetProperty("UserId").ToString();
                TranID = dataElement.GetProperty("TransactionID").ToString();
                //MessageBox.Show(TranID);
                //LoginManager.SetRemiduser(remid);

                //// Display the accessToken in a message box
                ////Console.WriteLine($"Access Token: {accessToken}");
                ////MessageBox.Show($"Message: {Message}");

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

            posttobranch();

        }



        public async void posttobranch()
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


            //MessageBox.Show(TranID + " Inside the PTB");
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxRemittance/Remittance/PostToBranch");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("1"), "AppID");
            content.Add(new StringContent("3"), "ModuleID");
            //content.Add(new StringContent(LoginManager.Userid), "UserID");
            content.Add(new StringContent(TranID), "TraceID");
            content.Add(new StringContent("kiosk"), "ChannelCode");
            content.Add(new StringContent(TransferManagers1.FCAmt), "FCAmount");
            content.Add(new StringContent(TransferManagers1.Rate), "Rate");
            content.Add(new StringContent(TransferManagers1.Operator), "RateOperator");
            content.Add(new StringContent(TransferManagers1.LCAmt), "LCAmount");
            content.Add(new StringContent(TransferManagers1.Commison), "Commission");
            content.Add(new StringContent("0"), "OtherCharge");
            content.Add(new StringContent("0"), "OtherChargePercent");
            content.Add(new StringContent(TransferManagers1.DiscoutValue), "Discount");
            content.Add(new StringContent(TransferManagers1.NetAmt), "NetAmount");
            content.Add(new StringContent(LoginManager.Remiduser), "RemID");
            content.Add(new StringContent("BSN"), "SOICode");
            content.Add(new StringContent("034"), "PurposeCode");
            //content.Add(new StringContent("11"), "PaymentMode");
            content.Add(new StringContent(paymentmodestr), "PaymentMode");
            content.Add(new StringContent(SelectedBeneficiaryManager.BENE_SLNO.ToString()), "BenCardSlNo");
            content.Add(new StringContent("0"), "PaymentRef");
            content.Add(new StringContent("0"), "TaxAmount");
            content.Add(new StringContent("0"), "TaxPerc");
            content.Add(new StringContent(""), "BeneRemark");
            content.Add(new StringContent(TransferManagers1.Sessionid), "SESSIONID");
            request.Content = content;
            using var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());

            // Read the response content as a string
            var responseBody = await response.Content.ReadAsStringAsync();



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
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/v1/sxRemittance/Remittance/PostToBranch\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxRemittance/Remittance/PostToBranch\n" + DateTime.Now + "\n" + responseString);






            // Parse the JSON response using System.Text.Json
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                //JsonElement dataElement = root.GetProperty("Message");

                // Extract the accessToken
                string Message = root.GetProperty("Message").GetString();


                if(Message == "SUCCESS")
                {
                    wThankyou mainpage = new wThankyou();
                    NavigationService.Navigate(mainpage);

                   
                    JsonElement dataElement = root.GetProperty("Data");
                    //MessageBox.Show("" + dataElement.GetProperty("TxnReferenceNo").GetRawText());
                    POSTTOBRANCHDONE.Setseltxnno(dataElement.GetProperty("TxnReferenceNo").ToString());

                } else
                {
                    //MessageBox.Show("Kindly Contact Customer Care");
                }


                // Navigate to the 'Data' object
               

                // Extract the accessToken
                //RemitterID
                //string remid = dataElement.GetProperty("UserId").ToString();
                //TranID = dataElement.GetProperty("TxnReferenceNo").GetRawText();
                
                //MessageBox.Show(TranID);
                //LoginManager.SetRemiduser(remid);

                //// Display the accessToken in a message box
                ////Console.WriteLine($"Access Token: {accessToken}");
                ////MessageBox.Show($"Message: {Message}");

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


           
        }
        

        //knet testing
        private void KNETTEST(object sender, RoutedEventArgs e)
        {
            statusofknet = "y";
            string filePath = "KNETINVOICE.txt"; // Replace with the actual path to your text file
            int getcurrentvalue = 0;
            try
            {
                // Read the existing value from the text file
                int currentValue = 0;
                if (File.Exists(filePath))
                {
                    using (StreamReader reader = new StreamReader(filePath))
                    {
                        int.TryParse(reader.ReadLine(), out currentValue);
                    }
                }

                // Increment the value
                currentValue++;

                // Write the incremented value back to the text file
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    writer.WriteLine(currentValue);
                }
                getcurrentvalue = currentValue;
               // MessageBox.Show($"Value incremented to: {currentValue}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }

           // turning this to true will display a lot of information that might hide the command menu
            Utility.DEBUG_MODE = false;

            eSocketClient = new eSocketClient();
            eSocketClient.StartReading();

            RequestPaymentCustom(getcurrentvalue.ToString());

            // Create a DispatcherTimer and set its interval to 3 seconds
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(3);

            // Set the Tick event handler
            timer.Tick += Timer_Tick;

            //if()

            //string a = "y";


            
            // Start the timer
          
            timer.Start();
            


        }
       
        private void Timer_Tick(object sender, EventArgs e)
        {
            ShowLastResponseCustom();
            // Your code to be executed every 3 seconds goes here
            // For example:
            Console.WriteLine("This code runs every 3 seconds.");

            if(statusofknet == "n")
            {
                timer.Stop();
            }
            // Update UI elements, perform calculations, or call other methods
        }
        
        //static private string TestTerminalId = "12700130";
        //14410949

        static void showPrompt()
        {
            Console.WriteLine("*************************************************");
            Console.WriteLine("Enter the number corresponding to your selection:");
            Console.WriteLine("1. Initialize terminal");
            Console.WriteLine("2. Request payment");
            Console.WriteLine("3. Close terminal");
            Console.WriteLine("4. Show all responses so far");
            Console.WriteLine("5. Show last response");
            Console.WriteLine("*************************************************");
        }

        void Init()
        {
            Console.WriteLine("Sending initialization request..");
            //Construct the proper XML 
            XDocument doc = XmlRequest.Initialization(TestTerminalId);
            //Convert it to bytes and write it to the TCP connection
            eSocketClient.TcpWrite(Utility.XmlToBytes(doc));
        }

        void RequestPayment()
        {
            int amount;
            string transactionId;
            while (true)
            {
                Console.Write("Enter Amount (fils): ");
                try
                {
                    amount = Int32.Parse(Console.ReadLine());
                    break;
                }
                catch
                {
                    Console.WriteLine("Please enter a number!");
                }
            }

            Console.WriteLine("A transaction ID should be 6 character numeric (xxxxxx, starting with 100001) and unique!");
            Console.Write("Enter New Transaction ID: ");
            transactionId = Console.ReadLine().Trim();

            //Construct the proper XML 
            XDocument doc = XmlRequest.Payment(amount.ToString(), TestTerminalId, transactionId);
            //Convert it to bytes and write it to the TCP connection
            eSocketClient.TcpWrite(Utility.XmlToBytes(doc));
        }

        //Burhan 29/09/2024
        void RequestPaymentCustom(string newinvoiceno)
        {

            //TransferManagers1.NetAmt = ""

            //string a = "20.000";
            int amt = int.Parse(TransferManagers1.NetAmt.Replace(".", ""));

            //int amount = 5000;
            int amount = amt;
            //string transactionId = "130024";
            string transactionId = newinvoiceno;
            //while (true)
            //{
            //    Console.Write("Enter Amount (fils): ");
            //    try
            //    {
            //        amount = Int32.Parse(Console.ReadLine());
            //        break;
            //    }
            //    catch
            //    {
            //        Console.WriteLine("Please enter a number!");
            //    }
            //}

            //Console.WriteLine("A transaction ID should be 6 character numeric (xxxxxx, starting with 100001) and unique!");
            //Console.Write("Enter New Transaction ID: ");
            //transactionId = Console.ReadLine().Trim();

            //Construct the proper XML 
            XDocument doc = XmlRequest.Payment(amount.ToString(), TestTerminalId, transactionId);
            //Convert it to bytes and write it to the TCP connection
            eSocketClient.TcpWrite(Utility.XmlToBytes(doc));
        }

        void CloseTerminal()
        {
            Console.WriteLine("Sending close terminal request..");
            //Construct the proper XML 
            XDocument doc = XmlRequest.Close(TestTerminalId);
            //Convert it to bytes and write it to the TCP connection
            eSocketClient.TcpWrite(Utility.XmlToBytes(doc));
        }

        void ShowAllResponses()
        {
            //avoid race conditions between list access and list modification
            lock (eSocketClient.ReceivedMessages)
            {
                if (eSocketClient.ReceivedMessages.Count() == 0)
                {
                    Console.WriteLine("No messages received, if you were expecting an INIT response, perhaps the device is still initializing?");
                }

                int index = 0;
                foreach (byte[] msg in eSocketClient.ReceivedMessages)
                {
                    Console.WriteLine($"[MSG {index}]");
                    Console.WriteLine(Utility.BytesToXml(msg).ToString());
                    Console.WriteLine("-------------------------------------------------");
                    index++;
                }
            }
        }

        void ShowLastResponse()
        {
            Console.WriteLine("Last message received:");

            //avoid race conditions between list access and list modification
            lock (eSocketClient.ReceivedMessages)
            {
                if (eSocketClient.ReceivedMessages.Count > 0)
                {
                    Console.WriteLine(Utility.BytesToXml(eSocketClient.LastMessage).ToString());
                }
                else
                {
                    Console.WriteLine("No messages received, if you were expecting an INIT response, perhaps the device is still initializing?");
                }
            }
        }

        public void ShowLastResponseCustom()
        {
            Console.WriteLine("Last message received:");

            //avoid race conditions between list access and list modification
            lock (eSocketClient.ReceivedMessages)
            {
                if (eSocketClient.ReceivedMessages.Count > 0)
                {
                    Console.WriteLine(Utility.BytesToXml(eSocketClient.LastMessage).ToString());

                    //RichMessageBox.Show("KNET MACHINE PULSE \n" + DateTime.Now + "\n" + Utility.BytesToXml(client.LastMessage).ToString());


                    // Replace "client.LastMessage" with your actual XML string
                    string xmlString = Utility.BytesToXml(eSocketClient.LastMessage).ToString();
                    //MessageBox.Show(xmlString);
                    try
                    {
                        XDocument doc = XDocument.Parse(xmlString);




                        //MessageBox.Show(doc.ToString());

                        var actionCodezzevent = doc.Descendants(XName.Get("Event", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("EventId"))
                                            .FirstOrDefault();

                        Console.WriteLine($"Event: {actionCodezzevent}");

                        if(actionCodezzevent != null || actionCodezzevent == "") { 

                           // MessageBox.Show(actionCodezzevent);
                        }


                        // Extract ActionCode from the Transaction element
                        var actionCodezz = doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                             .Select(x => (string)x.Attribute("ActionCode"))
                                             .FirstOrDefault();

                        Console.WriteLine($"Action Code: {actionCodezz}");
                        //MessageBox.Show(actionCodezz);
                        // Load the XML
                        //XDocument doc = XDocument.Parse(xml);

                        if(statusofknet == "y") { 

                        if (actionCodezz == "DECLINE") {
                                // MessageBox.Show(actionCodezz);
                            POSTTOBRANCHDONE.Setkt1(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                                .Select(x => (string)x.Attribute("TransactionId"))
                                                .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt2(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                                .Select(x => (string)x.Attribute("TerminalId"))
                                                .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt3(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("ActionCode"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt4(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("AuthorizationNumber"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt5(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("CardProductName"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt6(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("MerchantId"))
                                            .FirstOrDefault());


                            POSTTOBRANCHDONE.Setkn1(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("Type"))
                                        .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkn2(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("RetrievalRefNr"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn3(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("CardNumber"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn4(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("PanEntryMode"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn5(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("EmvAmount"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn6(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("ResponseCode"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn7(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("CardProductName"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn8(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("SignatureRequired"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn9(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("EmvApplicationLabel"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn10(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("EmvApplicationIdentifier"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn11(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("EmvTransactionDate"))
                                        .FirstOrDefault());
                            POSTTOBRANCHDONE.Setkn12(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                        .Select(x => (string)x.Attribute("LocalTime"))
                                        .FirstOrDefault());


                            if(POSTTOBRANCHDONE.kn4 == "02")
                            {
                                POSTTOBRANCHDONE.Setkn4("MGS");
                            }

                            if (POSTTOBRANCHDONE.kn4 == "05")
                            {
                                POSTTOBRANCHDONE.Setkn4("Chip");
                            }

                            if (POSTTOBRANCHDONE.kn4 == "07")
                            {
                                POSTTOBRANCHDONE.Setkn4("NFC");
                            }


                                if (POSTTOBRANCHDONE.kn6 == "00")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Approved");
                                }
                                else if (POSTTOBRANCHDONE.kn6 == "91")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Time out");
                                }
                                else if (POSTTOBRANCHDONE.kn6 == "05")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Declined");
                                }
                                else if (POSTTOBRANCHDONE.kn6 == "55")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Incorrect PIN");
                                }
                                else
                                {
                                    POSTTOBRANCHDONE.Setkn6("Declined");
                                }


                                statusofknet = "n";
                            RichMessageBox.Show("DECLINE KNET MACHINE PULSE \n" + DateTime.Now + "\n" + Utility.BytesToXml(eSocketClient.LastMessage).ToString());

                            validatetransation();


                            //wThankyou mainpage = new wThankyou();
                            //NavigationService.Navigate(mainpage);
                        }

                        if (actionCodezz == "APPROVE")
                        {
                            POSTTOBRANCHDONE.Setkt1(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                             .Select(x => (string)x.Attribute("TransactionId"))
                                             .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt2(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                                .Select(x => (string)x.Attribute("TerminalId"))
                                                .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt3(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("ActionCode"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt4(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("AuthorizationNumber"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt5(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("CardProductName"))
                                            .FirstOrDefault());

                            POSTTOBRANCHDONE.Setkt6(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("MerchantId"))
                                            .FirstOrDefault());


                                POSTTOBRANCHDONE.Setkn1(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("Type"))
                                            .FirstOrDefault());

                                POSTTOBRANCHDONE.Setkn2(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("RetrievalRefNr"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn3(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("CardNumber"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn4(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("PanEntryMode"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn5(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("EmvAmount"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn6(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("ResponseCode"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn7(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("CardProductName"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn8(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("SignatureRequired"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn9(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("EmvApplicationLabel"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn10(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("EmvApplicationIdentifier"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn11(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("EmvTransactionDate"))
                                            .FirstOrDefault());
                                POSTTOBRANCHDONE.Setkn12(doc.Descendants(XName.Get("Transaction", "http://www.mosaicsoftware.com/Postilion/eSocket.POS/"))
                                            .Select(x => (string)x.Attribute("LocalTime"))
                                            .FirstOrDefault());

                                if (POSTTOBRANCHDONE.kn4 == "02")
                                {
                                    POSTTOBRANCHDONE.Setkn4("MGS");
                                }

                                if (POSTTOBRANCHDONE.kn4 == "05")
                                {
                                    POSTTOBRANCHDONE.Setkn4("Chip");
                                }

                                if (POSTTOBRANCHDONE.kn4 == "07")
                                {
                                    POSTTOBRANCHDONE.Setkn4("NFC");
                                }


                                if (POSTTOBRANCHDONE.kn6 == "00")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Approved");
                                } else if (POSTTOBRANCHDONE.kn6 == "91")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Time out");
                                }
                                 else if (POSTTOBRANCHDONE.kn6 == "05")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Declined");
                                } else if (POSTTOBRANCHDONE.kn6 == "55")
                                {
                                    POSTTOBRANCHDONE.Setkn6("Incorrect PIN");
                                } else
                                {
                                    POSTTOBRANCHDONE.Setkn6("Declined");
                                }


                                //    MessageBox.Show(actionCodezz);
                                statusofknet = "n";
                            RichMessageBox.Show("APPROVE KNET MACHINE PULSE \n" + DateTime.Now + "\n" + Utility.BytesToXml(eSocketClient.LastMessage).ToString());
                            validatetransation();
                            }
                        }

                        // Define the namespace
                        // XNamespace ns = "http://www.mosaicsoftware.com/Postilion/eSocket.POS/";

                        // Extract the ActionCode value
                        // string actionCodez = doc.Descendants(ns + "Transaction")
                        //                        .FirstOrDefault()?
                        //                        .Attribute("ActionCode")?.Value;


                        // if(actionCodez == "DECLINE") { }
                        // Output the ActionCode
                        // Console.WriteLine("ActionCode: " + actionCodez);



                        //XElement interfaceElement = doc.Element("Interface"); // Access by local name
                        //XElement transactionElement = interfaceElement.Element("Transaction");
                        //XElement transactionElement = doc.Element("Esp:Interface").Element("Esp:Transaction");
                        //XElement interfaceElement = doc.Element("Esp:Interface");
                        //MessageBox.Show(interfaceElement.ToString());
                        //// Check for "Esp:Transaction" element (ignore other types)
                        //XElement transactionElement = interfaceElement.Element("Esp:Transaction");
                        //MessageBox.Show(transactionElement.ToString());
                        //if (transactionElement != null)
                        //{
                        //    string actionCode = transactionElement.Attribute("ActionCode").Value;
                        //    MessageBox.Show(actionCode.ToString());
                        //    if (actionCode == "APPROVE")
                        //    {
                        //        MessageBox.Show("The transaction was approved.", "Transaction Result");
                        //    }

                        //    else if (actionCode == "DECLINE")
                        //    {
                        //        MessageBox.Show("The transaction was declined.", "Transaction Result");
                        //    }
                        //    else
                        //    {

                        //        MessageBox.Show("Restart KNET Machine");
                        //        // Handle other action codes (optional)
                        //    }
                        //}

                        //string actionCode = transactionElement.Attribute("ActionCode").Value;

                        //if (actionCode == "APPROVE")
                        //{
                        //    MessageBox.Show("The transaction was approved.", "Transaction Result");
                        //}

                        //if (actionCode == "DECLINE")
                        //{
                        //    MessageBox.Show("The transaction was declined.", "Transaction Result");
                        //}
                        //else
                        //{

                        //    MessageBox.Show("Restart KNET Machine");
                        //    // Handle other action codes (optional)
                        //}
                    }
                    catch (Exception ex)
                    {
                        RichMessageBox.Show($"ShowLastResponseCustom-Error parsing XML: {ex.Message}\n {JsonConvert.SerializeObject(ex)}");
                    }
                }
                else
                {
                    RichMessageBox.Show("KNET MACHINE PULSE \n" + DateTime.Now + "\n" + "No messages received, if you were expecting an INIT response, perhaps the device is still initializing?");
                    Console.WriteLine("No messages received, if you were expecting an INIT response, perhaps the device is still initializing?");
                }
            }
        }

        void knetMain(string[] args)
        {
            //turning this to true will display a lot of information that might hide the command menu
            Utility.DEBUG_MODE = false;

            eSocketClient = new eSocketClient();
            eSocketClient.StartReading();


            //User interaction
            while (true)
            {
                Thread.Sleep(250);
                showPrompt();
                int input;
                while (true)
                {
                    try
                    {
                        input = Int32.Parse(Console.ReadLine());
                        if (input < 1 || input > 5)
                        {
                            Console.WriteLine("Please choose a number between 1 and 5!");
                            continue;
                        }
                        break;
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Please input a number!");
                    }
                }

                switch (input)
                {
                    case 1:
                        Init();
                        break;
                    case 2:
                        RequestPayment();
                        break;
                    case 3:
                        CloseTerminal();
                        break;
                    case 4:
                        ShowAllResponses();
                        break;
                    case 5:
                        ShowLastResponse();
                        break;
                    default:
                        break;
                }

            }
        }
        //KNET STUFF


        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            if (timer?.IsEnabled == true)
            {
                timer.Stop();
                timer.Tick -= Timer_Tick;
            }
            timer = null;
            eSocketClient?.Dispose();
        }
    }
}
