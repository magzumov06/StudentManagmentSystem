using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IcourseServices
{
    bool AddCourse(Course course);
    bool UpdateCourse(int Course_id,Course course);
    bool DeleteCourse(int Course_id);
    List<Course> GetCourseByStudentId(int StudentId);
    void GetCourseCountByStudent();
}

