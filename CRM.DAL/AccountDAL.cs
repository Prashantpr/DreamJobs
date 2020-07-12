using CRM.DbBridge;
using CRM.Entity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DAL
{
    public class AccountDAL
    {
        private SqlDbBridge _SqlDbBridge;
        public AccountDAL()
        {
            _SqlDbBridge = new SqlDbBridge();
        }

        public ReturnStatus setLoginLogout(string UserID, string Action)
        {
            ReturnStatus objReturnStatus = new ReturnStatus();
            List<SqlParameter> _param = new List<SqlParameter>();

            try
            {
                _param.Add(new SqlParameter("@UserID", UserID));
                _param.Add(new SqlParameter("@Action", Action));
                return _SqlDbBridge.ExecuteDataSet("proc_LoginLogout", _param).Tables[0].AsEnumerable().Select(d => new ReturnStatus { ErrorStatus = Convert.ToInt32(d["ErrorStatus"]), ErrorMessage = Convert.ToString(d["ErrorMessage"]) }).Single();

            }
            catch (Exception ex)
            {
                objReturnStatus.ErrorStatus = 1;
                objReturnStatus.ErrorMessage = ex.Message;
            }

            return objReturnStatus;
        }

        public DataSet getLoggedInUsers(string UserID = "")
        {
            List<SqlParameter> _param = new List<SqlParameter>();

            try
            {
                _param.Add(new SqlParameter("@UserID", UserID));
                return _SqlDbBridge.ExecuteDataSet("proc_LoggedInUsers", _param);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
