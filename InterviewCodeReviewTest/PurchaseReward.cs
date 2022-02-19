using InterviewCodeReviewTest.Model;
using System;
using System.Data.SqlClient;
using System.Transactions;

namespace InterviewCodeReviewTest
{
    public class PurchaseReward
    {
        readonly string SQL_INSERT_PURCHASE = $"INSERT INTO dbo.Purchase...";
        readonly string SQL_INSERT_REWARD = $"INSERT INTO dbo.Reward...";

        public Result UpdateCustomerHistory(Purchase customerPurchase)
        {
            var connPruchase = new SqlConnection("data source=TestPurchaseServer;initial catalog=PurchaseDB;Trusted_Connection=True");
            var connReward = new SqlConnection("data source=TestRewardServer;initial catalog=RewardDB;Trusted_Connection=True");
            System.IO.StringWriter writer = new System.IO.StringWriter();

            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (connPruchase)
                    {
                        connPruchase.Open();
                        SqlCommand recordPurchase = new SqlCommand(SQL_INSERT_PURCHASE, connPruchase);
                        recordPurchase.ExecuteNonQuery();
                        writer.WriteLine($"Purchase recorded successfully");

                        using (connReward)
                        {
                            connReward.Open();
                            SqlCommand command2 = new SqlCommand(SQL_INSERT_REWARD, connReward);
                            command2.ExecuteNonQuery();
                            writer.WriteLine($"Reward updated successfully");
                        }
                    }
                    
                    scope.Complete();
                    scope.Dispose();
                }
            }
            catch (TransactionAbortedException ex)
            {
                writer.WriteLine("TransactionAbortedException Message: {0}", ex.Message);
                return Result.Failed();
            }

            Console.WriteLine(writer.ToString());
            return Result.Success(); 
        }
    }
}
