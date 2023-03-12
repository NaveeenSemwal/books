namespace Books.Data.Interfaces
{
    public interface IUnitOfWork
    {
        IBooksRepository BooksRepository { get; }
        IRolesRepository RolesRepository { get; }
        IUserRepository UserRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
