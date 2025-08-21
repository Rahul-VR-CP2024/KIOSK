using Exchange.Common;
using Exchange.Managers;
using System.Drawing;
//using ESCPOS_NET;
//using ESCPOS_NET.Emitters;
//using ESCPOS_NET.Utilities;
//using ESC_POS_USB_NET;
//using ESC_POS_USB_NET.Printer;
//using ESC_POS_USB_NET.Printer;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using Brushes = System.Drawing.Brushes;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wThankyou.xaml
    /// </summary>
    public partial class wThankyou : Page
    {

        public void PrintReceipt()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Receipt";

            pd.PrintPage += (sender, e) =>
            {
                // Your printing logic here
                e.Graphics.DrawString("Hello, World!", new Font("Arial", 12), System.Drawing.Brushes.Black, 10, 10);
            };

            pd.Print();
        }

        public void PrintReceipt1()
        {
            //PrintDocument pd = new PrintDocument();
            //pd.PrinterSettings.PrinterName = "Receipt";

            //// Set the page size for the receipt (adjust as needed)
            //pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", 280, 1000);

            //pd.PrintPage += PrintPage;

            //try
            //{
            //    pd.Print();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Printing error: " + ex.Message);
            //}


            PrintDocument pd = new PrintDocument();
            pd.PrinterSettings.PrinterName = "Receipt";

            // Set only the width for the receipt
            int widthInHundredthsOfInch = (int)(126.9 / 25.4 * 100); // Convert 126.9mm to hundredths of inch
            //MessageBox.Show(widthInHundredthsOfInch+"");
            pd.DefaultPageSettings.PaperSize = new PaperSize("Custom", widthInHundredthsOfInch, 0);

            // Set to roll paper
            //pd.DefaultPageSettings.PaperSource = PaperSourceKind.Roll;

            pd.PrintPage += PrintPage;

            try
            {
                pd.Print();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Printing error: " + ex.Message);
            }
        }

        private void PrintPageBackup(object sender, PrintPageEventArgs e)
        {
            int paperWidth = 290; // Convert 126.9mm to hundredths of inch
            Graphics g = e.Graphics;
            Font regularFont = new Font("Arial", 10);
            Font boldFont = new Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Font arabicFont = new Font("Arial", 10);

            int startX = 10;
            int startY = 10;
            int offset = 20;

            // Print logo
            //string imagePath = @"C:\path\to\your\logo.png";
            string imagePath = @"C:\logo.png";
            //image.png
            if (System.IO.File.Exists(imagePath))
            {
                System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath);
                //g.DrawImage(logo, startX, startY, 100, 50);
                g.DrawImage(logo, 50, startY, 200, 100);
                startY += 120;
            }

            // Print header
            //g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            //g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            //startY += offset;

            // Print header
            g.DrawString("Transaction Processed Successfully", regularFont, Brushes.Black, startX, startY);
            startY += offset;

            //g.DrawString("Receipt", boldFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            //if (System.IO.File.Exists(imagePath))
            //{
            //    using (System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath))
            //    {
            //        int logoWidth = 100; // Set your desired logo width
            //        int logoHeight = 50; // Set your desired logo height
            //        int logoX = (paperWidth - logoWidth) / 2; // Center the logo horizontally
            //        g.DrawImage(logo, logoX, startY, logoWidth, logoHeight);
            //        startY += logoHeight + 10; // Add some space after the logo
            //    }
            //}

            // Print header
            //string companyName = "Your Company Name";
            //string receiptTitle = "Receipt";

            // Center "Your Company Name"
            //SizeF companyNameSize = g.MeasureString(companyName, boldFont);
            //float companyNameX = (paperWidth - companyNameSize.Width) / 2;
            //g.DrawString(companyName, boldFont, Brushes.Black, companyNameX, startY);
            //startY += (int)companyNameSize.Height + 5;

            // Center "Receipt"
            //SizeF receiptTitleSize = g.MeasureString(receiptTitle, boldFont);
            //float receiptTitleX = (paperWidth - receiptTitleSize.Width) / 2;
            //g.DrawString(receiptTitle, boldFont, Brushes.Black, receiptTitleX, startY);
            //startY += (int)receiptTitleSize.Height + 10;

            // Print receipt details
            //g.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), regularFont, Brushes.Black, startX, startY);
            //startY += offset;

            //g.DrawString("Transaction ID: 123456", regularFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            // Define your strings
            string label = "Date";
            string separator = ":";
            string dateValue = "22/10/2024 12:42:00";

            // Measure the width of the date string
            SizeF dateSize = g.MeasureString(dateValue, regularFont);

            // Calculate positions
            float labelX = startX;
            float separatorX = 100; // You can adjust this if needed
            float dateX = paperWidth - dateSize.Width - 10; // 10 is right margin, adjust as needed

            // Draw the strings
            g.DrawString(label, regularFont, Brushes.Black, labelX, startY);
            g.DrawString(separator, regularFont, Brushes.Black, separatorX, startY);
            g.DrawString(dateValue, regularFont, Brushes.Black, dateX, startY);

            startY += offset;

            // Print items
            //g.DrawString("Date", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("22/10/2024 12:42:00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("24220130000176", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("3213213123", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("24220130000176", regularFont, Brushes.Black, paperWidth - g.MeasureString("24220130000176", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("3213213123", regularFont, Brushes.Black, paperWidth - g.MeasureString("3213213123", regularFont).Width - 10, startY);

            startY += offset;



            // Draw the strings
            g.DrawString("Sender Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("Imran Khan", regularFont, Brushes.Black, paperWidth - g.MeasureString("Imran Khan", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Receiver Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("s s s", regularFont, Brushes.Black, paperWidth - g.MeasureString("s s s", regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("66.800 KWD", regularFont, Brushes.Black, paperWidth - g.MeasureString("66.800 KWD", regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Fee", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("2.000 KWD", regularFont, Brushes.Black, paperWidth - g.MeasureString("2.000 KWD", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Promo Discount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("0.000 KWD", regularFont, Brushes.Black, paperWidth - g.MeasureString("0.000 KWD", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Total", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("68.800 KWD", regularFont, Brushes.Black, paperWidth - g.MeasureString("66.800 KWD", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Receive Money", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString("800.000 AED", regularFont, Brushes.Black, paperWidth - g.MeasureString("800.000 AED", regularFont).Width - 10, startY);

            startY += offset;


            g.DrawString("------------------------------------------------------", boldFont, Brushes.Black, startX, startY);
            startY += offset;

            g.DrawString("KNET Details", boldFont, Brushes.Black, startX, startY);
            startY += offset;


            // Draw the strings
            g.DrawString("Date / Time", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Terminal ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Action Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Authorization No", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Merchant ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Retrieval Ref Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Pan Entry Mode", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Response Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Product Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Label", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Identifier", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(".", regularFont, Brushes.Black, paperWidth - g.MeasureString(".", regularFont).Width - 10, startY);

            startY += offset;
            startY += offset;
            //// Print items
            //g.DrawString("Item 1", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$10.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            //g.DrawString("Item 2", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$15.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            //// Print total
            //g.DrawString("Total:", boldFont, Brushes.Black, startX, startY);
            //g.DrawString("$25.00", boldFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            // Print Arabic text
            //string arabicText = "شكرا لك على تسوقك معنا";
            //g.DrawString(arabicText, arabicFont, Brushes.Black, startX, startY);
            //startY += offset;

            // Print footer
            g.DrawString("Thank you for Choosing Wall Street", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Exchange Co.", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Customer Care : +965-1822055", regularFont, Brushes.Black, startX, startY);
            startY += offset;
        }


        private void PrintPagexx(object sender, PrintPageEventArgs e)
        {
            int paperWidth = 290; // Convert 126.9mm to hundredths of inch
            Graphics g = e.Graphics;
            Font regularFont = new Font("Arial", 10);
            Font boldFont = new Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Font arabicFont = new Font("Arial", 10);

            int startX = 10;
            int startY = 10;
            int offset = 20;

            // Print logo
            //string imagePath = @"C:\path\to\your\logo.png";
            string imagePath = @"C:\logo.png";
            //image.png
            if (System.IO.File.Exists(imagePath))
            {
                System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath);
                //g.DrawImage(logo, startX, startY, 100, 50);
                g.DrawImage(logo, 50, startY, 200, 100);
                startY += 120;
            }

            // Print header
            //g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            startY += offset;

            // Print header
            g.DrawString("Transaction Processed Successfully", regularFont, Brushes.Black, startX, startY);
            startY += offset;

            //g.DrawString("Receipt", boldFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            //if (System.IO.File.Exists(imagePath))
            //{
            //    using (System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath))
            //    {
            //        int logoWidth = 100; // Set your desired logo width
            //        int logoHeight = 50; // Set your desired logo height
            //        int logoX = (paperWidth - logoWidth) / 2; // Center the logo horizontally
            //        g.DrawImage(logo, logoX, startY, logoWidth, logoHeight);
            //        startY += logoHeight + 10; // Add some space after the logo
            //    }
            //}

            // Print header
            //string companyName = "Your Company Name";
            //string receiptTitle = "Receipt";

            // Center "Your Company Name"
            //SizeF companyNameSize = g.MeasureString(companyName, boldFont);
            //float companyNameX = (paperWidth - companyNameSize.Width) / 2;
            //g.DrawString(companyName, boldFont, Brushes.Black, companyNameX, startY);
            //startY += (int)companyNameSize.Height + 5;

            // Center "Receipt"
            //SizeF receiptTitleSize = g.MeasureString(receiptTitle, boldFont);
            //float receiptTitleX = (paperWidth - receiptTitleSize.Width) / 2;
            //g.DrawString(receiptTitle, boldFont, Brushes.Black, receiptTitleX, startY);
            //startY += (int)receiptTitleSize.Height + 10;

            // Print receipt details
            //g.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), regularFont, Brushes.Black, startX, startY);
            //startY += offset;

            //g.DrawString("Transaction ID: 123456", regularFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            // Define your strings
            string label = "Date";
            string separator = ":";
            //string dateValue = wdatetimevalue.Content.ToString();
            string dateValue = string.IsNullOrEmpty(wdatetimevalue.Content?.ToString()) ? "" : wdatetimevalue.Content.ToString();

            // Measure the width of the date string
            SizeF dateSize = g.MeasureString(dateValue, regularFont);

            // Calculate positions
            float labelX = startX;
            float separatorX = 100; // You can adjust this if needed
            float dateX = paperWidth - dateSize.Width - 10; // 10 is right margin, adjust as needed

            // Draw the strings
            g.DrawString(label, regularFont, Brushes.Black, labelX, startY);
            g.DrawString(separator, regularFont, Brushes.Black, separatorX, startY);
            g.DrawString(dateValue, regularFont, Brushes.Black, dateX, startY);

            startY += offset;

            // Print items
            //g.DrawString("Date", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("22/10/2024 12:42:00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("24220130000176", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("3213213123", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankpaymentid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankpaymentid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;



            // Draw the strings
            g.DrawString("Sender Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanksendername.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanksendername.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Receiver Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankreceivername.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankreceivername.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktransferamount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktransferamount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Fee", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktransferfee.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktransferfee.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Other Charges", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wothercharges.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wothercharges.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Promo Discount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankdiscount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankdiscount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Total", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktotal.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktotal.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Receive Money", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankreceiveamount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankreceiveamount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            g.DrawString("------------------------------------------------------", boldFont, Brushes.Black, startX, startY);
            startY += offset;

            g.DrawString("KNET Details", boldFont, Brushes.Black, startX, startY);
            startY += offset;


            // Draw the strings
            g.DrawString("Date / Time", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket12.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket12.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ktidlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ktidlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Terminal ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ktiddlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ktiddlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Action Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(kactlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(kactlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Authorization No", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(kauthnumlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(kauthnumlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(kctylbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(kctylbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Merchant ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(kmerchid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(kmerchid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket1.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket1.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Retrieval Ref Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket2.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket2.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket3.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket3.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Pan Entry Mode", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket4.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket4.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket5.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket5.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Response Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket6.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket6.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Product Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket7.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket7.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Label", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket9.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket9.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Identifier", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            g.DrawString(ket10.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(ket10.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;
            startY += offset;
            //// Print items
            //g.DrawString("Item 1", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$10.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            //g.DrawString("Item 2", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$15.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            //// Print total
            //g.DrawString("Total:", boldFont, Brushes.Black, startX, startY);
            //g.DrawString("$25.00", boldFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            // Print Arabic text
            //string arabicText = "شكرا لك على تسوقك معنا";
            //g.DrawString(arabicText, arabicFont, Brushes.Black, startX, startY);
            //startY += offset;

            // Print footer
            g.DrawString("Thank you for Choosing Wall Street", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Exchange Co.", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Customer Care : +965-1822055", regularFont, Brushes.Black, startX, startY);
            startY += offset;
        }


        private void PrintPage(object sender, PrintPageEventArgs e)
        {
            int paperWidth = 290; // Convert 126.9mm to hundredths of inch
            Graphics g = e.Graphics;
            Font regularFont = new Font("Arial", 10);
            Font boldFont = new Font("Arial", 12, System.Drawing.FontStyle.Bold);
            Font arabicFont = new Font("Arial", 10);

            int startX = 10;
            int startY = 10;
            int offset = 20;

            // Print logo
            //string imagePath = @"C:\path\to\your\logo.png";
            string imagePath = @"C:\logo.png";
            //image.png
            if (System.IO.File.Exists(imagePath))
            {
                System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath);
                //g.DrawImage(logo, startX, startY, 100, 50);
                g.DrawImage(logo, 50, startY, 200, 100);
                startY += 120;
            }

            // Print header
            //g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            //g.DrawString("WALL STREET EXCHANGE CO.", boldFont, Brushes.Black, startX, startY);
            //startY += offset;

            // Print header
            string headerlabel = tpslbl.Text;
            //g.DrawString("Transaction Processed Successfully", regularFont, Brushes.Black, startX, startY);
            g.DrawString(headerlabel, regularFont, Brushes.Black, startX, startY);
            startY += offset;

            //g.DrawString("Receipt", boldFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            //if (System.IO.File.Exists(imagePath))
            //{
            //    using (System.Drawing.Image logo = System.Drawing.Image.FromFile(imagePath))
            //    {
            //        int logoWidth = 100; // Set your desired logo width
            //        int logoHeight = 50; // Set your desired logo height
            //        int logoX = (paperWidth - logoWidth) / 2; // Center the logo horizontally
            //        g.DrawImage(logo, logoX, startY, logoWidth, logoHeight);
            //        startY += logoHeight + 10; // Add some space after the logo
            //    }
            //}

            // Print header
            //string companyName = "Your Company Name";
            //string receiptTitle = "Receipt";

            // Center "Your Company Name"
            //SizeF companyNameSize = g.MeasureString(companyName, boldFont);
            //float companyNameX = (paperWidth - companyNameSize.Width) / 2;
            //g.DrawString(companyName, boldFont, Brushes.Black, companyNameX, startY);
            //startY += (int)companyNameSize.Height + 5;

            // Center "Receipt"
            //SizeF receiptTitleSize = g.MeasureString(receiptTitle, boldFont);
            //float receiptTitleX = (paperWidth - receiptTitleSize.Width) / 2;
            //g.DrawString(receiptTitle, boldFont, Brushes.Black, receiptTitleX, startY);
            //startY += (int)receiptTitleSize.Height + 10;

            // Print receipt details
            //g.DrawString("Date: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), regularFont, Brushes.Black, startX, startY);
            //startY += offset;

            //g.DrawString("Transaction ID: 123456", regularFont, Brushes.Black, startX, startY);
            //startY += offset * 2;

            // Define your strings
            string label = "Date";
            string separator = ":";
            //string dateValue = wdatetimevalue.Content.ToString();
            string dateValue = string.IsNullOrEmpty(wdatetimevalue.Content?.ToString()) ? "" : wdatetimevalue.Content.ToString();

            // Measure the width of the date string
            SizeF dateSize = g.MeasureString(dateValue, regularFont);

            // Calculate positions
            float labelX = startX;
            float separatorX = 130; // You can adjust this if needed
            float dateX = paperWidth - dateSize.Width - 10; // 10 is right margin, adjust as needed

            // Draw the strings
            g.DrawString(label, regularFont, Brushes.Black, labelX, startY);
            g.DrawString(separator, regularFont, Brushes.Black, separatorX, startY);
            g.DrawString(dateValue, regularFont, Brushes.Black, dateX, startY);

            startY += offset;

            // Print items
            //g.DrawString("Date", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("22/10/2024 12:42:00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("24220130000176", regularFont, Brushes.Black, 200, startY);
            //startY += offset;
            // Print items
            //g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            //g.DrawString(":", regularFont, Brushes.Black, 100, startY);
            //g.DrawString("3213213123", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            g.DrawString("TT Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wttid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wttid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Mobile", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankpaymentid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankpaymentid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            string wthanksendernameoriginalString = wthanksendername.Content.ToString();

            // Ensure the string is not null or empty
            if (!string.IsNullOrEmpty(wthanksendernameoriginalString))
            {
                // Truncate the string to 14 characters
                string truncatedString = wthanksendernameoriginalString.Length > 14
                                            ? wthanksendernameoriginalString.Substring(0, 14)
                                            : wthanksendernameoriginalString;

                // Assign the truncated string to the desired control
                //wthanksendername.Content = truncatedString;
                wthanksendernameoriginalString = truncatedString;
                //MessageBox.Show("" + originalString);
            }

            // Draw the strings
            g.DrawString("Sender Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            //g.DrawString(wthanksendername.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanksendername.Content.ToString(), regularFont).Width - 10, startY);
            g.DrawString(wthanksendernameoriginalString, regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanksendernameoriginalString, regularFont).Width - 10, startY);

            startY += offset;


            string wthankreceivernameoriginalString = wthankreceivername.Content.ToString();

            // Ensure the string is not null or empty
            if (!string.IsNullOrEmpty(wthankreceivernameoriginalString))
            {
                // Truncate the string to 14 characters
                string truncatedString = wthankreceivernameoriginalString.Length > 14
                                            ? wthankreceivernameoriginalString.Substring(0, 14)
                                            : wthankreceivernameoriginalString;

                // Assign the truncated string to the desired control
                //wthanksendername.Content = truncatedString;
                wthankreceivernameoriginalString = truncatedString;
                //MessageBox.Show("" + originalString);
            }

            // Draw the strings
            g.DrawString("Receiver Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            //g.DrawString(wthankreceivername.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankreceivername.Content.ToString(), regularFont).Width - 10, startY);
            g.DrawString(wthankreceivernameoriginalString, regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankreceivernameoriginalString, regularFont).Width - 10, startY);


            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktransferamount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktransferamount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Transfer Fee", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktransferfee.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktransferfee.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Other Charges", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wothercharges.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wothercharges.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            // Draw the strings
            g.DrawString("Promo Discount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankdiscount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankdiscount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Total", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthanktotal.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthanktotal.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Receive Money", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(wthankreceiveamount.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(wthankreceiveamount.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;


            g.DrawString("------------------------------------------------------", boldFont, Brushes.Black, startX, startY);
            startY += offset;

            g.DrawString("KNET Details", boldFont, Brushes.Black, startX, startY);
            startY += offset;


            // Draw the strings
            g.DrawString("Date / Time", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket12.Content?.ToString()) ? "" : ket12.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket12.Content?.ToString()) ? "" : ket12.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Transaction ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ktidlbl.Content?.ToString()) ? "" : ktidlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ktidlbl.Content?.ToString()) ? "" : ktidlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Terminal ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ktiddlbl.Content?.ToString()) ? "" : ktiddlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ktiddlbl.Content?.ToString()) ? "" : ktiddlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Action Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(kactlbl.Content?.ToString()) ? "" : kactlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(kactlbl.Content?.ToString()) ? "" : kactlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Authorization No", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(kauthnumlbl.Content?.ToString()) ? "" : kauthnumlbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(kauthnumlbl.Content?.ToString()) ? "" : kauthnumlbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(kctylbl.Content?.ToString()) ? "" : kctylbl.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(kctylbl.Content?.ToString()) ? "" : kctylbl.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Merchant ID", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(kmerchid.Content?.ToString()) ? "" : kmerchid.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(kmerchid.Content?.ToString()) ? "" : kmerchid.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Type", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket1.Content?.ToString()) ? "" : ket1.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket1.Content?.ToString()) ? "" : ket1.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Retrieval Ref Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 140, startY);
            g.DrawString(string.IsNullOrEmpty(ket2.Content?.ToString()) ? "" : ket2.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket2.Content?.ToString()) ? "" : ket2.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Number", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket3.Content?.ToString()) ? "" : ket3.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket3.Content?.ToString()) ? "" : ket3.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Pan Entry Mode", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket4.Content?.ToString()) ? "" : ket4.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket4.Content?.ToString()) ? "" : ket4.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Amount", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket5.Content?.ToString()) ? "" : ket5.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket5.Content?.ToString()) ? "" : ket5.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Response Code", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 130, startY);
            g.DrawString(string.IsNullOrEmpty(ket6.Content?.ToString()) ? "" : ket6.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket6.Content?.ToString()) ? "" : ket6.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Card Product Name", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 135, startY);
            g.DrawString(string.IsNullOrEmpty(ket7.Content?.ToString()) ? "" : ket7.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket7.Content?.ToString()) ? "" : ket7.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Label", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 150, startY);
            g.DrawString(string.IsNullOrEmpty(ket9.Content?.ToString()) ? "" : ket9.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket9.Content?.ToString()) ? "" : ket9.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;

            // Draw the strings
            g.DrawString("Emv Application Identifier", regularFont, Brushes.Black, startX, startY);
            g.DrawString(":", regularFont, Brushes.Black, 160, startY);
            g.DrawString(string.IsNullOrEmpty(ket10.Content?.ToString()) ? "" : ket10.Content.ToString(), regularFont, Brushes.Black, paperWidth - g.MeasureString(string.IsNullOrEmpty(ket10.Content?.ToString()) ? "" : ket10.Content.ToString(), regularFont).Width - 10, startY);

            startY += offset;
            startY += offset;
            //// Print items
            //g.DrawString("Item 1", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$10.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset;

            //g.DrawString("Item 2", regularFont, Brushes.Black, startX, startY);
            //g.DrawString("$15.00", regularFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            //// Print total
            //g.DrawString("Total:", boldFont, Brushes.Black, startX, startY);
            //g.DrawString("$25.00", boldFont, Brushes.Black, 200, startY);
            //startY += offset * 2;

            // Print Arabic text
            //string arabicText = "شكرا لك على تسوقك معنا";
            //g.DrawString(arabicText, arabicFont, Brushes.Black, startX, startY);
            //startY += offset;

            // Print footer
            g.DrawString("Thank you for Choosing Wall Street", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Exchange Co.", regularFont, Brushes.Black, startX, startY);
            startY += offset;
            g.DrawString("Customer Care : +965-1822055", regularFont, Brushes.Black, startX, startY);
            startY += offset;
        }
        public wThankyou()
        {
            InitializeComponent();

            //PrintReceipt1();


            //printcmds();
            //Printer printer = new Printer("Receipt");
            //printer.TestPrinter();
            //printer.FullPaperCut();
            //printer.PrintDocument();
            //return;

            if (TokenManager.Langofsoft == "ar")
            {
                DATETIME.Content = "التاريخ / الوقت";
                tpslbl.Text = "تم ارسال الحوالة بنجاح";
                tidlbl.Content = "رقم المعاملة:";
                ttno.Content = "رقم TT:";
                moblbl.Content = "الهاتف";
                ochlbl.Content = "رسوم أخرى";
                senderlbl.Content = "اسم المرسل";
                recnlbl.Content = "اسم المستفيد";
                tramtlbl.Content = "مبلغ التحويل";
                trfeelbl.Content = "عمولة التحويل";
                promdislbl.Content = "الخصم الترويجى";
                totlbl.Content = "إجمالي المستحق";
                //monwillbl.Content = "سيكون المبلغ جاهز";
                recamtlbl.Content = "لمبلغ المدفوع من العميل";
                emailreceiptbtn.Content = "إيصال البريد الإلكتروني";


                knetlabel.Content = "تفاصيل KNET";
                knetdatetime.Content = "التاريخ / الوقت";
                knettid.Content = "معرف المعاملة";
                knetterminal.Content = "معرف المحطة الطرفية";
                knetaction.Content = "رمز العمل";
                knetauthnum.Content = "رقم التفويض";
                knetcardtyp.Content = "نوع البطاقة";
                knetmerchant.Content = "معرف التاجر";
                knettype.Content = "يكتب";
                knetretrefno.Content = "رقم مرجع الاسترجاع";
                knetcardno.Content = "رقم البطاقة";
                knetpanentrymode.Content = "وضع دخول المقلاة";
                knetemvamt.Content = "مبلغ EMV";
                knetresponsecode.Content = "رمز الاستجابة";
                knetproductname.Content = "اسم منتج البطاقة";
                knetemvapplicationlbl.Content = "ملصق تطبيق EMV";
                knetemvappliidenti.Content = "معرف تطبيق EMV";
            }
             
            ktidlbl.Content = POSTTOBRANCHDONE.kt1;
            ktiddlbl.Content = POSTTOBRANCHDONE.kt2;
            kactlbl.Content = POSTTOBRANCHDONE.kt3;
            kauthnumlbl.Content = POSTTOBRANCHDONE.kt4;
            kctylbl.Content = POSTTOBRANCHDONE.kt5;
            kmerchid.Content = POSTTOBRANCHDONE.kt6;

            ket1.Content = POSTTOBRANCHDONE.kn1;
            ket2.Content = POSTTOBRANCHDONE.kn2;
            ket3.Content = POSTTOBRANCHDONE.kn3;
            ket4.Content = POSTTOBRANCHDONE.kn4;
            ket5.Content = POSTTOBRANCHDONE.kn5;
            ket6.Content = POSTTOBRANCHDONE.kn6;
            ket7.Content = POSTTOBRANCHDONE.kn7;
            //ket8.Content = "FALSE";
            ket9.Content = POSTTOBRANCHDONE.kn9;
            ket10.Content = POSTTOBRANCHDONE.kn10;

            wthanksendername.Content = LoginManager.UserFullname;

            DateTime now = DateTime.Now;
            string formattedDateTime = now.ToString("dd/MM/yyyy hh:mm:ss");
            wdatetimevalue.Content = formattedDateTime;


            ket12.Content = formattedDateTime;

            if (POSTTOBRANCHDONE.kt3 != "APPROVE")
            {
                tpslbl.Text = "Transaction Declined";
                if (TokenManager.Langofsoft == "ar")
                {
                    tpslbl.Text = "تم رفض المعاملة";
                } 

            }
        }



        //public void printcmds()
        //{

        //    // USB, Bluetooth, or Serial
        //    //var printer = new SerialPrinter(portName: "COM8", baudRate: 115200);
        //    // Samba
        //    var printer = new SambaPrinter(tempFileBasePath: @"C:\Temp", filePath: "\\\\Administrator\\Receipt");


        //    // In your program, register event handler to call the method when printer status changes:
        //    printer.StatusChanged += StatusChanged;

        //    // and start monitoring for changes.
        //    //printer.StartMonitoring();
        //    //var printera = new Printer("USB001");
        //    //var printer = new Escpos

        //    var e = new EPSON();
        //    MessageBox.Show("A1");
        //    printer.Write( // or, if using and immediate printer, use await printer.WriteAsync
        //      ByteSplicer.Combine(
        //        e.CenterAlign(),
        //        //e.PrintImage(File.ReadAllBytes("images/pd-logo-300.png"), true),
        //        e.PrintLine(""),
        //        //e.SetBarcodeHeightInDots(360),
        //        //e.SetBarWidth(BarWidth.Default),
        //        //e.SetBarLabelPosition(BarLabelPrintPosition.None),
        //        //e.PrintBarcode(BarcodeType.ITF, "0123456789"),
        //        e.PrintLine(""),
        //        e.PrintLine("B&H PHOTO & VIDEO"),
        //        e.PrintLine("420 NINTH AVE."),
        //        e.PrintLine("NEW YORK, NY 10001"),
        //        e.PrintLine("(212) 502-6380 - (800)947-9975"),
        //        e.SetStyles(PrintStyle.Underline),
        //        e.PrintLine("www.bhphotovideo.com"),
        //        e.SetStyles(PrintStyle.None),
        //        e.PrintLine(""),
        //        e.LeftAlign(),
        //        e.PrintLine("Order: 123456789        Date: 02/01/19"),
        //        e.PrintLine(""),
        //        e.PrintLine(""),
        //        e.SetStyles(PrintStyle.FontB),
        //        e.PrintLine("1   TRITON LOW-NOISE IN-LINE MICROPHONE PREAMP"),
        //        e.PrintLine("    TRFETHEAD/FETHEAD                        89.95         89.95"),
        //        e.PrintLine("----------------------------------------------------------------"),
        //        e.RightAlign(),
        //        e.PrintLine("SUBTOTAL         89.95"),
        //        e.PrintLine("Total Order:         89.95"),
        //        e.PrintLine("Total Payment:         89.95"),
        //        e.PrintLine(""),
        //        e.LeftAlign(),
        //        e.SetStyles(PrintStyle.Bold | PrintStyle.FontB),
        //        e.PrintLine("SOLD TO:                        SHIP TO:"),
        //        e.SetStyles(PrintStyle.FontB),
        //        e.PrintLine("  FIRSTN LASTNAME                 FIRSTN LASTNAME"),
        //        e.PrintLine("  123 FAKE ST.                    123 FAKE ST."),
        //        e.PrintLine("  DECATUR, IL 12345               DECATUR, IL 12345"),
        //        e.PrintLine("  (123)456-7890                   (123)456-7890"),
        //        e.PrintLine("  CUST: 87654321"),
        //        e.PrintLine(""),
        //        e.PrintLine(""),
        //        e.PrintLine(""),
        //        // Arabic text example
        //        e.PrintLine("مرحبا بكم في متجرنا"), // "Welcome to our store" in Arabic
        //        e.PrintLine("الرجاء مراجعة إيصال الدفع"), // "Please review the payment receipt"
        //        e.PrintLine(""),
        //        e.FullCut()
        //      )


        //    );

        //    MessageBox.Show("A2");
        //}

        //// Define a callback method.
        //static void StatusChanged(object sender, EventArgs ps)
        //{
        //    var status = (PrinterStatusEventArgs)ps;
        //    Console.WriteLine($"Status: {status.IsPrinterOnline}");
        //    Console.WriteLine($"Has Paper? {status.IsPaperOut}");
        //    Console.WriteLine($"Paper Running Low? {status.IsPaperLow}");
        //    Console.WriteLine($"Cash Drawer Open? {status.IsCashDrawerOpen}");
        //    Console.WriteLine($"Cover Open? {status.IsCoverOpen}");
        //}






        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            SendEmailButton_Click();
            NavigationManager.NavigateToHome();

        }


        private async void SendEmailButton_Click()
        {
            try
            {
                // Get user inputs
                string fromAddress = "support@wallstreetkwt.com";
                //LoginManager.UserEMAILID
                string toAddress = LoginManager.UserEMAILID;
                string ccAddress = "burhan@microsoft.com.ge"; // Add the CC email here
                string subject = "WallStreet Exchange TransactionID : " + wthanktid.Content + " " + wdatetimevalue.Content;
                // string body = "Test Details";

                string htmlBody = $@"<html><head><meta content=""text/html; charset=UTF-8"" http-equiv=""content-type""><style type=""text/css"">ol{{margin:0;padding:0}}table td,table th{{padding:0}}.c6{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:126.8pt;border-top-color:#000000;border-bottom-style:solid}}.c8{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:42.8pt;border-top-color:#000000;border-bottom-style:solid}}.c12{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:40.5pt;border-top-color:#000000;border-bottom-style:solid}}.c2{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:136.5pt;border-top-color:#000000;border-bottom-style:solid}}.c1{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:122.2pt;border-top-color:#000000;border-bottom-style:solid}}.c10{{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:137.2pt;border-top-color:#000000;border-bottom-style:solid}}.c14{{-webkit-text-decoration-skip:none;color:#000000;font-weight:700;text-decoration:underline;vertical-align:baseline;text-decoration-skip-ink:none;font-size:11pt;font-family:""Arial"";font-style:normal}}.c7{{padding-top:0pt;padding-bottom:0pt;line-height:1.15;orphans:2;widows:2;text-align:center;height:11pt}}.c16{{padding-top:0pt;padding-bottom:0pt;line-height:1.15;orphans:2;widows:2;text-align:left;height:11pt}}.c0{{color:#000000;font-weight:400;text-decoration:none;vertical-align:baseline;font-size:11pt;font-family:""Arial"";font-style:normal}}.c19{{color:#000000;text-decoration:none;vertical-align:baseline;font-size:11pt;font-family:""Arial"";font-style:normal}}.c15{{padding-top:0pt;padding-bottom:0pt;line-height:1.15;orphans:2;widows:2;text-align:center}}.c13{{text-decoration-skip-ink:none;-webkit-text-decoration-skip:none;color:#1155cc;font-weight:700;text-decoration:underline}}.c9{{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:right}}.c3{{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:left}}.c11{{margin-left:auto;border-spacing:0;border-collapse:collapse;margin-right:auto}}.c4{{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:center}}.c17{{background-color:#ffffff;max-width:468pt;padding:72pt 72pt 72pt 72pt}}.c18{{font-weight:700}}.c5{{height:0pt}}.title{{padding-top:0pt;color:#000000;font-size:26pt;padding-bottom:3pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}.subtitle{{padding-top:0pt;color:#666666;font-size:15pt;padding-bottom:16pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}li{{color:#000000;font-size:11pt;font-family:""Arial""}}p{{margin:0;color:#000000;font-size:11pt;font-family:""Arial""}}h1{{padding-top:20pt;color:#000000;font-size:20pt;padding-bottom:6pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}h2{{padding-top:18pt;color:#000000;font-size:16pt;padding-bottom:6pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}h3{{padding-top:16pt;color:#434343;font-size:14pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}h4{{padding-top:14pt;color:#666666;font-size:12pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}h5{{padding-top:12pt;color:#666666;font-size:11pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}}h6{{padding-top:12pt;color:#666666;font-size:11pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;font-style:italic;orphans:2;widows:2;text-align:left}}</style></head><body class=""c17 doc-content""><p class=""c15""><span style=""overflow: hidden; display: inline-block; margin: 0.00px 0.00px; border: 0.00px solid #000000; transform: rotate(0.00rad) translateZ(0px); -webkit-transform: rotate(0.00rad) translateZ(0px); width: 233.50px; height: 90.60px;""><img alt=""Image"" src=""cid:inlineImageId"" style=""width: 233.50px; height: 90.60px; margin-left: 0.00px; margin-top: 0.00px; transform: rotate(0.00rad) translateZ(0px); -webkit-transform: rotate(0.00rad) translateZ(0px);"" title=""""></span></p><p class=""c16""><span class=""c0""></span></p><p class=""c15""><span class=""c19 c18"">WALL STREET EXCHANGE CO.</span></p><p class=""c7""><span class=""c0""></span></p><p class=""c15""><span class=""c14"">Transaction Processed Successfully</span></p><p class=""c7""><span class=""c0""></span></p><p class=""c7""><span class=""c0""></span></p><table class=""c11""><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Date</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0""> {wdatetimevalue.Content} </span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Transaction ID</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthanktid.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">TT Number</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wttid.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Mobile</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthankpaymentid.Content}</span></p></td></tr>
<tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Sender Name</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthanksendername.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Receiver Name</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthankreceivername.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Transfer Amount </span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthanktransferamount.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Transfer Fee</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthanktransferfee.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Other Charges</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wothercharges.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Promo Discount</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthankdiscount.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Total</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthanktotal.Content}</span></p></td></tr><tr class=""c5""><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Receive Money</span></p></td><td class=""c8"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c1"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{wthankreceiveamount.Content}</span></p></td></tr></table><p class=""c7""><span class=""c0""></span></p><p class=""c15""><span class=""c0"">-------------------------------------------------------------------------------------</span></p><p class=""c7""><span class=""c0""></span></p><p class=""c15""><span class=""c14"">KNET Details</span></p><p class=""c7""><span class=""c0""></span></p><table class=""c11""><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Date / Time</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket12.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Transaction ID</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ktidlbl.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Terminal ID</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ktiddlbl.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Action Code</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{kactlbl.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Authorization No</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{kauthnumlbl.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Card Type</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{kctylbl.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Merchant ID</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{kmerchid.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Type</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket1.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Retrieval Ref Number</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket2.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Card Number</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket3.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Pan Entry Mode</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket4.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Emv Amount</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket5.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Response Code</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket6.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Card Product Name</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket7.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Emv Application Label</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket9.Content}</span></p></td></tr><tr class=""c5""><td class=""c10"" colspan=""1"" rowspan=""1""><p class=""c3""><span class=""c0"">Emv Application Identifier</span></p></td><td class=""c12"" colspan=""1"" rowspan=""1""><p class=""c4""><span class=""c0"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c9""><span class=""c0"">{ket10.Content}</span></p></td></tr></table><p class=""c7""><span class=""c14""></span></p><p class=""c15""><span class=""c19 c18"">Thank you for Choosing Wall Street Exchange Co. </span></p><p class=""c15""><span class=""c18"">Customer Care : </span><a href=""tel:009651822055""><span >+965-1822055</span></a></p></body></html>";

                // Configure email message
                using (var message = new MailMessage(fromAddress, toAddress, subject, ""))
                {
                    //message.CC.Add(ccAddress);
                    message.IsBodyHtml = true; // Set the body content as HTML
                    message.Body = htmlBody;

                    // Replace "path/to/your/image.png" with the actual path to your image file
                    byte[] imageBytes = File.ReadAllBytes("image.png");
                    //Attachment attachment = new Attachment(new MemoryStream(imageBytes), "image.png");
                    //message.Attachments.Add(attachment);


                    //byte[] imageBytes = File.ReadAllBytes("path/to/your/image.png");
                    MemoryStream imageStream = new MemoryStream(imageBytes);
                    Attachment inlineImage = new Attachment(imageStream, "image.png");

                    // Set the content type and content ID for embedding
                    inlineImage.ContentId = "inlineImageId";
                    inlineImage.ContentDisposition.Inline = true;
                    inlineImage.ContentDisposition.DispositionType = DispositionTypeNames.Inline;
                    inlineImage.ContentType.MediaType = "image/png";

                    // Add the attachment to the message
                    message.Attachments.Add(inlineImage);
                    // Set SMTP credentials and security options
                    using (var smtpClient = new SmtpClient("smtp.office365.com", 587))
                    {
                        smtpClient.EnableSsl = true;
                        smtpClient.Credentials = new NetworkCredential(fromAddress, "Pax38329");

                        // Add security certificate validation (optional)
                        ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

                        {
                            // You can customize certificate validation logic here
                            return true; // Accept all certificates for now
                        };

                        // Send the email
                        //smtpClient.Send(message);
                        await smtpClient.SendMailAsync(message);

                        // Display success message
                        //MessageBox.Show("Email Receipt sent successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }

        




        // Helper method to reverse Arabic text (if needed)
        private string ReverseArabic(string text)
        {
            char[] array = text.ToCharArray();
            Array.Reverse(array);
            return new string(array);
        }

        private async void Page_load(object sender, RoutedEventArgs e)
        {
            try
            {
                int memberCode = 1;
                var client = new HttpClient();

                // Create the GET request to the new API endpoint
                var request = new HttpRequestMessage(
                    HttpMethod.Get,
                    "http://" + Variable.apiipadd + "/api/Transaction/get-transaction-list?AppMemberCode=" + memberCode);

                request.Headers.Add("accept", "text/plain");
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                using var responseStream = await response.Content.ReadAsStreamAsync();
                using var jsonDoc = await JsonDocument.ParseAsync(responseStream);

                var root = jsonDoc.RootElement;

                // Access data.transactions array
                if (root.TryGetProperty("data", out JsonElement data) &&
                    data.TryGetProperty("transactions", out JsonElement transactions))
                {
                    // Just showing the first transaction (modify as needed)
                    if (transactions.GetArrayLength() > 0)
                    {
                        var txn = transactions[0];

                        wthankreceivername.Content = txn.GetProperty("beneficiary_name").GetString();
                        wthanktid.Content = txn.GetProperty("transaction_reference").ToString();
                        wttid.Content = txn.GetProperty("transaction_date").GetString();
                        wthankpaymentid.Content = txn.GetProperty("payment_mode").GetString();

                        wthanktransferamount.Content = txn.GetProperty("source_amount").GetDecimal().ToString("0.000") + " KWD";
                        wthanktransferfee.Content = txn.GetProperty("commission").GetDecimal().ToString("0.000") + " KWD";
                        wothercharges.Content = txn.GetProperty("tax_collected").GetDecimal().ToString("0.000") + " KWD";
                        wthankdiscount.Content = "0.000 KWD";
                        wthanktotal.Content = txn.GetProperty("pay_amount").GetDecimal().ToString("0.000") + " KWD";

                        wthankreceiveamount.Content =
                            txn.GetProperty("destination_amount").GetDecimal().ToString("0.000") + " " +
                            txn.GetProperty("destination_currency_code").GetString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading transaction data: " + ex.Message);
            }
        }

        private async void Page_load_old(object sender, RoutedEventArgs e)
        {

            if (POSTTOBRANCHDONE.kt3 != "APPROVE")
            {
               // return;
            }
             var client = new HttpClient();
           // MessageBox.Show(""+POSTTOBRANCHDONE.seltxnno);

            //int asjdsad = POSTTOBRANCHDONE.seltxnno
            // int asjdsad = int.TryParse(POSTTOBRANCHDONE.seltxnno, out int temp) ? temp : 0;
            string asjdsad = POSTTOBRANCHDONE.seltxnno;
            //asjdsad = "24220130000028";
            var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxRemittance/Transaction/"+ asjdsad);
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            //Console.WriteLine(await response.Content.ReadAsStringAsync());
           // MessageBox.Show(await response.Content.ReadAsStringAsync());

            var responseBody = await response.Content.ReadAsStringAsync();

            // Parse the JSON response using System.Text.Json
            //using (JsonDocument doc = JsonDocument.Parse(responseBody))
            //{
            //    // Access the root JSON object
            //    JsonElement root = doc.RootElement;

            //    // Navigate to the 'Data' object
            //    //JsonElement dataElement = root.GetProperty("Message");

            //    // Extract the accessToken
            //    string Message = root.GetProperty("Message").GetString();


            //    // Navigate to the 'Data' object
            //    JsonElement dataElement = root.GetProperty("Data");

            //    // Extract the accessToken
            //    //RemitterID
            //    //string remid = dataElement.GetProperty("UserId").ToString();
            //    //TranID = dataElement.GetProperty("TransactionID").ToString();
            //    MessageBox.Show(dataElement.GetProperty("Beneficiary").ToString());
            //    //MessageBox.Show(TranID);
            //    //LoginManager.SetRemiduser(remid);

            //    //// Display the accessToken in a message box
            //    ////Console.WriteLine($"Access Token: {accessToken}");
            //    ////MessageBox.Show($"Message: {Message}");

            //    //MessageBox.Show(Message + " RemID : " + remid);

            //    //if (Message == "Login Successfully")
            //    //{
            //    //    wMainPage wmainpage = new wMainPage();
            //    //    NavigationService.Navigate(wmainpage);
                //}


                // RemoveToken(accessToken);
                // SaveToken(accessToken);
                //TokenManager.SetToken(accessToken);
                // MessageBox.Show(LoadToken());

            //}


            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {

                // MessageBox.Show("Hi 1");
                // Parse JSON response using JsonDocument.Parse
                var jsonDocument = await JsonDocument.ParseAsync(responseStream);

                // MessageBox.Show("Hi 2");

                // Access root object (assuming it's an array) and iterate over its elements
                foreach (var dataElement in jsonDocument.RootElement.GetProperty("Data").EnumerateArray())
                {

                    // MessageBox.Show("Hi 3");

                     // MessageBox.Show(dataElement.GetProperty("Beneficiary").GetString());

                    wthankreceivername.Content = dataElement.GetProperty("Beneficiary").GetString();
                    wthanktid.Content = dataElement.GetProperty("TxnReferenceNo").GetRawText();
                    wttid.Content = dataElement.GetProperty("TTNumber").GetRawText();
                    wthankpaymentid.Content = dataElement.GetProperty("Mobile").GetString();
                    wthanktransferamount.Content = dataElement.GetProperty("LCAmount").GetRawText() + " KWD";
                    wthanktransferfee.Content = dataElement.GetProperty("Commission").GetRawText() + " KWD";
                    wothercharges.Content = dataElement.GetProperty("OtherCharge").GetRawText() + " KWD";
                    wthankdiscount.Content = "0.000 KWD";
                    wthanktotal.Content = dataElement.GetProperty("NetAmount").GetRawText() + " KWD";
                   // wthanktid.Content = dataElement.GetProperty("Beneficiary").GetString();
                    wthankreceiveamount.Content = dataElement.GetProperty("FCAmount").GetRawText() + " " + dataElement.GetProperty("FCCurrency").GetString();


                    //                    < Label Name = "" Content = "123456789" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label Name = ""  Content = "0786" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "30.000 KWD" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "2.500 KWD" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "0.000 KWD" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "32.500 KWD" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "In minutes" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >
                    //< Label  Name = ""  Content = "8069 INR" FontSize = "20" FontWeight = "Bold" Foreground = "#FFFF" ></ Label >

                    //Countries.Add(new Country
                    //{


                    //    CountryName = dataElement.GetProperty("ConCode").GetString(),
                    //    Amt = "",
                    //    Bene = dataElement.GetProperty("ConName").GetString(),
                    //    Date = "", // You need to specify how to get the date from the JSON response
                    //    TID = "", // You need to specify how to get the TID from the JSON response
                    //    BANK = "",
                    //    stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")), // You need to adjust this based on your logic
                    //    FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png"))
                    //    //FlagImage = GetFlagImage(dataElement.GetProperty("BENE_COUNTRY").GetString()) // Assuming you have a method to get flag image based on country code
                    //});

                    //new Country {
                    //FlagImage = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/INR.png")),
                    //CountryName = "Bank of Baroda",
                    //Amt = "2,00,000 INR",
                    //Bene = "India",
                    //Date = "01/01/2023", TID = "123456",
                    //BANK = "Bank of Baroda",
                    //stsimg = new BitmapImage(new Uri("pack://application:,,,/Exchange;component/Images/check.png")) },
                }
            }


        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            //SendEmailButton_Click();
            try { 
                PrintReceipt1();
            } catch (Exception ex)
            {
                MessageBox.Show("Error While Printing " + ex);
            }
            NavigationManager.NavigateToHome();
        }
    }
}
