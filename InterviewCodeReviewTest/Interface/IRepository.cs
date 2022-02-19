using InterviewCodeReviewTest.Model;
using System.Collections.Generic;

namespace InterviewCodeReviewTest.Interface
{
    public interface IRepository
    {
        IList<Address> Get(uint contextId, RequestModel model);
    }
}
