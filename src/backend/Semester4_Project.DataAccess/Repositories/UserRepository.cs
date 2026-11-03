// Reads and saves users in the database. The only place that touches the Users table.

using Microsoft.EntityFrameworkCore;
using Semester4_Project.DataAccess.Entities;

namespace Semester4_Project.DataAccess.Repositories;

public class UserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<User?> GetByEmailAsync(string email)
    {
        return _db.Users.FirstOrDefaultAsync(u => u.Email == email);
    }

    public Task<List<User>> GetByRoleAsync(string role)
    {
        return _db.Users.Where(u => u.Role == role).OrderBy(u => u.Email).ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
    }
}
