// The two kinds of account in the app: Student and Teacher.

namespace Semester4_Project.Business.Models;

public static class Roles
{
    public const string Student = "Student";
    public const string Teacher = "Teacher";

    public static bool IsValid(string role)
    {
        return role == Student || role == Teacher;
    }
}
