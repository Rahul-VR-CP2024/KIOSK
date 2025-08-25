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

        public void PrintReceipt1()
        {

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

          
            string headerlabel = tpslbl.Text;
            //g.DrawString("Transaction Processed Successfully", regularFont, Brushes.Black, startX, startY);
            g.DrawString(headerlabel, regularFont, Brushes.Black, startX, startY);
            startY += offset;

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
