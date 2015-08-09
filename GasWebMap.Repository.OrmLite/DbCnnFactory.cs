using System.Configuration;
using System.Data;
using GasWebMap.Core.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Oracle;

namespace GasWebMap.Repository.Oracle
{
    public class OracleDbCnnFactory : IDbCnnFactory
    {
        private IDbConnectionFactory _dbFactory;

        protected IDbConnectionFactory dbFactory
        {
            get
            {
                if (_dbFactory == null)
                {
                    string strCnn = ConfigurationManager.ConnectionStrings["GasWebMap"].ConnectionString;
                    //string strCnn = Config.Value.GetValue("sqlserver");
                    _dbFactory = new OrmLiteConnectionFactory(strCnn, true, OracleOrmLiteDialectProvider.Instance);
                }

                return _dbFactory;
            }
        }

        #region IDbCnnFactory Members

        public IDbConnection OpenConnection()
        {
            return dbFactory.OpenDbConnection();
        }

        public IDbConnection CreateConnection()
        {
            return dbFactory.CreateDbConnection();
        }

        #endregion
    }
}