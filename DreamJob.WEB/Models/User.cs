using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AapKaFinance.Models
{
    public partial class User
    {
        //[Key]
        //public int UserId { get; set; }
        [Required(ErrorMessage="Please enter username", AllowEmptyStrings=false)]
        public string UserName { get; set; }
        //public string FullName { get; set; }
        public string Password { get; set; }
        //public UserTypeMst userType { get; set; }
        public string UserType { get; set; }

    }
}