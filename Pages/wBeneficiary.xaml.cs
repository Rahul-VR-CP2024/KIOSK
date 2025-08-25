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

        public class MyDataItem
        {
            public string Bene { get; set; }
            // ... other properties
            public ICommand EditCommand { get; set; } // Implement this command to handle button clicks (optional)
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

        public ObservableCollection<Country> Countries { get; set; }

        public Country SelectedCountry { get; set; }

        public RoutedCommand selectBeneCommand { get; private set; }

        public wBeneficiary()
        {
            try
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

            // countryListView.ItemsSource = Countries;
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
                var request = new HttpRequestMessage(HttpMethod.Post, "http://" + Variable.apiipadd + "/api/Beneficiary​/get-beneficiary-list");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                //130824
                var content = new StringContent("{ \n    \"remID\":" + LoginManager.RemCode + ",\n    \"transferModeCode\":\"" + BCManager.selectedoptionborc + "\",\n    \"beneSLNO\":0,\n    \"beneCon\":\"\",\n    \"channelCode\":\"\",\n    \"ProductCode\":\"\"\n    }", null, "application/json");
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

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
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void OnEditCommandExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            try
            {
                if (sender is Button button)
                {
                    string beneValue = (string)button.DataContext.GetType().GetProperty("Bene").GetValue(button.DataContext);

                    MessageBox.Show($"You clicked Edit for Beneficiary Name: {beneValue}");
                }
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

        private void countryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the SelectedCountry property when the selection changes
            SelectedCountry = countryListView.SelectedItem as Country;
            if (SelectedCountry != null)
            {
               //  MessageBox.Show($"Selected Country: {SelectedCountry.CountryName}");
            }
        }

        private void RESEND_Click(object sender, RoutedEventArgs e)
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
