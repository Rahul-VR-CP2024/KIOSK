using Exchange.Managers;
using System.Net.Http;
using System.Text.Json;
using System.Web.Services.Description;
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

        public class Product
        {
            public string ProductName { get; set; }
            public string ProductCode { get; set; }
            public string EId { get; set; }
            public string RoutingBankCode { get; set; }
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

        public wSelectProduct()
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private void backbutton(object sender, RoutedEventArgs e)
        {
            wSelectcountry selprod = new wSelectcountry();
            NavigationService.Navigate(selprod);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Product selectedProduct = (Product)productcombo.SelectedItem;
                Deliverytype selecteddisp = (Deliverytype)deliverycombo.SelectedItem;


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

                ProductManager.Setselectedproductcode(productCode);
                //ProductManager.Setselecteddisbcode(dispCode);
                ProductManager.Setselecteddisbcode(dispCode);
                ProductManager.SetselectedProdCurrCode(currCode);


                string selectedText = deliverycombo.Text;

                if (BCManager.fromwhereopenedwtbc == "exchangerate")
                {
                    //MessageBox.Show(ProductManager.selectedproductcode + ProductManager.selectedProdCurrCode);
                    wExchangecalculator wx = new wExchangecalculator();
                    NavigationService.Navigate(wx);
                }
                else
                {
                    waddbeneficiary selprod = new waddbeneficiary("add");
                    NavigationService.Navigate(selprod);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private async void ONLOAD(object sender, RoutedEventArgs e)
        {
            try
            {
               
                runtheloadersource();
                //runtheloaderdelivery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }


        }

        private async void runtheloadersource()
        {
            try
            {
                var CPORBT = BCManager.selectedoptionborc;

                using var client = new HttpClient();

                var request = new HttpRequestMessage(HttpMethod.Get, "http://" + Variable.apiipadd + "/api/Customer/get-product-list?DisbursalMode=" + CPORBT + "&CountryCode=" + SelectedAddBeneCountry.seladdbenecount);

                // Add bearer token
                request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);
                request.Headers.Add("accept", "text/plain");

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

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
        }

        private void UpdateComboBoxsource(JsonElement root)
        {
            try
            {
                productcombo.Items.Clear();

                List<Product> products = new List<Product>();

                // Navigate: data → product_list
                if (root.TryGetProperty("data", out JsonElement dataElement) &&
                    dataElement.TryGetProperty("product_list", out JsonElement productListElement) &&
                    productListElement.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in productListElement.EnumerateArray())
                    {
                        string name = item.TryGetProperty("name", out JsonElement nameEl) ? nameEl.GetString() ?? "" : "";
                        string code = item.TryGetProperty("code", out JsonElement codeEl) ? codeEl.ToString() : "";
                        string eId = item.TryGetProperty("e_id", out JsonElement eIdEl) ? eIdEl.GetString() ?? "" : "";
                        string routingBankCode = item.TryGetProperty("routing_bank_code", out JsonElement rbEl) ? rbEl.ToString() : "";

                        products.Add(new Product
                        {
                            ProductName = name,
                            ProductCode = code,
                            EId = eId,
                            RoutingBankCode = routingBankCode
                        });
                    }
                }
                else
                {
                    Console.WriteLine("Invalid JSON response structure or missing product_list");
                }

                productcombo.ItemsSource = products;

                if (products.Count > 0)
                {
                    productcombo.SelectedItem = products[0];
                }
                productcombo.DisplayMemberPath = "ProductName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private async void runtheloaderdelivery()
        {
            try
            {
                var CPORBT = BCManager.selectedoptionborc;

                if (productcombo.SelectedItem is Product selectedProduct)
                {
                    var PRODCODE = selectedProduct.ProductCode;

                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Get,"https://" + Variable.apiipadd +"/api/Customer/get-Disb-list?ProductCode=" + PRODCODE + "&DisbursalMode=" + CPORBT);

                        request.Headers.Add("Authorization", "Bearer " + TokenManager.Token);

                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();

                        using (var responseStream = await response.Content.ReadAsStreamAsync())
                        using (var doc = JsonDocument.Parse(responseStream))
                        {
                            UpdateComboBoxdeliveryv2(doc.RootElement);
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        Console.WriteLine($"Error sending request: {ex.Message}");
                    }

                    if (deliverycombo.Items.Count > 0)
                    {
                        deliverycombo.SelectedIndex = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void UpdateComboBoxdeliveryv2(JsonElement root)
        {
            try
            {
                deliverycombo.ItemsSource = null;
                deliverycombo.Items.Clear();

                List<Deliverytype> deliveries = new List<Deliverytype>();

                if (root.TryGetProperty("data", out JsonElement dataElement) &&
                    dataElement.TryGetProperty("transfer_mode_list", out JsonElement transferList) &&
                    transferList.ValueKind == JsonValueKind.Array)
                {
                    foreach (var item in transferList.EnumerateArray())
                    {
                        if (item.TryGetProperty("name", out JsonElement disbNameElement) &&
                            item.TryGetProperty("code", out JsonElement disbCodeElement))
                        {
                            deliveries.Add(new Deliverytype
                            {
                                DisbName = disbNameElement.GetString(),
                                DisbCode = disbCodeElement.GetString()
                            });
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid JSON response structure or missing 'transfer_mode_list'");
                }

                deliverycombo.ItemsSource = deliveries;

                if (deliveries.Count > 0)
                {
                    deliverycombo.SelectedItem = deliveries[0];
                }
                deliverycombo.DisplayMemberPath = "DisbName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void productselectionchanged(object sender, SelectionChangedEventArgs e)
        {

            try
            {
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }

        }

        private async void runtheloadersource_old()
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
                var request = new HttpRequestMessage(HttpMethod.Get, "http://" + Variable.apiipadd + "/api/v1/sxgeneral/DefaultProduct?DisbursalMode=" + CPORBT + "&CountryCode=" + SelectedAddBeneCountry.seladdbenecount);
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


    }
}
