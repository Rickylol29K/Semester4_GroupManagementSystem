//get and save student profiles

using Semester4_Project.Business.Models;
using Semester4_Project.DataAccess.Entities;
using Semester4_Project.DataAccess.Repositories;

namespace Semester4_Project.Business.Services;

public class ProfileService
{
    private readonly ProfileRepository _profiles;

    public ProfileService(ProfileRepository profiles)
    {
        _profiles = profiles;
    }

    public async Task<ProfileInfo> GetAsync(int userId)
    {
        var profile = await _profiles.GetByUserIdAsync(userId);
        if (profile == null)
        {
            return new ProfileInfo();
        }

        return ToProfileInfo(profile);
    }

    //creates the profile the first time, after that just updates it
    public async Task<ProfileInfo> SaveAsync(int userId, ProfileInfo info)
    {
        var profile = await _profiles.GetByUserIdAsync(userId);
        if (profile == null)
        {
            profile = new StudentProfile { UserId = userId };
            _profiles.Add(profile);
        }

        profile.FullName = info.FullName.Trim();
        profile.Skills = JoinList(info.Skills);
        profile.Interests = info.Interests.Trim();
        profile.Strengths = info.Strengths.Trim();
        profile.Weaknesses = info.Weaknesses.Trim();
        profile.LearningGoals = info.LearningGoals.Trim();
        profile.WorkingPreferences = info.WorkingPreferences.Trim();
        profile.Availability = JoinList(info.Availability);

        await _profiles.SaveAsync();

        return ToProfileInfo(profile);
    }

    private static ProfileInfo ToProfileInfo(StudentProfile profile)
    {
        return new ProfileInfo
        {
            FullName = profile.FullName,
            Skills = SplitList(profile.Skills),
            Interests = profile.Interests,
            Strengths = profile.Strengths,
            Weaknesses = profile.Weaknesses,
            LearningGoals = profile.LearningGoals,
            WorkingPreferences = profile.WorkingPreferences,
            Availability = SplitList(profile.Availability),
        };
    }

    //lists are saved as one text
    private static string JoinList(List<string> items)
    {
        return string.Join(",", items);
    }

    private static List<string> SplitList(string text)
    {
        return text.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList();
    }
}
