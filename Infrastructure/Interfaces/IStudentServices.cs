using Domain.Models;

namespace Infrastructure.Interfaces;

public interface IStudentServices
{
    bool AddStudent(Student student);
    bool UpdateStudent(Student student);
    bool DeleteStudent(int id);
    List<Student> GetAllStudents();
    List<Student> GetStudentsByName(string name);
    void GetStudentCoursesWithJoin();
}