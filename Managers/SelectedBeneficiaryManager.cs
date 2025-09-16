namespace Exchange.Managers
{
    public static class SelectedBeneficiaryManager
    {
        public static string BENE_SLNO { get; set; }

        public static string BENE_EID { get; set; }
        public static void SetBENE_SLNO(string token)
        {
            BENE_SLNO = token;
        }
        public static void SetBENE_EID(string eid)
        {
            BENE_EID = eid;
        }
    }
}
