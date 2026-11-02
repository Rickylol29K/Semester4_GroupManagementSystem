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

    // Marks a new profile to be added. It is stored when SaveAsync is called.
    public void Add(StudentProfile profile)
    {
        _db.StudentProfiles.Add(profile);
    }

    public Task SaveAsync()
    {
        return _db.SaveChangesAsync();
    }
}
