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


        public wSelectcountry()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                countrytitle.Text = "دولة";

            }

            // Sample data (replace with your actual data)
            Countries = new ObservableCollection<Country>
            {
            //    new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png")), CountryName = "Bank of Baroda" , Amt = "2,00,000 INR", Bene = "India", Date = "01/01/2023", TID="123456" , BANK="Bank of Baroda", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/AUSTRAILA.png")), CountryName = "National Australia Bank", Amt = "200 AU$", Bene = "Australia", Date = "15/02/2023", TID="678912" , BANK="National Australia Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/cross.png")) },
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/USA.png")), CountryName = "Bank of America" , Amt = "9000 USD", Bene = "USA", Date = "06/03/2023", TID="789147" , BANK="Bank of America", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/UK.png")), CountryName = "London Bank", Amt = "500 £", Bene = "UK", Date = "10/04/2023" , TID="963852" , BANK="London Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/CANADA.png")), CountryName = "Bank of Canada" , Amt = "2500 CAD", Bene = "Canada", Date = "20/05/2023" , TID="753951" , BANK="Bank of Canada", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/CHINA.png")), CountryName = "ICBC", Amt = "10,000 USD", Bene = "China", Date = "14/06/2023" , TID="456741" , BANK="ICBC", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/MOROCCO.png")), CountryName = "BANK AL-MAGHRIB" , Amt = "3000 MAD", Bene = "Morocco", Date = "13/07/2023" , TID="159753" , BANK="BANK AL-MAGHRIB", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
            //     new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/PHILIPINES.png")), CountryName = "Philippine National Bank", Amt = "5000 PHP", Bene = "Philippine", Date = "02/12/2023", TID="897563" , BANK="Philippine National Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
                 
            //    //new Country { FlagImage = new BitmapImage(new Uri("/Images/KWTFlag.png")), CountryName = "Canada" },
            //    // Add more countries as needed
            };

           // countryListView.ItemsSource = Countries;

            //LoadCountries();
        }


        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
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

        //public SeriesCollection SeriesCollection { get; set; }

        private void RESEND_Click(object sender, RoutedEventArgs e)
        {

            var myValue = ((Button)sender).Tag;
           // MessageBox.Show(myValue.ToString());

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            //waddbeneficiary mainpage = new waddbeneficiary();
            // NavigationService.Navigate(mainpage);

            SelectedAddBeneCountry.Setaddbenecount(myValue.ToString());

            wSelectProduct selprod = new wSelectProduct();
            NavigationService.Navigate(selprod);

        }


        public static class SelectedAddBeneCountry
        {
            public static string seladdbenecount { get; set; }

            public static void Setaddbenecount(string token)
            {
                seladdbenecount = token;
            }

        }

        //
        private void addnewben(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            waddbeneficiary mainpage = new waddbeneficiary("add");
            NavigationService.Navigate(mainpage);

        }
        public class Country
        {
            public BitmapImage FlagImage { get; set; }

            public BitmapImage stsimg { get; set; }

            public string CountryName { get; set; }

            public string TID { get; set; }
            public string BANK { get; set; }

            public string Amt { get; set; }

            public string Date { get; set; }

            public string Bene { get; set; }
        }



        public async Task LoadCountries()
        {

       






            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxmaster/country");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

            //var content = new StringContent("{ \n    \"remID\":130824,\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            //request.Content = content;

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

           // MessageBox.Show((await response.Content.ReadAsStringAsync()));

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {

               // MessageBox.Show("Hi 1");
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

               // MessageBox.Show("Hi 2");

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {

                   // MessageBox.Show("Hi 3");

                  //  MessageBox.Show(dataElement.GetProperty("ConName").GetString());

                    Countries.Add(new Country
                    {


                        CountryName = dataElement.GetProperty("ConCode").GetString(),
                        Amt = "",
                        Bene = dataElement.GetProperty("ConName").GetString() + " " + dataElement.GetProperty("ConAName").GetString(),
                        Date = "", // You need to specify how to get the date from the JSON response
                        TID = "", // You need to specify how to get the TID from the JSON response
                        BANK = "",
                        stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                        FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                        //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                    });

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

            countryListView.ItemsSource = Countries;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {


            LoadCountries();

        }
    }
}
