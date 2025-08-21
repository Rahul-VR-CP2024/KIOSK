using Exchange.Managers;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wSelectbeneficary.xaml
    /// </summary>
    public partial class wSelectbeneficary : Page
    {

        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry { get; set; }

        public wSelectbeneficary()
        {
            try
            {
                InitializeComponent();

                Countries = new ObservableCollection<Country>();

                if (TokenManager.Langofsoft == "ar")
                {
                    backbtn.Content = "يرجع";
                    benetitle.Text = "المستفيد";
                    addnewbtn.Content = "إضافة مستفيد جديد";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            
        }

        public class Country
        {
            public string? CountryName { get; set; }

            public string? TID { get; set; }
            public string? BANK { get; set; }

            public string? Amt { get; set; }

            public string? Date { get; set; }

            public string? Bene { get; set; }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {

                LoadCountries();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        public async Task LoadCountries()
        {
            try
            {
                var client = new HttpClient();
                string url = $"https://{Variable.apiipadd}/api/Beneficiary/get-beneficiary-list?AppMemberCode={LoginManager.Remiduser}";


                var request = new HttpRequestMessage(HttpMethod.Get, url);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("accept", "application/json");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                using (var jsonDocument = await JsonDocument.ParseAsync(responseStream))
                {
                    bool dataElemen= jsonDocument.RootElement.TryGetProperty("data", out JsonElement dataElements);
                    bool beneficiaryLis = dataElements.TryGetProperty("beneficiary_list", out JsonElement beneficiaryLists);

                    // Response root contains success, status_code, message, data
                    if ((dataElemen==true) && (beneficiaryLis==true))
                    {
                        //jCountries.Clear();
                        foreach (var bene in beneficiaryLists.EnumerateArray())
                        {
                            string firstName = GetSafeString(bene, "beneficiary_first_name");
                            string middleName = GetSafeString(bene, "beneficiary_middle_name");
                            string lastName = GetSafeString(bene, "beneficiary_last_name");
                            string beneficiary_country_name = GetSafeString(bene, "beneficiary_country_name");
                            string beneficiary_bank_account_number = GetSafeString(bene, "beneficiary_bank_account_number");
                            string beneficiary_bank_name = GetSafeString(bene, "beneficiary_bank_name");

                            Countries.Add(new Country
                            {
                                CountryName = beneficiary_country_name,
                                Amt = "", // put actual mapping later
                                Bene = $"{firstName} {middleName} {lastName}".Trim(),
                                Date = "",
                                TID = beneficiary_bank_account_number,
                                BANK = beneficiary_bank_name
                            });

                        }

                    }
                }
                countryListView.ItemsSource = Countries;
            }
            catch (HttpRequestException ex)
            {
                MessageBox.Show($"Request error: {ex.Message}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unexpected error: {ex.Message}");
            }
        }

        private string GetSafeString(JsonElement element, string propertyName)
        {
            try
            {
                if (element.TryGetProperty(propertyName, out JsonElement prop))
                {
                    if (prop.ValueKind != JsonValueKind.Null && prop.ValueKind != JsonValueKind.Undefined)
                        return prop.GetString() ?? "";
                }
            }
            catch (Exception)
            {

                return "";
            }
           
            return "";
        }

        private void countryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the SelectedCountry property when the selection changes
            SelectedCountry = countryListView.SelectedItem as Country;
            if (SelectedCountry != null)
            {
                // MessageBox.Show($"Selected Country: {SelectedCountry.CountryName}");
            }
        }

        private void RESEND_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var myValue = ((Button)sender).Tag;
                SelectedBeneficiaryManager.SetBENE_SLNO(myValue.ToString());

                wViewBenficiaryDetails wx = new wViewBenficiaryDetails();
                NavigationService.Navigate(wx);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void RESEND_Click_edit(object sender, RoutedEventArgs e)
        {
            try
            {
                var myValue = ((Button)sender).Tag;
                SelectedBeneficiaryManager.SetBENE_SLNO(myValue.ToString());

                waddbeneficiary mainpage = new waddbeneficiary("edit");
                NavigationService.Navigate(mainpage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void addnewben(object sender, RoutedEventArgs e)
        {
            try
            {
                wSelectcountry mainpage = new wSelectcountry();
                NavigationService.Navigate(mainpage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationManager.NavigateToHome();
        }

        public async Task LoadCountries_old()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Beneficiary/get-beneficiary-list");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

            var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"" + BCManager.selectedoptionborc + "\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            //var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {
                    Countries.Add(new Country
                    {
                        CountryName = dataElement.GetProperty("BENNAME").GetString(),
                        Amt = $"{dataElement.GetProperty("COREDISB").GetString()} {dataElement.GetProperty("BENE_CURRENCY").GetString()}",
                        Bene = $"{dataElement.GetProperty("BENE_SALUTE").GetString()} {dataElement.GetProperty("BENNAME").GetString()}",
                        Date = dataElement.GetProperty("BENE_SLNO").ToString(), // You need to specify how to get the date from the JSON response
                        TID = dataElement.GetProperty("BENE_ACCNO").ToString(), // You need to specify how to get the TID from the JSON response BENE_SLNO
                        BANK = dataElement.GetProperty("BENE_BANK").GetString()


                        //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                    });
                }
            }

            countryListView.ItemsSource = Countries;
        }


    }
}
