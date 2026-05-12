using learndotnet.Models;

namespace learndotnet.Repositories;

public interface IUserRepository
{
    IEnumerable<User> GetAllUsers();
    User? GetUserById(int id);
}