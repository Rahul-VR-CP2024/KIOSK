using Exchange.Managers;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace Exchange.Pages
{
    /// <summary>
    /// Interaction logic for wFaq.xaml
    /// </summary>
    public partial class wFaq : Page
    {
        public ObservableCollection<FaqItem> FaqItems { get; set; }

        public wFaq()
        {
            InitializeComponent();

            
            if (TokenManager.Langofsoft == "ar")
            {
                backbtn.Content = "يرجع";
                titlef.Text = "التعليمات";
                h1.Header = "من نحن";
                b1.Text = "منذ أن تأسست وول ستريت للصرافة في الكويت ، تمكنت الشركة من معرفة القيم والمفاهيم في مجال إدارة وتسويق الأعمال المصرفية ، والعديد من الخطوات الإيجابية والمواتية نحو رؤية مستقبلية لاتجاهات الأعمال وكذلك لتبادلنا خطط التقدم فيما يتعلق بالأعمال المصرفية لشركتنا.\r\nتعد وول ستريت للصرافة واحدة من الشركات الرائدة في مجال الخدمات المصرفية والتحويلات المالية وصرافة العملات. قامت الشركة ، على مدى السنوات الماضية ، بتوسيع خدماتها المصرفية لتشمل أكبر قطاع من المواطنين والمقيمين في دولة الكويت. بناءً على التزامها تجاه عملائها والاهتمام المستمر بعملائها ، كانت الشركة حريصة دائمًا على تقديم أفضل الخدمات. ومنذ ذلك الحين ، تم تبني أفضل الممارسات والوسائل لتطوير الأعمال المصرفية بالإضافة إلى المبادئ القيمة التي تدعمها وعمل نسخة احتياطية من نظام العمل والرقابة الداخلية داخل الشركة بناءً على اعتقاد من جانبنا بأنه لكي تصبح أقوى وأكثر تقدمًا ، يجب أن تكون هناك قاعدة سليمة لأعمالنا للاستمرار فيها. وضعنا في وول ستريت للصرافة رؤية مستقبلية نسعى لتحقيقها وفقًا لقدراتنا وتقديرنا لشركتنا لتصبح كيان التبادل الرائد مع وجود ملموس في دولة الكويت وأن نكون الشركة الأولى على مستوى قطاع الصيرفة.\r\n";
                b1.HorizontalAlignment = HorizontalAlignment.Right;
                b1.FlowDirection = FlowDirection.RightToLeft;



                h2.Header = "ماذا نفعله؟";
                b2.Text = "منصة لتحويل الأموال عبر الإنترنت لإرسال المدفوعات الى جميع أنحاء العالم خلال مواقع الدفع في أكثر من 100 دولة. تم بناء نظام التحويل عبر الانترنت لوول ستريت مع الأخذ في الاعتبار رقمنة سوق المال وتأثيره على صناعة التحويلات.\r\nتتيح وول ستريت عبر الإنترنت ببساطة لجميع عملائها الوصول إلى كل ركن من أركان العالم. بالإضافة إلى ذلك ، توفر وول ستريت عبر الانترنت أيضًا طرق الدفع التالية لأجهزة الاستقبال في أكثر من 100 دولة.\r\n•\tالسداد نقدا\r\n•\tالايداعات البنكية\r\n";
                b2.HorizontalAlignment = HorizontalAlignment.Right;
                b2.FlowDirection = FlowDirection.RightToLeft;


                h3.Header = "لماذا نحن؟";
                b3.Text = "تعد وول ستريت للصرافة واحدة من الشركات الرائدة في مجال العملات والحوالات الأجنبية في كل من السوق المحلية والدولية.  في وول ستريت تحصل دائمًا على أفضل أسعار الصرف في السوق اليوم.";
                b3.HorizontalAlignment = HorizontalAlignment.Right;
                b3.FlowDirection = FlowDirection.RightToLeft;



                h4.Header = "هل هناك أي مزايا لاستخدام تطبيق وول ستريت عبر الإنترنت؟";
                b4.Text = "السرعة والكفاءة\r\n• الأمان\r\n• خدمة 24/7 ساعة\r\n• سهل الاستخدام\r\n";
                b4.HorizontalAlignment = HorizontalAlignment.Right;
                b4.FlowDirection = FlowDirection.RightToLeft;


            }
                DataContext = this;

            // Populate FAQ items
            FaqItems = new ObservableCollection<FaqItem>
            {
                new FaqItem { Question = "Question 1", Answer = "Answer 1" },
                new FaqItem { Question = "Question 2", Answer = "Answer 2" },
                // Add more items as needed
            };
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {

            // Pass parameters to Page1.xaml after successful login
            // Page1 page1 = new Page1(username);
            NavigationManager.NavigateToHome();

        }

        public class FaqItem
        {
            public string Question { get; set; }
            public string Answer { get; set; }
        }
    }
}
