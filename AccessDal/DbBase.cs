using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using DALProfile;


namespace AccessDal
{

    public class DbBase : IDbBase
    {
        //public static string constring = DBConfig.GetConString("AccessCodeMaker");

        DbCommand IDbBase.CreateCommand()
        {
            return new OleDbCommand();
        }
        DbConnection IDbBase.CreateConnection()
        {
            return new OleDbConnection();
        }
        DbDataAdapter IDbBase.CreateDataAdapter()
        {
            return new OleDbDataAdapter();
        }
        DbParameter IDbBase.CreateParameter()
        {
            return new OleDbParameter();
        }
        DbCommandBuilder IDbBase.CreateDbCommandBuilder()
        {
            return new OleDbCommandBuilder();
        }

        public DbBase()
        {
            DbHelper.Provider = this;
        }

    }

}
