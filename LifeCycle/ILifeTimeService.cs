public interface ILifeTimeService
{
    Guid Id { get; }

    int GetOperationCount();
}
