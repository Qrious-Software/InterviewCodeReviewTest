
using InterviewCodeReviewTest.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace InterviewCodeReviewTest
{
    internal class CustomerDAO : BaseDAO
    {
        #region params
        const string CUSTOMER_ADDRESS = "CustomerAddress";
        const string STATUS = "Status";
        #endregion

        #region sql statements
        readonly string SQL_READ = $"SELECT {CUSTOMER_ADDRESS} FROM dbo.Customer WHERE {STATUS} = @{STATUS}";
        #endregion

        public CustomerDAO(IBaseConnection dataBaseConnection) : base(dataBaseConnection) { }

        public IList<Address> Read(string status)
        {
            var addresslist = new List<string>();
            var command = CreateCommand(SQL_READ);
            command.Parameters.Add(CreateParameter(command, STATUS, status));
            using (var reader = command.ExecuteReader())
            {
                try
                {
                    while (reader.Read())
                    {
                        addresslist.Add(reader.GetString(0));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to read Customer Address: '{ ex.Message }' ");
                }
            }
            return addresslist
                    .Select(StringToAddress)
                    .Where(x => x != null)
                    .ToList();
        }

        private static Address StringToAddress(string addressString)
        {
            const string houseNumberPattern = @"(?<HouseNumber>\d+\w+\s*(-\s*\d+\w*\s)?)";
            const string streetPattern = @"(?<Street>\d+[ ](?:[A-Za-z0-9.-]+[ ]?))";
            const string cityPattern = @"(?<City>:[A-Z][a-z.-]+[ ]?)";
            const string postalCodePattern = @"(?<PostalCode>\b\d{5}(?:-\d{4})?\b)";

            var match = Regex.Match(
                    addressString,
                    houseNumberPattern + streetPattern + cityPattern + postalCodePattern,
                    RegexOptions.IgnoreCase
                );

            if (match.Success && match.Groups.Count == 4)
            {
                var address = new Address();
                address.HouseNumber = match.Groups["HouseNumber"].Success ? match.Groups["HouseNumber"].Value : "undefined";
                address.Street = match.Groups["Street"].Success ? match.Groups["Street"].Value : "undefined";
                address.City = match.Groups["City"].Success ? match.Groups["City"].Value : "undefined";
                address.PostalCode = match.Groups["PostalCode"].Success ? match.Groups["PostalCode"].Value : "undefined";
                Console.WriteLine($"Customer Address successfully parsed: '{ address}' ");
                return address;
            }
            else
                Console.WriteLine($"Failed to parse Customer Address: '{ addressString }' ");
            return null;

        }
    }
}
