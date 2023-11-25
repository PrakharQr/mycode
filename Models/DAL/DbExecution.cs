using Microsoft.Practices.EnterpriseLibrary.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuarTest.Models.DAL
{
    public class DbExecution
    {
        public DataSet GetExecute(MasterProduct_info Obj)
        {
            DataSet ds = new DataSet();
            string _procName = "Sp_MSt_Product";
            Database objDatabase = DatabaseFactory.CreateDatabase("Default");
            DbCommand objDbCommand = objDatabase.GetStoredProcCommand(_procName);
            try
            {          
                objDatabase.AddInParameter(objDbCommand, "@Flag", DbType.String, Obj.Flag);
                objDatabase.AddInParameter(objDbCommand, "@Id", DbType.String, Obj.Id);
                SqlParameter cDetail = new SqlParameter("@Dt_Prd_Master", Obj.Prddt);
                cDetail.SqlDbType = SqlDbType.Structured;
                objDbCommand.Parameters.Add(cDetail);
                ds = objDatabase.ExecuteDataSet(objDbCommand);
            }
            catch (Exception ee)
            {
                throw ee;
            }
            finally
            {
                objDbCommand.Dispose();
            }
            return ds;
        }
    }
}