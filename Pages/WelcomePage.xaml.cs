using Exchange.Common;
using Exchange.Managers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PACI.MobileId.IntegrationServices.Client;
using PACICardLibrary;
using System.Net.Http;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.Windows.Threading;


namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : Page
    {
        PACMAN test;
        DispatcherTimer LiveTime = new DispatcherTimer();
        string golang = "0";
        DisposableTimer QR_Timer;
        public WelcomePage()
        {
            InitializeComponent();
            QRCodePACIdata.Setisvalidqrcode("no");
            QRscantextbox.Focus();
            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                insertcivilidlbl.Text = "أدخل رقم الهوية المدنية";
                orlbl.Text = "أو";
                welcomelbl.Content = "مرحباً";
                scanmid.Text = "مسح رمز الاستجابة السريعة";
            }


            //MessageBox.Show(golang);
            //if (golang == "0")
            //{
            //LiveTime.Stop();

            LiveTime.Interval = TimeSpan.FromSeconds(3);
            LiveTime.Tick += timer_Tick;

            //if (golang != "0"){
            //    LiveTime.Stop();
            //}
                //}
                //LiveTime.Start();

                //if (golang == "0")
                //{
                //LiveTime.Tick += timer_Tick;
            LiveTime.Start();
                
           // } else
            //{
             //   LiveTime.Stop();
            //}

            pacionload();
            //pacionclick();

            // Subscribe to Unloaded event
            this.Unloaded += WelcomePage_Unloaded;
        }

        // Unloaded event handler to stop the timer
        private void WelcomePage_Unloaded(object sender, RoutedEventArgs e)
        {
            // Stop the DispatcherTimer when the page is closed or unloaded
            if (LiveTime.IsEnabled)
            {
                LiveTime.Stop();
                LiveTime = null;
            }
            QR_Timer?.Cancel();
        }


        private void Paci_ReadersEvent()
        {
            Console.WriteLine("Reader inserted or removed");
            //this.BeginInvoke((MethodInvoker)delegate () { listReaders(); });
            Dispatcher.BeginInvoke(new Action(() => listReaders()));
        }

        private void Paci_DisconnectionEvent(int ReaderIndex)
        {
            Console.WriteLine("Card in reader index: " + ReaderIndex + " is disconnected");
            //this.BeginInvoke((MethodInvoker)delegate () { ClearFields(); });
            Dispatcher.BeginInvoke(new Action(() => ClearFields()));
        }

        private void Paci_ConnectionEvent(int ReaderIndex)
        {
            Console.WriteLine("Card in reader index: " + ReaderIndex + " is connected");
        }


        public void MyCardRemovalHandler(int ReaderIndex)
        {
            Console.WriteLine("A card was removed from reader with index: " + ReaderIndex + " at time:" + DateTime.Now.ToString("h:mm:ss tt"));
        }

        private void ClearFields()
        {
            try
            {
                //textBox1.Text = "";
                //textBox2.Text = "";
                //textBox3.Text = "";
                //textBox4.Text = "";
                //textBox5.Text = "";
                //textBox6.Text = "";
                //textBox7.Text = "";
                //textBox8.Text = "";
                //textBox9.Text = "";
                //textBox10.Text = "";
                //textBox11.Text = "";
                //textBox12.Text = "";
                //textBox13.Text = "";
                //textBox14.Text = "";
                //textBox15.Text = "";
                //textBox16.Text = "";
                //textBox17.Text = "";
                //textBox18.Text = "";
                //textBox19.Text = "";
                //textBox20.Text = "";
                //textBox21.Text = "";
                //textBox22.Text = "";
                //textBox23.Text = "";
                //textBox24.Text = "";
                //textBox26.Text = "";
                //textBox25.Text = "";
                //textBox27.Text = "";
                //textBox28.Text = "";
                //textBox29.Text = "";
                //textBox30.Text = "";
                //pictureBox1.Visible = false;
                //label29.Visible = false;
                //label33.Visible = false;
                //label34.Visible = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void listReaders()
        {
            try
            {
                //comboBox1.Items.Clear();
                //comboBox1.Text = "";
                //string[] readers = test.GetReaders(true);
                //if (readers != null && readers.Length != 0)
                //{
                //    for (int i = 0; i < readers.Length; i++)
                //    {
                //        comboBox1.Items.Add(readers[i]);
                //    }
                //    comboBox1.SelectedIndex = 0;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void pacionload()
        {
            test = new PACMAN();
            test.CardConnectionEvent += new PACMAN.CardConnectionHandler(Paci_ConnectionEvent);
            test.ReadersEvent += new PACMAN.ReadersStateChanged(Paci_ReadersEvent);
            test.CardDisconnectionEvent += new PACMAN.CardDisconnectionHandler(Paci_DisconnectionEvent);

            //Console.WriteLine("Sample Load calling GetReaders");
            try
            {
                string[] readers = test.GetReaders(true);

                if (readers != null)
                {
                    for (int i = 0; i < readers.Length; i++)
                    {
                        //  comboBox1.Items.Add(readers[i]);
                       // MessageBox.Show(readers[i] +" "+ i);
                    }
                    //  comboBox1.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void pacionclick()
        {
            
            try
            {
                // button1.IsEnabled = false;
                // if (comboBox1.Items.Count == 0)
                {
                    //  MessageBox.Show("No connected Reader");
                    //  return;
                }
                ClearFields();
                //int index = comboBox1.SelectedIndex;
                int index = 0;
                //int index = 2;
                //MessageBox.Show("" + index);

                // civilid.Text = test.Civil_ID(index);


                string name = test.English_Name_Full(index);

                // Remove extra spaces using regular expression
                //name = Regex.Replace(name, @"\s+", " ");

                // Split into parts
                //string[] parts = name.Split(' ');

                // Construct the full name
                string firstName = "";
                string middleName = "";
                string lastName = "";



                //string name = "Burhan ali a";

                // Remove extra spaces using regular expression
               // name = Regex.Replace(name, @"\s+", " ");

                // Split into parts
                string[] parts = name.Split(' ');
                // MessageBox.Show(" : " + parts.Length);
                // Handle single-word names
                if (parts.Length == 1)
                {
                    firstName = parts[0];
                    //Console.WriteLine("Firstname : " + parts[0]);
                    //Console.WriteLine("Middle Name : N/A");
                    //Console.WriteLine("Last Name : N/A");


                    //MessageBox.Show("Firstname : " + firstName);
                    //MessageBox.Show("Middle Name : ");
                    //MessageBox.Show("Last Name : ");

                }
                else if (parts.Length == 2)
                {
                    firstName = parts[0];
                    lastName = parts[1];
                }
                else
                {
                    // MessageBox.Show(" : " );
                    // Construct the full name
                    firstName = parts[0];
                    middleName = parts[1];
                    lastName = string.Join(" ", parts.Skip(2));

                    // Print the result
                    //Console.WriteLine("Firstname : " + firstName);
                    //Console.WriteLine("Middle Name : " + middleName);
                    //Console.WriteLine("Last Name : " + lastName);
                    //MessageBox.Show("Firstname : " + firstName);
                    //MessageBox.Show("Middle Name : " + middleName);
                    //MessageBox.Show("Last Name : " + lastName);
                }

                // Print the result
                //Console.WriteLine("Firstname : " + firstName);
                //Console.WriteLine("Middle Name : " + middleName);
                //Console.WriteLine("Last Name : " + lastName);

                //MessageBox.Show("Firstname : " + firstName);
                //MessageBox.Show("Middle Name : " + middleName);
                //MessageBox.Show("Last Name : " + lastName);


                //firstnameTextBox.Text = test.English_Name_Full(index);
                //firstnameTextBox.Text = firstName;
                //middlenameTextBox.Text = middleName;
                //lastnameTextBox.Text = lastName;

                //address1TextBox.Text = " " + test.District(index) + " Block" + test.Block_Number(index) + " St." + test.Street_Name(index) + " Bld " + test.Building_Plot_Number(index) + " Unit " + test.Unit_Type(index) + " Unit#" + test.Unit_Number(index) + " Floor" + test.Floor_Number(index);



                //19-08-2024


                string numericDateString = test.Card_Expiry_Date(index);

                // Extract the day, month, and year components
                int year = int.Parse(numericDateString.Substring(0, 4));
                int month = int.Parse(numericDateString.Substring(4, 2));
                int day = int.Parse(numericDateString.Substring(6, 2));

                // Create a DateTime object from the extracted components
                DateTime parsedDate = new DateTime(year, month, day);

                // Format the DateTime into the desired string
                string formattedString = parsedDate.ToString("dd-MM-yyyy");

                //civilidexpiry.Text = formattedString;




                string numericDateString2 = test.Birthdate(index);

                // Extract the day, month, and year components
                int year2 = int.Parse(numericDateString2.Substring(0, 4));
                int month2 = int.Parse(numericDateString2.Substring(4, 2));
                int day2 = int.Parse(numericDateString2.Substring(6, 2));

                // Create a DateTime object from the extracted components
                DateTime parsedDate2 = new DateTime(year2, month2, day2);

                // Format the DateTime into the desired string
                string formattedString2 = parsedDate2.ToString("dd-MM-yyyy");

                //dob.Text = formattedString2;
                //textBox1.Content = test.Civil_ID(index); - OK
                //textBox2.Content = test.Arabic_Name_Full(index);
                //textBox3.Content = test.English_Name_Full(index); - OK
                //textBox4.Content = test.Sex_Arabic(index);
                //textBox5.Content = test.Sex_English(index);
                //textBox6.Content = test.Nationality_Arabic(index);
                //textBox7.Content = test.Nationality_English(index);
                //textBox8.Content = test.Birthdate(index);
                //textBox9.Content = test.Card_Issue_Date(index);
                //textBox10.Content = test.Card_Expiry_Date(index);
                //textBox11.Content = test.Document_Number(index);
                //textBox12.Content = test.Card_Serial_Number(index);
                //textBox13.Content = test.MOI_Reference(index);

                //textBox14.Content = test.District(index);
                //textBox15.Content = test.Block_Number(index);
                //textBox16.Content = test.Street_Name(index);
                //textBox17.Content = test.Building_Plot_Number(index);
                //textBox18.Content = test.Unit_Type(index);
                //textBox19.Content = test.Unit_Number(index);
                //textBox20.Content = test.Floor_Number(index);

                //textBox21.Content = test.Blood_Type(index);
                //textBox22.Content = test.Guardian_Civil_ID(index);
                //textBox23.Content = test.Telephone_1(index);
                //textBox24.Content = test.E_Mail_Address(index);
                //textBox26.Content = test.Additional_F_1(index);
                //textBox25.Content = test.Additional_F_2(index);
                //textBox27.Content = test.GetATR(index);
                //textBox28.Content = test.Application_Version(index);
                //textBox29.Content = test.MOI_Reference_Indic(index);
                //textBox30.Content = test.Passport(index);

                string fullpacidetails = "";
                fullpacidetails += "Civil_ID : " + test.Civil_ID(index) + ", ";
                fullpacidetails += "Arabic_Name_Full : " + test.Arabic_Name_Full(index) + ", ";
                fullpacidetails += "English_Name_Full : " + test.English_Name_Full(index) + ", ";
                fullpacidetails += "Sex_Arabic : " + test.Sex_Arabic(index) + ", ";
                fullpacidetails += "Sex_English : " + test.Sex_English(index) + ", ";

                string genderva = test.Sex_English(index);
                //gender.Text = "Female";
                if (genderva == "M")
                {
                    //genderv = "M";
                   // gender.Text = "Male";
                }
                else
                {
                    //genderv = "F";
                  //  gender.Text = "Female";
                }
                fullpacidetails += "Nationality_Arabic : " + test.Nationality_Arabic(index) + ", ";
                fullpacidetails += "Nationality_English : " + test.Nationality_English(index) + ", ";

                string nati = test.Nationality_English(index);
                //MessageBox.Show(nati);
                //MessageBox.Show(nati);
              //  SelectNationalityByCode(FindCountryCode(nati));

                fullpacidetails += "Birthdate : " + test.Birthdate(index) + ", ";
                fullpacidetails += "Card_Issue_Date : " + test.Card_Issue_Date(index) + ", ";
                fullpacidetails += "Card_Expiry_Date : " + test.Card_Expiry_Date(index) + ", ";
                fullpacidetails += "Document_Number : " + test.Document_Number(index) + ", ";
                fullpacidetails += "Card_Serial_Number : " + test.Card_Serial_Number(index) + ", ";
                fullpacidetails += "MOI_Reference : " + test.MOI_Reference(index) + ", ";
                fullpacidetails += "District : " + test.District(index) + ", ";
                fullpacidetails += "Block_Number : " + test.Block_Number(index) + ", ";
                fullpacidetails += "Street_Name : " + test.Street_Name(index) + ", ";
                fullpacidetails += "Building_Plot_Number : " + test.Building_Plot_Number(index) + ", ";
                fullpacidetails += "Unit_Type : " + test.Unit_Type(index) + ", ";
                fullpacidetails += "Unit_Number : " + test.Unit_Number(index) + ", ";
                fullpacidetails += "Floor_Number : " + test.Floor_Number(index) + ", ";
                fullpacidetails += "Blood_Type : " + test.Blood_Type(index) + ", ";
                fullpacidetails += "Guardian_Civil_ID : " + test.Guardian_Civil_ID(index) + ", ";
                fullpacidetails += "Telephone_1 : " + test.Telephone_1(index) + ", ";
                fullpacidetails += "E_Mail_Address : " + test.E_Mail_Address(index) + ", ";
                fullpacidetails += "Additional_F_1 : " + test.Additional_F_1(index) + ", ";
                fullpacidetails += "Additional_F_2 : " + test.Additional_F_2(index) + ", ";
                fullpacidetails += "GetATR : " + test.GetATR(index) + ", ";
                fullpacidetails += "Application_Version : " + test.Application_Version(index) + ", ";
                fullpacidetails += "MOI_Reference_Indic : " + test.MOI_Reference_Indic(index) + ", ";
                fullpacidetails += "Passport : " + test.Passport(index) + ". ";

                RichMessageBox.Show("Civil ID Details : \n" + DateTime.Now + "\n" + fullpacidetails);

                //label29.Visible = true;
                PACICardProperties properties = test.GetCardProperties(index);
                bool cardCertValidity = false;
                try
                {
                    cardCertValidity = test.ValidateCardCertificateWithOCSP(index);
                }
                catch (Exception)
                {
                    //There is something wrong with OCSP server so we try last resort CRL validation
                    cardCertValidity = test.ValidateCardCertificateWithCRL(index, false);
                }
                if (cardCertValidity)
                {
                    //label33.Content = "Card is valid";
                    //label33.ForeColor = Color.DarkGreen;
                    //label33.Visible = true;
                }
                else
                {
                    //label33.Content = "Card is not valid";
                    //label33.ForeColor = Color.DarkRed;
                    //label33.Visible = true;
                }
                if (properties.IsDigitalSignatureAvailable)
                {
                    if (properties.IsCardLocked)
                    {
                        //label34.Content = "Digital signature is locked";
                        //label34.ForeColor = Color.DarkRed;
                        //label34.Visible = true;
                    }
                    else
                    {
                        bool digitalCertValidity = false;
                        try
                        {
                            digitalCertValidity = test.ValidateDigitalSignatureCertificateWithOCSP(index);
                        }
                        catch (Exception)
                        {
                            //There is something wrong with OCSP server so we try last resort CRL validation
                            digitalCertValidity = test.ValidateDigitalSignatureCertificateWithCRL(index, false);
                        }
                        if (digitalCertValidity)
                        {
                            // label34.Content = "Digital certificate is valid";
                            //label34.ForeColor = Color.DarkGreen;
                            //label34.Visible = true;
                        }
                        else
                        {
                            // label34.Content = "Digital certificate is not valid";
                            //label34.ForeColor = Color.DarkRed;
                            //label34.Visible = true;
                        }
                    }
                }
                else
                {
                    //  label34.Content = "No digital signature is available";
                    //label34.ForeColor = Color.DarkRed;
                    //label34.Visible = true;
                }
                golang = "1";
            }
            catch (Exception e1)
            {
                //Disable / Enable this for Testing
                //golang = "2";

                //DEV ENVIROMENT
                //MessageBox.Show("PACI Card Reader Not Connected ");
                //MessageBox.Show("PACI Card Reader Not Connected " + e1.Message);
            }
            finally
            {//
             // button1.IsEnabled = true;

                //Disable / Enable this for Testing
                //if (golang == "2")
                if (golang == "1")
                {
                    // MessageBox.Show("Go Signal");
                    wRegister wmainpage = new wRegister();
                    NavigationService.Navigate(wmainpage);
                }
            }

            
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (golang != "0")
            {
                LiveTime.Stop();
                return;
            }
           // MessageBox.Show(golang);
            //LiveTimeLabel.Content = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            //MessageBox.Show("Hi");
            pacionclick();
            //MessageBox.Show(golang);


        }


        //register_Click
        private void register_Click(object sender, RoutedEventArgs e)
        {
            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            wRegister wmainpage = new wRegister();
            NavigationService.Navigate(wmainpage);

        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            MessageBox.Show(TokenManager.Token);
            //MessageBox.Show(LoadToken());

            NavigationManager.NavigateToHome();
        }

        private static Dictionary<string, byte[]> EncryptedTokens = new Dictionary<string, byte[]>();
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

        private void backbutton(object sender, RoutedEventArgs e)
        {
            NavigationManager.NavigateToHome();
        }

        private void qrbtn_Click(object sender, RoutedEventArgs e)
        {

            // Create an instance of your Windows Forms form
            //var winForm = new qrcodeint.Form1();

            // Enable keyboard input in the WinForms control
            //ElementHost.EnableModelessKeyboardInterop(winForm);

            // Show the form
            //winForm.test4();




            // MessageBox.Show("Hi");
            try
            {
               //  test4();
                
                //MessageBox.Show("" + );
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        //Authenticate using Push Notification
        public string test4()
        {
            X509Certificate2 cert = new X509Certificate2();
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            var certificate2Collection = x509Store.Certificates
                .Find(X509FindType.FindByThumbprint, "07C8705C099003552A9012A6B44627A931727756", false);
            if (certificate2Collection == null || certificate2Collection.Count == 0 ||
            certificate2Collection.Count > 1)
            {
                throw new Exception("Certificate not found");
            }
            cert = certificate2Collection[0];

            var authClient = new MIDAuthServiceClient(cert, "mid-auth-p.paci.gov.kw", "5869");
            //var authClient = new MIDAuthServiceClient(cert, "mid-auth-p.paci.gov.kw", "5869");

            //var res = authClient.InitiateAuthRequestPN(new
            //MIDAuthSignContract.Entities.AuthenticateRequest
            //{
            //    ServiceProviderId = "aa513452-fa95-47cc-9977-d5b60e0fa420",
            //    ServiceDescriptionEN = "TEST EN",
            //    ServiceDescriptionAR = "TEST AR",
            //    AuthenticationReasonEn = "TEST RES EN",
            //    AuthenticationReasonAr = "TEST RES AR",
            //    RequestUserDetails = true,
            //    SPCallbackURL = "https://api.wallstreetkwt.com/auth/AuthenticateUser",
            //    PersonCivilNo = "292120200792"
            //});

            //MessageBox.Show("Response : " + res.Data);
            //return res.Data;

            return "";
        }

        private void QRscantextbox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                // Handle the Enter keypress here
                string scannedText = QRscantextbox.Text;
                // Process the scanned text as needed
                // ...
                //MessageBox.Show(scannedText);
                qrcodemid(scannedText);
                QRscantextbox.Text = "";
                //MessageBox.Show(scannedText);
                e.Handled = true;
            }
        }

        private void QRscantextbox_Loaded(object sender, RoutedEventArgs e)
        {
            QRscantextbox.Focus();
        }

        private void QRscantextbox_LostFocus(object sender, RoutedEventArgs e)
        {

            QR_Timer?.Cancel();
            QR_Timer = new DisposableTimer(() => QRscantextbox.Focus(), 1);
        }

        public async void qrcodemid(string qrscan)
        {
            if (string.IsNullOrWhiteSpace(qrscan))
            {
                return;
            }

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.65:9999/WeatherForecast");
            var content = new StringContent("{\r\n    \"parameter1\": \"" + qrscan + "\",\r\n    \"parameter2\": \"b\"\r\n}", null, "application/json");
            request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());
            //return;
            try
            {
                using var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();


                var jsonString = await response.Content.ReadAsStringAsync();


                dynamic jsonObject = JsonConvert.DeserializeObject(jsonString);

                if (jsonObject is JArray jsonArray && jsonArray.Count > 0)
                {
                    dynamic firstObject = jsonArray[0];

                    if (firstObject != null)
                    {
                        QRCodePACIdata.Setisvalidqrcode("yes");
                        foreach (var property in firstObject)
                        {
                            if (property != null && property.Name != null && property.Value != null)
                            {
                                string propertyName = property.Name;
                                string propertyValue = property.Value;

                                //MessageBox.Show($"{propertyName}: {propertyValue}");

                                if(propertyName == "fullNameEn")
                                {
                                    QRCodePACIdata.SetfullNameEn(propertyValue);
                                }
                                if (propertyName == "fullNameAr")
                                {
                                    QRCodePACIdata.SetfullNameAr(propertyValue);
                                }
                                if (propertyName == "address")
                                {
                                    QRCodePACIdata.Setaddress(propertyValue);
                                }
                                if (propertyName == "birthDate")
                                {
                                    QRCodePACIdata.Setbirthdate(propertyValue);
                                }
                                if (propertyName == "bloodGroup")
                                {
                                    QRCodePACIdata.SetbloodGroup(propertyValue);
                                }
                                if (propertyName == "cardExpiryDate")
                                {
                                    QRCodePACIdata.SetcardExpiryDate(propertyValue);
                                }
                                if (propertyName == "civilID")
                                {
                                    QRCodePACIdata.SetcivilID(propertyValue);
                                }
                                if (propertyName == "emailAddress")
                                {
                                    QRCodePACIdata.SetemailAddress(propertyValue);
                                }
                                if (propertyName == "gender")
                                {
                                    QRCodePACIdata.Setgender(propertyValue);
                                }
                                if (propertyName == "govData")
                                {
                                    QRCodePACIdata.SetgovData(propertyValue);
                                }
                                if (propertyName == "mobileNumber")
                                {
                                    QRCodePACIdata.SetmobileNumber(propertyValue);
                                }
                                if (propertyName == "nationalityAr")
                                {
                                    QRCodePACIdata.SetnationalityAr(propertyValue);
                                }
                                if (propertyName == "nationalityEn")
                                {
                                    QRCodePACIdata.SetnationalityEn(propertyValue);
                                }
                                if (propertyName == "nationalityFlag")
                                {
                                    QRCodePACIdata.SetnationalityFlag(propertyValue);
                                }
                                if (propertyName == "passportNumber")
                                {
                                    QRCodePACIdata.SetpassportNumber(propertyValue);
                                }


                            }
                        }
                        wRegister wmainpage = new wRegister();
                        NavigationService.Navigate(wmainpage);
                    }
                    else
                    {
                        MessageBox.Show("(EC1) PACI QR , Try Again ");
                        // MessageBox.Show("The first object in the array is null.");
                    }
                }
                else
                {
                    MessageBox.Show("(EC2)Invalid PACI QR , Try Again");
                    //MessageBox.Show("The response was not an array or the array is empty.");
                }
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show("(EC3) PACI QR , Try Again" + ex);
                //MessageBox.Show($"An error occurred during the HTTP request: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show("(EC4) PACI QR , Try Again" + ex);
                //MessageBox.Show($"An error occurred: {ex.Message}");
            }

        }        
    }
}
