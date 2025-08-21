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

        List<BanksBanchC> productsbb = new List<BanksBanchC>();

        List<NationalityCountry> productsc = new List<NationalityCountry>();

        List<BanksC> productb = new List<BanksC>();

        JsonElement dataArrayedit;

        string looperjson = "";

        private JsonDocument jsonDocument;

        string BENE_PRODedit = "";

        string DISBTYPEedit = "";

        string BENE_CNTRYedit = "";

        string BENE_CURRedit = "";

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

        private void backbutton(object sender, RoutedEventArgs e)
        {

            wSelectbeneficary wsel = new wSelectbeneficary();
            NavigationService.Navigate(wsel);

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
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
                    Label lab = FindlabelBoxByName(myStackPanel, coreFieldName + "label");
                    if (textBox != null)
                    {
                        // Get the TextBox value
                        string value = textBox.Text;

                        if (value == null || value == "")
                        {
                            MessageBox.Show("" + lab.Content + " is Mandatory !");
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
                        else
                        {

                            looperjson += "\"" + coreFieldName + "\"" + " : \"" + value + "\",\n";

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public async void createbene()
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Beneficiary/update-beneficiary");
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

                    }
                    catch (Exception ex)
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

                    if (bankksid == "")
                    {
                        looperjson += "\"BENE_BANKID\"" + " : \"001\",\n";
                    }
                    else
                    {
                        looperjson += "\"BENE_BANKID\"" + " : \"" + bankksid + "\",\n";
                    }

                    if (bankkscode == "")
                    {
                        looperjson += "\"BENE_BANKCODE\"" + " : \"001\",\n";
                    }
                    else
                    {
                        looperjson += "\"BENE_BANKCODE\"" + " : \"" + bankkscode + "\",\n";
                    }

                    if (banbchesid == "")
                    {
                        looperjson += "\"BENE_BRANCHID\"" + " : \"001\",\n";
                    }
                    else
                    {
                        looperjson += "\"BENE_BRANCHID\"" + " : \"" + banbchesid + "\",\n";
                    }

                    if (banbchescode == "")
                    {
                        looperjson += "\"BENE_BRANCHCODE\"" + " : \"001\",\n";
                    }
                    else
                    {
                        looperjson += "\"BENE_BRANCHCODE\"" + " : \"" + banbchescode + "\",\n";
                    }

                    if (bankvisible == "yes")
                    {
                        looperjson += "\"BENE_BANK\"" + " : \"" + banknameis + "\",\n";
                    }
                    if (branchvisibile == "yes")
                    {
                        looperjson += "\"BENE_BRANCH\"" + " : \"" + branchnameis + "\",\n";
                    }


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

                var content = new StringContent("{\n  \"BENE_SLNO\": " + beneserialno + ",\n  \"BENE_DISB\": \"" + beneBENE_DISB + "\",\n  \"BENE_REMID\": " + LoginManager.Remiduser + ", \n  \"BENE_GENDER\": \"M\",\n" +
                    "" +
                    "" + looperjson +
                    //"\n  \"BENE_FNAME\": \"BURHAN\",\n  \"BENE_MNAME\": \"\",\n  \"BENE_LNAME\": \"ALI\",\n  \"BENE_ADDRESS1\": \"ADD1\",\n  \"BENE_ADDRESS2\": \"\",\n  \"BENE_CITY\": \"\",  \n  \"BENE_STATE\": \"\",  \n  \"BENE_CNTRY\": \"BD\",\n  \"BENE_NATION\": \"BD\",\n  \"BENE_MOBILE\":\"5656456457\",\n  \"BENE_PROD\": \"TF\",\n  \"BENE_CURR\": \"BDT\"," +
                    //"" +
                    //"" +
                    //"" +
                    //"" +
                    "" +
                   "\"BENE_CNTRY\": \"" + beneBENE_CNTRY + "\",\n" +
                    "\"BENE_PROD\": \"" + beneBENE_PROD + "\",\n  \"BENE_CURR\": \"" + beneBENE_CURR + "\",\n \"appID\": 3,\n  \"moduleID\": 3,\n  \"BENE_CHANNEL\": \"kiosk\",\n  \"BENE_DISBTYPE\": \"" + BCManager.selectedoptionborc + "\"\n}", null, "application/json");
                // "\n\"BENE_BRIFSCCODE\": \"IFCNR123456789\"," +
                //BENE_CURR
                //  BENE_PROD TF  SelectedAddBeneCountry

                //BENE_CHANNEL Channel ID Pass the Code from the API response / api / v{ v}/ AppAuth

                string jsonContent = content.ReadAsStringAsync().Result; // Replace with content.Encoding if known
                                                                         //MessageBox.Show(jsonContent);
                looperjson = "";
                //string jsonContent = content;

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

                if (code == "0")
                {
                    MessageBox.Show("Saved Sucessfully");
                    //wBeneficiary welocmepage = new wBeneficiary();
                    //NavigationService.Navigate(welocmepage);

                    wSelectbeneficary wsel = new wSelectbeneficary();
                    NavigationService.Navigate(wsel);
                }
                else
                {
                    // MessageBox.Show(jsonContent);
                    // MessageBox.Show(await response.Content.ReadAsStringAsync());
                    MessageBox.Show(Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private static TextBox FindTextBoxByName(DependencyObject parent, string name)
        {
            try
            {
                if (parent == null)
                    return null;

                if (parent is TextBox textBox && textBox.Name == name)
                    return textBox;

                var childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; i < childrenCount; i++)
                {
                    var child = VisualTreeHelper.GetChild(parent, i) as DependencyObject;
                    var foundTextBox = FindTextBoxByName(child, name);
                    if (foundTextBox != null)
                        return foundTextBox;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;

        }

        private static Label FindlabelBoxByName(DependencyObject parent, string name)
        {
            try
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;

        }

        private void CreateUI(string fieldname, string fielddisplayname, string textvaluein, string typeoffield)
        {
            try
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
                    Name = fieldname + "label",
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
            //
        }

        private async void Page_Load(object sender, RoutedEventArgs e)
        {

            try
            {
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

                    _ = new DisposableTimer(() => SelectBranchByConID(editmodebranch), 1);

                    //RUN THE CODE AFTER 3 SEOCNDS

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        public async void loadbenefieldstoedit()
        {
            try
            {
                var client = new HttpClient();

                // Construct the GET URL with eId
                var url = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-by-id" + SelectedBeneficiaryManager.BENE_SLNO);
                var request = new HttpRequestMessage(HttpMethod.Get, url.RequestUri);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("Accept", "text/plain");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseBody = await response.Content.ReadAsStringAsync();
                jsonDocument = JsonDocument.Parse(responseBody);
                JsonElement root = jsonDocument.RootElement;
                JsonElement dataElement = root.GetProperty("data").GetProperty("beneficiary_by_id");
                BENE_PRODedit = dataElement.TryGetProperty("product_name", out JsonElement prodElement) ? prodElement.GetString() : "";
                DISBTYPEedit = dataElement.TryGetProperty("disbursal_mode_name", out JsonElement disbElement) ? disbElement.GetString() : "";
                BENE_CNTRYedit = dataElement.TryGetProperty("beneficiary_country_name", out JsonElement cntryElement) ? cntryElement.GetString() : "";
                BENE_CURRedit = dataElement.TryGetProperty("beneficiary_country_code", out JsonElement currElement) ? currElement.GetString() : "";

                LoadBenefieldseditmode(
                    BENE_PRODedit,
                    DISBTYPEedit,
                    BENE_CNTRYedit,
                    DISBTYPEedit // This replaces "COREDISB" which doesn't exist in new response
                );

                runtheloadersource();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
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
                var request = new HttpRequestMessage(HttpMethod.Get, "http://" + Variable.apiipadd + "/api/Customer​/get-country-combo-list\r\n");
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

        private void UpdateNationilityComboBoxsource(JsonElement root)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        public void SelectNationalityByConID(string ConCode)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        public void SelectBankByConID(string ConCode)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        public void SelectBranchByConID(string ConCode)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void UpdateComboBoxsource(JsonElement root)
        {
            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            //bankcombo.ItemsSource = null;
            
        }

        public async Task LoadBenefieldseditmode(string BENE_PRODv, string DISBTYPEv, string BENE_CNTRYv, string COREDISBv)
        {

            //bankdropdown.Visibility = Visibility.Hidden;
            //branchdropdown.Visibility = Visibility.Hidden;
            try
            {
                var client = new HttpClient();
                var baseUrl = "http://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-by-id";

                // Replace with actual values
                string productCode = BENE_PRODv;
                string disbursalMode = COREDISBv;
                string memberSection = "Beneficiary";
                string countryCode = BENE_CNTRYv;
                string destinationCurrencyCode = "INR";
                string language = "EN";
                string token = TokenManager.Token; // Replace with actual token retrieval

                // Construct the full URL with query parameters
                string url = $"{baseUrl}?ProductCode={productCode}&DisbursalModeCode={disbursalMode}&MemberSection={memberSection}&CountryCode={countryCode}&DestinationCurrencyCode={destinationCurrencyCode}&Language={language}";

                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Headers.Add("Accept", "text/plain");

                // Send the request
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Read the response
                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);



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

        public string getthefieldvalue(string fieldvalue)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return null;
        }

        public async Task LoadBenefields()
        {
            try
            {
                //Thread.Sleep(2000);

                //MessageBox.Show(branchvisibile);
                var client = new HttpClient();
                var baseUrl = "http://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-by-id";

                // Replace with actual values
                string productCode = "";
                string disbursalMode = " ";
                string memberSection = "Beneficiary";
                string countryCode = "";
                string destinationCurrencyCode = "INR";
                string language = "EN";
                string token = TokenManager.Token; // Replace with actual token retrieval

                // Construct the full URL with query parameters
                string url = $"{baseUrl}?ProductCode={productCode}&DisbursalModeCode={disbursalMode}&MemberSection={memberSection}&CountryCode={countryCode}&DestinationCurrencyCode={destinationCurrencyCode}&Language={language}";

                // Create the request
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Headers.Add("Accept", "text/plain");

                // Send the request
                var response = await client.SendAsync(request);


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

                        if (counter == 0)
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


                            if (dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BANK" || dataElement.GetProperty("CoreFieldName").GetString() == "BENE_BRANCH")
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

                    }
                }

                //  countryListView.ItemsSource = Countries;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
           
        }

        private void bankselectionchanged(object sender, SelectionChangedEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        private void runtheloaderdelivery()
        {
            try
            {
                var selectedProduct = (BanksC)bankcombo.SelectedItem;
                var banid = selectedProduct?.BankID;
                var bancode = selectedProduct?.BankCode;
                var apiUrl = $"http://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-branch-combo-list";

                var token = TokenManager.Token;

                if (selectedProduct == null)
                {
                    Console.WriteLine("No product selected. Please select a product.");
                    return;
                }

                try
                {
                    using (var client = new HttpClient())
                    {
                        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                        string jsonBody;
                        if (addoreditvalue == "add")
                        {
                            jsonBody = $"{{\"BankID\": {banid}, \"BankCode\": \"{bancode}\", \"DestnCtry\": \"{SelectedAddBeneCountry.seladdbenecount}\", \"ProductCode\": \"{ProductManager.selectedproductcode}\", \"DisbursalMode\": \"{ProductManager.selecteddispcode}\"}}";
                        }
                        else
                        {
                            jsonBody = $"{{\"BankID\": {banid}, \"BankCode\": \"{bancode}\", \"DestnCtry\": \"{BENE_CNTRYedit}\", \"ProductCode\": \"{BENE_PRODedit}\", \"DisbursalMode\": \"{DISBTYPEedit}\"}}";
                        }

                        var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
                        var request = new HttpRequestMessage(HttpMethod.Get, apiUrl) { Content = content };
                        var response = client.SendAsync(request).Result;

                        response.EnsureSuccessStatusCode();

                        var responseString = response.Content.ReadAsStringAsync().Result;

                        using (var doc = JsonDocument.Parse(responseString))
                        {
                            if (doc.RootElement.TryGetProperty("data", out var dataElement) &&
                                dataElement.TryGetProperty("beneficiary_branch_list", out var branchListElement) &&
                                branchListElement.ValueKind == JsonValueKind.Array)
                            {
                                branchcombo.Items.Clear();

                                foreach (var branch in branchListElement.EnumerateArray())
                                {
                                    var code = branch.GetProperty("code").GetString();
                                    var name = branch.GetProperty("name").GetString();
                                    var isDefault = branch.GetProperty("is_default").GetBoolean();

                                    branchcombo.Items.Add($"{name} ({code})");

                                    if (isDefault)
                                        branchcombo.SelectedItem = $"{name} ({code})";
                                }

                                if (branchcombo.Items.Count > 0 && branchcombo.SelectedIndex == -1)
                                    branchcombo.SelectedIndex = 0;
                                else if (branchcombo.Items.Count == 0)
                                {
                                    branchvisibile = "no";
                                    branchdropdown.Visibility = Visibility.Hidden;
                                }
                            }
                            else
                            {
                                Console.WriteLine("Invalid API response format.");
                            }
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    Console.WriteLine($"Error sending request: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Unexpected error: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

    }
}
