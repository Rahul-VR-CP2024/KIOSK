using Exchange.Common;
using Exchange.Managers;
using LiveCharts;
using LiveCharts.Defaults;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Threading;
using static Exchange.Pages.wSelectcountry;
using static Exchange.Pages.wSelectProduct;
using LineSeries = LiveCharts.Wpf.LineSeries;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wExchangecalculator.xaml
    /// </summary>
    public partial class wExchangecalculator : Page
    {
        //  public ObservableCollection<Country> Countries { get; set; }

        /// <summary>
        /// public Country SelectedCountry { get; set; }
        /// </summary>

        public SeriesCollection SeriesCollection { get; set; }
        private DisposableTimer disposableTimer;
        string fcorlcswitch = "FC";
        string productcode = ProductManager.selectedproductcode;
        string disbtypecode = ProductManager.selecteddispcode;
        string CurrencyCode = ProductManager.selectedProdCurrCode;
        string CountryCode = SelectedAddBeneCountry.seladdbenecount;
        bool isUpdating = false;

        public wExchangecalculator()
        {
            InitializeComponent();


            if (TokenManager.Langofsoft == "ar")
            {
                //  backbtn.Content = "يرجع";
                //  exratecal.Text = "سعر الصرف:";
            }

            SeriesCollection = new SeriesCollection
            {
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0, 10),
                        new ObservablePoint(4, 7),
                        new ObservablePoint(5, 3),
                        new ObservablePoint(7, 6),
                        new ObservablePoint(10, 8)
                    },
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0, 2),
                        new ObservablePoint(2, 5),
                        new ObservablePoint(3, 6),
                        new ObservablePoint(6, 8),
                        new ObservablePoint(10, 5)
                    },
                    PointGeometrySize = 15
                },
                new LineSeries
                {
                    Values = new ChartValues<ObservablePoint>
                    {
                        new ObservablePoint(0, 4),
                        new ObservablePoint(5, 5),
                        new ObservablePoint(7, 7),
                        new ObservablePoint(9, 10),
                        new ObservablePoint(10, 9)
                    },
                    PointGeometrySize = 15
                }
            };

            DataContext = this;
            Unloaded += OnPageUnloaded;
        }

        private async void Page_Load(object sender, RoutedEventArgs e)
        {
            try
            {
                Environment.SetEnvironmentVariable("WEBVIEW2_DEFAULT_BACKGROUND_COLOR", "FF10358E"); // Blue
                await webView.EnsureCoreWebView2Async(); // Ensure WebView2 is initialized
                webView.CoreWebView2.Navigate("https://www.wallstreetkwt.com/cp/public/");
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

        private void StartTimer()
        {
            disposableTimer?.Cancel();
            disposableTimer = new DisposableTimer(() => DoSomethingAfter3Seconds(), 3);
        }

        private void DoSomethingAfter3Seconds()
        {
            REFRESHCURRENCYMETHOD("no");
        }

        private async void REFRESHCURRENCYMETHOD(string buttonclick)
        {

            //MessageBox.Show(ProductManager.selecteddispcode);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/v1/sxremittance/ControlValue");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var finalamount = "";

            if (fcorlcswitch == "FC")
            {
                //  finalamount = amounttosendTextbox.Text;
            }

            if (fcorlcswitch == "LC")
            {
                //  finalamount = kdamount.Text;
            }

            // currencymoneytoTextBlock.Text = CurrencyCode;

            var content = new StringContent("{\r\n  \"ProductCode\": \"" + productcode + "\"," +
                "\r\n  \"CurrencyCode\": \"" + CurrencyCode + "\"," +
                "\r\n  \"CountryCode\": \"" + CountryCode + "\"," +
                "\r\n  \"DisbursalCode\": \"" + disbtypecode + "\"," +
                "\r\n  \"Amount\": " + finalamount + "," +
                "\r\n  \"ReceiverCityId\": \"\"," +
                "\r\n  \"PayerId\": \"\"," +
                "\r\n  \"BankCode\": \"\"," +
                "\r\n  \"PayingAgentId\": \"\"," +
                "\r\n  \"ReceiverTownId\": \"\"," +
                "\r\n  \"RateType\": \"" + fcorlcswitch + "\"\r\n}", null, "application/json");

            string jsonContent = content.ReadAsStringAsync().Result; // Replace with content.Encoding if known
            MessageBox.Show(jsonContent);


            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            MessageBox.Show(await response.Content.ReadAsStringAsync());



            if (buttonclick == "yes")
            {
                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        // UpdateComboBoxsource(doc.RootElement);
                        updateadmounts(doc.RootElement);
                        wPaymentmethod mainpage = new wPaymentmethod();
                        NavigationService.Navigate(mainpage);

                    }
                }
            }
            else
            {
                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        // UpdateComboBoxsource(doc.RootElement);
                        updateadmounts(doc.RootElement);

                    }
                }
            }



        }

        private void updateadmounts(JsonElement root)
        {

            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("SessionID", out JsonElement SessionIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(SessionIDNameElement.GetString());
                        // TransferManagers1.SetSessionid(SessionIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("CurrencyCode", out JsonElement IDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        // TransferManagers1.SetCurrencyCode(IDNameElement.ToString());
                    }

                    if (item.TryGetProperty("CountryCode", out JsonElement CountryCodeIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        // TransferManagers1.SetCountryCode(CountryCodeIDNameElement.ToString());
                    }
                    if (item.TryGetProperty("Rate", out JsonElement RateIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        // TransferManagers1.SetRate(RateIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("Commison", out JsonElement CommisonIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //   TransferManagers1.SetCommison(CommisonIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("DiscoutPercentage", out JsonElement DiscoutPercentageIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //  TransferManagers1.SetDiscoutPercentage(DiscoutPercentageIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("Operator", out JsonElement OperatorIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        // TransferManagers1.SetOperator(OperatorIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("LCAmt", out JsonElement LCAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //  TransferManagers1.SetLCAmt(LCAmtIDNameElement.ToString());
                    }


                    if (item.TryGetProperty("NetAmt", out JsonElement NetAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //   TransferManagers1.SetNetAmt(NetAmtIDNameElement.ToString());
                    }


                    if (item.TryGetProperty("FCAmt", out JsonElement FCAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        // TransferManagers1.SetFCAmt(FCAmtIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("VatAmt", out JsonElement VatAmtIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //  TransferManagers1.SetVatAmt(VatAmtIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("VatPec", out JsonElement VatPecIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //  TransferManagers1.SetVatPec(VatPecIDNameElement.ToString());
                    }



                    if (item.TryGetProperty("DiscoutValue", out JsonElement DiscoutValueIDNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //  MessageBox.Show(IDNameElement.GetString());
                        //  TransferManagers1.SetDiscoutValue(DiscoutValueIDNameElement.ToString());
                    }

                    if (item.TryGetProperty("LCAmt", out JsonElement LCAmtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                        isUpdating = true;
                        //kdamount.Text = LCAmtNameElement.ToString();
                        isUpdating = false;

                        //  tal.Content = LCAmtNameElement.ToString() + " KWD";
                    }

                    if (item.TryGetProperty("Commison", out JsonElement CommisonNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());

                        //kdamount.Text = LCAmtNameElement.ToString();
                        //  tfl.Content = CommisonNameElement.ToString() + " KWD";

                    }


                    if (item.TryGetProperty("NetAmt", out JsonElement NetAmtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());


                        //  totl.Content = NetAmtNameElement.ToString() + " KWD";
                    }

                    if (item.TryGetProperty("FCAmt", out JsonElement FCamtNameElement))
                    {
                        //sourcecombo.Items.Add(purpNameElement.GetString());
                        //MessageBox.Show(LCAmtNameElement.GetString());
                        isUpdating = true;
                        //amounttosendTextbox.Text = FCamtNameElement.ToString();
                        isUpdating = false;

                        //    ral.Content = FCamtNameElement.ToString() + " " + CurrencyCode;
                    }


                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }

        private void OnPageUnloaded(object sender, RoutedEventArgs e)
        {
            disposableTimer?.Cancel();
        }
    }
}
