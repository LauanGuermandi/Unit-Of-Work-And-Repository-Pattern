using System.Threading.Tasks;

namespace PocketBook.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IUserRepository Users { get; }
        Task CompleteAsync();
    }
}
