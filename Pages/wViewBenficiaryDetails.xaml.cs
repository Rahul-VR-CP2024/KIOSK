using Exchange.Managers;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using static Exchange.Pages.wSelectcountry;
using static Exchange.Pages.wSelectProduct;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    public partial class wViewBenficiaryDetails : Page
    {
        // Map API field names to control names if needed
        private readonly Dictionary<string, string> fieldNameMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "beneficiary_first_name", "beneficiaryFirstName" },
            { "beneficiary_last_name", "beneficiaryLastName" },
            { "beneficiary_middle_name", "beneficiaryMiddleName" },
            { "beneficiary_d_o_b", "beneficiaryDOB" },
            { "beneficiary_address1", "beneficiaryAddress1" },
            { "beneficiary_address2", "beneficiaryAddress2" },
            { "beneficiary_state", "beneficiaryState" },
            { "beneficiary_city", "beneficiaryCity" },
            { "beneficiary_country_code", "beneficiaryCountryCode" },
            { "beneficiary_nationality_code", "beneficiaryNationalityCode" },
            { "beneficary_relation", "beneficaryRelation" },
            { "beneficiary_bank_code", "beneficiaryBankCode" },
            { "beneficiary_branch_code", "beneficiaryBranchCode" },
            { "beneficiary_bank_account_number", "beneficiaryBankAccountNumber" },
            { "beneficiary_category_code", "beneficiaryCategoryCode" },
            { "currency_code", "currencyCode" },
            { "disbursal_mode", "disbursalMode" },
            { "product_code", "productCode" }
        };
        string BENE_PRODedit = "";

        string DISBTYPEedit = "";

        string BENE_CNTRYedit = "";

        string BENE_CURRedit = "";
        // Dynamic lists for dropdowns
        private List<DropdownItem> nationalityList = new();
        private List<DropdownItem> relationshipList = new();
        private List<DropdownItem> beneficiaryBankList = new();
        private List<DropdownItem> beneficiaryBranchList = new();

        private List<string> coreFieldNames = new List<string>();
        private List<string> MANDATORYcoreFieldNames = new List<string>();

        public class DropdownItem
        {
            public string code { get; set; }
            public string name { get; set; }
            public override string ToString() => name;
        }

        public wViewBenficiaryDetails()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                verifybenetitle.Text = "بيانات المستفيد";
                backbtn.Content = "يرجع";
                proceedbtn.Content = "يتابع";
            }

            Loaded += Page_Loaded; // Hook up page load
        }
        public static class BeneficiaryDetailsManager
        {
            public static string BENE_MOBILE { get; set; }

            public static void SetBENE_MOBILE(string token)
            {
                BENE_MOBILE = token;
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            await loadbenefieldstoedit();
        }
        private async Task LoadBenefields(string productCode, string disbursalMode, string countryCode, string currencyCode)
        {
            try
            {
                using var client = new HttpClient();
                string language = "EN";
                string memberSection = "Beneficiary";
                string url = $"https://{Variable.apiipadd}/api/Beneficiary/get-all-product-field-settings" +
                             $"?ProductCode={productCode}&DisbursalModeCode={disbursalMode}&MemberSection={memberSection}" +
                             $"&CountryCode={countryCode}&DestinationCurrencyCode={currencyCode}&Language={language}";

                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("Accept", "application/json");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                string jsonString = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(jsonString);

                var fields = json["data"]?["all_product_field_setting_list"]?["beneficiary"];
                if (fields == null) return;

                myStackPanel.Children.Clear();

                foreach (var field in fields)
                {
                    string fieldName = field["field_name"]?.ToString();
                    string label = TokenManager.Langofsoft == "ar" ? field["arabic_display_field_name"]?.ToString() : field["display_field_name"]?.ToString();
                    string type = field["type"]?.ToString();
                    bool mandatory = field["mandatory"]?.ToObject<bool>() ?? false;
                    bool visible = field["visible"]?.ToObject<bool>() ?? true;

                    if (!visible) continue;
                    if (mandatory) label += " *";

                    AddDynamicField(fieldName, label, type, mandatory);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading fields: " + ex.Message);
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
                    if (string.IsNullOrEmpty(productCode))
                        productCode = BENE_PRODedit;

                    string disbursalMode = BCManager.selectedoptionborc;
                    if (string.IsNullOrEmpty(disbursalMode))
                        disbursalMode = DISBTYPEedit;

                    string countryCode = SelectedAddBeneCountry.seladdbenecount;
                    if (string.IsNullOrEmpty(countryCode))
                        countryCode = BENE_CNTRYedit;

                    string destinationCurrencyCode = ProductManager.selectedProdCurrCode;
                    if (string.IsNullOrEmpty(destinationCurrencyCode))
                        destinationCurrencyCode = BENE_CURRedit;

                    string language = "EN";
                    string token = TokenManager.Token;
                    string memberSection = "Beneficiary";

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

        public async Task loadbenefieldstoedit()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    //  Fetch the specific beneficiary data using eId
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

                    //  Save edit-mode variables
                    BENE_PRODedit = data["product_code"]?.ToString();
                    DISBTYPEedit = data["disbursal_mode"]?.ToString();
                    BENE_CNTRYedit = data["beneficiary_country_code"]?.ToString();
                    BENE_CURRedit = data["currency_code"]?.ToString();

                    //  Load dynamic fields if product/disbursal mode are available
                    if (!string.IsNullOrEmpty(BENE_PRODedit) && !string.IsNullOrEmpty(DISBTYPEedit))
                    {
                        await LoadDropdownData();

                        await LoadBenefields();
                    }

                    //  Convert JSON data to dictionary: fieldName → value
                    //await LoadBenefields();
                    var beneValues = new Dictionary<string, string>();
                    foreach (var prop in data.Children<JProperty>())
                    {
                        beneValues[prop.Name] = prop.Value?.ToString();
                    }

                    //  Prefill values into the dynamic UI
                    foreach (var kvp in beneValues)
                    {
                        // Try to map DB field name to dynamic field name
                        if (fieldNameMap.TryGetValue(kvp.Key, out string mappedName))
                        {
                            SetFieldValue(mappedName, kvp.Value);
                        }
                        else
                        {
                            // If no mapping exists, try with original key
                            SetFieldValue(kvp.Key, kvp.Value);
                        }
                    }

                    //  Optional: run any additional loader logic
                   // runtheloadersource();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
        }



        private async Task LoadDropdownData()
        {
            try
            {
                string baseUrl = "https://" + Variable.apiipadd + "/api/beneficiary/get-beneficiary-combo-list";

                // ✅ Use add-mode values first, else fallback to edit-mode values
                string productCode = !string.IsNullOrEmpty(ProductManager.selectedproductcode)
                                        ? ProductManager.selectedproductcode
                                        : BENE_PRODedit;

                string destinationCountryCode = !string.IsNullOrEmpty(SelectedAddBeneCountry.seladdbenecount)
                                        ? SelectedAddBeneCountry.seladdbenecount
                                        : BENE_CNTRYedit;

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
                            // Match DropdownItem by code or name
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
                        else if (control is DatePicker dp)
                        {
                            if (DateTime.TryParse(value, out var parsedDate))
                                dp.SelectedDate = parsedDate;
                        }
                        else if (control is CheckBox chk)
                        {
                            chk.IsChecked = value == "true" || value == "1";
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
            else if (type.Equals("Date", StringComparison.OrdinalIgnoreCase))
            {
                var datePicker = new DatePicker
                {
                    Width = 400,
                    Height = 30,
                    Name = fieldName,
                    Tag = fieldName
                };
                inputControl = datePicker;
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

        private void backbutton(object sender, RoutedEventArgs e)
        {
            wSelectbeneficary wsel = new wSelectbeneficary();
            NavigationService.Navigate(wsel);
        }

        private void Proceedbutton(object sender, RoutedEventArgs e)
        {
            wTransferpay wtpay = new wTransferpay();
            NavigationService.Navigate(wtpay);
        }
    }
}
