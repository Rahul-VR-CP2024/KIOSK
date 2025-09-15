using Exchange.Common;
using Exchange.Managers;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.ServiceModel;
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

        // Create an empty list to store CoreFieldNames
        List<string> coreFieldNames = new List<string>();

        List<string> MANDATORYcoreFieldNames = new List<string>();

        List<BanksBanchC> productsbb = new List<BanksBanchC>();

        List<NationalityCountry> productsc = new List<NationalityCountry>();

        List<BanksC> productb = new List<BanksC>();

        private Dictionary<string, Control> fieldControls = new Dictionary<string, Control>();


        public class DropdownItem
        {
            public string code { get; set; }
            public string name { get; set; }

            public override string ToString() => name; // for ComboBox display
        }

        private List<DropdownItem> nationalityList = new();
        private List<DropdownItem> relationshipList = new();
        private List<DropdownItem> beneficiaryBankList = new();
        private List<DropdownItem> beneficiaryBranchList = new();

        JsonElement dataArrayedit;

        string looperjson = "";

        private JsonDocument jsonDocument;

        string BENE_PRODedit = "";

        string DISBTYPEedit = "";

        string BENE_CNTRYedit = "";

        string BENE_CURRedit = "";

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

                if (addoreditvalue == "add")
                {
                    await LoadDropdownData();

                    LoadBenefields();
                }

                if (addoreditvalue == "edit")
                {
                    await LoadDropdownData();

                   await LoadBenefields();

                    await loadbenefieldstoedit();

                    _ = new DisposableTimer(() => SelectBranchByConID(editmodebranch), 1);

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void runtheloadersource()
        {
            try
            {
                string productCode, transferMode, countryCode;

                if (addoreditvalue == "add")
                {
                    productCode = ProductManager.selectedproductcode;
                    transferMode = ProductManager.selecteddispcode;
                    countryCode = SelectedAddBeneCountry.seladdbenecount;
                }
                else
                {
                    productCode = BENE_PRODedit;
                    transferMode = DISBTYPEedit;
                    countryCode = BENE_CNTRYedit;
                }

                // Build API URL with query params
                string url = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-bank-combo-list" + $"?ProductCode={productCode}&TransferMode={transferMode}&CountryCode={countryCode}";

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                try
                {
                    var response = client.Send(request);
                    string respString = await response.Content.ReadAsStringAsync();

                    response.EnsureSuccessStatusCode();
                    using (var doc = JsonDocument.Parse(respString))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }

                if (bankcombo.Items.Count >= 1)
                {
                    runtheloaderdelivery();
                }

                if (bankcombo.Items.Count > 0)
                {
                    bankcombo.IsEditable = bankcombo.Items.Count > 1;
                    bankcombo.SelectedIndex = 0;
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
                MessageBox.Show(ex.Message.ToString());
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
                List<BanksC> productb = new List<BanksC>();

                if (root.TryGetProperty("data", out JsonElement dataElement) &&
                    dataElement.TryGetProperty("beneficiary_bank_list", out JsonElement bankListElement) &&
                    bankListElement.ValueKind == JsonValueKind.Array)
                {
                    bankcombo.Items.Clear();

                    foreach (var item in bankListElement.EnumerateArray())
                    {
                        string name = item.TryGetProperty("name", out JsonElement nameEl) ? nameEl.GetString() ?? "" : "";
                        string code = item.TryGetProperty("code", out JsonElement codeEl) ? codeEl.GetString() ?? "" : "";
                        //bool isDefault = item.TryGetProperty("is_default", out JsonElement defEl) && defEl.GetBoolean();
                        productb.Add(new BanksC
                        {
                            BankName = name,
                            BankCode = code,
                            BankID = code   // no e_id in response, so marking default
                        });
                    }
                }
                else
                {
                    Console.WriteLine("Invalid JSON response structure");
                }

                bankcombo.ItemsSource = productb;

                if (productb.Count > 0)
                {
                    bankcombo.SelectedItem = productb[0];
                }
                bankcombo.DisplayMemberPath = "BankName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void runtheloaderdelivery()
        {
            try
            {
                var selectedProduct = (BanksC)bankcombo.SelectedItem;
                if (selectedProduct == null)
                {
                    Console.WriteLine("No bank selected. Please select a bank.");
                    return;
                }

                var bankCode = selectedProduct.BankCode;
                string productCode = (addoreditvalue == "add")
                    ? ProductManager.selectedproductcode
                    : BENE_PRODedit;

                // Build API URL with query params
                string apiUrl = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-branch-combo-list" + $"?ProductCode={productCode}&BankCode={bankCode}";

                var token = TokenManager.Token;
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

                    var response = await client.GetAsync(apiUrl);
                    string responseString = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    using (var doc = JsonDocument.Parse(responseString))
                    {
                        if (doc.RootElement.TryGetProperty("data", out var dataElement) &&
                            dataElement.TryGetProperty("beneficiary_branch_list", out var branchListElement) &&
                            branchListElement.ValueKind == JsonValueKind.Array)
                        {
                            branchcombo.Items.Clear();
                            foreach (var branch in branchListElement.EnumerateArray())
                            {
                                string code = branch.GetProperty("code").GetString() ?? "";
                                string name = branch.GetProperty("name").GetString() ?? "";
                                bool isDefault = branch.GetProperty("is_default").GetBoolean();

                                string display = $"{name} ({code})";
                                branchcombo.Items.Add(display);

                                if (isDefault)
                                    branchcombo.SelectedItem = display;
                            }
                            if (branchcombo.Items.Count > 0 && branchcombo.SelectedIndex == -1)
                            {
                                branchcombo.SelectedIndex = 0; // select first by default
                            }
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
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private async void runtheloaderfornationalitycountries()
        {

            var CPORBT = BCManager.selectedoptionborc;
            //MessageBox.Show(CPORBT);
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(
                    HttpMethod.Get, "https://" + Variable.apiipadd + "/api/Customer/get-country-combo-list"
                );
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("accept", "text/plain");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateNationilityComboBoxsource(doc);
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

        }

        public async Task LoadBenefields()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    string baseUrl = "https://" + Variable.apiipadd + "/api/Beneficiary/get-all-product-field-settings";

                    string productCode = ProductManager.selectedproductcode;
                    string disbursalMode = BCManager.selectedoptionborc;
                    string memberSection = "Beneficiary";
                    string countryCode = SelectedAddBeneCountry.seladdbenecount;
                    string destinationCurrencyCode = ProductManager.selectedProdCurrCode;
                    string language = "EN";
                    string token = TokenManager.Token;

                    string url = $"{baseUrl}?ProductCode={productCode}&DisbursalModeCode={disbursalMode}&MemberSection={memberSection}&CountryCode={countryCode}&DestinationCurrencyCode={destinationCurrencyCode}&Language={language}";

                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", "Bearer " + token);
                    request.Headers.Add("Accept", "application/json");
                    var response = await client.SendAsync(request);

                    response.EnsureSuccessStatusCode();

                    string jsonString = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(jsonString);

                    var fields = json["data"]?["all_product_field_setting_list"]?["beneficiary"];
                    if (fields == null)
                    {
                        MessageBox.Show("No fields found in API.");
                        return;
                    }

                    myStackPanel.Children.Clear(); // Clear previous dynamic UI

                    foreach (var field in fields)
                    {
                        string fieldName = field["field_name"]?.ToString();
                        string label = field["display_field_name"]?.ToString();
                        string type = field["type"]?.ToString();
                        bool mandatory = field["mandatory"]?.ToObject<bool>() ?? false;
                        bool visible = field["visible"]?.ToObject<bool>() ?? true;

                        if (!visible) continue;

                        // Add a * to mandatory fields
                        if (mandatory) label += " *";

                        AddDynamicField(fieldName, label, type, mandatory);
                    }

                    //using (var responseStream = await response.Content.ReadAsStreamAsync())
                    //{
                    //    var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                    //    if (jsonDocument.RootElement.TryGetProperty("data", out var dataElement) &&
                    //        dataElement.TryGetProperty("all_product_field_setting_list", out var allFieldsElement) &&
                    //        allFieldsElement.TryGetProperty("beneficiary", out var beneficiaryArray) &&
                    //        beneficiaryArray.ValueKind == JsonValueKind.Array)
                    //    {
                    //        foreach (var field in beneficiaryArray.EnumerateArray())
                    //        {
                    //            string fieldName = field.GetProperty("field_name").GetString();
                    //            string displayName = field.GetProperty("display_field_name").GetString();
                    //            string type = field.GetProperty("type").GetString();
                    //            bool mandatory = field.GetProperty("mandatory").GetBoolean();
                    //            bool visible = field.GetProperty("visible").GetBoolean();
                    //            if (!visible)
                    //                continue;

                    //            // Append * if mandatory
                    //            if (mandatory)
                    //                displayName += " *";

                    //            // Create UI control
                    //            CreateUI(fieldName, displayName, "", type);

                    //            // Track field names
                    //            coreFieldNames.Add(fieldName);
                    //            if (mandatory)
                    //                MANDATORYcoreFieldNames.Add(fieldName);

                    //        }
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Invalid API response format.");
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        //public async void loadbenefieldstoedit()
        //{
        //    try
        //    {
        //        using var client = new HttpClient();


        //        var url = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-by-id?eId={SelectedBeneficiaryManager.BENE_EID}";

        //        var request = new HttpRequestMessage(HttpMethod.Get, url);
        //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", TokenManager.Token);
        //        request.Headers.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        //MessageBox.Show(url); // Debug check

        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();

        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        using var jsonDocument = JsonDocument.Parse(responseBody);

        //        JsonElement root = jsonDocument.RootElement;
        //        JsonElement dataElement = root.GetProperty("data").GetProperty("beneficiary_by_id");

        //        BENE_PRODedit = dataElement.TryGetProperty("product_name", out var prodElement) ? prodElement.GetString() : "";
        //        DISBTYPEedit = dataElement.TryGetProperty("disbursal_mode_name", out var disbElement) ? disbElement.GetString() : "";
        //        BENE_CNTRYedit = dataElement.TryGetProperty("beneficiary_country_name", out var cntryElement) ? cntryElement.GetString() : "";
        //        BENE_CURRedit = dataElement.TryGetProperty("beneficiary_country_code", out var currElement) ? currElement.GetString() : "";

        //        // Load into edit fields
        //        LoadBenefieldseditmode(
        //            BENE_PRODedit,
        //            DISBTYPEedit,
        //            BENE_CNTRYedit,
        //            DISBTYPEedit // You might want another property here instead of reusing
        //        );

        //        //runtheloadersource();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        //public async Task loadbenefieldstoedit()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        // Construct the GET URL with eId
        //        var url = new HttpRequestMessage(HttpMethod.Post, "https://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-by-id" + SelectedBeneficiaryManager.BENE_SLNO);
        //        var request = new HttpRequestMessage(HttpMethod.Get, url.RequestUri);
        //        request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
        //        request.Headers.Add("Accept", "text/plain");
        //        MessageBox.Show(url.RequestUri.ToString());
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var responseBody = await response.Content.ReadAsStringAsync();
        //        jsonDocument = JsonDocument.Parse(responseBody);
        //        JsonElement root = jsonDocument.RootElement;
        //        JsonElement dataElement = root.GetProperty("data").GetProperty("beneficiary_by_id");
        //        BENE_PRODedit = dataElement.TryGetProperty("product_name", out JsonElement prodElement) ? prodElement.GetString() : "";
        //        DISBTYPEedit = dataElement.TryGetProperty("disbursal_mode_name", out JsonElement disbElement) ? disbElement.GetString() : "";
        //        BENE_CNTRYedit = dataElement.TryGetProperty("beneficiary_country_name", out JsonElement cntryElement) ? cntryElement.GetString() : "";
        //        BENE_CURRedit = dataElement.TryGetProperty("beneficiary_country_code", out JsonElement currElement) ? currElement.GetString() : "";

        //        LoadBenefieldseditmode(
        //            BENE_PRODedit,
        //            DISBTYPEedit,
        //            BENE_CNTRYedit,
        //            DISBTYPEedit // This replaces "COREDISB" which doesn't exist in new response
        //        );

        //        runtheloadersource();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        public async Task loadbenefieldstoedit()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // 1️⃣ Fetch the specific beneficiary data using eId
                    var url = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-by-id?eId={SelectedBeneficiaryManager.BENE_EID}";
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();
                    var json = JObject.Parse(responseBody);
                    var data = json["data"]?["beneficiary_by_id"];

                    if (data == null)
                    {
                        MessageBox.Show("No beneficiary data found.");
                        return;
                    }

                    // 2️⃣ Convert JSON data to dictionary: fieldName → value
                    var beneValues = new Dictionary<string, string>();
                    foreach (var prop in data.Children<JProperty>())
                    {
                        beneValues[prop.Name] = prop.Value?.ToString();
                    }

                    // 3️⃣ Fetch all field definitions (same as LoadBenefields)
                    string baseUrl = $"https://{Variable.apiipadd}/api/Beneficiary/get-all-product-field-settings";
                    string fieldUrl = $"{baseUrl}?ProductCode={ProductManager.selectedproductcode}&DisbursalModeCode={BCManager.selectedoptionborc}&MemberSection=Beneficiary&CountryCode={SelectedAddBeneCountry.seladdbenecount}&DestinationCurrencyCode={ProductManager.selectedProdCurrCode}&Language=EN";

                    var fieldRequest = new HttpRequestMessage(HttpMethod.Get, fieldUrl);
                    fieldRequest.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                    fieldRequest.Headers.Add("Accept", "application/json");

                    var fieldResponse = await client.SendAsync(fieldRequest);
                    fieldResponse.EnsureSuccessStatusCode();

                    string fieldJsonString = await fieldResponse.Content.ReadAsStringAsync();
                    var fieldJson = JObject.Parse(fieldJsonString);
                    var fields = fieldJson["data"]?["all_product_field_setting_list"]?["beneficiary"];

                    if (fields == null)
                    {
                        MessageBox.Show("No field definitions found.");
                        return;
                    }

                    // 4️⃣ Clear previous UI
                    myStackPanel.Children.Clear();

                    // 5️⃣ Create dynamic fields and prefill values
                    foreach (var field in fields)
                    {
                        string fieldName = field["field_name"]?.ToString();
                        string label = field["display_field_name"]?.ToString();
                        string type = field["type"]?.ToString();
                        bool mandatory = field["mandatory"]?.ToObject<bool>() ?? false;
                        bool visible = field["visible"]?.ToObject<bool>() ?? true;

                        if (!visible) continue;
                        if (mandatory) label += " *";

                        AddDynamicField(fieldName, label, type, mandatory);

                        // Prefill value if it exists in beneficiary data
                        if (beneValues.TryGetValue(fieldName, out string value))
                        {
                            SetFieldValue(fieldName, value);
                        }
                    }

                    // 6️⃣ Optional: run any additional loader logic
                    runtheloadersource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }


        private void backbutton(object sender, RoutedEventArgs e)
        {

            wSelectbeneficary wsel = new wSelectbeneficary();
            NavigationService.Navigate(wsel);

        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{

        //    try
        //    {
        //        BanksC selectedProduct = (BanksC)bankcombo.SelectedItem;



        //        //BanksBanchC selecteddisp = (BanksBanchC)branchcombo.SelectedItem;

        //        // Access the product code
        //        string bankksid = "";
        //        string banknameis = "";
        //        //string bankksid = selectedProduct.BankID;
        //        //string banksid = selectedProduct.BankID != null ? selectedProduct.BankID : "";


        //        if (selectedProduct != null)
        //        {
        //            bankksid = selectedProduct.BankID;
        //            banknameis = selectedProduct.BankName;
        //            //MessageBox.Show(selectedProduct.BankName + "");
        //        }
        //        //string banbchesid = selecteddisp.BanksBanchID;
        //        string banbchesid = "";
        //        string branchnameis = "";

        //        if (branchcombo.Items.Count != 0)
        //        {
        //            // MessageBox.Show(branchcombo.Items.Count + "");
        //            //MessageBox.Show(branchcombo.Text);
        //            BanksBanchC selecteddisp = (BanksBanchC)branchcombo.SelectedItem;
        //            //MessageBox.Show(selecteddisp + "");
        //            banbchesid = selecteddisp.BanksBanchID;
        //            branchnameis = selecteddisp.BanksBanchName;
        //            //MessageBox.Show(selecteddisp.BanksBanchName + "");
        //        }

        //        if (coreFieldNames == null || coreFieldNames.Count == 0)
        //        {
        //            MessageBox.Show("No CoreFieldNames available!");
        //            return;
        //        }

        //        // Create a dictionary to store field names and their values
        //        Dictionary<string, string> fieldValues = new Dictionary<string, string>();


        //        foreach (string coreFieldName in MANDATORYcoreFieldNames)
        //        {
        //            TextBox textBox = FindTextBoxByName(myStackPanel, coreFieldName);
        //            Label lab = FindlabelBoxByName(myStackPanel, coreFieldName + "label");
        //            if (textBox != null)
        //            {
        //                // Get the TextBox value
        //                string value = textBox.Text;

        //                if (value == null || value == "")
        //                {
        //                    MessageBox.Show("" + lab.Content + " is Mandatory !");
        //                    return;
        //                }

        //            }
        //        }

        //        // Loop through coreFieldNames
        //        foreach (string coreFieldName in coreFieldNames)
        //        {
        //            // Find TextBox recursively starting from myStackPanel
        //            TextBox textBox = FindTextBoxByName(myStackPanel, coreFieldName);

        //            if (textBox != null)
        //            {
        //                // Get the TextBox value
        //                string value = textBox.Text;

        //                //textBox.Text = "Hi";
        //                //SetTextBoxText(myStackPanel, coreFieldName, "Test");

        //                //MessageBox.Show(coreFieldName + " : " + value);


        //                if (coreFieldName == "BENE_NATION" || coreFieldName == "BENE_CNTRY" || coreFieldName == "BENE_CURR" || coreFieldName == "BENE_BANKID" || coreFieldName == "BENE_BANKCODE" || coreFieldName == "BENE_BRANCHID" || coreFieldName == "BENE_BRANCHCODE")
        //                {

        //                }
        //                else
        //                {

        //                    looperjson += "\"" + coreFieldName + "\"" + " : \"" + value + "\",\n";

        //                }






        //                // Add the field name and value to the dictionary
        //                fieldValues.Add(coreFieldName, value);




        //            }
        //            else
        //            {
        //                MessageBox.Show($"TextBox with name '{coreFieldName}' not found!");
        //            }
        //        }

        //        // ... Use fieldValues as needed
        //        //MessageBox.Show("Saved Sucessfully");
        //        createbene();

        //        //XXXXX V2
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message.ToString());
        //    }
        //}

        //public async Task createbene()
        //{
        //    try
        //    {
        //        using var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post,
        //            $"https://{Variable.apiipadd}/api/Beneficiary/update-beneficiary");

        //        request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

        //        // --- Bank & Branch ---
        //        BanksC selectedBank = (BanksC)bankcombo.SelectedItem;
        //        BanksBanchC selectedBranch = (BanksBanchC)branchcombo.SelectedItem;

        //        string bankId = selectedBank?.BankID ?? "";
        //        string bankCode = selectedBank?.BankCode ?? "";
        //        string bankName = selectedBank?.BankName ?? "";

        //        string branchId = selectedBranch?.BanksBanchID ?? "";
        //        string branchCode = selectedBranch?.BanksBanchCode ?? "";
        //        string branchName = selectedBranch?.BanksBanchName ?? "";

        //        // --- Nationality ---
        //        NationalityCountry selectedNationality = (NationalityCountry)NationalityCOUNTRYcombo.SelectedItem;
        //        string nationalityCode = selectedNationality?.ConCode ?? "";

        //        // --- Old values handling ---
        //        string beneSerialNo = "0";
        //        string beneDisb = "";
        //        string beneCountry = "";
        //        string beneProd = "";
        //        string beneCurr = "";

        //        if (addoreditvalue == "add")
        //        {
        //            beneDisb = ProductManager.selecteddispcode;
        //            beneCountry = SelectedAddBeneCountry.seladdbenecount;
        //            beneProd = ProductManager.selectedproductcode;
        //            beneCurr = ProductManager.selectedProdCurrCode;
        //        }
        //        else if (addoreditvalue == "edit")
        //        {
        //            beneSerialNo = SelectedBeneficiaryManager.BENE_SLNO;
        //            beneDisb = DISBTYPEedit;
        //            beneCountry = BENE_CNTRYedit;
        //            beneProd = BENE_PRODedit;
        //            beneCurr = BENE_CURRedit;
        //        }

        //        // --- For now: hardcoded / empty values ---
        //        string firstName = "Haritha";
        //        string middleName = "T";
        //        string lastName = "H";
        //        string mobileNumber = "";
        //        string relation = "";
        //        string accountNo = "";

        //        // --- Build Payload ---
        //        var payload = new
        //        {
        //            mobile_code = 965,
        //            mobile_number = mobileNumber,
        //            id_number = "",
        //            member_code = LoginManager.Remiduser,

        //            bene_slno = beneSerialNo,
        //            bene_disb = beneDisb,
        //            bene_gender = "M",

        //            bene_currency = beneCurr,
        //            bene_channel = "kiosk",
        //            bene_disbtype = BCManager.selectedoptionborc,
        //            appID = 3,
        //            moduleID = 3,

        //            source_of_fund = "",
        //            income_source_code = "",
        //            purpose_of_transaction = "",
        //            purpose_code = 123,

        //            source_of_fund_name = "",
        //            purpose_of_transaction_name = "",

        //            product_code = 539,  // match working sample
        //            product_name = "DIRECT TRANSFER",
        //            beneficiary_first_name = "Haritha",
        //            beneficiary_last_name = "Thomas",


        //            beneficiary_middle_name = middleName,

        //            beneficiary_first_name_unicode = "",
        //            beneficiary_last_name_unicode = "",
        //            beneficiary_middle_name_unicode = "",

        //            beneficiary_salutation = "1",
        //            beneficiary_nationality_code = nationalityCode,
        //            beneficiary_country_code = beneCountry,
        //            beneficiary_country_name = "",
        //            beneficary_relation = relation,

        //            beneficiary_address1 = "",
        //            beneficiary_address2 = "",
        //            beneficiary_city = "",
        //            beneficiary_state = "",

        //            beneficiary_bank_id = bankId,
        //            beneficiary_bank_code = bankCode,
        //            beneficiary_bank_name = bankName,
        //            beneficiary_bank_account_number = accountNo,

        //            beneficiary_branch_id = branchId,
        //            beneficiary_branch_code = branchCode,
        //            beneficiary_branch_name = branchName

        //        };

        //        string jsonString = JsonSerializer.Serialize(payload,
        //            new JsonSerializerOptions { WriteIndented = true });

        //        request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

        //        // --- Call API ---
        //        var response = await client.SendAsync(request);
        //        var responseBody = await response.Content.ReadAsStringAsync();

        //        if (!response.IsSuccessStatusCode)
        //        {
        //            MessageBox.Show($"API Error: {(int)response.StatusCode} {response.ReasonPhrase}\n{responseBody}");
        //            return;
        //        }

        //        // --- Parse Response ---
        //        using JsonDocument doc = JsonDocument.Parse(responseBody);

        //        string message = doc.RootElement.TryGetProperty("Message", out var msgEl) ? msgEl.GetString() : "No message";

        //        var root = doc.RootElement;

        //        string code = root.TryGetProperty("data", out var dataEl) &&
        //                     dataEl.TryGetProperty("e_id", out var eIdEl)
        //                     ? eIdEl.GetString()
        //                     : "-1";

        //        if (code != "-1")
        //        {
        //            MessageBox.Show("Saved Successfully");
        //            wSelectbeneficary wsel = new wSelectbeneficary();
        //            NavigationService.Navigate(wsel);
        //        }
        //        else
        //        {
        //            MessageBox.Show(message);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Error: " + ex.Message);
        //    }
        //}

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // 1. Validate Mandatory Fields
                foreach (string coreFieldName in MANDATORYcoreFieldNames)
                {
                    var ctrl = FindControlByName(myStackPanel, coreFieldName);
                    if (ctrl is TextBox tb && string.IsNullOrWhiteSpace(tb.Text))
                    {
                        Label lbl = FindlabelBoxByName(myStackPanel, coreFieldName + "label");
                        MessageBox.Show($"{lbl?.Content ?? coreFieldName} is Mandatory!");
                        return;
                    }
                    else if (ctrl is ComboBox cb && cb.SelectedItem == null)
                    {
                        Label lbl = FindlabelBoxByName(myStackPanel, coreFieldName + "label");
                        MessageBox.Show($"{lbl?.Content ?? coreFieldName} is Mandatory!");
                        return;
                    }
                }

                // 2. Collect Field Values
                var fieldValues = new Dictionary<string, object>();
                foreach (string coreFieldName in coreFieldNames)
                {
                    var ctrl = FindControlByName(myStackPanel, coreFieldName);
                    string value = "";

                    if (ctrl is TextBox tb)
                        value = tb.Text.Trim();
                    else if (ctrl is ComboBox cb && cb.SelectedItem != null)
                        value = (cb.SelectedItem as DropdownItem)?.code ?? cb.SelectedItem.ToString();

                    fieldValues[coreFieldName] = value;
                }

                // 3. Build payload dynamically
                var payload = new Dictionary<string, object>
                {
                    ["product_code"] = Convert.ToInt32(ProductManager.selectedproductcode),
                    ["beneficiary_country_code"] = SelectedAddBeneCountry.seladdbenecount,
                    ["beneficiary_currency"] = ProductManager.selectedProdCurrCode,
                    ["beneficiary_channel"] = "kiosk",
                    ["appID"] = 3,
                    ["moduleID"] = 3,
                    ["bene_disbtype"] = BCManager.selectedoptionborc,
                };

                // Map dynamic fields to API keys
                foreach (var kvp in fieldValues)
                {
                    string apiKey = ToSnakeCase(kvp.Key); // map field name
                    payload[apiKey] = kvp.Value ?? "";
                }

                // 4. Send API Request
                await SaveBeneficiary(payload);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}");
            }
        }

        private Control FindControlByName(DependencyObject parent, string name)
        {
            // Loop through all child elements
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement fe && fe.Name == name)
                    return fe as Control;

                var result = FindControlByName(child, name);
                if (result != null)
                    return result;
            }
            return null;
        }

        private string ToSnakeCase(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;
            var sb = new StringBuilder();
            foreach (char c in input)
            {
                if (char.IsUpper(c) && sb.Length > 0)
                    sb.Append('_');
                sb.Append(char.ToLower(c));
            }
            return sb.ToString();
        }

        private async Task SaveBeneficiary(Dictionary<string, object> payload)
        {
            try
            {
                using var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post,
                    $"https://{Variable.apiipadd}/api/Beneficiary/update-beneficiary");

                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                string jsonString = JsonSerializer.Serialize(payload,
                    new JsonSerializerOptions { WriteIndented = true });
                request.Content = new StringContent(jsonString, Encoding.UTF8, "application/json");

                var response = await client.SendAsync(request);
                var responseBody = await response.Content.ReadAsStringAsync();

                // 🔹 Handle non-200 HTTP codes first
                if (!response.IsSuccessStatusCode)
                {
                    try
                    {
                        using JsonDocument doc = JsonDocument.Parse(responseBody);
                        string apiMessage = doc.RootElement.TryGetProperty("message", out var msgEl)
                            ? msgEl.GetString() ?? "Unknown error"
                            : "Unknown error";

                        string errorDetails = "";
                        if (doc.RootElement.TryGetProperty("errors", out var errorsEl) &&
                            errorsEl.ValueKind == JsonValueKind.Array)
                        {
                            foreach (var error in errorsEl.EnumerateArray())
                            {
                                string field = error.TryGetProperty("error_field", out var f) ? f.GetString() ?? "" : "";
                                string desc = error.TryGetProperty("error_description", out var d) ? d.GetString() ?? "" : "";

                                errorDetails += $"- {field}: {desc}\n";

                                // 🔹 Highlight the field in UI
                                HighlightInvalidField(field);
                            }
                        }
                        MessageBox.Show($"API Error: {(int)response.StatusCode} {response.ReasonPhrase}\n{apiMessage + "  " + errorDetails}");
                    }
                    catch
                    {
                        MessageBox.Show($"API Error: {(int)response.StatusCode} {response.ReasonPhrase}\n{responseBody}");
                    }
                    return;
                }

                // 🔹 Parse JSON for success/failure
                using JsonDocument jsonDoc = JsonDocument.Parse(responseBody);

                string success = jsonDoc.RootElement.TryGetProperty("success", out var successEl)
                    ? successEl.GetString() ?? "false"
                    : "false";

                string message = jsonDoc.RootElement.TryGetProperty("message", out var msgEl2)
                    ? msgEl2.GetString() ?? "Unknown response"
                    : "Unknown response";

                if (!success.Equals("true", StringComparison.OrdinalIgnoreCase))
                {
                    string statusCode = jsonDoc.RootElement.TryGetProperty("status_code", out var statusEl)
                        ? statusEl.GetRawText()
                        : "N/A";
                    MessageBox.Show($"Save Failed ({statusCode}): {message}");
                    return;
                }

                // 🔹 Check if `e_id` is present
                string eId = jsonDoc.RootElement.TryGetProperty("data", out var dataEl) &&
                             dataEl.TryGetProperty("e_id", out var eIdEl)
                    ? eIdEl.GetString()
                    : "-1";

                if (eId != "-1")
                {
                    MessageBox.Show("Beneficiary saved successfully!");
                    NavigationService.Navigate(new wSelectbeneficary());
                }
                else
                {
                    MessageBox.Show($"Save failed: {message}");
                }
            }
            catch (JsonException)
            {
                MessageBox.Show("Invalid JSON response from server.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Save Error: {ex.Message}");
            }
        }

        private void HighlightInvalidField(string fieldName)
        {
            try
            {
                // Find TextBox by name
                var textBox = FindTextBoxByName(myStackPanel, fieldName);
                if (textBox != null)
                {
                    textBox.BorderBrush = Brushes.Red;
                    textBox.BorderThickness = new Thickness(2);
                }
            }
            catch
            {
                // If the field doesn't exist visually, ignore
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

        private void UpdateNationilityComboBoxsource(JsonDocument jsonDocuments)
        {
            try
            {

                List<NationalityCountry> productsc = new List<NationalityCountry>();

                NationalityCOUNTRYcombo.Items.Clear();

                if (jsonDocuments.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                        dataElement.TryGetProperty("country_list", out JsonElement countryListElement))
                {
                    foreach (var country in countryListElement.EnumerateArray())
                    {
                        string name = country.TryGetProperty("name", out JsonElement nameEl) ? nameEl.GetString() ?? "" : "";
                        string code = country.TryGetProperty("code", out JsonElement codeEl) ? codeEl.ToString() : "";
                        ///string isDefault = country.TryGetProperty("is_default", out JsonElement eIdEl) ? eIdEl.GetString() ?? "" : "";

                        productsc.Add(new NationalityCountry
                        {
                            ConID = code.ToString(),
                            ConName = name.ToString(),
                            ConCode = code.ToString()
                        });
                    }

                }
                else
                {
                    Console.WriteLine("Invalid JSON response structure or missing product_list");
                }

                NationalityCOUNTRYcombo.ItemsSource = productsc;

                if (productsc.Count > 0)
                {
                    NationalityCOUNTRYcombo.SelectedItem = productsc[0];
                }

                NationalityCOUNTRYcombo.DisplayMemberPath = "ConName";

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

        public async Task LoadBenefields_old()
        {
            try
            {
                //Thread.Sleep(2000);
                MessageBox.Show("22222222222222");
                //MessageBox.Show(branchvisibile);

                var client = new HttpClient();
                var baseUrl = "https://" + Variable.apiipadd + "/api​/Beneficiary​/get-all-product-field-settings";

                // Replace with actual values
                string productCode = ProductManager.selectedproductcode;
                string disbursalMode = BCManager.selectedoptionborc;
                string memberSection = "Beneficiary";
                string countryCode = SelectedAddBeneCountry.seladdbenecount;
                string destinationCurrencyCode = ProductManager.selectedProdCurrCode;
                string language = "EN";
                string token = TokenManager.Token;

                // Construct the full URL with query parameters
                string url = $"{baseUrl}?ProductCode={productCode}&DisbursalModeCode={disbursalMode}&MemberSection={memberSection}&CountryCode={countryCode}&DestinationCurrencyCode={destinationCurrencyCode}&Language={language}";

                MessageBox.Show(url);

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + token);
                request.Headers.Add("Accept", "text/plain");

                MessageBox.Show("33333");
                // Send the request
                var response = await client.SendAsync(request);

                MessageBox.Show(response.ToString());

                var contentString = await request.Content.ReadAsStringAsync();
                string responseString = await response.Content.ReadAsStringAsync();


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

        private async Task LoadDropdownData()
        {
            try
            {
                string baseUrl = "https://" + Variable.apiipadd + "/api/beneficiary/get-beneficiary-combo-list";
                string productCode = ProductManager.selectedproductcode;
                string destinationCountryCode = SelectedAddBeneCountry.seladdbenecount;
                string url = $"{baseUrl}?destination_country_code={destinationCountryCode}&product_code={productCode}";

                using (var client = new HttpClient())
                {
                    var request = new HttpRequestMessage(HttpMethod.Get, url);
                    request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                    request.Headers.Add("Accept", "application/json");

                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    string json = await response.Content.ReadAsStringAsync();
                    var data = JObject.Parse(json)["data"];

                    nationalityList = data["country_list"]?.ToObject<List<DropdownItem>>() ?? new();
                    relationshipList = data["relationship_list"]?.ToObject<List<DropdownItem>>() ?? new();
                    beneficiaryBankList = data["beneficiaryBankList"]?.ToObject<List<DropdownItem>>() ?? new();
                    beneficiaryBranchList = data["beneficiaryBranchList"]?.ToObject<List<DropdownItem>>() ?? new();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Dropdown load error: " + ex.Message);
            }

        }

        private void SetFieldValue(string fieldName, string value)
        {
            foreach (var child in myStackPanel.Children)
            {
                if (child is StackPanel row && row.Tag?.ToString() == fieldName)
                {
                    foreach (var control in row.Children)
                    {
                        if (control is TextBox tb)
                        {
                            tb.Text = value;
                        }
                        else if (control is ComboBox cb)
                        {
                            // Try to match DropdownItem by Code or ToString()
                            foreach (var item in cb.Items)
                            {
                                if (item is DropdownItem di &&
                                    (di.code == value || di.name == value))
                                {
                                    cb.SelectedItem = item;
                                    break;
                                }
                                else if (item?.ToString() == value)
                                {
                                    cb.SelectedItem = item;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        private void AddDynamicField(string fieldName, string label, string type, bool mandatory)
        {
            if (!coreFieldNames.Contains(fieldName))
                coreFieldNames.Add(fieldName);

            if (mandatory && !MANDATORYcoreFieldNames.Contains(fieldName))
                MANDATORYcoreFieldNames.Add(fieldName);

            var rowPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                Margin = new Thickness(0, 10, 0, 0),
                Tag = fieldName
            };

            var lbl = new Label
            {
                Content = label,
                Width = 250,
                Foreground = Brushes.White,
                FontSize = 20
            };
            rowPanel.Children.Add(lbl);

            FrameworkElement inputControl;

            if (type.Equals("Dropdown", StringComparison.OrdinalIgnoreCase) ||
                type.Equals("SubDropdown", StringComparison.OrdinalIgnoreCase))
            {
                var combo = new ComboBox
                {
                    Width = 400,
                    Height = 30,
                    Name = fieldName,
                    Background = Brushes.White,
                    Foreground = Brushes.Black,
                    BorderThickness = new Thickness(0),
                    Tag = fieldName
                };

                // Populate based on fieldName
                List<DropdownItem> source = fieldName switch
                {
                    "beneficiaryNationalityCode" => nationalityList,
                    "beneficaryRelation" => relationshipList,
                    "beneficiaryBankCode" => beneficiaryBankList,
                    "beneficiaryBranchCode" => beneficiaryBranchList,
                    _ => new List<DropdownItem>()
                };

                foreach (var item in source)
                {
                    combo.Items.Add(item);
                }

                inputControl = combo;
            }
            else
            {
                var txt = new TextBox
                {
                    Width = 400,
                    Height = 30,
                    Name = fieldName,
                    Background = Brushes.White,
                    Foreground = Brushes.Black,
                    BorderThickness = new Thickness(0),
                    Tag = fieldName
                };
                inputControl = txt;
            }

            rowPanel.Children.Add(inputControl);
            myStackPanel.Children.Add(rowPanel);
        }




    }
}
