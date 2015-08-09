using System.ComponentModel.Composition;
using System.Configuration;
using System.Data;
using GasWebMap.Core.Data;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.MySql;

namespace GasWebMap.Repository.MySql
{
    [Export(typeof(IDbCnnFactory))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DbCnnFactory : IDbCnnFactory
    {
        private IDbConnectionFactory _dbFactory;

        protected IDbConnectionFactory dbFactory
        {
            get
            {
                if (_dbFactory == null)
                {
                    string strCnn = ConfigurationManager.ConnectionStrings["mysql"].ConnectionString;
                 
                    _dbFactory = new OrmLiteConnectionFactory(strCnn, true, MySqlDialectProvider.Instance, true);
                }

                return _dbFactory;
            }
        }

        public string DbProvider
        {
            get { return "System.Data.MySqlClient"; }
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