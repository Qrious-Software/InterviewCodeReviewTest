using System;
using System.Data;
using System.Data.Common;


namespace InterviewCodeReviewTest
{
    internal abstract class BaseDAO
    {
        protected IBaseConnection _dataBaseConnection = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseDAO"/> class.
        /// </summary>
        public BaseDAO(IBaseConnection dataBaseConnection)
        {
            _dataBaseConnection = dataBaseConnection;
        }

        #region protected methods

        /// <summary>
        ///Creates and returns a System.Data.Common.DbCommand object associated with the current connection.
        /// </summary>
        /// <param name="SQL">A SQL statement as a string.</param>
        /// <param name="connection">The current connection object.</param>
        /// <returns>A System.Data.Common.DbCommand object.</returns>
        protected DbCommand CreateCommand(string sql, CommandType commandType = CommandType.Text)
        {
            DbCommand command;
            try
            {
                command = _dataBaseConnection.Connection.CreateCommand();
                command.CommandType = commandType;
                command.CommandText = sql;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to Create Command in basedao. Error:" + ex.Message);
                throw;
            }
            return command;
        }

        /// <summary>
        /// Creates and returns a System.Data.Common.DbParameter object.
        /// </summary>
        /// <param name="command">A System.Data.Common.DbCommand object.</param>
        /// <param name="parameterName">Specifies the name of the parameter.</param>
        /// <param name="parameterValue">Specifies the value of the parameter.</param>
        /// <returns>A System.Data.Common.DbParameter object.</returns>
        protected DbParameter CreateParameter(DbCommand command, string parameterName, object parameterValue)
        {
            var result = command.CreateParameter();
            result.ParameterName = parameterName;
            result.Direction = ParameterDirection.Input;
            result.SourceColumnNullMapping = true;
            result.Value = parameterValue;
            return result;
        }


        #endregion
    }
}

