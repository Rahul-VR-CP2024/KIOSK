using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Model
{
    public class Beneficiary
    {
        public string BeneficiarySalutation { get; set; }
        public string BeneficiaryGender { get; set; }
        public string BeneficiaryFirstName { get; set; }
        public string BeneficiaryMiddleName { get; set; }
        public string BeneficiaryLastName { get; set; }
        public string BeneficiaryAddress1 { get; set; }
        public string BeneficiaryAddress2 { get; set; }
        public string BeneficiaryCity { get; set; }
        public string BeneficiaryState { get; set; }
        public string BeneficiaryCountryCode { get; set; }
        public string BeneficiaryNationalityCode { get; set; }
        public string BeneficiaryZipCode { get; set; }
        public string BeneficiaryPhone { get; set; }
        public string BeneficiaryMobile { get; set; }
        public string BeneficiaryEmail { get; set; }
        public string BeneficiaryFax { get; set; }
        public string ProductCode { get; set; }
        public string CurrencyCode { get; set; }
        public string BeneficiaryBankAccountNumber { get; set; }
        public string BeneficiaryBankAccountType { get; set; }
        public string BeneficiaryBankCode { get; set; }
        public string BeneficiaryBankName { get; set; }
        public string BeneficiaryBranchCode { get; set; }
        public string BeneficiaryBranchName { get; set; }
        public string BeneficiaryBranchLandMark { get; set; }
        public string Remarks { get; set; }
        public string Status { get; set; }
    }

    public class TransactionRequest
    {
        public int TransactionReference { get; set; }
        public string SourceCurrencyCode { get; set; }
        public decimal SourceAmount { get; set; }
        public string DestinationCurrencyCode { get; set; }
        public decimal DestinationAmount { get; set; }
        public decimal Rate { get; set; }
        public decimal Commission { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxCollected { get; set; }
        public decimal NetAmount { get; set; }
        public string TransferModeCode { get; set; }
        public string PurposeCode { get; set; }
        public string IncomeSourceCode { get; set; }
        public int BeneficiaryCode { get; set; }
        public string BeneficiaryName { get; set; }
        public Beneficiary Beneficiary { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime ModifiedOn { get; set; }
    }
}
