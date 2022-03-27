﻿using System;
using System.Net.Mail;
using System.Threading;

namespace InterviewCodeReviewTest
{
	public class Test3
	{
		// This class represents a queue for email sending.
		// There are multiple active queues at any given time and they have activities all the time.
		// Each queue can be handled by multiple threads.
		public class EmailSendQueue
		{
			public int SentCount { get; private set; }
			public int FailedCount { get; private set; }

			// Assign each email to different thread for performance
			public void SendNextEmail()
			{
				var thread = new Thread(SendEmail);
				thread.Start();
			}

			private void SendEmail()
			{
				try
                {
					var client = new SmtpClient();
					// Send email via Smtp and returns Result object...
					MailMessage mail = new MailMessage();
					client.Send(mail);

					UpdateStatistics(Result.Success());
				}
				catch(Exception e)
                {
					UpdateStatistics(Result.Failed());
				}
				
			}

			private void UpdateStatistics(Result result)
			{
				lock (typeof(EmailSendQueue))
				{
					if (result.IsSuccessful)
					{
						SentCount++;
					}
					else
					{
						FailedCount++;
					}
				}
			}
		}
	}
}
