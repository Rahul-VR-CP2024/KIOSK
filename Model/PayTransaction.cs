using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    public class PayTransaction
    {
        public List<TransactionReference> transaction_references { get; set; }
        public Payment payment { get; set; }
        public int pay_location { get; set; }
        public string remarks { get; set; }
    }

    public class TransactionReference
    {
        public int transaction_reference { get; set; }
    }

    public class Payment
    {
        public string payment_mode { get; set; }
        public string bank_account_no { get; set; }
        public string receipt_no { get; set; }
        public string doc_ref { get; set; }
        public int id_doc_ref { get; set; }
        public int account_code { get; set; }
    }
}
