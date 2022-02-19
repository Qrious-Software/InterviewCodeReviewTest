using System.Data.Common;
using System.Threading.Tasks;

namespace InterviewCodeReviewTest
{
    public interface IBaseConnection
    {
        /// <summary>Gets the connection string.</summary>
        /// <value>The connection string.</value>
        string ConnectionString { get; }

        /// <summary>Gets the connection.</summary>
        /// <value>The connection.</value>
        DbConnection Connection { get; }

        /// <summary>Opens this instance.</summary>
        /// <returns>
        ///   <br />
        /// </returns>
        Task Open();

        /// <summary>Closes this instance.</summary>
        void Close();

    }
}
