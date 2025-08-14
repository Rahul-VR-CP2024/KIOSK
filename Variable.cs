namespace Exchange
{
    internal class Variable
    {

        //UAT
        //public static string apiipadd = "192.168.1.67:55525";   
        //public static string apiClientKey = "WALLSTKIOSKUAT";

        static Variable()
        {
            SetKisokId();
        }

        //LIVE
        public static string apiipadd = "wallstreet-kuwait-mob-api.codepoint-solutions.com";
        public static string apiClientKey = "WALLSTKIOSK";

        public static string _kioskidno;
        public static string kioskidno => _kioskidno; 

        public static bool connected;   //whether opened device or not
        public static bool iscallback;  //whether use callback mode or not

        public static bool isCaptureIR;  //whether cature IR image
        public static bool isCaptureWH;  //whether cature IR image
        public static bool isCaptureUV;  //whether cature IR image

        public static bool isDecodeDM;
        public static bool isDecodePdf417;
        public static bool isDecodeQR;

        public static bool isAutoOcr;

        private static void SetKisokId()
        {
            #if KIOSK_14410942
                _kioskidno = "14410942"; //1
            #elif KIOSK_14410943
                _kioskidno = "14410943"; //2
            #elif KIOSK_14410944
                _kioskidno = "14410944"; //3
            #elif KIOSK_14410945
                _kioskidno = "14410945"; //4
            #elif KIOSK_14410946
                _kioskidno = "14410946"; //5
            #elif KIOSK_14410947
                _kioskidno = "14410947"; //6
            #elif KIOSK_14410948
                _kioskidno = "14410948"; //7
            #elif KIOSK_14410949
                _kioskidno = "14410949"; //8
            #elif KIOSK_14410950
                _kioskidno = "14410950"; //9
            #elif KIOSK_14410951
                _kioskidno = "14410951"; //10
            #elif KIOSK_14410952
                _kioskidno = "14410952"; //11
            #elif KIOSK_14410967
                _kioskidno = "14410967"; //12
            #elif KIOSK_14410968
                _kioskidno = "14410968"; //13
            #elif KIOSK_14410970
                _kioskidno = "14410970"; //14
            #elif KIOSK_14410971
                _kioskidno = "14410971"; //15
            #elif KIOSK_14410972
                _kioskidno = "14410972"; //16
            #elif KIOSK_14410973
                _kioskidno = "14410973"; //17
            #elif KIOSK_14410974
                _kioskidno = "14410974"; //18
            #elif KIOSK_14410975
                _kioskidno = "14410975"; //19
            #elif KIOSK_14410976
                _kioskidno = "14410976"; //20
            #elif KIOSK_14410977
                _kioskidno = "14410977"; //21
            #elif KIOSK_14410978
                _kioskidno = "14410978"; //22
            #elif KIOSK_14410979
                _kioskidno = "14410979"; //23
            #elif KIOSK_14410980
                _kioskidno = "14410980"; //24
            #elif KIOSK_14410981
                _kioskidno = "14410981"; //25
            #elif KIOSK_14410982
                _kioskidno = "14410982"; //26
            #else
                _kioskidno = ""; //for manual builds
            #endif


        }
    }
}
