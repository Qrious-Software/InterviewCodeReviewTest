
using InterviewCodeReviewTest.Model;
using System;
using System.Collections.Generic;

namespace InterviewCodeReviewTest
{
    public class CustomerRepository
    {
        readonly IDataAccessLayerResolver _DataAccessLayerResolver;
        private string _ConnectionSetting;
        private readonly IConfiguration _Configuration;

        public CustomerRepository(IConfiguration config)
        {
            _Configuration = config ?? throw new ArgumentNullException(nameof(config));
            _ConnectionSetting = _Configuration
                .GetConfigurationStringValue("connectionStrings:Server");
        }

        public IList<Address> Get(uint contextId, RequestModel model)
        {
            try
            {
                var dataAccessLayer = _DataAccessLayerResolver.Resolve(contextId);
                BaseConnection dbConnection = new BaseConnection(_ConnectionSetting);
                var dao = new CustomerDAO(dbConnection);
                dbConnection.Open();
                return dao.Read(model.Status);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading customer address, contextId:{contextId}, Error:{ex.Message}");
                throw;
            }
        }


    }
}
