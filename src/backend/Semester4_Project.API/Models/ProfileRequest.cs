// The data the frontend sends to save a student profile.
// The [MaxLength] rules are checked automatically, so nobody can send huge texts.

using System.ComponentModel.DataAnnotations;

namespace Semester4_Project.API.Models;

public class ProfileRequest
{
    [MaxLength(100)]
    public string FullName { get; set; } = "";

    [MaxLength(20)]
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

    [MaxLength(7)]
    public List<string> Availability { get; set; } = new();
}
