using Exchange.Managers;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wSelectcountry.xaml
    /// </summary>
    public partial class wSelectcountry : Page
    {

        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry { get; set; }

        public class Country
        {
            //public BitmapImage FlagImage { get; set; }

            //public BitmapImage stsimg { get; set; }

            public string CountryName { get; set; }

            public string TID { get; set; }
            public string BANK { get; set; }

            public string Amt { get; set; }

            public string Date { get; set; }

            public string Bene { get; set; }
        }

        public static class SelectedAddBeneCountry
        {

            public static string seladdbenecount { get; set; }

            public static void Setaddbenecount(string token)
            {
                seladdbenecount = token;
            }

        }

        public wSelectcountry()
        {
            try
            {
                InitializeComponent();

                Countries = new ObservableCollection<Country>();

                if (TokenManager.Langofsoft == "ar")
                {
                    backbtn.Content = "يرجع";
                    countrytitle.Text = "دولة";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


            LoadCountries();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
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
                var request = new HttpRequestMessage(
                    HttpMethod.Get, "https://" + Variable.apiipadd + "/api/Customer/get-country-combo-list"
                );
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("accept", "text/plain");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                    // Navigate to data.country_list
                    if (jsonDocument.RootElement.TryGetProperty("data", out JsonElement dataElement) &&
                        dataElement.TryGetProperty("country_list", out JsonElement countryListElement))
                    {
                        foreach (var country in countryListElement.EnumerateArray())
                        {
                            string code = country.GetProperty("code").GetString() ?? "";
                            string name = country.GetProperty("name").GetString() ?? "";
                            bool isDefault = country.TryGetProperty("is_default", out JsonElement defaultElement)
                                             && defaultElement.GetBoolean();

                            Countries.Add(new Country
                            {
                                CountryName = code,
                                Bene = name,
                                Amt =  "",
                                Date = "",
                                TID = "",
                                BANK = ""//,
                               // stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")),
                               // FlagImage = new BitmapImage(new Uri($"pack://application:,,,/Exchange;component/Images/{code}.png"))
                            });
                        }
                    }
                }

                countryListView.ItemsSource = Countries;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            NavigationManager.NavigateToHome();
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

                SelectedAddBeneCountry.Setaddbenecount(myValue.ToString());

                wSelectProduct selprod = new wSelectProduct();
                NavigationService.Navigate(selprod);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            

        }

        
    }
}
