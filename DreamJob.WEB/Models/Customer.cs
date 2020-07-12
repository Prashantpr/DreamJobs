using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CRM.WEB.Models
{
    public class PartialViewLoader
    {
        public string strPartialView { get; set; }
        public string strCallDispositionPartialView { get; set; }
    }
    public class Customer
    {
        [Key]
        public int CustomerID { get; set; }
        [Required(ErrorMessage="Please enter customer name")]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter mobile number")]
        public string MobileNo { get; set; }
        public string AltContactNo { get; set; }
        [Required(ErrorMessage = "Please enter Email ID")]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please enter residential address")]
        public string Age { get; set; }
        public string ResidentialAddress { get; set; }
        [Required(ErrorMessage = "Please enter official address")]
        public string OfficialAddress { get; set; }
        public string ExistingEMI { get; set; }
        public string PAN { get; set; }
        public string PANUploaded { get; set; }
        public VerificationStatus PANVerificationStatus { get; set; }
        public string AddressProof { get; set; }

        public string AnnualIncome { get; set; }

        public EmploymentType employmentType { get; set; }

        public string CreateDateTime { get; set; }
        public string CreatedBy { get; set; }
        public string UpdateDateTime { get; set; }
        public string UpdatedBy { get; set; }
        public int? CustomerUserId { get; set; }
        public string ViewMode { get; set; }
        public List<Customer> ShowAllCustomers { get; set; }

        public decimal LoanAmount { get; set; }

        public Int64 LastCallID { get; set; }
    }

    public class EmploymentType
    {
        public string DisplayText { get; set; }
        public string DisplayValue { get; set; }

        public List<EmploymentType> listEmploymentType()
        {
            return new List<EmploymentType> { 
            new EmploymentType{DisplayText="Salaried",DisplayValue="Salaried"}
            ,new EmploymentType{DisplayText="Self Employed",DisplayValue="Self Employed"}
            };
        }
    }

    public class VerificationStatus
    {
        public string DisplayText { get; set; }
        public string DisplayValue { get; set; }

        public List<VerificationStatus> listVerificationStatus()
        {
            return new List<VerificationStatus> { 
            new VerificationStatus{DisplayText="Pending",DisplayValue="0"}
            ,new VerificationStatus{DisplayText="Verified",DisplayValue="1"}
            ,new VerificationStatus{DisplayText="Discarded",DisplayValue="2"}
            };
        }
    }

    public class TeleCallerVM
    {

        public Customer customer { get; set; }
        public NatureOfLoan _NatureOfLoan { get; set; }

        public List<NatureOfLoan> lstNatureOfLoan { get; set; }
        public List<ProperyDetailType> lstProperyDetailType { get; set; }

        public OutcomeMaster outcomeMaster { get; set; }
        public List<OutcomeMaster> lstOutcomeMaster { get; set; }
        public SubOutcomeMaster subOutcomeMaster { get; set; }
        public List<SubOutcomeMaster> lstSubOutcomeMaster { get; set; }

        public CallHistory callHistory { get; set; }

    }

    public class ProperyDetailType
    {
        public int PropertyDetailTypeID { get; set; }
        public string PropertyDetailType { get; set; }
    }

    public class NatureOfLoan
    {
        public int natureOfLoanID { get; set; }
        public string natureOfLoan { get; set; }
    }

    public class CallHistory
    {
        public Int64 CallID { get; set; }
        public Int64 CrmID { get; set; }
        public DateTime CallDateTime { get; set; }
        public DateTime CallEndDateTime { get; set; }
        public SubOutcomeMaster subOutcomeMaster { get; set; }
        public string Remarks { get; set; }
        public DateTime CallBackDateTime { get; set; }

        public string CallBackDate { get; set; }
        public string CallBackTime { get; set; }

        public string CallbackType { get; set; }
        public Customer customer { get; set; }
    }

    public class OutcomeMaster
    {
        public int OutcomeID { get; set; }
        public string Outcome { get; set; }
    }

    public class SubOutcomeMaster
    {
        public OutcomeMaster outcomeMaster { get; set; }
        public int SubOutcomeID { get; set; }
        public string SubOutcome { get; set; }
    }

    public class BankMaster
    {
        public int BankID { get; set; }
        public string BankName { get; set; }
    }
}