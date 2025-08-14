using ExcelDataReader;
using Exchange.Common;
using Exchange.Managers;
using PACICardLibrary;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
//using static Exchange.Pages.waddbeneficiary;
using static Exchange.Pages.wtobankorcash;
//using DocumentFormat.OpenXml.Drawing.Charts;
//using DocumentFormat.OpenXml.Drawing.Charts;

//using ExcelDataReader;
//using OfficeOpenXml;
//using OfficeOpenXml.Core.ExcelPackage;
//using OfficeOpenXml.Core.ExcelPackage;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wRegister.xaml
    /// </summary>
    public partial class wRegister : Page
    {
        PACMAN test;

        //string civilidfrontbase64 = "a";
        //string civilidbackbase64 = "a";
        //string signbase64 = "a";

        string civilidfrontbase64 = "";
        string civilidbackbase64 = "";
        string signbase64 = "";

        private BackgroundWorker backgroundWorker;

        public wRegister()
        {
            InitializeComponent();




            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                registertitle.Text = "تسجيل";
                regibtn.Content = "تسجيل";
                usernamelbl.Content = "اسم المستخدم";
                passwordlbl.Content = "كلمه السر/ المرور";
                firstnamelbl.Content = "الاسم الاول";
                middlelbl.Content = "الاسم الاوسط";
                lastnlbl.Content = "الاسم الاخير ";
                add1lbl.Content = "العنوان الاول";
                add2lbl.Content = "العنوان الثانى";
                gendlbl.Content = "جنس";
                nationlbl.Content = "الجنسية";
                emaillbl.Content = "البريد الإلكتروني";
                moblbl.Content = "الهاتف ";
                civilidlbl.Content = "الهوية المدنية";
                civilidexplbl.Content = "تاريخ انتهاء الصلاحية (يوم/ شهر / سنة)";
                doblbl.Content = "تاريخ الميلاد (يوم / شهر / سنة)";
                salarylbl.Content = "الراتب";
                employerlbl.Content = "صاحب العمل";
                designatlbl.Content = "المسمى الوظيفى";



            }

            InitData();
        }

        private void InitData()
        {
            if (QRCodePACIdata.isvalidqrcode != "yes")
                return;
            try
            {
                runtheloaderfornationalitycountries();
                civilid.Text = QRCodePACIdata.civilID;


                string name = QRCodePACIdata.fullNameEn;

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
                name = Regex.Replace(name, @"\s+", " ");

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
                firstnameTextBox.Text = firstName;
                middlenameTextBox.Text = middleName;
                lastnameTextBox.Text = lastName;
                // address1TextBox.Text = " " + test.District(index) + " Block" + test.Block_Number(index) + " St." + test.Street_Name(index) + " Bld " + test.Building_Plot_Number(index) + " Unit " + test.Unit_Type(index) + " Unit#" + test.Unit_Number(index) + " Floor" + test.Floor_Number(index);



                //19-08-2024


                string numericDateString = QRCodePACIdata.cardExpiryDate;

                // Extract the day, month, and year components
                //int year = int.Parse(numericDateString.Substring(0, 4));
                //int month = int.Parse(numericDateString.Substring(4, 2));
                //int day = int.Parse(numericDateString.Substring(6, 2));

                //// Create a DateTime object from the extracted components
                //DateTime parsedDate = new DateTime(year, month, day);

                //// Format the DateTime into the desired string
                //string formattedString = parsedDate.ToString("dd-MM-yyyy");
                //civilidexpiry.Text = formattedString;

                //string inputDate = numericDateString;

                // Parse the input string into a DateTime object
                //DateTime dateTime = DateTime.ParseExact(inputDate, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                // Format the DateTime object into the desired format
                //string formattedDate = dateTime.ToString("dd-MM-yyyy");

                string originalDate = numericDateString;

                // Parse the date string
                DateTime parsedDate = DateTime.ParseExact(originalDate, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                // Format to "dd-MM-yyyy"
                string formattedDate = parsedDate.ToString("dd-MM-yyyy");
                //MessageBox.Show(formattedDate);


                civilidexpiry.Text = formattedDate;


                string numericDateString2 = QRCodePACIdata.birthdate;


                string inputDate2 = numericDateString2;

                // Parse the input string into a DateTime object
                //DateTime dateTime2 = DateTime.ParseExact(inputDate2, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);
                DateTime dateTime2 = DateTime.ParseExact(inputDate2, "M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);

                // Format the DateTime object into the desired format
                string formattedDate2 = dateTime2.ToString("dd-MM-yyyy");

                //// Extract the day, month, and year components
                //int year2 = int.Parse(numericDateString2.Substring(0, 4));
                //int month2 = int.Parse(numericDateString2.Substring(4, 2));
                //int day2 = int.Parse(numericDateString2.Substring(6, 2));

                //// Create a DateTime object from the extracted components
                //DateTime parsedDate2 = new DateTime(year2, month2, day2);

                //// Format the DateTime into the desired string
                //string formattedString2 = parsedDate2.ToString("dd-MM-yyyy");

                // dob.Text = formattedString2;
                dob.Text = formattedDate2;
                //textBox1.Content = test.Civil_ID(index); - OK
                //textBox2.Content = test.Arabic_Name_Full(index);
                //textBox3.Content = test.English_Name_Full(index); - OK
                //textBox4.Content = test.Sex_Arabic(index);



                string genderva = QRCodePACIdata.gender;
                //gender.Text = "Female";
                if (genderva == "M")
                {
                    //genderv = "M";
                    gender.Text = "Male";
                }
                else
                {
                    //genderv = "F";
                    gender.Text = "Female";
                }

                string nati = QRCodePACIdata.nationalityEn;
                //MessageBox.Show(nati);
                //MessageBox.Show(nati);

                _ = new DisposableTimer(() => SelectNationalityByCode(FindCountryCode(nati)), 2);

                address1TextBox.Text = QRCodePACIdata.address;

                employer.Text = QRCodePACIdata.govData;
            }
            catch (Exception ex)
            {
                RichMessageBox.Show($"Register.Init-Error-{ex.Message}");
            }
        }

        private async void ShowLoadingPopup()
        {
            // Create the loading window
            LoadingScreen loadingWindow = new LoadingScreen();

            // Show the loading window in a non-blocking manner
            loadingWindow.Show();

            // Run your background task asynchronously
            //await Task.Run(() =>
            //{
                // Simulate a background task (e.g., loading data)
                //System.Threading.Thread.Sleep(3000);  // Replace this with your actual task
                pacionload();

                runtheloaderfornationalitycountries();
            //});

            // Close the loading window after the task is done
            loadingWindow.Close();
        }


        private async void ShowLoadingPopupBACKUP()
        {
            // Create the loading window
            LoadingScreen loadingWindow = new LoadingScreen();

            // Show the loading window in a non-blocking manner
            loadingWindow.Show();

            // Run your background task asynchronously
            await Task.Run(() =>
            {
                // Simulate a background task (e.g., loading data)
                System.Threading.Thread.Sleep(3000);  // Replace this with your actual task
            });

            // Close the loading window after the task is done
            loadingWindow.Close();
        }




        public void SelectNationalityByCode(string conCode)
        {
            // Show the initial message with the provided conCode
            //MessageBox.Show("Hi " + conCode + " - Items Count: " + nationality.Items.Count);

            // Loop through each item in the ComboBox
            foreach (var item in nationality.Items)
            {
                // Check if the item is of type NationalityCountryr
                if (item is NationalityCountryr nationalityItem)
                {
                    // Show each ConCode for debugging
                   // MessageBox.Show("Checking: " + nationalityItem.ConCode);

                    // Check if the ConCode matches
                    if (nationalityItem.ConCode.Equals(conCode, StringComparison.OrdinalIgnoreCase))
                    {
                        // Show a message with the found ConCode
                        //MessageBox.Show("Found: " + nationalityItem.ConCode);

                        // Set the selected item in the ComboBox
                        nationality.SelectedItem = nationalityItem;
                        return; // Exit once found
                    }
                }
            }

            MessageBox.Show("No matching nationality found.");
        }

        public string FindCountryCode(string countryAbbreviation)
        {
            string filePath = "country.xlsx"; // Replace with your actual file path

            // Register the encoding provider for reading older Excel files
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    // Use AsDataSet to load the data into a DataSet
                    var result = reader.AsDataSet();

                    // Assuming data is in the first table (first sheet)
                    DataTable table = result.Tables[0];

                    // Iterate through rows to find the matching country code
                    for (int row = 0; row < table.Rows.Count; row++) // Start from 1 if row 0 is header
                    {
                        var cellValue = table.Rows[row][0].ToString(); // Column 1 (e.g., "IN")
                        if (cellValue.Equals(countryAbbreviation, StringComparison.OrdinalIgnoreCase))
                        {
                            return table.Rows[row][1].ToString(); // Return corresponding value from Column 2 (e.g., "IND")
                        }
                    }
                }
            }

            return null; // Return null if not found
        }

        //public string findcountrycode(string countryCode)
        //{
        //    string filePath = "country.xlsx"; // Replace with your actual file path

        //    // Register CodePagesEncodingProvider for broader encoding support
        //    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        //    using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
        //    {
        //        using (var reader = ExcelDataReaderFactory.CreateReader(stream))

        //        {
        //            var dataSet = reader.AsDataSet();
        //            var dataTable = dataSet.Tables[0]; // Assuming the data is in the first sheet

        //            var matchingRow = dataTable.AsEnumerable()
        //                .FirstOrDefault(row => row.Field<string>(0) == countryCode);

        //            if (matchingRow != null)
        //            {
        //                return matchingRow.Field<string>(1);
        //            }
        //            else
        //            {
        //                // Handle case where country code is not found
        //                return null; // Or throw an exception (your preference)
        //            }
        //        }
        //    }
        //}

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
                //MessageBox.Show("" + index);

                civilid.Text = test.Civil_ID(index);


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
                name = Regex.Replace(name, @"\s+", " ");

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
                else if (parts.Length == 2) {
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
                firstnameTextBox.Text = firstName;
                middlenameTextBox.Text = middleName;
                lastnameTextBox.Text = lastName;

                address1TextBox.Text = " " + test.District(index) + " Block" + test.Block_Number(index) + " St." + test.Street_Name(index) + " Bld " + test.Building_Plot_Number(index) + " Unit " + test.Unit_Type(index) + " Unit#" + test.Unit_Number(index) + " Floor" + test.Floor_Number(index);



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

                civilidexpiry.Text = formattedString;



               
                string numericDateString2 = test.Birthdate(index);

                // Extract the day, month, and year components
                int year2 = int.Parse(numericDateString2.Substring(0, 4));
                int month2 = int.Parse(numericDateString2.Substring(4, 2));
                int day2 = int.Parse(numericDateString2.Substring(6, 2));

                // Create a DateTime object from the extracted components
                DateTime parsedDate2 = new DateTime(year2, month2, day2);

                // Format the DateTime into the desired string
                string formattedString2 = parsedDate2.ToString("dd-MM-yyyy");

                dob.Text = formattedString2;
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
                    gender.Text = "Male";
                }
                else
                {
                    //genderv = "F";
                    gender.Text = "Female";
                }
                fullpacidetails += "Nationality_Arabic : " + test.Nationality_Arabic(index) + ", ";
                fullpacidetails += "Nationality_English : " + test.Nationality_English(index) + ", ";

                string nati = test.Nationality_English(index);
                //MessageBox.Show(nati);
                //MessageBox.Show(nati);
                SelectNationalityByCode(FindCountryCode(nati));

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
            }
            catch (Exception e1)
            {
                //MessageBox.Show("PACI Card Reader Not Connected ");
                //MessageBox.Show("PACI Card Reader Not Connected " + e1.Message);
            }
            finally
            {//
               // button1.IsEnabled = true;
            }
        }

        private async void backbutton(object sender, RoutedEventArgs e)
        {



            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            NavigationManager.NavigateToHome();

            //wChooseLang

        }


        public string convertdate(string getdate)
        {
            string result = "";

            //string inputDate = "02-12-2024";

            // Use DateTime.ParseExact for strict format checking
            try
            {
                DateTime parsedDate = DateTime.ParseExact(getdate, "dd-MM-yyyy", CultureInfo.InvariantCulture);
                string formattedDate = parsedDate.ToString("yyyy-MM-dd");
                Console.WriteLine(formattedDate); // Output: 2024-12-02
               
                result = formattedDate;
            }
            catch (FormatException)
            {
               // Console.WriteLine("Invalid date format. Please use DD-MM-YYYY");
            }

            //MessageBox.Show(result);
            return result;
        }


        //public bool IsValidPassword(string password)
        //{
        //    Regex regex = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$");
        //    return regex.IsMatch(password);
        //}


        public bool IsValidPassword(string password)
        {
            if (password.Length < 8)
                return false;

            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;
            bool hasSpecialChar = false;


            foreach (char c in password)
            {
                if (char.IsUpper(c))
                    hasUppercase = true;
                else if (char.IsLower(c))
                    hasLowercase = true;
                else if (char.IsDigit(c))
                    hasDigit
         = true;
                else if (!char.IsLetterOrDigit(c))
                    hasSpecialChar
         = true;
            }

            return hasUppercase && hasLowercase && hasDigit && hasSpecialChar;
        }

        private async void registerbutton(object sender, RoutedEventArgs e)
        {

           // MessageBox.Show("Clicked");
            Button button = sender as Button;
            button.IsEnabled = false;

            //await Task.Delay(3000); // Delay for 3 seconds

            //Dispatcher.Invoke(() =>
            //{
            //    button.IsEnabled = true;
            //});

            //var selectedItemNATIONALITYa = (ComboBoxItem)nationality.SelectedItem;
            //string contentdb = selectedItem.Content.ToString();
            //string valueNATIONALITYa = (string)selectedItemNATIONALITYa.Tag; // Assuming Tag is castable to int

            NationalityCountryr selectedNATIONALITYa = (NationalityCountryr)nationality.SelectedItem;
            string selectedNATIONALITYstrga = selectedNATIONALITYa.ConCode;

            //MessageBox.Show(selectedNATIONALITYstrga);

            string username = usernameTextBox.Text;
            string password = passwordTextBox.Password;
            string firstname = firstnameTextBox.Text;
            string middlename = middlenameTextBox.Text;
            string lastname = lastnameTextBox.Text;
            string address1 = address1TextBox.Text;
            string address2 = address2TextBox.Text;

           
            string genderv = gender.Text;
            //gender.Text = "Female";
            if (genderv == "Male")
            {
                genderv = "M";
            }else
            {
                genderv = "F";
            }
            //MessageBox.Show(genderv);
            string nationalityv = nationality.Text;
            string emailidv = emailid.Text;
            string mobilenov = mobileno.Text;
            string civilidv = civilid.Text;
            string civilidexpiryv = civilidexpiry.Text;
            string dobv = dob.Text;
            string salarya = salary.Text;
            string occupationa = designation.Text;
            string emploerr = employer.Text;

            //VALIDATING IF FIELDS ARE NOT EMPTY
            {


                string filePath = "signature.png";

                RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
                rtb.Render(inkCanvas);

                PngBitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(rtb));

                using (var stream = File.Create(filePath))
                {
                    encoder.Save(stream);
                }

                // Convert to Base64 string
                byte[] imageBytes = File.ReadAllBytes(filePath);
                string base64String = Convert.ToBase64String(imageBytes);

                signbase64 = base64String;

                //civilidfrontbase64 = signbase64;
                //civilidbackbase64 = signbase64;

                if (civilidfrontbase64 == null || civilidfrontbase64 == "")
                {
                    MessageBox.Show("Kindly Scan Front of your Civil ID");
                    button.IsEnabled = true; return;
                }
                if (civilidbackbase64 == null || civilidbackbase64 == "")
                {
                    MessageBox.Show("Kindly Scan Back of your Civil ID");
                    button.IsEnabled = true; return;
                }
                if (username == null || username == "")
                {
                    MessageBox.Show("Username is required.");
                    button.IsEnabled = true; return;
                }


                //MessageBox.Show("" + IsValidPassword(password));
                //MessageBox.Show(password);
                if (IsValidPassword(password))
                {
                    //MessageBox.Show("Valid Password");

                }
                else
                {
                    MessageBox.Show("Password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special symbol");
                    button.IsEnabled = true; return;
                }

                if (password == null || password == "")
                {
                    MessageBox.Show("password is required.");
                    button.IsEnabled = true; return;
                }

                if (password.Length<8)
                {
                    MessageBox.Show("Minimum 8 Length Password Required");
                    button.IsEnabled = true; return;
                }

                if (firstname == null || firstname == "")
                {
                    MessageBox.Show("first name is required.");
                    button.IsEnabled = true; return;
                }

                if (lastname == null || lastname == "")
                {
                    MessageBox.Show("last name is required.");
                    button.IsEnabled = true; return;
                }

                if (address1 == null || address1 == "")
                {
                    MessageBox.Show("address 1 is required.");
                    button.IsEnabled = true; return;
                }

                if (emailidv == null || emailidv == "")
                {
                    MessageBox.Show("email id is required.");
                    button.IsEnabled = true; return;
                }

                if (mobilenov == null || mobilenov == "")
                {
                    MessageBox.Show("mobile no  is required.");
                    button.IsEnabled = true; return;
                }
                if (mobilenov.Length < 8)
                {
                    MessageBox.Show("Minimum 8 Length Mobile No Required");
                    button.IsEnabled = true; return;
                }
                if (civilidv == null || civilidv == "")
                {
                    MessageBox.Show("civil id  is required.");
                    button.IsEnabled = true; return;
                }

                if (civilidv.Length < 12)
                {
                    MessageBox.Show("12 Digit Civil id  is required.");
                    button.IsEnabled = true; return;
                }

                if (civilidexpiryv == null || civilidexpiryv == "")
                {
                    MessageBox.Show("civil id expiry is required.");
                    button.IsEnabled = true; return;
                }

                if (dobv == null || dobv == "")
                {
                    MessageBox.Show("dob is required.");
                    button.IsEnabled = true; return;
                }

                if (salarya == null || salarya == "")
                {
                    MessageBox.Show("Salary is required.");
                    button.IsEnabled = true; return;
                }

                if (emploerr == null || emploerr == "")
                {
                    MessageBox.Show("Employer is required.");
                    button.IsEnabled = true; return;
                }

                if (occupationa == null || occupationa == "")
                {
                    MessageBox.Show("Designation is required.");
                    button.IsEnabled = true; return;
                }
            }

            //VALIDATIONS
            {





                //TO VERIFY IF USERNAME EXISTS OR NOT
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://" + Variable.apiipadd + "/api/v1/sxuser/User/" + username);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                //MessageBox.Show("sxuser/UserControl "+ await response.Content.ReadAsStringAsync());


                var responseBody = await response.Content.ReadAsStringAsync();

                string VERIFYUSERCODE = "";

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody))
                {
                    // Access the root JSON object
                    JsonElement root = doc.RootElement;

                    // Navigate to the 'Data' object
                    //JsonElement dataElement = root.GetProperty("Code");

                    // Extract the accessToken
                    //string accessToken = dataElement.GetProperty("accessToken").GetString();

                    // Extract the accessToken
                    //VERIFYUSERCODE = root.GetProperty("Code").GetString();
                    //VERIFYUSERCODE = root.GetProperty("Code").GetString();

                    //MessageBox.Show(responseBody);

                    VERIFYUSERCODE = root.GetProperty("Code").ToString();

                    var message = root.GetProperty("Data").ToString();

                    // MessageBox.Show(VERIFYUSERCODE + message);

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Message: {Message}");

                    //MessageBox.Show(OTP);

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Message: {Message}");

                    if (VERIFYUSERCODE == "99")
                    {

                        MessageBox.Show("API Error " + message + responseBody);
                        button.IsEnabled = true; return;
                    }

                    if (VERIFYUSERCODE == "2")
                    {
                        MessageBox.Show("User Already Exists");
                        button.IsEnabled = true; return;
                    }

                    if (VERIFYUSERCODE == "0")
                    {
                        // MessageBox.Show("Can Proceed.");
                    } else
                    {
                        MessageBox.Show(message);
                        button.IsEnabled = true; return;
                    }

                    //MessageBox.Show(VERIFYUSERCODE);



                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Access Token: {accessToken}");
                    // RemoveToken(accessToken);
                    // SaveToken(accessToken);
                    //TokenManager.SetToken(accessToken);
                    // MessageBox.Show(LoadToken());

                }


                //TO VERIFY IF CIVIL ID EXISTS OR NOT
                var client2 = new HttpClient();
                var request2 = new HttpRequestMessage(HttpMethod.Get, "http://" + Variable.apiipadd + "/api/v1/sxuser/IDType/" + civilidv + "/CI");
                request2.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                var content = new StringContent("", null, "text/plain");
                request2.Content = content;
                var response2 = await client2.SendAsync(request2);
                response2.EnsureSuccessStatusCode();
                Console.WriteLine(await response2.Content.ReadAsStringAsync());
                //MessageBox.Show("/sxuser/IDType/ " + await response2.Content.ReadAsStringAsync());


                var responseBody2 = await response2.Content.ReadAsStringAsync();

                string VERIFYCIVILCODE = "";

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody2))
                {
                    // Access the root JSON object
                    JsonElement root = doc.RootElement;

                    VERIFYCIVILCODE = root.GetProperty("Code").ToString();
                    var message = root.GetProperty("Data").ToString();

                    //MessageBox.Show(message);

                    if (VERIFYCIVILCODE == "4")
                    {

                        //MessageBox.Show("API Error" + responseBody2);
                        //MessageBox.Show(message);
                        //button.IsEnabled = true;
                        //return;
                    }

                    if (VERIFYCIVILCODE == "99")
                    {

                        MessageBox.Show(message);
                        button.IsEnabled = true; return;
                    }

                    if (VERIFYCIVILCODE == "2")
                    {
                        MessageBox.Show("User Already Exists");
                        button.IsEnabled = true; return;
                    }

                    if (VERIFYCIVILCODE == "0")
                    {
                        // MessageBox.Show("Can Proceed.");
                    }

                    //MessageBox.Show(VERIFYCIVILCODE);
                    //button.IsEnabled = true; return;


                }


                string existingremid = "";
                if (VERIFYCIVILCODE == "4")
                {
                    var clientcs = new HttpClient();
                    var requestcs = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/sxRemitter/Remitter/CheckStatus");
                    requestcs.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                    var contentcs = new StringContent(" {\n     \"appID\":10018,\n     \"moduleID\":1,\n     \"channelCode\":\"kiosk\",\n     \n     \"mobileNo\":\""+ mobilenov + "\",\n     \"identityType\":\"CI\",\n     \"identityNo\":\""+ civilidv + "\",\n     \"emailID\":\""+ emailidv + "\",\n     \"category\":\"I\"\n }", null, "application/json");
                    requestcs.Content = contentcs;
                    var responsecs = await clientcs.SendAsync(requestcs);
                    responsecs.EnsureSuccessStatusCode();
                    Console.WriteLine(await responsecs.Content.ReadAsStringAsync());


                    string VERIFYCIVILCODEcs = "";

                    var responseBody2cs = await responsecs.Content.ReadAsStringAsync();
                    using (JsonDocument doc = JsonDocument.Parse(responseBody2cs))
                    {



                        // Access the root JSON object
                        JsonElement root = doc.RootElement;
                        VERIFYCIVILCODEcs = root.GetProperty("Code").ToString();
                        JsonElement dataArray = root.GetProperty("Data");
                        //MessageBox.Show(VERIFYCIVILCODEcs );
                        // Loop through each element in the data array

                        if(VERIFYCIVILCODEcs == "0") { 
                            foreach (JsonElement dataElement in dataArray.EnumerateArray())
                            {


                                // Check if the element has a property named "REM_ID"
                                if (dataElement.TryGetProperty("REM_ID", out JsonElement remIdElement))
                                {
                                    //REM_ID = remIdElement.ToString();
                                    existingremid = remIdElement.ToString();
                                    //MessageBox.Show(existingremid);
                                    break; // Stop after finding the first REM_ID (optional)
                                }
                            }
                        } else
                        {
                            MessageBox.Show("" + root.GetProperty("Message").ToString());
                            button.IsEnabled = true;
                            return;
                        }
                    }




                }
                //MessageBox.Show(existingremid);
                //return;


                //GENERATE OTP VIA API 
                var client3 = new HttpClient();
                var request3 = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/Auth/request-otp");
                request3.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                var content3 = new StringContent("{\n    \"userID\":0,\n    \"channel\":\"kiosk\",\n    \"moduleID\":1,\n    \"username\":\"" + username + "\",\n    \"Mobile\":\"" + mobilenov + "\",\n    \"email\":\"" + emailidv + "\"\n}", null, "application/json");
                request3.Content = content3;
                var response3 = await client3.SendAsync(request3);
                response3.EnsureSuccessStatusCode();
                Console.WriteLine(await response3.Content.ReadAsStringAsync());
                //MessageBox.Show(await response3.Content.ReadAsStringAsync());


                var responseBody3 = await response3.Content.ReadAsStringAsync();

                string OTP = "";

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody3))
                {
                    // Access the root JSON object
                    JsonElement root = doc.RootElement;

                    // Navigate to the 'Data' object
                    //JsonElement dataElement = root.GetProperty("Data");

                    // Extract the accessToken
                    //string accessToken = dataElement.GetProperty("accessToken").GetString();

                    // Extract the accessToken
                    OTP = root.GetProperty("Data").GetString();

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Message: {Message}");

                    //MessageBox.Show(OTP);
                    //button.IsEnabled = true; return;

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Access Token: {accessToken}");
                    // RemoveToken(accessToken);
                    // SaveToken(accessToken);
                    //TokenManager.SetToken(accessToken);
                    // MessageBox.Show(LoadToken());

                }


                //VERIFY OTP VIA API 
                var client4 = new HttpClient();
                var request4 = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/sxgeneral/GenerateOTP/ValidateOTP");
                request4.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                var content4 = new StringContent("{\r\n    \"userID\":0,\r\n    \"channelCode\":\"kiosk\",\r\n    \"moduleID\":1,\r\n    \"username\":\"" + username + "\",\r\n    \"OTP\":\"" + OTP + "\"\r\n}", null, "application/json");
                request4.Content = content4;
                var response4 = await client4.SendAsync(request4);
                response4.EnsureSuccessStatusCode();
                Console.WriteLine(await response4.Content.ReadAsStringAsync());
                //MessageBox.Show(await response4.Content.ReadAsStringAsync());




                //MessageBox.Show("I STARTED 1");
                //button.IsEnabled = true; return;
                //CUSTOMER PROFILE CREATION


                // var selectedItemNATIONALITY = (ComboBoxItem)nationality.SelectedItem;
                ////////string contentdb = selectedItem.Content.ToString();
                // string valueNATIONALITY = (string)selectedItemNATIONALITY.Tag; // Assuming Tag is castable to int

                string valueNATIONALITY = selectedNATIONALITYstrga;
                string REM_ID = "";
                //MessageBox.Show("I STARTED 2");

                //MessageBox.Show(value);
                //button.IsEnabled = true; return;

                if (existingremid == "") { 

                var client5 = new HttpClient();
                var request5 = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxRemitter/Remitter/post");
                request5.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                var content5 = new MultipartFormDataContent();
                content5.Add(new StringContent("1"), "appID");
                content5.Add(new StringContent("1"), "moduleID");
                content5.Add(new StringContent("KIOSK"), "channelCode");
                //content5.Add(new StringContent("1114"), "userID");
                //content5.Add(new StringContent("1114"), "userID");
                content5.Add(new StringContent("0"), "RemID");
                content5.Add(new StringContent("M"), "Gender");
                content5.Add(new StringContent(firstname), "RemFName");
                content5.Add(new StringContent(middlename), "RemMName");
                content5.Add(new StringContent(lastname), "RemLName");
                content5.Add(new StringContent(""), "RemARFName");
                content5.Add(new StringContent(""), "RemARMName");
                content5.Add(new StringContent(""), "RemARLName");
                content5.Add(new StringContent(dobv), "RemDOB"); //DD-MM-YYYY 05-08-1988
                content5.Add(new StringContent(valueNATIONALITY), "RemNation");
                content5.Add(new StringContent(valueNATIONALITY), "RemCountry");
                content5.Add(new StringContent(mobilenov), "MobileNo");
                content5.Add(new StringContent("CI"), "IdentityType");
                content5.Add(new StringContent(civilidv), "IdentityNo");
                content5.Add(new StringContent(civilidexpiryv), "IDExpiryOn");//DD-MM-YYYY 05-08-1988
                content5.Add(new StringContent(emailidv), "EmailID");
                content5.Add(new StringContent("I"), "Category");
                content5.Add(new StringContent("KW"), "IDCountry");
                content5.Add(new StringContent("SAL"), "SOI");
                content5.Add(new StringContent(occupationa), "Occupation");
                content5.Add(new StringContent(salarya), "Salary");
                content5.Add(new StringContent(emploerr), "Employer");
                //content5.Add(new StringContent("Testing"), "Occupation");
                content5.Add(new StringContent(address1 + " " + address2), "Address");
                //content5.Add(new StreamContent(File.OpenRead("")), "IDImgFile1", "");
                //content5.Add(new StreamContent(File.OpenRead("/C:/Users/rifayath/Downloads/depositphotos_48410095-stock-photo-sample-blue-square-grungy-stamp.jpg")), "IDImgFile2", "/C:/Users/rifayath/Downloads/depositphotos_48410095-stock-photo-sample-blue-square-grungy-stamp.jpg");
                content5.Add(new StringContent(civilidfrontbase64), "IDImg1");
                content5.Add(new StringContent(civilidbackbase64), "IDImg2");
                content5.Add(new StringContent(signbase64), "Sign");

                //content5.Add(new StringContent(""), "IDImg1");
                //content5.Add(new StringContent(""), "IDImg2");
                request5.Content = content5;
                var response5 = await client5.SendAsync(request5);
                response5.EnsureSuccessStatusCode();
                Console.WriteLine(await response5.Content.ReadAsStringAsync());
                //MessageBox.Show("Remitter/post " + await response5.Content.ReadAsStringAsync());


                var contentString2 = "";
                foreach (var part in content5)
                {
                    //contentString += part.Headers.ToString() + "\n" + await part.ReadAsStringAsync() + "\n";
                    if (part.Headers.ContentDisposition != null)
                    {
                        var name = part.Headers.ContentDisposition.Name?.Trim('"'); // Trims the quotes around the name
                        var value = await part.ReadAsStringAsync(); // Reads the value of the part
                                                                    //contentString += $"Name: {name}, Value: {value}\n";
                        contentString2 += $"{name}: {value}\n";
                    }
                }
                string responseString2 = await response5.Content.ReadAsStringAsync();
                RichMessageBox.Show("Request Data to api/v1/sxRemitter/Remitter/post\n" + DateTime.Now + "\n" + contentString2);
                RichMessageBox.Show("Response from api/v1/sxRemitter/Remitter/post\n" + DateTime.Now + "\n" + responseString2);







                //if code 0 then get data remid
                //button.IsEnabled = true; return;

                var responseBody5 = await response5.Content.ReadAsStringAsync();

                


                string validatesymaxregister = "99";

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody5))
                {


                    



                    // Access the root JSON object
                    JsonElement root = doc.RootElement;


                    validatesymaxregister = root.GetProperty("Code").ToString();
                    var message = root.GetProperty("Message").ToString();

                   // MessageBox.Show(message);


                    if (validatesymaxregister == "2")
                    {
                        


                        //MessageBox.Show(message + "api/v1/sxRemitter/Remitter/post " + validatesymaxregister + " API Error " + responseString);
                        MessageBox.Show(message);
                        button.IsEnabled = true; return;
                    }





                    // Navigate to the 'Data' object
                    //JsonElement dataElement = root.GetProperty("Data");
                    JsonElement dataArray = root.GetProperty("Data");

                    // Extract the accessToken
                    //string REM_ID_val = dataElement.GetProperty("REM_ID").GetString();
                    //REM_ID = dataElement.GetProperty("REM_ID").ToString();

                    //REM_ID = dataElement.GetProperty("REM_ID").GetInt32().ToString();
                    //REM_ID = dataElement.GetProperty("REM_ID").GetString();


                    // Loop through each element in the data array
                    foreach (JsonElement dataElement in dataArray.EnumerateArray())
                    {
                        // Check if the element has a property named "REM_ID"
                        if (dataElement.TryGetProperty("REM_ID", out JsonElement remIdElement))
                        {
                            REM_ID = remIdElement.ToString();
                            break; // Stop after finding the first REM_ID (optional)
                        }
                    }

                    // Extract the accessToken
                    //OTP = root.GetProperty("Data").GetString();

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Message: {Message}");

                   // MessageBox.Show(REM_ID);
                    //button.IsEnabled = true; return;

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Access Token: {accessToken}");
                    // RemoveToken(accessToken);
                    // SaveToken(accessToken);
                    //TokenManager.SetToken(accessToken);
                    // MessageBox.Show(LoadToken());

                }

                } else
                {
                    REM_ID = existingremid;
                }


                //MessageBox.Show("I STARTED 3");
                ///button.IsEnabled = true; return;
                ///

                //SIGN UP OF USER
                var client6 = new HttpClient();
                var request6 = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxuser/user/signup");
                request6.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //button.IsEnabled = true; return;
                var content6 = new StringContent("{\r\n  \"UserId\": 0,\r\n  \"Username\": \"" + username + "\",\r\n  \"Password\": \"" + password + "\",\r\n  \"Firstname\": \"" + firstname + "\",\r\n  \"MiddleName\": \"" + middlename + "\",\r\n  \"Lastname\": \"" + lastname + "\",\r\n  \"ARFirstname\": \"\",\r\n  \"ARMiddlename\": \"\",\r\n  \"ARLastname\": \"\",\r\n  \"Email\": \"" + emailidv + "\",\r\n  \"Mobile\": \"" + mobilenov + "\",\r\n  \"Nationality\": \"IN\",\r\n  \"IDTypeCode\": \"CI\",\r\n  \"IDNumber\": \"" + civilidv + "\",\r\n  \"RemitterID\": " + REM_ID + ",\r\n  \"RefreshToken\": \"\",\r\n  \"IDExpiryDate\": \"" + convertdate(civilidexpiryv) + "\",\r\n  \"DOB\": \"" + convertdate(dobv) + "\",\r\n  \"Gender\": \"M\",\r\n  \"VideoKYCUploaded\": false,\r\n  \"SecurityAnswers\": [\r\n    {\r\n      \"UserId\": 0,\r\n      \"SecurityQnID\": 18,\r\n      \"Answer\": \"NONE\"\r\n    }\r\n  ]\r\n}", null, "application/json");
                request6.Content = content6;
                var response6 = await client6.SendAsync(request6);
                response6.EnsureSuccessStatusCode();
                Console.WriteLine(await response6.Content.ReadAsStringAsync());
                //MessageBox.Show("sxuser/user/signup " + await response6.Content.ReadAsStringAsync());

                var responseBody6 = await response6.Content.ReadAsStringAsync();

                //contentString = "";
                var contentString = await request6.Content.ReadAsStringAsync();
                string responseString = await response6.Content.ReadAsStringAsync();
                RichMessageBox.Show("Request Data to api/v1/sxuser/user/signup\n" + DateTime.Now + "\n" + contentString);
                RichMessageBox.Show("Response from api/v1/sxuser/user/signup\n" + DateTime.Now + "\n" + responseString);






                string validatesignupuser = "2";

                // Parse the JSON response using System.Text.Json
                using (JsonDocument doc = JsonDocument.Parse(responseBody6))
                {






                    // Access the root JSON object
                    JsonElement root = doc.RootElement;


                    validatesignupuser = root.GetProperty("Code").ToString();

                    var message = root.GetProperty("Message").ToString();

                    //MessageBox.Show(message);

                    if (validatesignupuser == "2")
                    {
                        //MessageBox.Show(message + "api/v1/sxuser/user/signup " + validatesignupuser + " API Error " + responseString);
                        MessageBox.Show(message);
                        button.IsEnabled = true; return;
                    }

                    

                }



            }

            MessageBox.Show("Registered Successfully !");



            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);


            NavigationManager.NavigateToHome();


        }

        private void Page_loaded(object sender, RoutedEventArgs e)
        {
            //runtheloaderfornationalitycountries();
            //MessageBox.Show("Outside " + nationality.Items.Count);
            //pacionload();
            //pacionclick();

            //pacionload();

            //runtheloaderfornationalitycountries();
            ShowLoadingPopup();

        }

        //NationalityCOUNTRYcombo

        private void runtheloaderfornationalitycountriesx()
        {

            var CPORBT = BCManager.selectedoptionborc;
            //MessageBox.Show(CPORBT);
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxmaster/SOI");
                //SelectedAddBeneCountry.seladdbenecount
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode="+ CPORBT + "&CountryCode=IN");
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode=" + CPORBT + "&CountryCode=" + SelectedAddBeneCountry.seladdbenecount);

                // var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxbeneficiary/BeneBank/BANK");
                var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxGeneral/country");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                // var content = new StringContent("{\n    \"productCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"disbMode\":\"" + ProductManager.selecteddispcode + "\",\n    \"destnCntry\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");

                var content = new StringContent("", null, "text/plain");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());

                //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                //var content = new StringContent("", null, "text/plain");
                request.Content = content;

                var response = client.Send(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = response.Content.ReadAsStream())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateNationilityComboBoxsource(doc.RootElement);
                        //MessageBox.Show("" + nationality.Items.Count);

                    }
                }

             

                // runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (nationality.Items.Count > 0)
            {
                //  productcombo.SelectedIndex = 0;
                //SelectNationalityByCode("AF");
                //FindCountryCode("AFG")


                //GETS THE 2 DIGIT FROM 3
                //MessageBox.Show(FindCountryCode("IND"));

                //SelectNationalityByCode(FindCountryCode("IND"));

            }
        }


        private async void runtheloaderfornationalitycountries()
        {

            var CPORBT = BCManager.selectedoptionborc;
            //MessageBox.Show(CPORBT);
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxmaster/SOI");
                //SelectedAddBeneCountry.seladdbenecount
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode="+ CPORBT + "&CountryCode=IN");
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode=" + CPORBT + "&CountryCode=" + SelectedAddBeneCountry.seladdbenecount);

                // var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxbeneficiary/BeneBank/BANK");
                var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+ "/api/Customer​/get-country-combo-list\r\n");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                // var content = new StringContent("{\n    \"productCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"disbMode\":\"" + ProductManager.selecteddispcode + "\",\n    \"destnCntry\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");

                var content = new StringContent("", null, "text/plain");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());

                //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                //var content = new StringContent("", null, "text/plain");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateNationilityComboBoxsource(doc.RootElement);
                        //MessageBox.Show("Inside " + nationality.Items.Count);
                        pacionclick();
                    }
                }



                // runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (nationality.Items.Count > 0)
            {
                //  productcombo.SelectedIndex = 0;
                //SelectNationalityByCode("AF");
                //FindCountryCode("AFG")


                //GETS THE 2 DIGIT FROM 3
                //MessageBox.Show(FindCountryCode("IND"));

                //SelectNationalityByCode(FindCountryCode("IND"));

            }
        }
        public class NationalityCountryr
        {
            public string ConID { get; set; }
            public string ConName { get; set; }
            public string ConCode { get; set; }
        }

        List<NationalityCountryr> productsc = new List<NationalityCountryr>();
        private void UpdateNationilityComboBoxsource(JsonElement root)
        {
            //NationalityCOUNTRYcombo.Items.Clear();
            nationality.ItemsSource = null;
            nationality.Items.Clear();

            // List<NationalityCountry> products = new List<NationalityCountry>();



            //}

            // Assuming "Data" is an array and contains "Productname" and "Productcode" properties
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("ConID", out JsonElement productNameElement) &&
                        item.TryGetProperty("ConName", out JsonElement productCodeElement) &&
                        item.TryGetProperty("ConCode", out JsonElement DisbursalModeCodeElement))

                    //DisbursalModeCode
                    {

                        //   MessageBox.Show(productCodeElement.GetString());
                        //   MessageBox.Show(productNameElement.GetString());

                        productsc.Add(new NationalityCountryr
                        {
                            ConID = productNameElement.ToString(),
                            ConName = productCodeElement.ToString(),
                            ConCode = DisbursalModeCodeElement.ToString()
                        });
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }

            // Bind the ComboBox to the products list
            nationality.ItemsSource = productsc;

            // Assuming products is a collection and has at least one item
            if (productsc.Count > 0)
            {
                nationality.SelectedItem = productsc[0];
            }
            nationality.DisplayMemberPath = "ConName";

            //SelectNationalityByConID("IN");
        }




        private void ClearCanvas_Click(object sender, RoutedEventArgs e)
        {
            inkCanvas.Strokes.Clear();
        }

        private void SaveSignature_Click(object sender, RoutedEventArgs e)
        {
            // Save as PNG image
            //string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            //string filePath = System.IO.Path.Combine(desktopPath, "signature.png");

            string filePath = "signature.png";

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)inkCanvas.ActualWidth, (int)inkCanvas.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(inkCanvas);

            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(rtb));

            using (var stream = File.Create(filePath))
            {
                encoder.Save(stream);
            }

            // Convert to Base64 string
            byte[] imageBytes = File.ReadAllBytes(filePath);
            string base64String = Convert.ToBase64String(imageBytes);

            // Convert byte array to a string representation
            string byteString = "";
            foreach (byte b in imageBytes)
            {
                //byteString += b.ToString("X2") + " ";
            }

            // Display the byte string in a message box
            //MessageBox.Show(byteString);

            // Display Base64 string in a message box
          //  MessageBox.Show("Base64 String:\n" + base64String);

            //result = imageBytes.ToString();

            //result.AppendText(base64String);
        }


        public void scannerconsole()
        {
            // Path to the .NET 4 Console application
            string consoleAppPath = @"C:\kiosk\scan\PACIMID.exe";

            // Parameters you want to pass to the Console app
            string parameters = "param1 param2";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = consoleAppPath,
                Arguments = parameters,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                // Optionally read the output if needed
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                //MessageBox.Show(output);
                //MessageBox.Show(error);
                // Handle the output or error as needed
            }
        }

        //SCAN FRONT SIDE
        private void button2_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Clicked");
            try
            {
                scannerconsole();
                //MessageBox.Show("START");
                // Get   the path to the text file
                //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SCHASH.txt");

                // Specify the file path for the Base64 content
                string filePath = "SCHASH.txt";

                // Read the Base64 string from the file
                string base64String = File.ReadAllText(filePath);

                civilidfrontbase64 = base64String;

                // Decode the Base64 string to a byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Convert the byte array to a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }

                // Set the BitmapImage as the source of the Image control
                civilfront.Source = bitmap;

              //  MessageBox.Show("END");

                //string filePath = "SCHASH.txt";
                //MessageBox.Show(filePath);
                //string basecode = "";

                //if (File.Exists(filePath))
                //{
                //    using (StreamReader reader = new StreamReader(filePath))
                //    {
                //       //string.TryParse(reader.ReadLine(), out basecode)

                //            basecode = reader.ReadLine();
                //            MessageBox.Show(basecode);
                //    }
                //}

                //MessageBox.Show(basecode);

                //// Read the Base64 encoded string from the file
                ////string base64String = File.ReadAllText(filePath);
                //string base64String = basecode;

                //MessageBox.Show(base64String);
                //// Convert the Base64 string to a byte array
                //byte[] imageBytes = Convert.FromBase64String(base64String);

                //// Create a BitmapImage from the byte array
                //BitmapImage imageSource = new BitmapImage();
                //using (var stream = new MemoryStream(imageBytes))
                //{
                //    imageSource.BeginInit();
                //    imageSource.StreamSource = stream;
                //    imageSource.EndInit();
                //}
                //MessageBox.Show(base64String);

                //// Set the Source property of the image tag
                //civilfront.Source = imageSource;
            }
            catch (Exception ex)
            {
               // MessageBox.Show("Error reading or decoding the image: " + ex.Message);
            }
        }

        public async void qrcodemid(string qrscan)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.65:9999/WeatherForecast");
            var content = new StringContent("{\r\n    \"parameter1\": \""+qrscan+"\",\r\n    \"parameter2\": \"b\"\r\n}", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
           // MessageBox.Show(await response.Content.ReadAsStringAsync());

        }

        private void button2_click_back(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Clicked");
            try
            {
                scannerconsole();
                // MessageBox.Show("START");
                // Get   the path to the text file
                //string filePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SCHASH.txt");

                // Specify the file path for the Base64 content
                string filePath = "SCHASH.txt";

                // Read the Base64 string from the file
                string base64String = File.ReadAllText(filePath);

                civilidbackbase64 = base64String;

                // Decode the Base64 string to a byte array
                byte[] imageBytes = Convert.FromBase64String(base64String);

                // Convert the byte array to a BitmapImage
                BitmapImage bitmap = new BitmapImage();
                using (MemoryStream stream = new MemoryStream(imageBytes))
                {
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                }

                // Set the BitmapImage as the source of the Image control
                civilback.Source = bitmap;

              //  MessageBox.Show("END");

               
            }
            catch (Exception ex)
            {
               //MessageBox.Show("Error reading or decoding the image: " + ex.Message);
            }
        }
    }
}
