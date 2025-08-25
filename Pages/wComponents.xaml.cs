using LiveCharts;
using LiveCharts.Defaults;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using LineSeries = LiveCharts.Wpf.LineSeries;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wComponents.xaml
    /// </summary>
    /// 
    // <lvc:CartesianChart Series="{Binding SeriesCollection}"/>
    public partial class wComponents : Page
    {
        public ObservableCollection<Country> Countries { get; set; }
        public SeriesCollection SeriesCollection { get; set; }

        public Country SelectedCountry { get; set; }

        public wComponents()
        {
            InitializeComponent();

            // Sample data (replace with your actual data)
            Countries = new ObservableCollection<Country>
            {
                new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/KWTFlag.png")), CountryName = "United States" },
                 new Country { FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/KWTFlag.png")), CountryName = "Canada" },
                 
                //new Country { FlagImage = new BitmapImage(new Uri("/Images/KWTFlag.png")), CountryName = "Canada" },
                // Add more countries as needed
            };

            countryListView.ItemsSource = Countries;



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



        }

        private void countryListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Update the SelectedCountry property when the selection changes
            SelectedCountry = countryListView.SelectedItem as Country;
            if (SelectedCountry != null)
            {
                MessageBox.Show($"Selected Country: {SelectedCountry.CountryName}");
            }
        }

        public class Country
        {
            public BitmapImage FlagImage { get; set; }
            public string CountryName { get; set; }
        }


    }
}
