using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace Exchange.Pages
{
    public class RichMessageBox : Window
    {
        private RichTextBox _richTextBox;

        public RichMessageBox(string message)
        {
            Title = "Log Message";

            // Option 2: Setting Width and Height Directly
            Width = 400;
            Height = 300;


            //SizeToContent = SizeToContent.WidthAndHeight;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            _richTextBox = new RichTextBox();
            _richTextBox.Document.Blocks.Add(new Paragraph(new Run(message)));
            //_richTextBox.IsReadOnly = true;

            Content = _richTextBox;
        }

        public static void Show(string message)
        {
            //DISABLE THIS IF LIVE VERSION
            //to show pop up
           // new RichMessageBox(message).ShowDialog();



            //TO MAKE A NEW TEXT FILE START
            // Example error message
            //string errorMessage = "An error occurred. Please contact support.";
            string errorMessage = message;

            // Get the directory of the executable file
            string appPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            // Create the file path
            string filePath = Path.Combine(appPath, "log.txt");

            try
            {

                {//NEW
                 // Create a new file or overwrite an existing one
                    //using (StreamWriter writer = new StreamWriter(filePath))
                    //{
                    //    writer.WriteLine(errorMessage);
                    //}
                    //MessageBox.Show("Error message written to error.txt");

                    if (!File.Exists(filePath))
                    {
                        using (StreamWriter writer = new StreamWriter(filePath))
                        {
                            writer.WriteLine("----------------------------------------------------");
                        }
                        //MessageBox.Show("Error message written to error.txt");
                    }
                    else
                    {
                       // MessageBox.Show("File already exists.");
                    }
                }

               

                { //APPEND
                  // Append to the existing file (create if it doesn't exist)
                    //using (StreamWriter writer = new StreamWriter(filePath, true, System.Text.Encoding.UTF8))
                    //{
                    //    writer.WriteLine(errorMessage);
                    //}

                    //MessageBox.Show("Error message appended to error.txt");


                }

                { //PREPEND
                    // Read the existing file contents
                    string existingContent = File.ReadAllText(filePath);

                    // Prepend the new message
                    string newContent = errorMessage + Environment.NewLine + "----------------------------------------------------" + Environment.NewLine + existingContent;

                    // Write the modified content back to the file
                    File.WriteAllText(filePath, newContent);

                   // MessageBox.Show("Error message prepended to error.txt");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error writing to file: " + ex.Message);
            }

            //TO MAKE A NEW TEXT FILE END









        }
    }
}
