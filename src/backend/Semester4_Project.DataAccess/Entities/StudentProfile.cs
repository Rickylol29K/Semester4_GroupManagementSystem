// One row in the StudentProfiles table: what a student tells us about themselves,
// so the group generation can find a group that fits them. Each student has at most one.

namespace Semester4_Project.DataAccess.Entities;

public class StudentProfile
{
    public int Id { get; set; }

    // The student this profile belongs to.
    public int UserId { get; set; }

    public string FullName { get; set; } = "";

    // Lists are saved as one comma separated text, e.g. "Back end,Testing".
    public string Skills { get; set; } = "";
    public string Availability { get; set; } = "";

    public string Interests { get; set; } = "";
    public string Strengths { get; set; } = "";
    public string Weaknesses { get; set; } = "";
    public string LearningGoals { get; set; } = "";
    public string WorkingPreferences { get; set; } = "";
}
