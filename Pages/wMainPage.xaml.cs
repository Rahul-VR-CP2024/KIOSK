using Exchange.Managers;
using PACI.MobileId.IntegrationServices.Client;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wMainPage.xaml
    /// </summary>
    public partial class wMainPage : Page
    {
        public wMainPage()
        {
            InitializeComponent();

            //testqrpaci();
            //SendEmailButton_Click();
            //test2();

            LanguageInit();

            if (!LoginManager.IsLoggedIn())
            {
                logoutbtn.Visibility = Visibility.Hidden;
            }
        }


        //foreach (var certa in x509Store.Certificates)
        //{
        //    //Console.WriteLine(certa.Thumbprint);

        //    //MessageBox.Show(certa.Thumbprint);
        //}



        private void SendEmailButton_Click()
        {
            try
            {
                // Get user inputs
                string fromAddress = "support@wallstreetkwt.com";
                string toAddress = "burhan@microsoft.com.ge";
                string subject = "Test";
               // string body = "Test Details";

                string htmlBody = @"<html><head><meta content=""text/html; charset=UTF-8"" http-equiv=""content-type""><style type=""text/css"">ol{margin:0;padding:0}table td,table th{padding:0}.c14{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:136.5pt;border-top-color:#000000;border-bottom-style:solid}.c5{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:137.2pt;border-top-color:#000000;border-bottom-style:solid}.c6{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:126.8pt;border-top-color:#000000;border-bottom-style:solid}.c16{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:42.8pt;border-top-color:#000000;border-bottom-style:solid}.c4{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:122.2pt;border-top-color:#000000;border-bottom-style:solid}.c2{border-right-style:solid;padding:5pt 5pt 5pt 5pt;border-bottom-color:#000000;border-top-width:0pt;border-right-width:0pt;border-left-color:#000000;vertical-align:top;border-right-color:#000000;border-left-width:0pt;border-top-style:solid;border-left-style:solid;border-bottom-width:0pt;width:40.5pt;border-top-color:#000000;border-bottom-style:solid}.c12{-webkit-text-decoration-skip:none;color:#000000;font-weight:700;text-decoration:underline;vertical-align:baseline;text-decoration-skip-ink:none;font-size:11pt;font-family:""Arial"";font-style:normal}.c1{color:#000000;font-weight:400;text-decoration:none;vertical-align:baseline;font-size:11pt;font-family:""Arial"";font-style:normal}.c15{color:#000000;font-weight:700;text-decoration:none;vertical-align:baseline;font-size:11pt;font-family:""Arial"";font-style:normal}.c9{padding-top:0pt;padding-bottom:0pt;line-height:1.15;orphans:2;widows:2;text-align:center}.c13{padding-top:0pt;padding-bottom:0pt;line-height:1.15;orphans:2;widows:2;text-align:left}.c3{margin-left:auto;border-spacing:0;border-collapse:collapse;margin-right:auto}.c8{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:right}.c0{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:center}.c18{text-decoration-skip-ink:none;-webkit-text-decoration-skip:none;color:#1155cc;text-decoration:underline}.c10{padding-top:0pt;padding-bottom:0pt;line-height:1.0;text-align:left}.c19{background-color:#ffffff;max-width:468pt;padding:72pt 72pt 72pt 72pt}.c17{font-weight:700}.c7{height:0pt}.c11{height:11pt}.title{padding-top:0pt;color:#000000;font-size:26pt;padding-bottom:3pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}.subtitle{padding-top:0pt;color:#666666;font-size:15pt;padding-bottom:16pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}li{color:#000000;font-size:11pt;font-family:""Arial""}p{margin:0;color:#000000;font-size:11pt;font-family:""Arial""}h1{padding-top:20pt;color:#000000;font-size:20pt;padding-bottom:6pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}h2{padding-top:18pt;color:#000000;font-size:16pt;padding-bottom:6pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}h3{padding-top:16pt;color:#434343;font-size:14pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}h4{padding-top:14pt;color:#666666;font-size:12pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}h5{padding-top:12pt;color:#666666;font-size:11pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;orphans:2;widows:2;text-align:left}h6{padding-top:12pt;color:#666666;font-size:11pt;padding-bottom:4pt;font-family:""Arial"";line-height:1.15;page-break-after:avoid;font-style:italic;orphans:2;widows:2;text-align:left}</style></head><body class=""c19 doc-content""><p class=""c9""><span style=""overflow: hidden; display: inline-block; margin: 0.00px 0.00px; border: 0.00px solid #000000; transform: rotate(0.00rad) translateZ(0px); -webkit-transform: rotate(0.00rad) translateZ(0px); width: 233.50px; height: 90.60px;""><img alt=""Image""  src=""cid:inlineImageId"" style=""width: 233.50px; height: 90.60px; margin-left: 0.00px; margin-top: 0.00px; transform: rotate(0.00rad) translateZ(0px); -webkit-transform: rotate(0.00rad) translateZ(0px);"" title=""""></span></p><p class=""c11 c13""><span class=""c1""></span></p><p class=""c9""><span class=""c15"">WALL STREET EXCHANGE CO.</span></p><p class=""c9 c11""><span class=""c1""></span></p><p class=""c9""><span class=""c12"">Transaction Processed Successfully</span></p><p class=""c9 c11""><span class=""c1""></span></p><p class=""c9 c11""><span class=""c1""></span></p><table class=""c3""><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Date</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Transaction ID</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Mobile</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Receiver Name</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Transfer Amount </span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Transfer Fee</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Promo Discount</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Total</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr><tr class=""c7""><td class=""c14"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Receive Money</span></p></td><td class=""c16"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c4"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">TPARA</span></p></td></tr></table><p class=""c9 c11""><span class=""c1""></span></p><p class=""c9""><span class=""c1"">&mdash;-------------------------------------------------------------------------------------</span></p><p class=""c9 c11""><span class=""c1""></span></p><p class=""c9""><span class=""c12"">KNET Details</span></p><p class=""c9 c11""><span class=""c1""></span></p><table class=""c3""><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Date / Time</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Transaction ID</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Terminal ID</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Action Code</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Authorization No</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Card Type</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Merchant ID</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Type</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Retrieval Ref Number</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Card Number</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Pan Entry Mode</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Emv Amount</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Response Code</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Card Product Name</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Emv Application Label</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr><tr class=""c7""><td class=""c5"" colspan=""1"" rowspan=""1""><p class=""c10""><span class=""c1"">Emv Application Identifier</span></p></td><td class=""c2"" colspan=""1"" rowspan=""1""><p class=""c0""><span class=""c1"">:</span></p></td><td class=""c6"" colspan=""1"" rowspan=""1""><p class=""c8""><span class=""c1"">KPARA</span></p></td></tr></table><p class=""c9 c11""><span class=""c12""></span></p><p class=""c9""><span class=""c15"">Thank you for Choosing Wall Street Exchange Co. </span></p><p class=""c9""><span class=""c17"">Customer Care : </span><a href=""tel:009651822055""><span class=""c17 c18"">+965-1822055</span></a></p></body></html>";

                // Configure email message
                using (var message = new MailMessage(fromAddress, toAddress, subject, ""))
                {

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
                        smtpClient.Send(message);

                        // Display success message
                        MessageBox.Show("Email sent successfully.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and display an error message
                MessageBox.Show("Error sending email: " + ex.Message);
            }
        }

       // public IdentityDetails testqrpaci()
        public void testqrpaci()
        {
            MessageBox.Show("Signal 1");
            X509Certificate2 cert = new X509Certificate2();
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);

            var certificate2Collection = x509Store.Certificates.Find(X509FindType.FindByThumbprint, "3315fe376fec98cf8a67db8829afe13a4a9013fd", false);

            if (certificate2Collection == null || certificate2Collection.Count == 0 ||
            certificate2Collection.Count > 1)
            {
                throw new Exception("Certificate not found");
            }
            cert = certificate2Collection[0];

            //foreach (var certa in x509Store.Certificates)
            //{
            //    //Console.WriteLine(certa.Thumbprint);

            //    //MessageBox.Show(certa.Thumbprint);
            //}

            MessageBox.Show("Signal 2");
            try { 
            var authClient = new MIDAuthServiceClient(cert, "mid-auth-p.paci.gov.kw","5869");
            } catch(Exception ex)
            {
                MessageBox.Show("" + ex);
            }
            MessageBox.Show("Signal 3");
            //var res = authClient.VerifyMobileIdQRData("aa513452-fa95-47cc-9977-d5b60e0fa420", QRBase64);
            //var res = authClient.VerifyMobileIdQRData("aa513452-fa95-47cc-9977-d5b60e0fa420", "");

            MessageBox.Show("Signal 4");
            //return new MIDAuthSignContract.Entities.IdentityDetails
            //{

            //    PersonIdentityData = res.Data.PersonIdentityData,
            //    DataSigningCertificate = res.Data.DataSigningCertificate,
            //    DataSignature = res.Data.DataSignature
            //};
        }

        public void test2()
        {
            X509Certificate2 cert = new X509Certificate2();
            X509Store x509Store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            x509Store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            var certificate2Collection = x509Store.Certificates
                .Find(X509FindType.FindByThumbprint, "07c8705c099003552a9012a6b44627a931727756", false);
            if (certificate2Collection == null || certificate2Collection.Count == 0 ||
            certificate2Collection.Count > 1)
            {
                throw new Exception("Certificate not found");
            }
            cert = certificate2Collection[0];

            var authClient = new MIDAuthServiceClient(cert, "mid-auth-p.paci.gov.kw",
            "5869");

            var res = authClient.NotifyPerson(
            new MIDAuthSignContract.Entities.NotificationRequest
            {
                ServiceProviderId = "aa513452-fa95-47cc-9977-d5b60e0fa420",
                PersonCivilNo = "292120200000",
                NotificationSubjectEn = "Subject English",
                NotificationSubjectAr = "Subject Arabic",
                NotificationMessageEn = "Message English",
                NotificationMessageAr = "message Arabic"
            });

            //MessageBox.Show(res.ToString());
        }


        //Sendmoney_Button_Click

        private void Sendmoney_Button_Click(object sender, RoutedEventArgs e)
        {


            //wThankyou wxa = new wThankyou();
            //NavigationService.Navigate(wxa);
            //return;

            BCManager.SetTokenfromwhereopenedwtbc("sendmoney");
            
            wtobankorcash wx = new wtobankorcash();
            if (LoginManager.IsLoggedIn())
            {
                NavigationService.Navigate(wx);
                return;
            }
            Login2 login2 = new Login2(wx);
            NavigationService.Navigate(login2);



            //wSelectbeneficary wx = new wSelectbeneficary();
            //NavigationService.Navigate(wx);
        }
        //
        private void Bene_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);

            //BCManager.SetTokenfromwhereopenedwtbc("sendmoney");
            BCManager.SetTokenfromwhereopenedwtbc("beneficiary");

            wtobankorcash wx = new wtobankorcash();
            NavigationService.Navigate(wx);


            //wBeneficiary welocmepage = new wBeneficiary();
            //NavigationService.Navigate(welocmepage);

        }
        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {

            LoginManager.Logout();
            logoutbtn.Visibility = Visibility.Hidden;
            RefreshWelcomeText();
        }
        private void LangButton_Click(object sender, RoutedEventArgs e)
        {
            if (TokenManager.Langofsoft == "ar")
            {
                TokenManager.SetLang("en");
            }
            else
            {
                TokenManager.SetLang("ar");
            }
            
            LanguageInit();
        }

        private void FAQButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            wFaq welocmepage = new wFaq();
            NavigationService.Navigate(welocmepage);

        }

        //

        private void THISTORYButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            wHistorytransaction welocmepage = new wHistorytransaction();
            NavigationService.Navigate(welocmepage);

        }

        private void ContactButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            wContact welocmepage = new wContact();
            NavigationService.Navigate(welocmepage);

        }

        private void Exchange_Button_Click(object sender, RoutedEventArgs e)
        {


            BCManager.SetTokenfromwhereopenedwtbc("exchangerate");
            //wtobankorcash wx = new wtobankorcash();
            //NavigationService.Navigate(wx);


            wExchangecalculator wx = new wExchangecalculator();
            NavigationService.Navigate(wx);
        }

        private void userButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateProfile wx = new UpdateProfile();
            if (LoginManager.IsLoggedIn())
            {
                NavigationService.Navigate(wx);
                return;
            }
            Login2 login2 = new Login2(wx);
            NavigationService.Navigate(login2);
        }

        private void LanguageInit()
        {
            if (TokenManager.Langofsoft == "ar")
            {
                sendmoneybtn.Text = "إرسال الأموال";
                exchangeratebtn.Text = "سعر الصرف";
                //tranhistbtn.Text = "سجل المعاملات";
                //benebtn.Text = "المستفيد";
                //faqbtn.Text = "د- التعليمات";
                contbtn.Text = "اتصل بنا";
                updtprofibtn.Text = "تحديث الملف الشخصي";
                logoutbtn.Content = "خروج";
                langbtn.Content = "English";
            }
            else
            {
                sendmoneybtn.Text = "SEND MONEY";
                exchangeratebtn.Text = "EXCHANGE RATE";
                //tranhistbtn.Text = "سجل المعاملات";
                //benebtn.Text = "المستفيد";
                //faqbtn.Text = "د- التعليمات";
                contbtn.Text = "CONTACT US";
                updtprofibtn.Text = "UPDATE PROFILE";
                logoutbtn.Content = "Logout";
                langbtn.Content = "عربي";
            }
            RefreshWelcomeText();
        }

        private void RefreshWelcomeText()
        {
            if (TokenManager.Langofsoft == "ar")
            {
                welcomtext.Text = "! " + LoginManager.UserFullname + " مرحباً";
                langbtn.Content = "English";
            }
            else
            {
                welcomtext.Text = "Welcome " + LoginManager.UserFullname + " !";
            }
        }
    }
}
