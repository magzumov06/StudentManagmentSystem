using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IcourseServices
{
    bool AddCourse(Course course);
    bool UpdateCourse(int Course_id);
    bool DeleteCourse(int Course_id);
    Course GetCourseByStudentId(int StudentId);
}

