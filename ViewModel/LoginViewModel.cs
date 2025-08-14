using Exchange.Model;
using Exchange.Pages;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Navigation;

namespace Exchange.ViewModel
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private string _username;
        private string _password;
        private bool _isLoggedIn;
        private readonly NavigationService _navigationService;

        public string Username
        {
            get { return _username; }
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(Username));
                }
            }

        }

        public string Password
        {
            get { return _password; }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        public bool IsLoggedIn
        {
            get { return _isLoggedIn; }
            set
            {
                if (_isLoggedIn != value)
                {
                    _isLoggedIn = value;
                    OnPropertyChanged(nameof(IsLoggedIn));
                }
            }

        }

        public RelayCommand LoginCommand { get; }

        public LoginViewModel(NavigationService navigationService)
        {
            _navigationService = navigationService;
            LoginCommand = new RelayCommand(Login, CanLogin);

            Debug.WriteLine("This is a debug message.");
        }

        private void Login(object parameter)
        {

            Debug.WriteLine("This is a debug message.");
            var userModel = new UserModel { Username = "user", Password = "password" };

            if (Username == userModel.Username && Password == userModel.Password)
            {
                IsLoggedIn = true;

                // Use NavigationService to navigate to the WelcomePage
                _navigationService.Navigate(new WelcomePage());
            }
            else
            {
                IsLoggedIn = false;
                System.Windows.MessageBox.Show("Invalid username or password.");
            }
        }

        private bool CanLogin(object parameter)
        {
            Debug.WriteLine("This is a debug message.");
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            Debug.WriteLine("This is a debug message.");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
