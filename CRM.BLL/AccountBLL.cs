using CRM.DAL;
using CRM.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.BLL
{
    public class AccountBLL
    {
        AccountDAL _objAccountDAL;
        public AccountBLL()
        {
            _objAccountDAL = new AccountDAL();
        }

        public ReturnStatus setLoginLogout(string UserID,string Action)
        {
            return _objAccountDAL.setLoginLogout(UserID, Action);
        }

        public DataSet getLoggedInUsers(string UserID = "")
        {
            return _objAccountDAL.getLoggedInUsers(UserID);

        }
    }
}
