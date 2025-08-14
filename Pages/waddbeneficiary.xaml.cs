using Exchange.Common;
using Exchange.Managers;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Navigation;
using System.Windows.Threading;
using static Exchange.Pages.wSelectcountry;
using static Exchange.Pages.wSelectProduct;
using static Exchange.Pages.wtobankorcash;


namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for waddbeneficiary.xaml
    /// </summary>
    public partial class waddbeneficiary : Page
    {

        string addoreditvalue;

        string bankvisible = "yes";
        string branchvisibile = "yes";

        string editmodebranch = "";
        // SelectBranchByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));

        public waddbeneficiary(string addoredit)
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                benefidetlstitle.Text = "بيانات المستفيد";
                addnewbtn.Content = "حفظ بيانات المستفيد";
                banklabel.Content = " البنك";
                branchlabel.Content = " الفرع";
                nationalitylabel.Content = "الجنسية";
            }


            addoreditvalue = addoredit;

            bankcombo.Items.Clear();
            //deliverycombo.Items.Clear();
            //MessageBox.Show(LoginManager.Remiduser);


            //MessageBox.Show(addoredit);
            //if()

        }

        string looperjson = "";


        private void backbutton(object sender, RoutedEventArgs e)
        {

            wSelectbeneficary wsel = new wSelectbeneficary();
            NavigationService.Navigate(wsel);

            //wBeneficiary mainpage = new wBeneficiary();
            //NavigationService.Navigate(mainpage);

        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //bankcombo.SelectedIndex = 2;
            //MessageBox.Show(bankcombo.Items.Count + "");
            //MessageBox.Show((BanksC)bankcombo.SelectedItem.BankName + "");

            //BanksC selectedProductx = (BanksC)bankcombo.SelectedItem;
            //var Banidx = selectedProductx.BankID;
            //var bancodex = selectedProductx.BankCode;

            //MessageBox.Show(Banidx + "");
            //MessageBox.Show(bancodex + "");

            //return;
            //MessageBox.Show(branchcombo.text());

            //if (BCManager.selectedoptionborc != "CP")
            //{

                BanksC selectedProduct = (BanksC)bankcombo.SelectedItem;


                
                //BanksBanchC selecteddisp = (BanksBanchC)branchcombo.SelectedItem;

                // Access the product code
                string bankksid = "";
                string banknameis = "";
                //string bankksid = selectedProduct.BankID;
                //string banksid = selectedProduct.BankID != null ? selectedProduct.BankID : "";


                if (selectedProduct != null)
                {
                    bankksid = selectedProduct.BankID;
                    banknameis = selectedProduct.BankName;
                    //MessageBox.Show(selectedProduct.BankName + "");
                }
                //string banbchesid = selecteddisp.BanksBanchID;
                string banbchesid = "";
                string branchnameis = "";

                if (branchcombo.Items.Count != 0)
                {
                   // MessageBox.Show(branchcombo.Items.Count + "");
                    //MessageBox.Show(branchcombo.Text);
                    BanksBanchC selecteddisp = (BanksBanchC)branchcombo.SelectedItem;
                    //MessageBox.Show(selecteddisp + "");
                    banbchesid = selecteddisp.BanksBanchID;
                    branchnameis = selecteddisp.BanksBanchName;
                    //MessageBox.Show(selecteddisp.BanksBanchName + "");
                }
                //return;

                // MessageBox.Show(bankksid + " " + banbchesid);

                // return;

                //ProductManager.selectedproductcode;

                //ProductManager.selecteddispcode;
                //MessageBox.Show("" + ProductManager.selectedproductcode + " " + ProductManager.selecteddispcode);
            //}

            // Assuming you have a reference to the main StackPanel named 'mainStackPanel'
            //StackPanel targetStackPanel = myStackPanel.Children[0] as StackPanel; // Assuming the first child is the desired StackPanel


            // Create Label and TextBox
            //Label newLabel = new Label { Content = "New Label", FontSize = 20, Foreground = Brushes.White };
            //TextBox newTextBox = new TextBox { Text = "New Text", FontSize = 25 };




            // Add elements to the StackPanel
            //myStackPanel.Children.Add(newLabel);
            //myStackPanel.Children.Add(newTextBox);

            //CreateUI("BENE_FNAME", "Beneficiary Firstname");

            // LoadBenefields();

            //MessboxshowBENE_FNAME.text()
            //MessageBox.Show(BENE_FNAME.text);

            // Loop through the coreFieldNames array for further processing
            //foreach (string coreFieldName in coreFieldNames)
            //{
            //    // Assuming you have TextBoxes with names matching CoreFieldNames
            //    TextBox textBox = myStackPanel.FindName(coreFieldName) as TextBox; // Replace myStackPanel with actual parent
            //    if (textBox != null)
            //    {
            //        // Access the corresponding "DisplayText" (optional)
            //        string displayText = dataArray.FirstOrDefault(x => x.GetProperty("CoreFieldName").GetString() == coreFieldName)?.GetProperty("DisplayText").GetString();
            //        textBox.Text = displayText ?? ""; // Set Textbox content (optional display text or empty string)
            //    }
            //    else
            //    {
            //        // Handle case where TextBox is not found (optional: logging or error message)
            //        MessageBox.Show($"TextBox with name '{coreFieldName}' not found!");
            //    }
            //}




            //XXXXX V1
            //// Assuming coreFieldNames is populated with data (from previous code or LoadBenefields())
            //if (coreFieldNames == null || coreFieldNames.Count == 0)
            //{
            //    MessageBox.Show("No CoreFieldNames available!");
            //    return; // Early exit if no data
            //}

            //// Create a dictionary to store field names and their values
            //Dictionary<string, string> fieldValues = new Dictionary<string, string>();

            //// Loop through coreFieldNames
            //foreach (string coreFieldName in coreFieldNames)
            //{
            //    // Find the corresponding TextBox
            //    TextBox textBox = myStackPanel.FindName(coreFieldName) as TextBox;

            //    if (textBox != null)
            //    {
            //        // Get the TextBox value
            //        string value = textBox.Text;

            //        // Add the field name and value to the dictionary
            //        fieldValues.Add(coreFieldName, value);
            //    }
            //    else
            //    {
            //        // Handle case where TextBox is not found (optional: logging or error message)
            //        MessageBox.Show($"TextBox with name '{coreFieldName}' not found!");
            //    }
            //}

            //// Now you have a dictionary containing field names and their values from TextBoxes
            //// Use this dictionary for further processing, like sending data to a server

            //// For example, convert the dictionary to JSON:
            //string jsonData = JsonConvert.SerializeObject(fieldValues);
            //// ... Use jsonData as needed

            //MessageBox.Show("Save Clicked");
            //XXXXX V1


            //createbene();

            //XXXXX V2
            // Assuming coreFieldNames is populated with data
            if (coreFieldNames == null || coreFieldNames.Count == 0)
            {
                MessageBox.Show("No CoreFieldNames available!");
                return;
            }

            // Create a dictionary to store field names and their values
            Dictionary<string, string> fieldValues = new Dictionary<string, string>();


            foreach (string coreFieldName in MANDATORYcoreFieldNames)
            {
                TextBox textBox = FindTextBoxByName(myStackPanel, coreFieldName);
                Label lab = FindlabelBoxByName(myStackPanel, coreFieldName+"label");
                if (textBox != null)
                {
                    // Get the TextBox value
                    string value = textBox.Text;

                    if(value == null || value == "")
                    {
                        MessageBox.Show(""+ lab.Content + " is Mandatory !" );
                        return;
                    }

                }
            }

                // Loop through coreFieldNames
                foreach (string coreFieldName in coreFieldNames)
            {
                // Find TextBox recursively starting from myStackPanel
                TextBox textBox = FindTextBoxByName(myStackPanel, coreFieldName);

                if (textBox != null)
                {
                    // Get the TextBox value
                    string value = textBox.Text;

                    //textBox.Text = "Hi";
                    //SetTextBoxText(myStackPanel, coreFieldName, "Test");

                    //MessageBox.Show(coreFieldName + " : " + value);


                    if (coreFieldName == "BENE_NATION" || coreFieldName == "BENE_CNTRY" || coreFieldName == "BENE_CURR" || coreFieldName == "BENE_BANKID" || coreFieldName == "BENE_BANKCODE" || coreFieldName == "BENE_BRANCHID" || coreFieldName == "BENE_BRANCHCODE")
                    {

                    }
                    else { 

                    looperjson += "\"" + coreFieldName  + "\"" + " : \"" + value + "\",\n";

                    }






                    // Add the field name and value to the dictionary
                    fieldValues.Add(coreFieldName, value);

                   
                    
                        
                    }
                else
                {
                    MessageBox.Show($"TextBox with name '{coreFieldName}' not found!");
                }
            }

            // ... Use fieldValues as needed
            //MessageBox.Show("Saved Sucessfully");
            createbene();

            //XXXXX V2



        }

        public async void createbene()
        {

            
          
         


            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary/update-beneficiary");
           // request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6IjI1ZmRjZjI4LTJiNGItNDMyNS05ZjQ2LWYwZmY3YjIwMjYyNCIsImlhdCI6IjA4LzEyLzIwMjQgMTU6MjE6UE0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyMzQ2NTI3NywiZXhwIjoxNzIzNDY3MDc3LCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.3HBsleRdWh5v1uXrwdkgD6gKkYvlpTTgEu6U4AsY4Mk");

            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //var content = new StringContent("{\n  \"BENE_SLNO\": 0,\n  \"BENE_DISB\": \"2\",\n  \"BENE_REMID\": 10168, "+ looperjson + "\n  \"BENE_GENDER\": \"M\",\n  \"BENE_NICKNAME\": \"TFCASH\",\n  \"BENE_FNAME\": \"BURHAN\",\n  \"BENE_MNAME\": \"\",\n  \"BENE_LNAME\": \"ALI\",\n  \"BENE_ADDRESS1\": \"ADD1\",\n  \"BENE_ADDRESS2\": \"\",\n  \"BENE_CITY\": \"\",  \n  \"BENE_STATE\": \"\",  \n  \"BENE_CNTRY\": \"BD\",\n  \"BENE_NATION\": \"BD\",\n  \"BENE_MOBILE\":\"5656456457\",\n  \"BENE_PROD\": \"TF\",\n  \"BENE_CURR\": \"BDT\",\n  \"appID\": 10018,\n  \"moduleID\": 2,\n  \"BENE_CHANNEL\": \"kiosk\",\n  \"BENE_DISBTYPE\": \"CP\"\n}", null, "application/json");


            //Disbursal Code Pass the DisbursalCode from the API response/ api / v{ v}/ sxgeneral/ DefaultProduct
           

            if (BCManager.selectedoptionborc == "CPX")
            {
               // onlyshowonbt.Visibility = Visibility.Hidden;
            }
            else
            {

                BanksC selectedProduct = (BanksC)bankcombo.SelectedItem;
                BanksBanchC selecteddisp = (BanksBanchC)branchcombo.SelectedItem;

                // Access the product code
                string bankksid = "";
                string banknameis = "";
                string bankkscode = "";
                string branchnameis = "";


                // string bankksid = selectedProduct.BankID;
                // string bankkscode = selectedProduct.BankCode;


                if (selectedProduct != null)
                {
                    bankksid = selectedProduct.BankID;
                    banknameis = selectedProduct.BankName;
                }

                try
                {
                    if (selecteddisp != null)
                    {
                        bankkscode = selectedProduct.BankCode;

                    }

                } catch (Exception ex)
                {
                    MessageBox.Show("Bank is not Selected");
                    return;
                }

                if (selecteddisp != null)
                {
                    bankkscode = selectedProduct.BankCode;
                    
                }

                string banbchesid = "";
                string banbchescode = "";


                if (branchcombo.Items.Count != 0)
                {
                    banbchesid = selecteddisp.BanksBanchID;
                    banbchescode = selecteddisp.BanksBanchCode;
                    branchnameis = selecteddisp.BanksBanchName;

                }
                // runtheloadersource();
                // looperjson += ;

                //            "BENE_BANKID": "113",
                //"BENE_BANKCODE": "IND11",
                //"BENE_BRANCHID": "ID98000001",
                //"BENE_BRANCHCODE": "ID98 | ADCB0000001",
                //looperjson += "\"BENE_BANKID\"" + " : \"" + bankksid + "\",\n";
                //looperjson += "\"BENE_BANKCODE\"" + " : \"" + bankkscode + "\",\n";

                if(bankksid == "")
                {
                    looperjson += "\"BENE_BANKID\"" + " : \"001\",\n";
                } else
                {
                    looperjson += "\"BENE_BANKID\"" + " : \"" + bankksid + "\",\n";
                }

                if (bankkscode == "")
                {
                    looperjson += "\"BENE_BANKCODE\"" + " : \"001\",\n";
                } else
                {
                    looperjson += "\"BENE_BANKCODE\"" + " : \"" + bankkscode + "\",\n";
                }

                if (banbchesid == "")
                {
                    looperjson += "\"BENE_BRANCHID\"" + " : \"001\",\n";
                } else
                {
                    looperjson += "\"BENE_BRANCHID\"" + " : \"" + banbchesid + "\",\n";
                }

                if (banbchescode == "")
                {
                    looperjson += "\"BENE_BRANCHCODE\"" + " : \"001\",\n";
                } else
                {
                    looperjson += "\"BENE_BRANCHCODE\"" + " : \"" + banbchescode + "\",\n";
                }

                if(bankvisible == "yes")
                {
                    looperjson += "\"BENE_BANK\"" + " : \"" + banknameis + "\",\n";
                }
                if(branchvisibile == "yes")
                {
                    looperjson += "\"BENE_BRANCH\"" + " : \"" + branchnameis + "\",\n";
                }


                //looperjson += "\"BENE_BANK\"" + " : \"" + banknameis + "\",\n";
                //looperjson += "\"BENE_BRANCHID\"" + " : \"" + banbchesid + "\",\n";
                //looperjson += "\"BENE_BRANCHCODE\"" + " : \"" + banbchescode + "\",\n";
                //looperjson += "\"BENE_BRANCHID\"" + " : \"001\",\n";
                //looperjson += "\"BENE_BRANCHCODE\"" + " : \"001\",\n";
                //looperjson += "\"BENE_BRANCH\"" + " : \"" + branchnameis + "\",\n";



                //looperjson += "\"BENE_BRIFSCCODE\": \"IFCNR123456789\",\n";
            }

            //looperjson

            NationalityCountry selectedNATIONALITY = (NationalityCountry)NationalityCOUNTRYcombo.SelectedItem;
            string selectedNATIONALITYstrg = selectedNATIONALITY.ConCode;
            looperjson += "\"BENE_NATION\"" + " : \"" + selectedNATIONALITYstrg + "\",\n";


            string beneserialno = "0";
            string benecurrency = "";

            string beneBENE_DISB = "";
            string beneBENE_CNTRY = "";
            string beneBENE_PROD = "";
            string beneBENE_CURR = "";

            if (addoreditvalue == "add")
            {
                benecurrency = ProductManager.selectedProdCurrCode;
                beneBENE_DISB = ProductManager.selecteddispcode;
                beneBENE_CNTRY = SelectedAddBeneCountry.seladdbenecount;
                beneBENE_PROD = ProductManager.selectedproductcode;
                beneBENE_CURR = ProductManager.selectedProdCurrCode;
            }

            if (addoreditvalue == "edit")
            {
                beneserialno = SelectedBeneficiaryManager.BENE_SLNO;
                beneBENE_DISB = DISBTYPEedit;
                beneBENE_CNTRY = BENE_CNTRYedit;
                beneBENE_PROD = BENE_PRODedit;
                beneBENE_CURR = BENE_CURRedit;
                //benecurrency = SelectedBeneficiaryManager.c;
            }

            var content = new StringContent("{\n  \"BENE_SLNO\": "+ beneserialno + ",\n  \"BENE_DISB\": \""+ beneBENE_DISB + "\",\n  \"BENE_REMID\": "+ LoginManager.Remiduser + ", \n  \"BENE_GENDER\": \"M\",\n" +
                "" +
                "" + looperjson + 
                //"\n  \"BENE_FNAME\": \"BURHAN\",\n  \"BENE_MNAME\": \"\",\n  \"BENE_LNAME\": \"ALI\",\n  \"BENE_ADDRESS1\": \"ADD1\",\n  \"BENE_ADDRESS2\": \"\",\n  \"BENE_CITY\": \"\",  \n  \"BENE_STATE\": \"\",  \n  \"BENE_CNTRY\": \"BD\",\n  \"BENE_NATION\": \"BD\",\n  \"BENE_MOBILE\":\"5656456457\",\n  \"BENE_PROD\": \"TF\",\n  \"BENE_CURR\": \"BDT\"," +
                //"" +
                //"" +
                //"" +
                //"" +
                "" +
               "\"BENE_CNTRY\": \"" + beneBENE_CNTRY + "\",\n" +
                "\"BENE_PROD\": \"" + beneBENE_PROD + "\",\n  \"BENE_CURR\": \""+ beneBENE_CURR + "\",\n \"appID\": 3,\n  \"moduleID\": 3,\n  \"BENE_CHANNEL\": \"kiosk\",\n  \"BENE_DISBTYPE\": \"" + BCManager.selectedoptionborc + "\"\n}", null, "application/json");
            // "\n\"BENE_BRIFSCCODE\": \"IFCNR123456789\"," +
            //BENE_CURR
            //  BENE_PROD TF  SelectedAddBeneCountry

            //BENE_CHANNEL Channel ID Pass the Code from the API response / api / v{ v}/ AppAuth

            string jsonContent = content.ReadAsStringAsync().Result; // Replace with content.Encoding if known
            //MessageBox.Show(jsonContent);
            looperjson = "";
            //string jsonContent = content;

            //// Convert the JSON string to a formatted string for better readability
            //string formattedContent = JObject.Parse(jsonContent).ToString(Formatting.Indented);



            // OR (if encoding is known)
            // string jsonContent = content.Encoding.GetString(content.ToArray());



            //MessageBox.Show(formattedContent);
            //MessageBox.Show("" + content);
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());


            var responseBody = await response.Content.ReadAsStringAsync();




            var contentString = await request.Content.ReadAsStringAsync();
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/Beneficiary/update-beneficiary\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/Beneficiary/update-beneficiary\n" + DateTime.Now + "\n" + responseString);





            // Parse the JSON response using System.Text.Json
            string code;
            string Message;
            using (JsonDocument doc = JsonDocument.Parse(responseBody))
            {
                // Access the root JSON object
                JsonElement root = doc.RootElement;

                // Navigate to the 'Data' object
                //JsonElement dataElement = root.GetProperty("Message");

                // Extract the accessToken
                 Message = root.GetProperty("Message").ToString();
                 code = root.GetProperty("Code").ToString();

               // MessageBox.Show(code + Message);


            }

            if(code == "0")
            {
                MessageBox.Show("Saved Sucessfully");
                //wBeneficiary welocmepage = new wBeneficiary();
                //NavigationService.Navigate(welocmepage);

                wSelectbeneficary wsel = new wSelectbeneficary();
                NavigationService.Navigate(wsel);
            } else
            {
               // MessageBox.Show(jsonContent);
               // MessageBox.Show(await response.Content.ReadAsStringAsync());
                MessageBox.Show(Message);
            }



        }



        //Bankcomboloader
        private async void loadthebanks()
        {
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                //var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxmaster/POT");
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/api​/Beneficiary​/get-beneficiary-bank-combo-list");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                var content = new StringContent("{\n    \"ProductCode\":\"TF\",\n    \"transferMode\":\"C\",\n    \"countryCode\":\"IN\"\n}", null, "application/json");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();



                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    //MessageBox.Show(""+ await response.Content.ReadAsStreamAsync());
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
            if (bankcombo.Items.Count > 0)
            {
                bankcombo.SelectedIndex = 0;
            }
        }


        //Bank combo update
        private void UpdateComboBox(JsonElement root)
        {
            // Clear existing items (optional)
            bankcombo.Items.Clear();

           // MessageBox.Show("Hi");
            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
              //  MessageBox.Show("Hi 2 ");
                foreach (var item in dataElement.EnumerateArray())
                {

                   // MessageBox.Show("Hi 3 ");
                    if (item.TryGetProperty("NAME", out JsonElement purpNameElement))
                    {
                        bankcombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }




        // Helper method to find TextBox by name recursively
        private static TextBox FindTextBoxByName(DependencyObject parent, string name)
        {
            if (parent == null)
                return null;

            if (parent is TextBox textBox && textBox.Name == name)
                return textBox;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent,i) as DependencyObject;
                var foundTextBox = FindTextBoxByName(child, name);
                if (foundTextBox != null)
                    return foundTextBox;
            }

            return null;
        }

        private static Label FindlabelBoxByName(DependencyObject parent, string name)
        {
            if (parent == null)
                return null;

            if (parent is Label textBox && textBox.Name == name)
                return textBox;

            var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i) as DependencyObject;
                var foundTextBox = FindlabelBoxByName(child, name);
                if (foundTextBox != null)
                    return foundTextBox;
            }

            return null;
        }

        private static void SetTextBoxText(DependencyObject parent, string name, string text)
        {
            if (parent == null)
            {
                return; // Handle null parent case
            }

            var textBox = FindTextBoxByName(parent, name);

            if (textBox != null)
            {
                textBox.Text = text;
            }
            else
            {
                // Handle case where textbox not found (optional)
                // You could throw an exception, log a message, or take some other action
                // Console.WriteLine($"TextBox with name '{name}' not found.");
                MessageBox.Show($"TextBox with name '{name}' not found.");
            }
        }

        private void CreateUI(string fieldname , string fielddisplayname, string textvaluein, string typeoffield)
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
                //Content = fielddisplayname + " *",
                Name = fieldname+"label",
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

            if (typeoffield == "Number")
            {
                textBox.PreviewTextInput += (sender, e) =>
                {
                    if (!char.IsDigit(e.Text, 0))
                    {
                        e.Handled = true; // Prevent non-numeric characters
                    }
                };

            }




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

        

        private async void Page_Load(object sender, RoutedEventArgs e)
        {


            //// Assuming you have a reference to the main StackPanel named 'mainStackPanel'
            //StackPanel targetStackPanel = myStackPanel.Children[0] as StackPanel; // Assuming the first child is the desired StackPanel


            //// Create Label and TextBox
            //Label newLabel = new Label { Content = "New Label", FontSize = 20, Foreground = Brushes.White };
            //TextBox newTextBox = new TextBox { Text = "New Text", FontSize = 25 };

            //// Add elements to the StackPanel
            //targetStackPanel.Children.Add(newLabel);
            //targetStackPanel.Children.Add(newTextBox);


            //ORIGINAL POSITION OF THIS SCRIPT -- THIS IS UPDATED POSITION ABOVE THE LOADBENEFIELDS... BEFORE IT WAS BELOW
            if (BCManager.selectedoptionborc == "CPX")
            {
                //onlyshowonbt.Visibility = Visibility.Hidden;
            }
            else
            {

                if (addoreditvalue == "add")
                {
                    runtheloadersource();
                }
            }

            runtheloaderfornationalitycountries();
            //ORIGINAL POSITION OF THIS SCRIPT


            Thread.Sleep(2000); // Sleep for 2 seconds

            if (addoreditvalue == "add")
            {
                LoadBenefields();
                //SetTextBoxText(myStackPanel, "BENE_FNAME", "Test");
            }

            if (addoreditvalue == "edit")
            {

                runtheloaderfornationalitycountries();

                //MessageBox.Show("add screen " + SelectedBeneficiaryManager.BENE_SLNO);
                loadbenefieldstoedit();
                //
                //LoadBenefields();
                // loadbenefieldstoedit();
                //BENE_MNAME.text("hiii");
                //SetTextBoxText(myStackPanel, "BENE_MNAME", "Test");

                // Retrieve the TextBox by name
                //TextBox textBox = myStackPanel.FindName("BENE_MNAME") as TextBox;
                //if (textBox != null)
                //{
                //    textBox.Text = "Desired Text";
                //}

                //RUN THE CODE AFTER 3 SEOCNDS
                _ = new DisposableTimer(() => SelectBranchByConID(editmodebranch), 1);

                //RUN THE CODE AFTER 3 SEOCNDS

            }

            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct");
            //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            ////request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6IjQ1MDg4Mjg5LWMwZTUtNDNmNi05NTg5LWYxMTBmZTQ3ZTkyOCIsImlhdCI6IjA4LzEzLzIwMjQgMDg6NDY6QU0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyMzUyNzk3OSwiZXhwIjoxNzIzNTI5Nzc5LCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.uoP5Tb4Dn-ADdwiVUKdAhg8v9UND4ALvFmg3xarGqto");
            //var CPORBT = BCManager.selectedoptionborc;

            //var content = new StringContent("{\n    \"productCode\":\""+ ProductManager.selectedproductcode + "\",\n    \"disbursalMode\":\""+ CPORBT + "\",\n    \"countryCode\":\""+ SelectedAddBeneCountry.seladdbenecount + "\",\n    \"action\":\"BEN\",\n    \"coredisbcode\":\""+ ProductManager.selecteddispcode + "\"\n}", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
            //   MessageBox.Show(await response.Content.ReadAsStringAsync());


            //loadthebanks();



            //ORIGINAL POSITION OF THIS SCRIPT 
            //if (BCManager.selectedoptionborc == "CPX")
            //{
            //    //onlyshowonbt.Visibility = Visibility.Hidden;
            //} else
            //{
                
            //    if (addoreditvalue == "add")
            //    {
            //        runtheloadersource();
            //    }
            //}

            //runtheloaderfornationalitycountries();
            //ORIGINAL POSITION OF THIS SCRIPT




            //SelectNationalityByConID("IN");


        }

        JsonElement dataArrayedit;
       

        public async void loadbenefieldstoeditBK1()
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxBeneficiary/Beneficiary/Get");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                //LoginManager.Remiduser
                //130824
                var content = new StringContent("{\n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"\",\n    \"beneSLNO\":" + SelectedBeneficiaryManager.BENE_SLNO + ",\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
                //MessageBox.Show(await response.Content.ReadAsStringAsync());


                var responseBody5 = await response.Content.ReadAsStringAsync();

                //string REM_ID = "";


                // string validatesymaxregister = "99";

                // Parse the JSON response using System.Text.Json



                using (JsonDocument doc = JsonDocument.Parse(responseBody5))
                {






                    // Access the root JSON object
                    //root = doc.RootElement;
                    JsonElement root = doc.RootElement;


                    //validatesymaxregister = root.GetProperty("Code").ToString();



                    //if (validatesymaxregister == "2")
                    //{
                    //    //return;
                    //}





                    // Navigate to the 'Data' object
                    //JsonElement dataElement = root.GetProperty("Data");
                    // dataArray = root.GetProperty("Data");
                    JsonElement dataArray = root.GetProperty("Data");

                    dataArrayedit = dataArray;
                    MessageBox.Show("Step 1 " + dataArrayedit.ToString());
                    // Extract the accessToken
                    //string REM_ID_val = dataElement.GetProperty("REM_ID").GetString();
                    //REM_ID = dataElement.GetProperty("REM_ID").ToString();

                    //REM_ID = dataElement.GetProperty("REM_ID").GetInt32().ToString();
                    //REM_ID = dataElement.GetProperty("REM_ID").GetString();



                    // Loop through each element in the data array
                    foreach (JsonElement dataElement in dataArray.EnumerateArray())
                    {
                        // Check if the element has a property named "REM_ID"
                        if (dataElement.TryGetProperty("BENE_FNAME", out JsonElement rem1IdElement))
                        {
                            //REM_ID = remIdElement.ToString();
                            //     firstnameTextbox.Text = remIdElement.ToString();
                            //break; // Stop after finding the first REM_ID (optional)
                        }
                        //MessageBox.Show(dataElement.TryGetProperty("BENE_FNAME", out JsonElement mdElement) ? mdElement.GetString() : "");


                        LoadBenefieldseditmode(dataElement.TryGetProperty("BENE_PROD", out JsonElement mdElement) ? mdElement.GetString() : "", dataElement.TryGetProperty("DISBTYPE", out JsonElement md2Element) ? md2Element.GetString() : "", dataElement.TryGetProperty("BENE_CNTRY", out JsonElement md3Element) ? md3Element.GetString() : "", dataElement.TryGetProperty("COREDISB", out JsonElement md4Element) ? md4Element.GetString() : "");

                        //SetTextBoxText(myStackPanel, "BENE_MNAME", "Test");

                        //middlenametextbox.Text = dataElement.TryGetProperty("BENE_MNAME", out JsonElement mdElement) ? mdElement.GetString() : "";
                        //lastnametextbox.Text = dataElement.TryGetProperty("BENE_LNAME", out JsonElement lnElement) ? lnElement.GetString() : "";


                        //addr1textbox.Text = dataElement.TryGetProperty("BENE_ADDRESS1", out JsonElement ad1Element) ? ad1Element.GetString() : "";
                        //addr2textbox.Text = dataElement.TryGetProperty("BENE_ADDRESS2", out JsonElement ad2Element) ? ad2Element.GetString() : "";


                        //nationalitytextbox.Text = dataElement.TryGetProperty("BENE_NATION", out JsonElement beneElement) ? beneElement.GetString() : "";
                        //mobiletextbox.Text = dataElement.TryGetProperty("BENE_MOBILE", out JsonElement mobElement) ? mobElement.GetString() : "";


                        //banknametextbox.Text = dataElement.TryGetProperty("BENE_BANK", out JsonElement bankElement) ? bankElement.GetString() : "";
                        //bankbranchtextbox.Text = dataElement.TryGetProperty("BENE_BRANCH", out JsonElement bankbranchElement) ? bankbranchElement.GetString() : "";


                        //acntnotextbox.Text = dataElement.TryGetProperty("BENE_IBANCODE", out JsonElement acntnoElement) ? acntnoElement.GetString() : "";
                        //swiftnotextbox.Text = dataElement.TryGetProperty("BENE_SWIFTCODE", out JsonElement swiftnoElement) ? swiftnoElement.GetString() : "";


                        //remarkstextbox.Text = dataElement.TryGetProperty("BENE_LNDMRK", out JsonElement remarkesElement) ? remarkesElement.GetString() : "";
                        //currtextbox.Text = dataElement.TryGetProperty("BENE_CURR", out JsonElement currentext) ? currentext.GetString() : "";

























                    }

                    // Extract the accessToken
                    //OTP = root.GetProperty("Data").GetString();

                    // Display the accessToken in a message box
                    //Console.WriteLine($"Access Token: {accessToken}");
                    //MessageBox.Show($"Message: {Message}");

                    // MessageBox.Show(REM_ID);

                }

               // runtheloadersource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }


        }

        private JsonDocument jsonDocument;

        string BENE_PRODedit = "";
        string DISBTYPEedit = "";
        string BENE_CNTRYedit = "";
        string BENE_CURRedit = "";


        public async void loadbenefieldstoedit()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxBeneficiary/Beneficiary/Get");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var content = new StringContent("{\n  \"eId\":" + SelectedBeneficiaryManager.BENE_SLNO + "\"\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                var responseBody = await response.Content.ReadAsStringAsync();

                // Parse and store the JsonDocument at the class level
                jsonDocument = JsonDocument.Parse(responseBody);
                JsonElement root = jsonDocument.RootElement;

                dataArrayedit = root.GetProperty("Data");
               // MessageBox.Show("Step 1 " + dataArrayedit.ToString());

                foreach (JsonElement dataElement in dataArrayedit.EnumerateArray())
                {

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

                runtheloadersource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }

        // Call this method to clean up the JsonDocument when done
        public void DisposeJsonDocument()
        {
            jsonDocument?.Dispose();
            jsonDocument = null;
        }


        private void runtheloadersourceNONASYNC()
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

                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary​/get-beneficiary-bank-combo-list\r\n");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                var content = new StringContent("");

                // var content = new StringContent("{\n    \"productCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"disbMode\":\"" + ProductManager.selecteddispcode + "\",\n    \"destnCntry\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");
                if (addoreditvalue == "add")
                {



                    content = new StringContent("{\n    \"ProductCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"TransferMode\":\"" + ProductManager.selecteddispcode + "\",\n    \"CountryCode\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");

                }
                else
                {
                    content = new StringContent("{\n    \"ProductCode\":\"" + BENE_PRODedit + "\",\n    \"TransferMode\":\"" + DISBTYPEedit + "\",\n    \"CountryCode\":\"" + BENE_CNTRYedit + "\"\n}", null, "application/json");
                }
                // MessageBox.Show(" " + BENE_PRODedit + " " + DISBTYPEedit+ " " + BENE_CNTRYedit);
                // MessageBox.Show(content.ToString());

                //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                //var content = new StringContent("", null, "text/plain");
                request.Content = content;

                var response = client.Send(request);
                //Send
                //var response = client.Send(request);
                response.EnsureSuccessStatusCode();

                var contentString = request.Content.ReadAsStringAsync().Result;
                string responseString = response.Content.ReadAsStringAsync().Result;
                RichMessageBox.Show("Request Data to api/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + contentString);
                RichMessageBox.Show("Response from api/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + responseString);


                // Parse the JSON response with JsonDocument
                //using (var responseStream = response.Content.ReadAsStream())
                using (var responseStream = response.Content.ReadAsStream())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                    }
                }

                if (bankcombo.Items.Count >= 1)
                {

                    runtheloaderdelivery();
                }

                //MessageBox.Show(bankcombo.Items.Count + "");
                   // runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            //MessageBox.Show(bankcombo.Items.Count+"");
            // Select the first item (if any)
            if (bankcombo.Items.Count > 0)
            //    if (bankcombo.Items.Count > 1)
            {
                if (bankcombo.Items.Count == 1)
                {
                    bankcombo.IsEditable = false;

                }
                else
                {
                    bankcombo.IsEditable = true;
                }
                bankcombo.SelectedIndex = 1;

                //bankcombo.SelectedItem = productb[1];
                //bankcombo.SelectedItem = productb[0];

                //bankcombo.SelectedIndex = 0;
                //  productcombo.SelectedIndex = 0;

            }
        }


        private async void runtheloadersourceBK19112024()
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

                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary​/get-beneficiary-bank-combo-list\n");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                //var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                var content = new StringContent("");

               // var content = new StringContent("{\n    \"productCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"disbMode\":\"" + ProductManager.selecteddispcode + "\",\n    \"destnCntry\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");
                if (addoreditvalue == "add")
                {

                

                 content = new StringContent("{\n    \"productCode\":\""+ ProductManager.selectedproductcode + "\",\n    \"transferMode\":\""+ProductManager.selecteddispcode+ "\",\n    \"countryCode\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}", null, "application/json");

                } else
                {
                    content = new StringContent("{\n    \"productCode\":\"" + BENE_PRODedit + "\",\n    \"transferMode\":\"" + DISBTYPEedit + "\",\n    \"countryCode\":\"" + BENE_CNTRYedit + "\"\n}", null, "application/json");
                }
               // MessageBox.Show(" " + BENE_PRODedit + " " + DISBTYPEedit+ " " + BENE_CNTRYedit);
               // MessageBox.Show(content.ToString());

                //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                //var content = new StringContent("", null, "text/plain");
                request.Content = content;

                var response = await client.SendAsync(request);
                //Send
                //var response = client.Send(request);
                response.EnsureSuccessStatusCode();

                var contentString = await request.Content.ReadAsStringAsync();
                string responseString = await response.Content.ReadAsStringAsync();
                RichMessageBox.Show("Request Data to api/api​/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + contentString);
                RichMessageBox.Show("Response from api/api​/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + responseString);


                // Parse the JSON response with JsonDocument
                //using (var responseStream = response.Content.ReadAsStream())
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                    }
                }

                if (bankcombo.Items.Count >= 1)
                //if (bankcombo.Items.Count > 1)
                {

                    runtheloaderdelivery();
                }
                //runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            //MessageBox.Show(bankcombo.Items.Count+"");
            // Select the first item (if any)
            if (bankcombo.Items.Count > 0)
            //    if (bankcombo.Items.Count > 1)
            {
                if(bankcombo.Items.Count == 1) { 
                bankcombo.IsEditable = false;

                } else
                {
                    bankcombo.IsEditable = true;
                }
                bankcombo.SelectedIndex = 1;
                
                //bankcombo.SelectedItem = productb[1];
                //bankcombo.SelectedItem = productb[0];

                //bankcombo.SelectedIndex = 0;
                //  productcombo.SelectedIndex = 0;

            }else
            {
                //MessageBox.Show("No Banks");
                bankdropdown.Visibility = Visibility.Hidden;
                branchdropdown.Visibility = Visibility.Hidden;
                bankvisible = "no";
                branchvisibile = "no";
            }
        }


        private void runtheloadersource()
        {
            try
            {
                var CPORBT = BCManager.selectedoptionborc;

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "​/api​/Beneficiary​/get-beneficiary-bank-combo-list/");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                string contentString;
                if (addoreditvalue == "add")
                {
                    contentString = "{\n  \"ProductCode\":\"" + ProductManager.selectedproductcode + "\",\n  \"TransferMode\":\"" + ProductManager.selecteddispcode + "\",\n  \"CountryCode\":\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n}";
                }
                else
                {
                    contentString = "{\n  \"ProductCode\":\"" + BENE_PRODedit + "\",\n  \"TransferMode\":\"" + DISBTYPEedit + "\",\n  \"CountryCode\":\"" + BENE_CNTRYedit + "\"\n}";
                }

                var content = new StringContent(contentString, Encoding.UTF8, "application/json");
                request.Content = content;

                var response = client.Send(request); // Synchronous call

                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException($"Request failed with status code: {response.StatusCode}");
                }

                var responseString = response.Content.ReadAsStringAsync().Result;

                RichMessageBox.Show("Request Data to api​/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + contentString);
                RichMessageBox.Show("Response from api​/Beneficiary​/get-beneficiary-bank-combo-list\n" + DateTime.Now + "\n" + responseString);

                using (var responseStream = response.Content.ReadAsStream())
                using (var doc = JsonDocument.Parse(responseStream))
                {
                    UpdateComboBoxsource(doc.RootElement);
                }

                if (bankcombo.Items.Count >= 1)
                {
                    runtheloaderdelivery();
                }

                if (bankcombo.Items.Count > 0)
                {
                    if (bankcombo.Items.Count == 1)
                    {
                        bankcombo.IsEditable = false;
                    }
                    else
                    {
                        bankcombo.IsEditable = true;
                    }
                    bankcombo.SelectedIndex = 1;
                }
                else
                {
                    bankdropdown.Visibility = Visibility.Hidden;
                    branchdropdown.Visibility = Visibility.Hidden;
                    bankvisible = "no";
                    branchvisibile = "no";
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
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
                    }
                }

               // runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (NationalityCOUNTRYcombo.Items.Count > 0)
            {
                //  productcombo.SelectedIndex = 0;

            }
        }

        List<NationalityCountry> productsc = new List<NationalityCountry>();
        private void UpdateNationilityComboBoxsource(JsonElement root)
        {
            //NationalityCOUNTRYcombo.Items.Clear();
            NationalityCOUNTRYcombo.ItemsSource = null;
            NationalityCOUNTRYcombo.Items.Clear();

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

                        productsc.Add(new NationalityCountry
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
            NationalityCOUNTRYcombo.ItemsSource = productsc;

            // Assuming products is a collection and has at least one item
            if (productsc.Count > 0)
            {
                NationalityCOUNTRYcombo.SelectedItem = productsc[0];
            }
            NationalityCOUNTRYcombo.DisplayMemberPath = "ConName";

            //SelectNationalityByConID("IN");
        }



        // Assuming `products` is a collection of `NationalityCountry` objects
        public void SelectNationalityByConID(string ConCode)
        {
            //MessageBox.Show
            //List<NationalityCountry> products = new List<NationalityCountry>();

            // Find the item in the collection by ConID
            var selectedItem = productsc.FirstOrDefault(p => p.ConCode == ConCode);

            if (selectedItem != null)
            {
                // Set the selected item in the ComboBox
                NationalityCOUNTRYcombo.SelectedItem = selectedItem;
            }
            else
            {
                //MessageBox.Show("Item not found.");
            }
        }

        // Assuming `products` is a collection of `NationalityCountry` objects
        public void SelectBankByConID(string ConCode)
        {
           // MessageBox.Show(ConCode);

            //MessageBox.Show(productb.ToString());

            // Read all items in productb (optional)
            if (productb.Any()) // Check if there are any items
            {
               // MessageBox.Show(string.Join(Environment.NewLine, productb.Select(b => $"BankID: {b.BankID}, BankName: {b.BankName}, BankCode: {b.BankCode}")));
            }
            else
            {
                //MessageBox.Show("No banks in productb collection.");
            }
            //List<NationalityCountry> products = new List<NationalityCountry>();

            // Find the item in the collection by ConID
            var selectedItem = productb.FirstOrDefault(p => p.BankCode == ConCode);

            if (selectedItem != null)
            {
                // Set the selected item in the ComboBox
                bankcombo.SelectedItem = selectedItem;
            }
            else
            {
              //  MessageBox.Show("Item not found.");
            }
        }

        public void SelectBranchByConID(string ConCode)
        {
             //MessageBox.Show(ConCode);

            //MessageBox.Show(productb.ToString());

            // Read all items in productb (optional)
            if (productsbb.Any()) // Check if there are any items
            {
                // MessageBox.Show(string.Join(Environment.NewLine, productsbb.Select(b => $"BankID: {b.BanksBanchID}, BankName: {b.BanksBanchName}, BankCode: {b.BanksBanchCode}")));
            }
            else
            {
                //MessageBox.Show("No banks in productb collection.");
            }
            //List<NationalityCountry> products = new List<NationalityCountry>();

            // Find the item in the collection by ConID
            var selectedItem = productsbb.FirstOrDefault(p => p.BanksBanchID == ConCode);

            if (selectedItem != null)
            {
                // Set the selected item in the ComboBox
                branchcombo.SelectedItem = selectedItem;
            }
            else
            {
               // MessageBox.Show("Item not found.");
            }
        }







        List<BanksC> productb = new List<BanksC>();
        private void UpdateComboBoxsource(JsonElement root)
        {
            //bankcombo.ItemsSource = null;
            bankcombo.Items.Clear();

            //List<BanksC> productb = new List<BanksC>();


          
            //}

            // Assuming "Data" is an array and contains "Productname" and "Productcode" properties
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("ID", out JsonElement productNameElement) &&
                        item.TryGetProperty("NAME", out JsonElement productCodeElement) &&
                        item.TryGetProperty("CODE", out JsonElement DisbursalModeCodeElement))

                    //DisbursalModeCode
                    {

                        //   MessageBox.Show(productCodeElement.GetString());
                        //   MessageBox.Show(productNameElement.GetString());

                        productb.Add(new BanksC
                        {
                            BankID = productNameElement.ToString(),
                            BankName = productCodeElement.ToString(),
                            BankCode = DisbursalModeCodeElement.ToString()
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
            bankcombo.ItemsSource = productb;

            // Assuming products is a collection and has at least one item
            if (productb.Count > 0)
            {
                bankcombo.SelectedItem = productb[0];
                //bankcombo.SelectedItem = productb[1];
            }
            bankcombo.DisplayMemberPath = "BankName";
            //runtheloaderdelivery();
        }
        public class NationalityCountry
        {
            public string ConID { get; set; }
            public string ConName { get; set; }
            public string ConCode { get; set; }
        }


        public class BanksC
        {
            public string BankID { get; set; }
            public string BankName { get; set; }
            public string BankCode { get; set; }
        }

        public class BanksBanchC
        {
            public string BanksBanchID { get; set; }
            public string BanksBanchName { get; set; }
            public string BanksBanchCode { get; set; }
        }

        // Create an empty list to store CoreFieldNames
        List<string> coreFieldNames = new List<string>();
        List<string> MANDATORYcoreFieldNames = new List<string>();

        // Access the "Data" array

        //= jsonDocument.RootElement.GetProperty("Data").EnumerateArray();

        public async Task LoadBenefieldseditmode(string BENE_PRODv, string DISBTYPEv, string BENE_CNTRYv, string COREDISBv)
        {

            //bankdropdown.Visibility = Visibility.Hidden;
            //branchdropdown.Visibility = Visibility.Hidden;
            try
            {






                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);


                var CPORBT = BCManager.selectedoptionborc;

                var content = new StringContent("{\n    \"productCode\":\"" + BENE_PRODv + "\",\n    \"disbursalMode\":\"" + DISBTYPEv + "\",\n    \"countryCode\":\"" + BENE_CNTRYv + "\",\n    \"action\":\"BEN\",\n    \"coredisbcode\":\"" + COREDISBv + "\"\n}", null, "application/json");
                request.Content = content;
                //var content = new StringContent("{ \n    \"remID\":130824,\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
                //request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();



                //var contentString = await request.Content.ReadAsStringAsync();
                //string responseString = await response.Content.ReadAsStringAsync();
                //RichMessageBox.Show("Request Data to api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct\n" + DateTime.Now + "\n" + contentString);
                //RichMessageBox.Show("Response from api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct\n" + DateTime.Now + "\n" + responseString);


                // MessageBox.Show((await response.Content.ReadAsStringAsync()));

                //  MessageBox.Show("Here 1");

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {

                    // MessageBox.Show("Here 2");

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

                        // if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CNTRY" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CURR" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH")


                        if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CNTRY" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CURR" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHCODE" || dataElement.GetProperty("IsVisible").GetBoolean() == false)
                        {
                            //SelectedAddBeneCountry

                            if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION")
                            {
                                SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                            }


                            if (BCManager.selectedoptionborc == "BT" || BCManager.selectedoptionborc == "CP")
                            {

                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID")
                                {
                                    //SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                    SelectBankByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                }
                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID")
                                {
                                    //SelectNationalityByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                    SelectBranchByConID(getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()));
                                    editmodebranch = getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString());

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

                            if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH")
                            {
                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" && bankvisible == "no")
                                {
                                    if (TokenManager.Langofsoft == "ar")
                                    {

                                        string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                        if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                        {
                                            displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                        }


                                        CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                    }
                                    else
                                    {
                                        string displaything = dataElement.GetProperty("DisplayText").GetString();

                                        if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                        {
                                            displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                        }

                                        CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                    }

                                    //MessageBox.Show(dataArrayedit.ToString());
                                    // Add the CoreFieldName to the list
                                    coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                                    }
                                }
                                if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH" && branchvisibile == "no")
                                {
                                    //MessageBox.Show(dataElement.GetProperty("FieldType").GetString());
                                    if (TokenManager.Langofsoft == "ar")
                                    {

                                        string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                        if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                        {
                                            displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                        }


                                        CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                    }
                                    else
                                    {
                                        string displaything = dataElement.GetProperty("DisplayText").GetString();

                                        if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                        {
                                            displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                        }

                                        CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                    }

                                    //MessageBox.Show(dataArrayedit.ToString());
                                    // Add the CoreFieldName to the list
                                    coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                                    }
                                }
                            }
                            else
                            {

                                //MessageBox.Show(dataElement.GetProperty("FieldType").GetString());
                                if (TokenManager.Langofsoft == "ar")
                                {

                                    string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                    }


                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                }
                                else
                                {
                                    string displaything = dataElement.GetProperty("DisplayText").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                    }

                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, getthefieldvalue(dataElement.GetProperty("CoreFieldName").GetString()), dataElement.GetProperty("FieldType").GetString());

                                }

                                //MessageBox.Show(dataArrayedit.ToString());
                                // Add the CoreFieldName to the list
                                coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                                if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                {
                                    MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                                }

                            }

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

        public string getthefieldvalue19112024(string fieldvalue)
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

        public async Task LoadBenefields()
        {

            //Thread.Sleep(2000);

            //MessageBox.Show(branchvisibile);





            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);


            var CPORBT = BCManager.selectedoptionborc;

            var content = new StringContent("{\n    \"productCode\":\"" + ProductManager.selectedproductcode + "\",\n    \"disbursalMode\":\"" + CPORBT + "\",\n    \"countryCode\":\"" + SelectedAddBeneCountry.seladdbenecount + "\",\n    \"action\":\"BEN\",\n    \"coredisbcode\":\"" + ProductManager.selecteddispcode + "\"\n}", null, "application/json");
            request.Content = content;
            //var content = new StringContent("{ \n    \"remID\":130824,\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            //request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            var contentString = await request.Content.ReadAsStringAsync();
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxGeneral/DefaultProduct/FieldSettingsbyProduct\n" + DateTime.Now + "\n" + responseString);

            // MessageBox.Show((await response.Content.ReadAsStringAsync()));

            //  MessageBox.Show("Here 1");

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {

               // MessageBox.Show("Here 2");

                // MessageBox.Show("Hi 1");
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);



                // MessageBox.Show("Hi 2");

                // Access root object (assuming it's an array) and iterate over its elements
                int counter = 0;
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {

                    // MessageBox.Show("Hi 3");

                    //  MessageBox.Show(dataElement.GetProperty("ConName").GetString());

                    //  dataElement.GetProperty("ConCode").GetString(),
                    //   dataElement.GetProperty("ConName").GetString(),

                    if(counter == 0)
                    {
                       // CreateUI("BENE_GENDER", "GENDER");
                       // CreateUI("BENE_NICKNAME", "Nickname", "");
                       // coreFieldNames.Add("BENE_NICKNAME");
                        //coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                    }
                    counter++;


                    //if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CNTRY" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CURR" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH")

                    if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_NATION" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CNTRY" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_CURR" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANKCODE" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHID" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCHCODE" || dataElement.GetProperty("IsVisible").GetBoolean() == false)
                    {
                        // || dataElement.GetProperty("IsVisible").GetBoolean() == false



                    }
                    else
                    {


                       // foreach (JsonElement dataElementa in dataArrayedit.EnumerateArray())
                        {
                            //MessageBox.Show(dataElementa.ToString());
                            // Check if the element has a property named "REM_ID"
                            // if (dataElement.TryGetProperty("BENE_FNAME", out JsonElement rem1IdElement))
                            //{
                            //REM_ID = remIdElement.ToString();
                            //     firstnameTextbox.Text = remIdElement.ToString();
                            //break; // Stop after finding the first REM_ID (optional)
                            //}
                            //MessageBox.Show(dataElement.TryGetProperty("BENE_FNAME", out JsonElement mdElement) ? mdElement.GetString() : "");


                            //LoadBenefieldseditmode(dataElement.TryGetProperty("BENE_PROD", out JsonElement mdElement) ? mdElement.GetString() : "", dataElement.TryGetProperty("DISBTYPE", out JsonElement md2Element) ? md2Element.GetString() : "", dataElement.TryGetProperty("BENE_CNTRY", out JsonElement md3Element) ? md3Element.GetString() : "", dataElement.TryGetProperty("COREDISB", out JsonElement md4Element) ? md4Element.GetString() : "");


                            //CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(), dataElementa.TryGetProperty(dataElement.GetProperty("CoreFieldName").GetString(), out JsonElement mdElement) ? mdElement.GetString() : "");



                        }


                        if(dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH")
                        {
                            if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" && bankvisible == "no")
                            {
                                //MessageBox.Show(dataElement.GetProperty("FieldType").GetString());
                                if (TokenManager.Langofsoft == "ar")
                                {

                                    string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                    }


                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                                }
                                else
                                {

                                    string displaything = dataElement.GetProperty("DisplayText").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                    }


                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                                }


                                // Add the CoreFieldName to the list
                                coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                                if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                {
                                    MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                                }
                            }
                            //MessageBox.Show("I am here" + dataElement.GetProperty("CoreFieldName").GetString() + " " + dataElement.GetProperty("DisplayText").GetString());
                            if(dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH" && branchvisibile == "no")
                            {

                                //MessageBox.Show(dataElement.GetProperty("FieldType").GetString());
                                if (TokenManager.Langofsoft == "ar")
                                {

                                    string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                    }


                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                                }
                                else
                                {

                                    string displaything = dataElement.GetProperty("DisplayText").GetString();

                                    if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                    {
                                        displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                    }


                                    CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                                }


                                // Add the CoreFieldName to the list
                                coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                                if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                {
                                    MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                                }
                            }
                        } else { 

                            //MessageBox.Show(dataElement.GetProperty("FieldType").GetString());
                            if (TokenManager.Langofsoft == "ar")
                            {

                                string displaything = dataElement.GetProperty("ArabicDisplay").GetString();

                                if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                {
                                    displaything = dataElement.GetProperty("ArabicDisplay").GetString() + " *";
                                }


                                CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                            }
                            else
                            {

                                string displaything = dataElement.GetProperty("DisplayText").GetString();

                                if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                                {
                                    displaything = dataElement.GetProperty("DisplayText").GetString() + " *";
                                }


                                CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), displaything, "", dataElement.GetProperty("FieldType").GetString());
                            }
                        

                            // Add the CoreFieldName to the list
                            coreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());

                            if (dataElement.GetProperty("IsMandatory").GetBoolean() == true)
                            {
                                MANDATORYcoreFieldNames.Add(dataElement.GetProperty("CoreFieldName").GetString());
                            }
                        }

                    }




                    //CreateUI(dataElement.GetProperty("CoreFieldName").GetString(), dataElement.GetProperty("DisplayText").GetString(),"");

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

        private void bankselectionchanged(object sender, SelectionChangedEventArgs e)
        {
            // MessageBox.Show(productcombo.Text);
            if (bankcombo.SelectedItem != null && bankcombo.Text != "")
            {
                // MessageBox.Show("Hi" + productcombo.SelectedItem.ToString);
                runtheloaderdelivery();
                // string selectedItemName = ((ComboBoxItem)productcombo.SelectedItem).Content.ToString();
                // MessageBox.Show(selectedItemName);
            }


            // Assuming productcombo is your ComboBox control
            var selectedItem = bankcombo.SelectedItem;

            // If the ComboBox is bound to a list of strings
            if (selectedItem is string selectedTexta)
            {
                //MessageBox.Show(selectedTexta);
                //runtheloaderdelivery();
            }
            else
            {
                // If the ComboBox is bound to a list of objects,
                // you might need to handle it differently depending on the type
                // For example:
                // MessageBox.Show(selectedItem.ToString());
            }
        }

        private async void runtheloaderdeliveryBACKUP19112024()
        {

            var CPORBT = BCManager.selectedoptionborc;

           // MessageBox.Show("Branch 1");
            if ((BanksC)bankcombo.SelectedItem != null)
            {
                //MessageBox.Show("Branch 2");

                // Get the selected product
                BanksC selectedProduct = (BanksC)bankcombo.SelectedItem;
                var Banid = selectedProduct.BankID;
                var bancode = selectedProduct.BankCode;
                //MessageBox.Show(CPORBT);
                try
                {
                    var client = new HttpClient();

                  
                   // var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct/Disbmodes?ProductCode=" + PRODCIDE + "&MobDisbcode=" + CPORBT);
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxbeneficiary/BeneBank/Branch");
                    request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                    //var content = new StringContent("{\r\n  \"BankID\": " + Banid + ",\r\n  \"BankCode\": \"" + bancode + "\",\r\n  \"DestnCtry\": \"" + SelectedAddBeneCountry.seladdbenecount + "\",\r\n  \"ProductCode\": \"" + ProductManager.selectedproductcode + "\",\r\n  \"DisbursalMode\": \"" + ProductManager.selecteddispcode + "\"\r\n}", null, "application/json");

                    var content = new StringContent("");
                    if (addoreditvalue == "add")
                    {
                         content = new StringContent("{\r\n  \"BankID\": " + Banid + ",\r\n  \"BankCode\": \"" + bancode + "\",\r\n  \"DestnCtry\": \"" + SelectedAddBeneCountry.seladdbenecount + "\",\r\n  \"ProductCode\": \"" + ProductManager.selectedproductcode + "\",\r\n  \"DisbursalMode\": \"" + ProductManager.selecteddispcode + "\"\r\n}", null, "application/json");
                    }

                    if(addoreditvalue == "edit")
                    {
                        content = new StringContent("{\r\n  \"BankID\": " + Banid + ",\r\n  \"BankCode\": \"" + bancode + "\",\r\n  \"DestnCtry\": \"" + BENE_CNTRYedit + "\",\r\n  \"ProductCode\": \"" + BENE_PRODedit + "\",\r\n  \"DisbursalMode\": \"" + DISBTYPEedit + "\"\r\n}", null, "application/json");
                    }
                   







                    // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                    //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                    //var content = new StringContent("", null, "text/plain");
                    request.Content = content;

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();


                    var contentString = await request.Content.ReadAsStringAsync();
                    string responseString = await response.Content.ReadAsStringAsync();
                    RichMessageBox.Show("Request Data to api/v1/sxbeneficiary/BeneBank/Branch\n" + DateTime.Now + "\n" + contentString);
                    RichMessageBox.Show("Response from api/v1/sxbeneficiary/BeneBank/Branch\n" + DateTime.Now + "\n" + responseString);

                    // Parse the JSON response with JsonDocument
                    using (var responseStream = await response.Content.ReadAsStreamAsync())
                    {
                        using (var doc = JsonDocument.Parse(responseStream))
                        {
                            //UpdateComboBoxdelivery(doc.RootElement);
                            UpdateComboBoxBranch(doc.RootElement);
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error sending request: {ex.Message}");
                }

                // Select the first item (if any)
                if (branchcombo.Items.Count > 0)
                {
                    branchcombo.SelectedIndex = 0;

                } else
                {

                   // MessageBox.Show("No Branch To Load");
                    branchvisibile = "no";
                    branchdropdown.Visibility = Visibility.Hidden;
                }
            }
        }

        private void runtheloaderdelivery()
        {
            // Capture current selection and API details
            var selectedProduct = (BanksC)bankcombo.SelectedItem;
            var banid = selectedProduct?.BankID;
            var bancode = selectedProduct?.BankCode;
            var apiUrl = "http://" + Variable.apiipadd + "/api/v1/sxbeneficiary/BeneBank/Branch";
            var token = TokenManager.Token;

            if (selectedProduct == null)
            {
                // Handle no product selected gracefully
                Console.WriteLine("No product selected. Please select a product.");
                return;
            }

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    var content = new StringContent("");
                    if (addoreditvalue == "add")
                    {
                        content = new StringContent(
                            $"{{\"BankID\": {banid}, \"BankCode\": \"{bancode}\", \"DestnCtry\": \"{SelectedAddBeneCountry.seladdbenecount}\", \"ProductCode\": \"{ProductManager.selectedproductcode}\", \"DisbursalMode\": \"{ProductManager.selecteddispcode}\"}}",
                            Encoding.UTF8,
                            "application/json");
                    }
                    else if (addoreditvalue == "edit")
                    {
                        content = new StringContent(
                            $"{{\"BankID\": {banid}, \"BankCode\": \"{bancode}\", \"DestnCtry\": \"{BENE_CNTRYedit}\", \"ProductCode\": \"{BENE_PRODedit}\", \"DisbursalMode\": \"{DISBTYPEedit}\"}}",
                            Encoding.UTF8,
                            "application/json");
                    }

                    var request = new HttpRequestMessage(HttpMethod.Post, apiUrl)
                    {
                        Content = content
                    };

                    var response = client.SendAsync(request).Result; // Blocking operation, use with caution

                    response.EnsureSuccessStatusCode(); // Throw exception for non-2xx status codes

                    var responseString = response.Content.ReadAsStringAsync().Result;

                    // Handle response data
                    //RichMessageBox.Show($"Request Data to {apiUrl}\n{DateTime.Now}\n{content}");
                    //RichMessageBox.Show($"Response from {apiUrl}\n{DateTime.Now}\n{responseString}");

                    // Parse the JSON response with JsonDocument (if needed)
                    using (var responseStream = response.Content.ReadAsStreamAsync().Result)
                    {
                        using (var doc = JsonDocument.Parse(responseStream))
                        {
                            UpdateComboBoxBranch(doc.RootElement);
                        }
                    }
                }

                // Update UI based on branch availability
                if (branchcombo.Items.Count > 0)
                {
                    branchcombo.SelectedIndex = 0;
                }
                else
                {
                    branchvisibile = "no";
                    branchdropdown.Visibility = Visibility.Hidden;
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
                // Handle exception appropriately (e.g., display user-friendly message)
            }
            catch (Exception ex) // Catch broader exceptions for unexpected errors
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                // Handle general exceptions (e.g., logging, user notification)
            }
        }

        private void UpdateComboBoxdelivery(JsonElement root)
        {
            // Clear existing items (optional)
            branchcombo.Items.Clear();


            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("NAME", out JsonElement purpNameElement))
                    {
                        branchcombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }

        List<BanksBanchC> productsbb = new List<BanksBanchC>();
        private void UpdateComboBoxBranch(JsonElement root)
        {
            branchcombo.ItemsSource = null;
            branchcombo.Items.Clear();

            //List<BanksBanchC> products = new List<BanksBanchC>();



            //}

            // Assuming "Data" is an array and contains "Productname" and "Productcode" properties
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("ID", out JsonElement productNameElement) &&
                        item.TryGetProperty("NAME", out JsonElement productCodeElement) &&
                        item.TryGetProperty("CODE", out JsonElement DisbursalModeCodeElement))

                    //DisbursalModeCode
                    {

                        //   MessageBox.Show(productCodeElement.GetString());
                        //   MessageBox.Show(productNameElement.GetString());

                        productsbb.Add(new BanksBanchC
                        {
                            BanksBanchID = productNameElement.ToString(),
                            BanksBanchName = productCodeElement.ToString(),
                            BanksBanchCode = DisbursalModeCodeElement.ToString()
                        });
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }

            //Bind the ComboBox to the products list
            branchcombo.ItemsSource = productsbb;

            // Assuming products is a collection and has at least one item
            if (productsbb.Count > 0)
            {
                branchcombo.SelectedItem = productsbb[0];
            }
            branchcombo.DisplayMemberPath = "BanksBanchName";
        }





    }
}
