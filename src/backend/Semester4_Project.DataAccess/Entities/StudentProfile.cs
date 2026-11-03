//StudentProfiles table

namespace Semester4_Project.DataAccess.Entities;

public class StudentProfile
{
    public int Id { get; set; }

    //the student this belongs to
    public int UserId { get; set; }

    public string FullName { get; set; } = "";

    //comma separated lists
    public string Skills { get; set; } = "";
    public string Availability { get; set; } = "";

    public string Interests { get; set; } = "";
    public string Strengths { get; set; } = "";
    public string Weaknesses { get; set; } = "";
    public string LearningGoals { get; set; } = "";
    public string WorkingPreferences { get; set; } = "";
}
