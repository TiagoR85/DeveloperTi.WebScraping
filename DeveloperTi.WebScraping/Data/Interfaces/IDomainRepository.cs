namespace DeveloperTi.WebScraping.Data.Interfaces
{
    interface IDomainRepository<TEntity> : IRepositoryGeneric<TEntity> where TEntity : class, IIdentityEntity
    {
    }
}
