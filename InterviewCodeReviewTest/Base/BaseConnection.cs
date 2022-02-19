using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace InterviewCodeReviewTest
{
    internal sealed class BaseConnection : IDisposable, IBaseConnection
    {
        #region private members
        private readonly DbProviderFactory _dbProviderFactory = null;
        private readonly string _providerString;
        #endregion

        #region properties
        public string ConnectionString { get; }

        /// <summary>
        /// Gets the underlaying connection. Open() must be called before this.
        /// </summary>
        /// <value>The instance of DbConnection.</value>
        public DbConnection Connection { get; private set; } = null;

        #endregion

        #region C'tors

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseConnection"/> class.
        /// </summary>
        public BaseConnection(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException("ConnectionString is empty ( BaseConnection() )");
            }

            ConnectionString = connectionString;
            _dbProviderFactory = GetProviderFactory();
        }

        /// <summary>
        /// Closes and disposes underlaying connection
        /// </summary>
        public void Dispose()
        {
            if (Connection != null)
            {
                Connection.Close();
                Connection.Dispose();
            }
        }

        #endregion

        #region public methods


        /// <summary>
        /// Creates a System.Data.Common.DbConnection object and opens it.
        /// </summary>
        public async Task Open()
        {
            if (Connection == null)
            {
                Connection = _dbProviderFactory.CreateConnection();
            }
            Connection.ConnectionString = ConnectionString;
            await Connection.OpenAsync();
        }

        /// <summary>
        /// Closes underlaying connection
        /// </summary>
        public void Close()
        {
            if (Connection != null)
            {
                Connection.Close();
            }
        }
        
        #endregion

        #region private methods
        /// <summary>
        /// Creates and returns a System.Data.Common.DbProviderFactory object.
        /// </summary>
        /// <returns>A System.Data.Common.DbProviderFactory object.</returns>
        private DbProviderFactory GetProviderFactory()
        {
            DbProviderFactory dbProviderFactoryLocal = null;
            if (!string.IsNullOrEmpty(_providerString))
            {
                dbProviderFactoryLocal = DbProviderFactories.GetFactory(_providerString);
            }
            return dbProviderFactoryLocal;
        }
        #endregion
    }
}
