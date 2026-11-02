// Reads and saves student profiles in the database. The only place that touches the StudentProfiles table.

using Microsoft.EntityFrameworkCore;
using Semester4_Project.DataAccess.Entities;

namespace Semester4_Project.DataAccess.Repositories;

public class ProfileRepository
{
    private readonly AppDbContext _db;

    public ProfileRepository(AppDbContext db)
    {
        _db = db;
    }

    public Task<StudentProfile?> GetByUserIdAsync(int userId)
    {
        return _db.StudentProfiles.FirstOrDefaultAsync(p => p.UserId == userId);
    }

    public async Task AddAsync(StudentProfile profile)
    {
        _db.StudentProfiles.Add(profile);
        await _db.SaveChangesAsync();
    }

    // Saves the changes made to a profile that was loaded with GetByUserIdAsync.
    public Task SaveAsync()
    {
        return _db.SaveChangesAsync();
    }
}
