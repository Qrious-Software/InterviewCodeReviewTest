namespace InterviewCodeReviewTest
{
    public interface IDataAccessLayerResolver
    {
        IDataAccessLayer Resolve(uint contextId);
    }
}
