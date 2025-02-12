using firstPetProject.Core.Domain.Entities;
using Task = System.Threading.Tasks.Task;

namespace firstPetProject.Core.Auth.Interfaces
{
    public interface IAccountRepository
    {
        Task Add(User user);
        Task<User> GetByEmail(string email);
        Task<bool> EmailExists(string email);
        Task<User?> GetById(Guid id);
    }
}
