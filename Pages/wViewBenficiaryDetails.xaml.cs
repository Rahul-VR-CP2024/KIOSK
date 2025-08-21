using Exchange.Managers;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wViewBenficiaryDetails.xaml
    /// </summary>
    public partial class wViewBenficiaryDetails : Page
    {
        public wViewBenficiaryDetails()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                verifybenetitle.Text = "بيانات المستفيد";
                backbtn.Content = "يرجع";
                proceedbtn.Content = "يتابع";


            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {


            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-by-id");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var content = new StringContent("{\n \"eid\":" + SelectedBeneficiaryManager.BENE_SLNO + "\"\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());


                var responseBody5 = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(responseBody5))
                {

                    JsonElement root = doc.RootElement;

                    JsonElement dataArray = root.GetProperty("Data");

                    foreach (JsonElement dataElement in dataArray.EnumerateArray())
                    {
                        // Check if the element has a property named "REM_ID"
                        if (dataElement.TryGetProperty("BENE_FNAME", out JsonElement remIdElement))
                        {
                            //REM_ID = remIdElement.ToString();
                            //firstnameTextbox.Text = remIdElement.ToString();
                            //break; // Stop after finding the first REM_ID (optional)
                        }


                    }

                }


                loadbenefieldstoedit();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }



        }

        JsonElement dataArrayedit;
        private JsonDocument jsonDocument;
        // Create an empty list to store CoreFieldNames
        List<string> coreFieldNames = new List<string>();

        string BENE_PRODedit = "";
        string DISBTYPEedit = "";
        string BENE_CNTRYedit = "";
        string BENE_CURRedit = "";

        public async void loadbenefieldstoedit()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary/get-beneficiary-by-id");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var content = new StringContent("{\n    \"eid\":" + SelectedBeneficiaryManager.BENE_SLNO + "\"\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();


                //MessageBox.Show(await response.Content.ReadAsStringAsync());
                // Parse and store the JsonDocument at the class level
                jsonDocument = JsonDocument.Parse(responseBody);
                JsonElement root = jsonDocument.RootElement;

                dataArrayedit = root.GetProperty("Data");
                // MessageBox.Show("Step 1 " + dataArrayedit.ToString());

                foreach (JsonElement dataElement in dataArrayedit.EnumerateArray())
                {

                    BeneficiaryDetailsManager.SetBENE_MOBILE(dataElement.TryGetProperty("BENE_MOBILE", out JsonElement mdElementemobile) ? mdElementemobile.GetString() : "12345678");
                    BENE_PRODedit = dataElement.TryGetProperty("BENE_PROD", out JsonElement mdElemente) ? mdElemente.GetString() : "";
                    DISBTYPEedit = dataElement.TryGetProperty("COREDISB", out JsonElement mdElementeee) ? mdElementeee.GetString() : "";
                    BENE_CNTRYedit = dataElement.TryGetProperty("BENE_CNTRY", out JsonElement mdElementeeee) ? mdElementeeee.GetString() : "";
                    BENE_CURRedit = dataElement.TryGetProperty("BENE_CURR", out JsonElement mdElementeeeee) ? mdElementeeeee.GetString() : "";



                    LoadBenefieldseditmode(
                        dataElement.TryGetProperty("BENE_PROD", out JsonElement mdElement) ? mdElement.GetString() : "",
                        dataElement.TryGetProperty("DISBTYPE", out JsonElement md2Element) ? md2Element.GetString() : "",
                        dataElement.TryGetProperty("BENE_CNTRY", out JsonElement md3Element) ? md3Element.GetString() : "",
                        dataElement.TryGetProperty("COREDISB", out JsonElement md4Element) ? md4Element.GetString() : ""
                    );
                }

               // runtheloadersource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public async Task LoadBenefieldseditmode(string BENE_PRODv, string DISBTYPEv, string BENE_CNTRYv, string COREDISBv)
        {




            try
            {






                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);


                var CPORBT = BCManager.selectedoptionborc;

                var content = new StringContent("{\n    \"productCode\":\"" + BENE_PRODv + "\",\n    \"disbursalMode\":\"" + DISBTYPEv + "\",\n    \"countryCode\":\"" + BENE_CNTRYv + "\",\n    \"action\":\"BEN\",\n    \"coredisbcode\":\"" + COREDISBv + "\"\n}", null, "application/json");


               // MessageBox.Show(""+content);
                //ADD BENEFICIARY
                //var content = new StringContent("{\n    \"productCode\":\"" + BENE_PRODv + "\",\n    \"disbursalMode\":\"" + DISBTYPEv + "\",\n    \"countryCode\":\"" + BENE_CNTRYv + "\",\n    \"action\":\"BEN\",\n    \"coredisbcode\":\"" + COREDISBv + "\"\n}", null, "application/json");


                request.Content = content;
                //var content = new StringContent("{ \n    \"remID\":130824,\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
                //request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                //MessageBox.Show("FSP " + await response.Content.ReadAsStringAsync());




                var contentString = await request.Content.ReadAsStringAsync();
                var responseString = response.Content.ReadAsStringAsync().Result;


                // Handle response data
                RichMessageBox.Show($"Request Data to \n{DateTime.Now}\n{contentString}");
                RichMessageBox.Show($"Response from \n{DateTime.Now}\n{responseString}");

                // MessageBox.Show("Here 1");

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {

                    //MessageBox.Show("Here 2");

                    //MessageBox.Show("Hi 1");
                    // Parse JSON response using JsonDocument.Parse
                    var jsonDocument = await JsonDocument.ParseAsync(responseStream);



                    // MessageBox.Show("Hi 2");
                    //MessageBox.Show("Step 3 " + dataArrayedit.ToString());
                    // Access root object (assuming it's an array) and iterate over its elements
                    int counter = 0;
                    foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                    {

                        // MessageBox.Show("Hi 3");

                        //  MessageBox.Show(dataElement.GetProperty("ConName").GetString());

                        //  dataElement.GetProperty("ConCode").GetString(),
                        //   dataElement.GetProperty("ConName").GetString(),

                        if (counter == 0)
                        {
                            // CreateUI("BENE_GENDER", "GENDER");
                            // CreateUI("BENE_NICKNAME", "Nickname", "");
                            //coreFieldNames.Add("BENE_NICKNAME");
                            //coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                        }
                        counter++;


                        //select country here 
                        //NationalityCOUNTRYcombo.SelectedIndex = ;

                        //SelectNationalityByConID("IN");
                        if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CNTRY" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CURR" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHCODE" || dataElement.GetProperty("IsVisible").GetBoolean() == false)
                        {
                            //SelectedAddBeneCountry

                            if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION")
                            {
                                //SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                            }


                            if (BCManager.selectedoptionborc == "BT")
                            {

                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID")
                                {
                                    //SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                    // SelectBankByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                }
                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID")
                                {
                                    //SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                    // SelectBranchByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));

                                }




                            }
                        }
                        else
                        {

                            // MessageBox.Show("Step 2 " + dataArrayeditinside.ToString());
                            // Convert JsonElement to a string
                            // string dataArrayeditText = dataArrayedit.GetRawText();

                            // Show the string in a MessageBox
                            // MessageBox.Show(dataArrayeditText, "Data Array Content");
                            //string dataArrayString = JsonSerializer.Serialize(dataArrayedit);
                            //System.Windows.MessageBox.Show(dataArrayString);
                            // MessageBox.Show(""+dataArrayedit);
                            // foreach (JsonElement dataElement2 in dataArrayedit.EnumerateArray())
                            {
                                //string ans = dataElement2.TryGetProperty(dataElement.GetProperty("CoreFieldName").GetString(), out JsonElement mdElement) ? mdElement.GetString() : "";


                                // CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(), ans);
                            }
                            //SelectedAddBeneCountry
                           // MessageBox.Show(dataElement.GetProperty("CoreFieldName").GetString());

                            if(TokenManager.Langofsoft == "ar")
                            {
                                CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("ArabicDisplay").GetString(), getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                               // CreateUI(dataElement.GetProperty("CoreFieldName").ToString(), dataElement.GetProperty("ArabicDisplay").ToString(), getthefieldvalue(dataElement.GetProperty("CoreFieldName").ToString()));

                            } 
                            else
                            {
                                CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(), getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                //CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(), "");
                                //CreateUI(dataElement.GetProperty("CoreFieldName").ToString(), dataElement.GetProperty("DisplayText").ToString(), getthefieldvalue(dataElement.GetProperty("CoreFieldName").ToString()));
                            }

                            
                            //MessageBox.Show(dataArrayedit.ToString());
                            // Add the CoreFieldName to the list
                            coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                            //coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").ToString());

                        }


                        //CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString());

                        //// Add the CoreFieldName to the list
                        //coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                        //Countries.Add(new Country
                        //{


                        //    CountryName = dataElement.GetProperty("ConCode").GetString(),
                        //    Amt = "",
                        //    Bene = dataElement.GetProperty("ConName").GetString(),
                        //    Date = "", // You need to specify how to get the date from the JSON response
                        //    TID = "", // You need to specify how to get the TID from the JSON response
                        //    BANK = "",
                        //    stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                        //    FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                        //    //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                        //});

                        //new Country {
                        //FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png")),
                        //CountryName = "Bank of Baroda",
                        //Amt = "2,00,000 INR",
                        //Bene = "India",
                        //Date = "01/01/2023", TID = "123456",
                        //BANK = "Bank of Baroda",
                        //stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
                    }
                }

                //  countryListView.ItemsSource = Countries;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        public string getthefieldvaluebackup19112024(string fieldvalue)
        {
            //MessageBox.Show("Step 34 " );
            //MessageBox.Show("Step 3 " + ABC.ToString());
            //JsonElement dataArrayeditinside = dataArrayedit;
            string ans = "";

            foreach (JsonElement dataElement2 in dataArrayedit.EnumerateArray())
            {
                ans = dataElement2.TryGetProperty(fieldvalue, out JsonElement mdElement) ? mdElement.GetString() : "";


                //CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(), ans);
            }


            return (ans);

        }

        public string getthefieldvalue(string fieldvalue)
        {
            string ans = "";

            foreach (JsonElement dataElement2 in dataArrayedit.EnumerateArray())
            {
                if (dataElement2.TryGetProperty(fieldvalue, out JsonElement mdElement))
                {
                    if (mdElement.ValueKind == JsonValueKind.String)
                    {
                        ans = mdElement.GetString();
                    }
                    else if (mdElement.ValueKind == JsonValueKind.Number)
                    {
                        // Convert numeric value to string
                        ans = mdElement.GetRawText();
                    }
                    else
                    {
                        // Handle other value kinds as needed (e.g., log a warning)
                        Console.WriteLine($"Warning: Unexpected value kind for field '{fieldvalue}': {mdElement.ValueKind}");
                    }
                }
            }

            return ans;
        }

        private void CreateUI(string fieldname, string fielddisplayname, string textvaluein)
        {
            // Create the outer StackPanel
            StackPanel outerStackPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 20, 0, 0)
            };

            // Create the first StackPanel
            StackPanel labelStackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(30, 0, 0, 0)
            };

            // Create the Label
            Label label = new Label
            {
                //Content = "Beneficiary Firstname",
                Content = fielddisplayname,
                FontSize = 20,
                Width = 400,
                Foreground = Brushes.White
            };

            // Add the Label to the first StackPanel
            labelStackPanel.Children.Add(label);

            // Create the second StackPanel
            StackPanel textBoxStackPanel = new StackPanel
            {
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Center
            };

            // Create the TextBox
            TextBox textBox = new TextBox
            {
                FontFamily = new FontFamily("Helvetica"),
                FontWeight = FontWeights.Medium,
                //Text = "",
                Text = textvaluein,
                FontSize = 25,
                IsReadOnly = true,
                HorizontalAlignment = HorizontalAlignment.Left,
                Foreground = Brushes.Black,
                Background = Brushes.White,
                BorderThickness = new Thickness(0),
                Width = 600,
                HorizontalContentAlignment = HorizontalAlignment.Left,
                Opacity = 0.5,
                Height = 30,
                Margin = new Thickness(0),
                Padding = new Thickness(20, 0, 0, 0),
                Name = fieldname
            };

            // Create a Style for the TextBox border
            Style textBoxBorderStyle = new Style(typeof(Border));
            textBoxBorderStyle.Setters.Add(new Setter(Border.CornerRadiusProperty, new CornerRadius(10)));
            textBox.Resources.Add(typeof(Border), textBoxBorderStyle);

            // Add the TextBox to the second StackPanel
            textBoxStackPanel.Children.Add(textBox);

            // Add both StackPanels to the outer StackPanel
            outerStackPanel.Children.Add(labelStackPanel);
            outerStackPanel.Children.Add(textBoxStackPanel);

            // Add the outer StackPanel to your desired parent container (e.g., myStackPanel)
            myStackPanel.Children.Add(outerStackPanel);
            //myStackPanel.Children.Insert(0, outerStackPanel);
            //
        }

        private void backbutton(object sender, RoutedEventArgs e)
        {
            wSelectbeneficary wx = new wSelectbeneficary();
            NavigationService.Navigate(wx);
        }

        private void Proceedbutton(object sender, RoutedEventArgs e)
        {
            //wtobankorcash wx = new wtobankorcash();
            //NavigationService.Navigate(wx);

            wTransferpay wtpay = new wTransferpay();
            NavigationService.Navigate(wtpay);
        }


        public static class BeneficiaryDetailsManager
        {
            public static string BENE_MOBILE { get; set; }

            public static void SetBENE_MOBILE(string token)
            {
                BENE_MOBILE = token;
            }
        }
    }
}
