using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace InterviewCodeReviewTest
{
	public class Test1
	{
		// Called by web API and returns list of strongly typed customer address for given status
		// CustomerAddress is populated by external import and could be dirty
		public IEnumerable<Address> GetCustomerNumbers(string status)
		{
			var connection = new SqlConnection("data source=TestServer;initial catalog=CustomerDB;Trusted_Connection=True");
			
			
			
			try
			{
				//Use using statement for closing connection automatically
                using (connection)
                {
					//Use parameter query for preventing injection
					var cmd = new SqlCommand();
					cmd.Connection = connection;
					cmd.CommandText = "SELECT CustomerAddress FROM dbo.Customer WHERE Status = @status";
					cmd.CommandType = CommandType.Text;
					cmd.Parameters.Add(new SqlParameter("@status", SqlDbType.VarChar, 100)).Value = status;

					connection.Open();

					var reader = cmd.ExecuteReader();

					var addressStrings = new List<string>();

					while (reader.Read())
					{
						addressStrings.Add(reader.GetString(0));
					}

					return addressStrings
						.Select(StringToAddress)
						.Where(x => x != null)
						.ToList();

				}
				
			}
			catch (Exception ex)
			{
				throw ex;
			}
			
		}

		private static Address StringToAddress(string addressString)
		{
			return new Address(addressString);
		}
	}

	public class Address
	{
		// Some members...

		public Address(string addressString)
		{
			// Assume there are logic here to parse address and return strongly typed object
		}
	}
}
