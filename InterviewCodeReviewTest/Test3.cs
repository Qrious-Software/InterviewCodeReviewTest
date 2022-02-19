using InterviewCodeReviewTest.Model;
using System;
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
                var result = Result.Success();
                try
                {
                    var client = new SmtpClient
                    {
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = true
                    };

                    Thread T1 = new Thread(delegate ()
                    {
                        using (var message = new MailMessage("senderAdrress", "toAddress")
                        {
                            Subject = "",
                            Body = ""
                        })
                        {
                            {
                                client.Send(message);
                            }
                        }
                    });

                    T1.Start();
                }

                catch (SmtpFailedRecipientException ex)
                {
                    result = Result.Failed();
                    Console.WriteLine($"Failed exception {ex}");
                }
                UpdateStatistics(result);
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
