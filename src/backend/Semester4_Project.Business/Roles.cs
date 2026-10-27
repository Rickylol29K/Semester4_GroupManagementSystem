namespace Semester4_Project.Business;

public static class Roles
{
    public const string Student = "Student";
    public const string Teacher = "Teacher";

    public static bool IsValid(string role)
    {
        return role == Student || role == Teacher;
    }
}
