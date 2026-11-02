// A student's profile as the rest of the app sees it. Skills and availability are real lists here.

namespace Semester4_Project.Business;

public class ProfileInfo
{
    public string FullName { get; set; } = "";
    public List<string> Skills { get; set; } = new();
    public string Interests { get; set; } = "";
    public string Strengths { get; set; } = "";
    public string Weaknesses { get; set; } = "";
    public string LearningGoals { get; set; } = "";
    public string WorkingPreferences { get; set; } = "";

    // The days the student can work on the project, e.g. ["Monday", "Thursday"].
    public List<string> Availability { get; set; } = new();
}
