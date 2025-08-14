namespace Exchange.Common
{
    public static class QRCodePACIdata
    {
        public static string isvalidqrcode { get; set; }
        public static string fullNameEn { get; set; }
        public static string fullNameAr { get; set; }
        public static string address { get; set; }
        public static string birthdate { get; set; }
        public static string bloodGroup { get; set; }
        public static string cardExpiryDate { get; set; }
        public static string civilID { get; set; }
        public static string emailAddress { get; set; }
        public static string gender { get; set; }
        public static string govData { get; set; }
        public static string mobileNumber { get; set; }
        public static string nationalityAr { get; set; }
        public static string nationalityEn { get; set; }
        public static string nationalityFlag { get; set; }
        public static string passportNumber { get; set; }


        public static void Setisvalidqrcode(string token)
        {
            isvalidqrcode = token;
        }
        public static void SetfullNameEn(string token)
        {
            fullNameEn = token;
        }

        public static void SetfullNameAr(string token)
        {
            fullNameAr = token;
        }

        public static void Setaddress(string token)
        {
            address = token;
        }

        public static void Setbirthdate(string token)
        {
            birthdate = token;
        }
        public static void SetbloodGroup(string token)
        {
            bloodGroup = token;
        }
        public static void SetcardExpiryDate(string token)
        {
            cardExpiryDate = token;
        }
        public static void SetcivilID(string token)
        {
            civilID = token;
        }
        public static void SetemailAddress(string token)
        {
            emailAddress = token;
        }
        public static void Setgender(string token)
        {
            gender = token;
        }
        public static void SetgovData(string token)
        {
            govData = token;
        }
        public static void SetmobileNumber(string token)
        {
            mobileNumber = token;
        }
        public static void SetnationalityAr(string token)
        {
            nationalityAr = token;
        }
        public static void SetnationalityEn(string token)
        {
            nationalityEn = token;
        }
        public static void SetnationalityFlag(string token)
        {
            nationalityFlag = token;
        }
        public static void SetpassportNumber(string token)
        {
            passportNumber = token;
        }


    }
}
