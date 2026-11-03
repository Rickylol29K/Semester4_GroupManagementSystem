//student profile that goes to and from the frontend

using System.ComponentModel.DataAnnotations;

namespace Semester4_Project.Business.Models;

public class ProfileInfo
{
    [MaxLength(100)]
    public string FullName { get; set; } = "";

    public List<string> Skills { get; set; } = new();

    [MaxLength(1000)]
    public string Interests { get; set; } = "";

    [MaxLength(1000)]
    public string Strengths { get; set; } = "";

    [MaxLength(1000)]
    public string Weaknesses { get; set; } = "";

    [MaxLength(1000)]
    public string LearningGoals { get; set; } = "";

    [MaxLength(1000)]
    public string WorkingPreferences { get; set; } = "";

    //days the student can work
    public List<string> Availability { get; set; } = new();
}
