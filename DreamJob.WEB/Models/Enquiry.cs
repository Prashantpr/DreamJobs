using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AapKaFinance.Models
{
    public class Enquiry
    {
        [Key]
        public int EnquiryId { get; set; }
        [Required(ErrorMessage="Please enter your name",AllowEmptyStrings=false)]
        public string CustomerName { get; set; }
        [Required(ErrorMessage = "Please enter your mobile number", AllowEmptyStrings = false)]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please enter your email id", AllowEmptyStrings = false)]
        public string EmailID { get; set; }
        [Required(ErrorMessage = "Please enter your message", AllowEmptyStrings = false)]
       // public string Subject { get; set; }
        public string Message { get; set; }
        public string CreateDateTime { get; set; }

        public int? SN { get; set; }

        public string VerificationStatusId { get; set; }
        public string VerificationStatus { get; set; }


    }
}