
namespace InterviewCodeReviewTest.Model
{
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
