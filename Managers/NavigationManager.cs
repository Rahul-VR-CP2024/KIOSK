using Exchange.Pages;
using System.Windows;

namespace Exchange.Managers
{
    public static class NavigationManager
    {
        public static void NavigateToHome()
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
               
                if (mainWindow.MainFrame.Content is not wMainPage)
                {
                    mainWindow.MainFrame.Navigate(new wMainPage());
                }

                while (mainWindow.MainFrame.CanGoBack)
                {
                    mainWindow.MainFrame.RemoveBackEntry();
                }
            }
        }
    }
}
