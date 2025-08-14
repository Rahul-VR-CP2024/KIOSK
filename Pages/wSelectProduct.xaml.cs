using Exchange.Managers;
using System.Net.Http;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using static Exchange.Pages.wSelectcountry;
using static Exchange.Pages.wtobankorcash;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wSelectProduct.xaml
    /// </summary>
    public partial class wSelectProduct : Page
    {
        public wSelectProduct()
        {
            InitializeComponent();

            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                selectproducttitle.Text = "اختار المنتج";
                productlabel.Content = "اختار المنتج";
                deliverytypelabel.Content = "وضع التوصيل";
                nextbtn.Content = "التالي";
            }
            productcombo.Items.Clear();
            deliverycombo.Items.Clear();
        }

        private void backbutton(object sender, RoutedEventArgs e)
        {
            wSelectcountry selprod = new wSelectcountry();
            NavigationService.Navigate(selprod);
        }



        //

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Test" + deliverycombo.Content());
            //MessageBox.Show("Test");

            // Get the selected product
            Product selectedProduct = (Product)productcombo.SelectedItem;
            Deliverytype selecteddisp = (Deliverytype)deliverycombo.SelectedItem;

            //MessageBox.Show(selectedProduct + "");
            //MessageBox.Show(selectedProduct.DisbursalCode);
            if (selectedProduct == null)
            {
                MessageBox.Show("API NOT MAPPED");
                return;
            }

            if (selectedProduct.ProductName == "" || selectedProduct.ProductName == null)
            {
                MessageBox.Show("Product Name is Blank from API");
                //return;
            }

            if (selecteddisp.DisbCode == "" || selecteddisp.DisbCode == null)
            //    if (selectedProduct.DisbursalCode == "" || selectedProduct.DisbursalCode == null)
            {
                MessageBox.Show("Delivery Type Name is Blank from API");
                //return;
            }

            // Access the product code
            string productCode = selectedProduct.ProductCode;
            string dispCode = selecteddisp.DisbCode;

            string currCode = selectedProduct.ProductCurrencyCode;

            // Perform actions with the product code
            //MessageBox.Show($"Selected product code: {productCode}");

            ProductManager.Setselectedproductcode(productCode);
            //ProductManager.Setselecteddisbcode(dispCode);
            ProductManager.Setselecteddisbcode(dispCode);
            ProductManager.SetselectedProdCurrCode(currCode);


            string selectedText = deliverycombo.Text;

            //MessageBox.Show(selectedText);

            //MessageBox.Show(ProductManager.selectedproductcode + " " + ProductManager.selecteddispcode + " " + ProductManager.selectedProdCurrCode);




           


            if (BCManager.fromwhereopenedwtbc == "exchangerate")
            {
                //MessageBox.Show(ProductManager.selectedproductcode + ProductManager.selectedProdCurrCode);
                wExchangecalculator wx = new wExchangecalculator();
                NavigationService.Navigate(wx);
            } else
            {
                waddbeneficiary selprod = new waddbeneficiary("add");
                NavigationService.Navigate(selprod);
            }




        }

        private async void ONLOAD(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            var CPORBT = BCManager.selectedoptionborc;
            //MessageBox.Show(CPORBT);
            //SelectedAddBeneCountry
            //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode="+CPORBT+"&CountryCode=IN");
            var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxgeneral/DefaultProduct?DisbursalMode=" + CPORBT + "&CountryCode="+ SelectedAddBeneCountry.seladdbenecount);
            //request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiJXQUxMU1RLSU9TS1VBVCIsImp0aSI6ImU0NDFjZDIyLTNiYzgtNGI2ZS05Yjg1LTdhM2M0ZmYxMzI0YiIsImlhdCI6IjA4LzAzLzIwMjQgMTM6MDY6UE0iLCJLaW9za0lEIjoiMTU0MzU0MyIsIm5iZiI6MTcyMjY3OTU2NywiZXhwIjoxNzIyNjgxMzY3LCJpc3MiOiJodHRwOi8vd3d3LmNpbnF1ZS5hZSIsImF1ZCI6IkNpbnF1ZSBDdXN0b21lcnMifQ.fHwf2yO7ZyQ7bFmDAGlqkvOc7GpJRumDrLXXCk-in4U");
            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
            var content = new StringContent("", null, "text/plain");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            //MessageBox.Show(await response.Content.ReadAsStringAsync());


            var contentString = await request.Content.ReadAsStringAsync();
            string responseString = await response.Content.ReadAsStringAsync();
            RichMessageBox.Show("Request Data to api/v1/sxgeneral/DefaultProduct?DisbursalMode=\"" + CPORBT + "\"&CountryCode=\""+ SelectedAddBeneCountry.seladdbenecount +"\"\n" + DateTime.Now + "\n" + contentString);
            RichMessageBox.Show("Response from api/v1/sxgeneral/DefaultProduct?DisbursalMode=\"" + CPORBT + "\"&CountryCode=\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n" + DateTime.Now + "\n" + responseString);

            runtheloadersource();
            //runtheloaderdelivery();

        }

        


        private async void runtheloaderdelivery()
        {

            var CPORBT = BCManager.selectedoptionborc;


            if((Product)productcombo.SelectedItem != null) { 

                        // Get the selected product
                        Product selectedProduct = (Product)productcombo.SelectedItem;
                        var PRODCIDE = selectedProduct.ProductCode;
                        //MessageBox.Show(CPORBT);
                        try
                        {
                            var client = new HttpClient();

                            // Assuming you have the authorization token
                            // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";
                            //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode=CP&CountryCode=IN");
                            //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct/Disbmodes?ProductCode=TF&MobDisbcode="+CPORBT);
                            var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxgeneral/DefaultProduct/Disbmodes?ProductCode="+ PRODCIDE + "&MobDisbcode=" + CPORBT);
                            request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                            // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                            //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                            var content = new StringContent("", null, "text/plain");
                            request.Content = content;

                            var response = await client.SendAsync(request);
                            response.EnsureSuccessStatusCode();

                            //var contentString = await request.Content.ReadAsStringAsync();
                            //string responseString = await response.Content.ReadAsStringAsync();
                            //RichMessageBox.Show("Request Data to api/v1/sxgeneral/DefaultProduct?DisbursalMode=\"" + CPORBT + "\"&CountryCode=\"" + SelectedAddBeneCountry.seladdbenecount + "\"\n" + DateTime.Now + "\n" + contentString);
                            //RichMessageBox.Show("Response from api/v1/sxgeneral/DefaultProduct/Disbmodes?ProductCode=\"" + PRODCIDE + "&MobDisbcode=" + CPORBT + "\"\n" + DateTime.Now + "\n" + responseString);


                            // Parse the JSON response with JsonDocument
                            using (var responseStream = await response.Content.ReadAsStreamAsync())
                            {
                                using (var doc = JsonDocument.Parse(responseStream))
                                {
                                    //UpdateComboBoxdelivery(doc.RootElement);
                                    UpdateComboBoxdeliveryv2(doc.RootElement);
                                }
                            }
                        }
                        catch (HttpRequestException ex)
                        {
                            Console.WriteLine($"Error sending request: {ex.Message}");
                        }

                        // Select the first item (if any)
                        if (deliverycombo.Items.Count > 0)
                        {
                            deliverycombo.SelectedIndex = 0;

                        }
            }
        }



        private async void runtheloadersource()
        {

            var CPORBT = BCManager.selectedoptionborc;
            //MessageBox.Show(CPORBT);
            try
            {
                var client = new HttpClient();

                // Assuming you have the authorization token
                // string authorizationToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...";

                //var request = new HttpRequestMessage(HttpMethod.Post, "http://192.168.1.67:55525/api/v1/sxmaster/SOI");
                //SelectedAddBeneCountry.seladdbenecount
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://192.168.1.67:55525/api/v1/sxgeneral/DefaultProduct?DisbursalMode="+ CPORBT + "&CountryCode=IN");
                var request = new HttpRequestMessage(HttpMethod.Get, "http://"+Variable.apiipadd+"/api/v1/sxgeneral/DefaultProduct?DisbursalMode=" + CPORBT + "&CountryCode="+ SelectedAddBeneCountry.seladdbenecount);
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                // var content = new StringContent("{\n\"purpID\":0,\n\"purpCode\":\"\",\n\"bankID\":1160\n}", Encoding.UTF8, "application/json");
                //var content = new StringContent("{\n  \"SrcIncomeId\": 0,\n  \"Sou_Code\": \"\",\n  \"CustType\": \"I\",\n  \"BankID\": 1160\n}", null, "application/json");
                var content = new StringContent("", null, "text/plain");
                request.Content = content;

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                // Parse the JSON response with JsonDocument
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    using (var doc = JsonDocument.Parse(responseStream))
                    {
                        UpdateComboBoxsource(doc.RootElement);
                    }
                }

                runtheloaderdelivery();
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error sending request: {ex.Message}");
            }

            // Select the first item (if any)
            if (productcombo.Items.Count > 0)
            {
              //  productcombo.SelectedIndex = 0;

            }
        }


        private void UpdateComboBoxsourcex(JsonElement root)
        {
            // Clear existing items (optional)
            productcombo.Items.Clear();


            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("Productname", out JsonElement purpNameElement))
                    {
                        productcombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }

        private void UpdateComboBoxsource(JsonElement root)
        {
            productcombo.Items.Clear();

            List<Product> products = new List<Product>();


            //if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            //{
            //    foreach (var item in dataElement.EnumerateArray())
            //    {
            //        if (item.TryGetProperty("Productname", out JsonElement productNameElement) &&
            //            item.TryGetProperty("ProductCode", out JsonElement productCodeElement))
            //        {
            //            var product = new Product
            //            {
            //                ProductName = productNameElement.GetString(),
            //                ProductCode = productCodeElement.GetString()
            //            };
            //            productcombo.Items.Add(product);
            //        }
            //    }
            //}
            //else
            //{
            //    // Handle potential errors (optional)
            //    Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            //}

            // Assuming "Data" is an array and contains "Productname" and "Productcode" properties
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("Productname", out JsonElement productNameElement) &&
                        item.TryGetProperty("ProductCode", out JsonElement productCodeElement) && 
                        item.TryGetProperty("DisbursalModeCode", out JsonElement DisbursalModeCodeElement) &&
                        item.TryGetProperty("CurrencyCode", out JsonElement CurrencyCodeElement))

                    //DisbursalModeCode
                    {

                        //   MessageBox.Show(productCodeElement.GetString());
                        //   MessageBox.Show(productNameElement.GetString());

                        products.Add(new Product
                        {
                            ProductName = productNameElement.GetString(),
                            ProductCode = productCodeElement.GetString(),
                            DisbursalCode = DisbursalModeCodeElement.GetString(),
                            ProductCurrencyCode = CurrencyCodeElement.GetString()
                        });
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }

            // Bind the ComboBox to the products list
            productcombo.ItemsSource = products;

            // Assuming products is a collection and has at least one item
            if (products.Count > 0)
            {
                productcombo.SelectedItem = products[0];
            }
            productcombo.DisplayMemberPath = "ProductName";
        }


        private void UpdateComboBoxdeliveryv2(JsonElement root)
        {
            deliverycombo.ItemsSource = null;
            deliverycombo.Items.Clear();

            List<Deliverytype> products = new List<Deliverytype>();


            //if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            //{
            //    foreach (var item in dataElement.EnumerateArray())
            //    {
            //        if (item.TryGetProperty("Productname", out JsonElement productNameElement) &&
            //            item.TryGetProperty("ProductCode", out JsonElement productCodeElement))
            //        {
            //            var product = new Product
            //            {
            //                ProductName = productNameElement.GetString(),
            //                ProductCode = productCodeElement.GetString()
            //            };
            //            productcombo.Items.Add(product);
            //        }
            //    }
            //}
            //else
            //{
            //    // Handle potential errors (optional)
            //    Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            //}

            // Assuming "Data" is an array and contains "Productname" and "Productcode" properties
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("DisbName", out JsonElement productNameElement) &&
                        item.TryGetProperty("DisbCode", out JsonElement productCodeElement))

                    //DisbursalModeCode
                    {

                        //   MessageBox.Show(productCodeElement.GetString());
                        //   MessageBox.Show(productNameElement.GetString());

                        products.Add(new Deliverytype
                        {
                            DisbName = productNameElement.GetString(),
                            DisbCode = productCodeElement.GetString()
                        });
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }

            // Bind the ComboBox to the products list
            deliverycombo.ItemsSource = products;

            // Assuming products is a collection and has at least one item
            if (products.Count > 0)
            {
                deliverycombo.SelectedItem = products[0];
            }
            deliverycombo.DisplayMemberPath = "DisbName";
        }


        public class Product
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string DisbursalCode { get; set; }
            public string ProductCurrencyCode { get; set; }
        }


        public class Deliverytype
        {
            public string DisbName { get; set; }
            public string DisbCode { get; set; }
        }




        public static class ProductManager
        {
            public static string selectedproductcode { get; set; }
            public static string selecteddispcode { get; set; }

            public static string selectedProdCurrCode { get; set; }


            public static void Setselectedproductcode(string token)
            {
                selectedproductcode = token;
            }

            public static void Setselecteddisbcode(string token)
            {
                selecteddispcode = token;
            }


            public static void SetselectedProdCurrCode(string token)
            {
                selectedProdCurrCode = token;
            }

        }



        private void UpdateComboBoxdelivery(JsonElement root)
        {
            // Clear existing items (optional)
            deliverycombo.Items.Clear();


            // Assuming "Data" is an array and contains "PURPNAME" property
            if (root.TryGetProperty("Data", out JsonElement dataElement) && dataElement.ValueKind == JsonValueKind.Array)
            {
                foreach (var item in dataElement.EnumerateArray())
                {
                    if (item.TryGetProperty("DisbName", out JsonElement purpNameElement))
                    {
                        deliverycombo.Items.Add(purpNameElement.GetString());
                    }
                }
            }
            else
            {
                // Handle potential errors (optional)
                Console.WriteLine("Invalid JSON response structure or missing 'Data' array");
            }
        }


        private void productselectionchanged(object sender, SelectionChangedEventArgs e)
        {


            //runtheloaderdelivery();
            //string selectedText = productcombo.Text;
            //MessageBox.Show(selectedText);


           // MessageBox.Show(productcombo.Text);
            if (productcombo.SelectedItem != null && productcombo.Text != "")
            {
              // MessageBox.Show("Hi" + productcombo.SelectedItem.ToString);
                runtheloaderdelivery();
                // string selectedItemName = ((ComboBoxItem)productcombo.SelectedItem).Content.ToString();
                // MessageBox.Show(selectedItemName);
            }


            // Assuming productcombo is your ComboBox control
            var selectedItem = productcombo.SelectedItem;

            // If the ComboBox is bound to a list of strings
            if (selectedItem is string selectedTexta)
            {
               // MessageBox.Show(selectedTexta);
                //runtheloaderdelivery();
            }
            else
            {
                // If the ComboBox is bound to a list of objects,
                // you might need to handle it differently depending on the type
                // For example:
                // MessageBox.Show(selectedItem.ToString());
            }

           





        }
    }
}
