namespace Domain.Repositories
{
    public interface IRepositoryManager
    {
        ICategoryRepository CategoryRepository { get; }

        IProductRepository ProductRepository { get; }

        IUnitOfWork UnitOfWork { get; }
    }
}
