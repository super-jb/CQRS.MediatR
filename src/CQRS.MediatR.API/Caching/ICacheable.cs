namespace CQRS.MediatR.API.Caching
{
    public interface ICacheable
    {
        string CacheKey { get; }
    }
}
