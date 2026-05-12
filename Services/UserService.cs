using learndotnet.Models;
using learndotnet.Repositories;

namespace learndotnet.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public IEnumerable<User> GetUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public User? GetUserById(int id)
    {
        return _userRepository.GetUserById(id);
    }
}