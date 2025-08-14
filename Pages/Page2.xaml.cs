using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for Page2.xaml
    /// </summary>
    public partial class Page2 : Page
    {
        public string Parameter { get; }
        public Page2(string parameter)
        {
            InitializeComponent();
            Parameter = parameter;
            DataContext = this;
        }
    }
}
