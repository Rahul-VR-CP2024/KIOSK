using Exchange.Common;
using Exchange.Knet;
using Exchange.Managers;
using System.Diagnostics;
using System.IO;
using System.Management;
using System.Text;
using System.Windows;
using System.Xml.Linq;

namespace Exchange
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    //WindowStyle="None" ResizeMode="NoResize"
    public partial class MainWindow : Window
    {

        private InactivityMonitor _inactivityMonitor;


        public MainWindow()
        {
            InitializeComponent();

            // Set up inactivity monitor to navigate after 3 minutes (180 seconds)
            _inactivityMonitor = new InactivityMonitor(
                TimeSpan.FromMinutes(3),
                () =>
                {
                    LoginManager.Logout();
                    // Action to perform when inactivity is detected
                    //wSelectbeneficary wx = new wSelectbeneficary();
                    //NavigationService.Navigate(wx);
                    NavigationManager.NavigateToHome();
                }
            );

            // Start monitoring inactivity
            _inactivityMonitor.StartMonitoring();
            //ReadFile();
            //scannerconsole();
            //testconsole();
            //return;

            string machineid = "";

            string machinshman = "";

            #if DEBUG
                NavigationManager.NavigateToHome();
                return;
            #endif

            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT VolumeSerialNumber FROM Win32_LogicalDisk WHERE DeviceID='C:'");
            foreach (ManagementObject mo in searcher.Get())
            {
                string diskSerialNumber = mo["VolumeSerialNumber"].ToString();
                // ...
                string randomString = RandomStringGenerator.GenerateRandomString(20); // Generates a 10-character random string
                //Console.WriteLine(randomString);
                machineid = diskSerialNumber;
                //MessageBox.Show(randomString + "B" + diskSerialNumber + "b" + randomString);
                //MessageBox.Show("" + diskSerialNumber);
                machinshman = randomString + "B" + diskSerialNumber + "b" + randomString;
            }

            //SALMIYA ABWORLD OFFICE KIOSK MACHINE
            if(machineid == "A91B1075" || machineid == "3E9AEA71")
            {
               // return;
            }
            else if(machineid == "120DF2D8")//SALMIYA WALLSTREET OFFICE KIOSK MACHINE
            { 

               // return;
            }
            else if (machineid == "120DF2D8")
            { //DEV ENVIROMENT LAPTOP
               // return;
            }
            else if (machineid == "72690CE2") //DEV HYPER-V
            {
                //return;
            }
            else if (machineid == "384C7CE3") // PC OF SUDHIR / ARIF
            {
                //return;
            }
            else if (machineid == "C08F6D8F") // PC ARIF
            {
                //return;
            }
            else
            {
                MessageBox.Show("Unauthorized " + machinshman);
                return;
            }
            //MessageBox.Show("!!! TESTING MODE !!!");
            // MainFrame.Navigate(new Page1());
            //MainFrame.Navigate(new Login2());
            //MainFrame.Navigate(new wComponents());
            //MainFrame.Navigate(new WelcomePage());

            //Initiate KNET
            try
            {
                Init();
            } catch (Exception ex)
            {
                MessageBox.Show("KNET MACHINE NOT INITIATED");
            }
            //MainFrame.Navigate(new wThankyou());
            NavigationManager.NavigateToHome();

            Debug.WriteLine("This is a debug message.");

        }


        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            // Stop monitoring when the window is closed
            _inactivityMonitor.StopMonitoring();
        }


        private void ReadFile()
        {
            try
            {
                // Specify the file path
                //string filePath = "SCHASH.txt";
                string filePath = "Hi.txt";

                // Option 1: Read all text as a single string
                string content = File.ReadAllText(filePath);
                Console.WriteLine("File Content (ReadAllText):");
                Console.WriteLine(content);
                MessageBox.Show(content);

                // Option 2: Read all lines as an array of strings
                string[] lines = File.ReadAllLines(filePath);
                Console.WriteLine("\nFile Content (ReadAllLines):");
                foreach (string line in lines)
                {
                    Console.WriteLine(line);
                    MessageBox.Show(line);
                }

                // Specify the file path
                filePath = "SCHASH.txt";

                // Read the Base64 content from the file
                string base64Content = File.ReadAllText(filePath);

                // Decode the Base64 string
                byte[] decodedBytes = Convert.FromBase64String(base64Content);

                // Convert the decoded bytes back to a readable string (assuming it's text)
                string decodedText = Encoding.UTF8.GetString(decodedBytes);

                // Display the decoded text in a MessageBox
                MessageBox.Show(decodedText, "Decoded Content");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error reading file: " + ex.Message);
            }
        }

        public void testconsole()
        {
            // Path to the .NET 4 Console application
            string consoleAppPath = @"C:\Users\Burhan\Desktop\PACIMID\PACIMID\bin\Debug\PACIMID.exe";

            // Parameters you want to pass to the Console app
            string parameters = "param1 param2";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = consoleAppPath,
                Arguments = parameters,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                // Optionally read the output if needed
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                MessageBox.Show(output);
                //MessageBox.Show(error);
                // Handle the output or error as needed
            }
        }
        public void scannerconsole()
        {
            // Path to the .NET 4 Console application
            string consoleAppPath = @"C:\kiosk\scan\PACIMID.exe";

            // Parameters you want to pass to the Console app
            string parameters = "param1 param2";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = consoleAppPath,
                Arguments = parameters,
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                // Optionally read the output if needed
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                //MessageBox.Show(output);
                //MessageBox.Show(error);
                // Handle the output or error as needed
            }
        }

        public class RandomStringGenerator
        {
            private static Random random = new Random();
            private static string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            public static string GenerateRandomString(int
         length)
            {
                char[] buffer = new char[length];
                for (int i = 0; i < length; i++)
                {
                    buffer[i] = chars[random.Next(chars.Length)];
                }
                return new string(buffer);

            }
        }

        //KNET STUFF
        //static private string TestTerminalId = "12700130";

        static private string TestTerminalId = Variable.kioskidno;
        static void Init()
        {

            //turning this to true will display a lot of information that might hide the command menu
            Utility.DEBUG_MODE = false;


            using eSocketClient client = new eSocketClient();

            Console.WriteLine("Sending initialization request..");
            //Construct the proper XML 
            XDocument doc = XmlRequest.Initialization(TestTerminalId);
            //Convert it to bytes and write it to the TCP connection
            client.TcpWrite(Utility.XmlToBytes(doc));
        }
    }
}