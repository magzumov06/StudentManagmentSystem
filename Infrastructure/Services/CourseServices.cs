using Domain.Models;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class CourseServices:IcourseServices
{
    private readonly string connectionString ="Server=localhost;Database=StudentManagmenSystem_DB;Username=postgres;Password=12345";
    public bool AddCourse(Course course)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            Insert into courses(title, description)
            values(@title, @description);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("title",course.Title);
            command.Parameters.AddWithValue("description",course.Description);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Course added");
                return true;
            }

            Console.WriteLine("Course not added");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool UpdateCourse(int Course_id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            update courses
            set title=@title,description=@description 
            where id=@id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", Course_id);
            
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Course updated");
                return true;
            }
            Console.WriteLine("Course not updated");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteCourse(int Course_id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = "select id,title,Description from courses where id=@id;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id", Course_id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result>0)
            {
                Console.WriteLine(" Course deleted");
                return true;
            }
            Console.WriteLine("Course not deleted");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public Course GetCourseByStudentId(int StudentId)
    {
        throw new NotImplementedException();
    }
}