using Exchange.Managers;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wBeneficiary.xaml
    /// </summary>
    /// 
   

    public partial class wBeneficiary : Page
    {

        public static readonly DependencyProperty EditCommandProperty =
        DependencyProperty.RegisterAttached(
            "EditCommand",
            typeof(ICommand),
            typeof(wBeneficiary),
            new FrameworkPropertyMetadata(null, OnEditCommandPropertyChanged));

        public static ICommand GetEditCommand(DependencyObject obj)
        {
            return (ICommand)obj.GetValue(EditCommandProperty);
        }

        public static void SetEditCommand(DependencyObject obj, ICommand value)
        {
            obj.SetValue(EditCommandProperty, value);
        }

        private static void OnEditCommandPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is Button button && e.NewValue is ICommand command)
            {
                button.Click += (sender, args) =>
                {
                    // Find the parent ListViewItem (data row) using VisualTreeHelper
                    var listViewItem = VisualTreeHelper.GetParent(button) as ListViewItem;
                    if (listViewItem != null)
                    {
                       // command.Execute(listViewItem.DataContext); // Pass the data context

                        var dataItem = listViewItem.DataContext;


                        if (dataItem is MyDataItem myDataItem) // Cast to your data item type
                        {
                            string beneValue = myDataItem.Bene;
                            MessageBox.Show($"You clicked Edit for Beneficiary Name: {beneValue}");
                            command.Execute(dataItem); // Pass the data context
                        }
                    }
                };
            }
        }

        public class MyDataItem
        {
            public string Bene { get; set; }
            // ... other properties
            public ICommand EditCommand { get; set; } // Implement this command to handle button clicks (optional)
        }





        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry { get; set; }

       // public ICommand selectBeneCommand { get; private set; }

        public RoutedCommand selectBeneCommand { get; private set; }

        public wBeneficiary()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                benetitle.Text = "المستفيد";
                addnewbtn.Content = "إضافة مستفيد جديد";

            }

            //selectBeneCommand = new DelegateCommand(OnEditButtonClicked);

            selectBeneCommand = new RoutedCommand();
            CommandManager.RegisterClassCommandBinding(typeof(Button),
                new CommandBinding(selectBeneCommand, OnEditCommandExecuted));


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

           // countryListView.ItemsSource = Countries;
        }


        private void OnEditCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (sender is Button button)
            {
                // Access the Bene property from the button's DataContext (data item)
                string beneValue = (string)button.DataContext.GetType().GetProperty("Bene").GetValue(button.DataContext);

                // Process the beneValue here (e.g., display a message box)
                MessageBox.Show($"You clicked Edit for Beneficiary Name: {beneValue}");
            }
        }


        private void OnEditButtonClicked(object parameter)
        {
            if (parameter is string beneValue)
            {
                // Access and process the Bene value here
                MessageBox.Show($"You clicked Edit for Beneficiary Name: {beneValue}");
            }
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
               //  MessageBox.Show($"Selected Country: {SelectedCountry.CountryName}");
            }
        }

        //public SeriesCollection SeriesCollection { get; set; }

        private void RESEND_Click(object sender, RoutedEventArgs e)
        {

            //var myValue = ((Button)sender).Tag;
            //MessageBox.Show(myValue.ToString());
            //SelectedBeneficiaryManager.SetBENE_SLNO(myValue.ToString());

            var myValue = ((Button)sender).Tag;
            SelectedBeneficiaryManager.SetBENE_SLNO(myValue.ToString());
            //MessageBox.Show(myValue.ToString());

            SelectedCountry = countryListView.SelectedItem as Country;
            if (SelectedCountry != null)
            {
                 // MessageBox.Show($"Selected Country: {SelectedCountry.CountryName}");
            }

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            waddbeneficiary mainpage = new waddbeneficiary("edit");
            NavigationService.Navigate(mainpage);

        }

        //
        private void addnewben(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            //waddbeneficiary mainpage = new waddbeneficiary();
            wSelectcountry mainpage = new wSelectcountry();
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

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //API CALLING STARTS

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary​/get-beneficiary-list");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

           

            //130824
            var content = new StringContent("{ \n    \"appMemberCode\":" + LoginManager.RemCode + ",\n    \"transferbModeCode\":\""+BCManager.selectedoptionborc+"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"productCode\":\"\"\n    }", null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());

           // MessageBox.Show(await response.Content.ReadAsStringAsync());

            LoadCountries();
            //API CALLING ENDS
        }



        public async Task LoadCountries()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://"+Variable.apiipadd+ "/api/Beneficiary​/get-beneficiary-list");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            //130824
            var content = new StringContent("{ \n    \"remID\":"+ LoginManager.RemCode + ",\n    \"transferModeCode\":\""+BCManager.selectedoptionborc+"\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
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
                        BANK = dataElement.GetProperty("BENE_BANK").GetString(),
                        stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                        FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                        //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                    });
                }
            }

            countryListView.ItemsSource = Countries;
        }

        
    }
}
