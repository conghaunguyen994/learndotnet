using learndotnet.Models;
using learndotnet.Data;

namespace learndotnet.Repositories;



public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }

    public User? GetUserById(int id)
    {
        return _context.Users.FirstOrDefault(user => user.Id == id);
    }
}