using System;
using System.Data.SqlClient;
using System.Transactions;

namespace InterviewCodeReviewTest
{
	public class Test2
	{
		// Record customer purchase and update customer reward programme
		public Result UpdateCustomerHistory(Purchase customerPurchase)
		{
			var connPruchase = new SqlConnection("data source=TestPurchaseServer;initial catalog=PurchaseDB;Trusted_Connection=True");
			var connReward = new SqlConnection("data source=TestRewardServer;initial catalog=RewardDB;Trusted_Connection=True");

			var cmdPurchase = new SqlCommand("INSERT INTO dbo.Purchase..."); // omitted the columns
			var cmdReward = new SqlCommand("INSERT INTO dbo.Reward..."); // omitted the columns

			try
			{
				connPruchase.Open();
				connReward.Open();

				//Use transactionscope for handling multiple connections' transaction at once
				using (TransactionScope scope = new TransactionScope())
				{
					cmdPurchase.ExecuteNonQuery();
					cmdReward.ExecuteNonQuery();

					scope.Complete();
				}
				
				return Result.Success();
			}
			catch (Exception ex)
			{
				return Result.Failed();
			}
            finally
            {
				//Add DB connection close
				connPruchase.Close();
				connReward.Close();

			}
		}
	}

	public class Purchase
	{
		// Some members
	}

	public class Result
	{
		public bool IsSuccessful { get; private set; }

		public static Result Success()
		{
			return new Result { IsSuccessful = true };
		}

		public static Result Failed()
		{
			return new Result { IsSuccessful = false };
		}
	}
}
