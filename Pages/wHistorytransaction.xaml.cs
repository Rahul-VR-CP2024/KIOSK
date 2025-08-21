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
    /// Interaction logic for wHistorytransaction.xaml
    /// </summary>
    public partial class wHistorytransaction : Page
    {

        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry { get; set; }
        public wHistorytransaction()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtnlbl.Content = "يرجع";
                tranhistlbl.Text = "سجل المعاملات";
            }



                // Sample data (replace with your actual data)
                Countries = new ObservableCollection<Country>
            {
                //new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png")), CountryName = "Bank of Baroda" , Amt = "2,00,000 INR", Bene = "John Doe", Date = "01/01/2023", TID="123456" , BANK="Bank of Baroda", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/AUSTRAILA.png")), CountryName = "National Australia Bank", Amt = "200 AU$", Bene = "Steave", Date = "15/02/2023", TID="678912" , BANK="National Australia Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/cross.png")) },
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/USA.png")), CountryName = "Bank of America" , Amt = "9000 USD", Bene = "Harvey", Date = "06/03/2023", TID="789147" , BANK="Bank of America", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/UK.png")), CountryName = "London Bank", Amt = "500 £", Bene = "Nicole", Date = "10/04/2023" , TID="963852" , BANK="London Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/CANADA.png")), CountryName = "Bank of Canada" , Amt = "2500 CAD", Bene = "Sama", Date = "20/05/2023" , TID="753951" , BANK="Bank of Canada", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/CHINA.png")), CountryName = "ICBC", Amt = "10,000 USD", Bene = "Rio", Date = "14/06/2023" , TID="456741" , BANK="ICBC", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/MOROCCO.png")), CountryName = "BANK AL-MAGHRIB" , Amt = "3000 MAD", Bene = "Michael", Date = "13/07/2023" , TID="159753" , BANK="BANK AL-MAGHRIB", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png"))},
                // new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/PHILIPINES.png")), CountryName = "Philippine National Bank", Amt = "5000 PHP", Bene = "Mohammed", Date = "02/12/2023", TID="897563" , BANK="Philippine National Bank", stsimg=new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
                 
                //new Country { FlagImage = new BitmapImage(new Uri("/Images/KWTFlag.png")), CountryName = "Canada" },
                // Add more countries as needed
            };

            countryListView.ItemsSource = Countries;
        }
        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            NavigationManager.NavigateToHome();
        }

        private void RESEND_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            wTransferpay mainpage = new wTransferpay();
            NavigationService.Navigate(mainpage);

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

            public string Countryname { get; set; }

            public string LCamt { get; set; }
        }

        private void Page_load(object sender, RoutedEventArgs e)
        {
            //API CALLING STARTS

            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxBeneficiary/Beneficiary/GetList");
            //request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            ////LoginManager
            ////LoginManager.Remiduser;

            ////BCManager
            //var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"" + BCManager.selectedoptionborc + "\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            ////var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());

            // MessageBox.Show(await response.Content.ReadAsStringAsync());

            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxRemittance/Transaction/History");
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6ImI0NzA4MDhhLWU4MTYtNDc5Mi05NWYyLTYzOWRlNjllNjZkMSIsImlhdCI6IjA4LzE5LzIwMjQgMDc6NTc6QU0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyNDA0MzQ0MCwiZXhwIjoxNzI0MDQ1MjQwLCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.NpCtStGEemka7EFRMhdck_XIO8c17GvBKWk1VDReV3M");
            //var content = new StringContent("{\n    \"remitterID\": 130854,\n    \"fromDate\": \"2020-02-12 15:16:27.054933\",\n    \"toDate\": \"2025-04-25 15:16:27.054951\",\n    \"pageSize\": 0,\n    \"pageNo\": 0\n}", null, "application/json");
            //request.Content = content;
            //var response = await client.SendAsync(request);
            //response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());


            LoadTransactionHistory("1","","","","");
            //API CALLING ENDS
        }

        public async Task LoadTransactionHistory(string memberCode, string fromDate, string toDate, string pageIndex, string noOfRecords)
        {
            var client = new HttpClient();
             
            var request = new HttpRequestMessage(
                HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Transaction/get-transaction-list?AppMemberCode="+ memberCode + "&FromDate="+fromDate+"&ToDate="+toDate+"&PageIndex="+pageIndex+"&NoOfRecords="+noOfRecords
            );

            request.Headers.Add("accept", "text/plain");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();

            using var responseStream = await response.Content.ReadAsStreamAsync();
            using var jsonDocument = await JsonDocument.ParseAsync(responseStream);

            var root = jsonDocument.RootElement;

            if (root.TryGetProperty("data", out JsonElement data) &&
                data.TryGetProperty("transactions", out JsonElement transactions))
            {
                Countries.Clear();

                foreach (var txn in transactions.EnumerateArray())
                {
                    try
                    {
                        Countries.Add(new Country
                        {
                            CountryName = txn.GetProperty("destination_country_name").GetString(),
                            Countryname = txn.GetProperty("destination_country_name").GetString(),

                            Amt = $"{txn.GetProperty("destination_amount").GetDecimal():0.000} {txn.GetProperty("destination_currency_code").GetString()}",
                            LCamt = $"{txn.GetProperty("pay_amount").GetDecimal():0.000} KWD",

                            Bene = txn.GetProperty("beneficiary_name").GetString(),
                            Date = txn.GetProperty("transaction_date").ToString(),
                            TID = txn.GetProperty("transaction_reference").ToString(),
                            BANK = txn.GetProperty("product").GetString(),

                            stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")),
                            FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                        });
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error parsing transaction: " + ex.Message);
                    }
                }
            }

            countryListView.ItemsSource = Countries;
        }

        public async Task LoadTransactionHistory_OLD()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+"/api/v1/sxRemittance/Transaction/History");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

            //var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"" + BCManager.selectedoptionborc + "\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            //var content = new StringContent("{ \n    \"remID\":" + LoginManager.Remiduser + ",\n    \"disbMode\":\"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
            var content = new StringContent("{\n    \"remitterID\": " + LoginManager.Remiduser + ",\n    \"fromDate\": \"2000-01-01 00:00:00.054933\",\n    \"toDate\": \"2025-04-25 15:16:27.054951\",\n    \"pageSize\": 0,\n    \"pageNo\": 0\n}", null, "application/json");
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
                    try
                    {

                        Countries.Add(new Country
                        {
                            CountryName = dataElement.GetProperty("Beneficiary").GetString(),
                            Amt = $"{dataElement.GetProperty("FCAmount").GetRawText()} {dataElement.GetProperty("FCCurrency").GetString()}",
                            Bene = $"{dataElement.GetProperty("Beneficiary").GetString()} {dataElement.GetProperty("Beneficiary").GetString()}",
                            Date = dataElement.GetProperty("TxnDate").ToString(), // You need to specify how to get the date from the JSON response
                            TID = dataElement.GetProperty("TxnReferenceNo").GetRawText(), // You need to specify how to get the TID from the JSON response BENE_SLNO
                            BANK = dataElement.GetProperty("Agent").GetString(),
                            stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                            //FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                            FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png")),
                            Countryname = dataElement.GetProperty("Country").GetString(), 
                            LCamt = dataElement.GetProperty("NetAmount").GetRawText() + " KD"
                            //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                        });

                    }
                    catch (Exception ex) { 
                    MessageBox.Show(ex.Message);
                    }
                }
            }

            countryListView.ItemsSource = Countries;
        }
    }
}
