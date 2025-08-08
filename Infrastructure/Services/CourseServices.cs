using Domain.Models;
using Infrastructure.Helper;
using Infrastructure.Interfaces;
using Npgsql;
namespace Infrastructure.Services;

public class CourseServices:IcourseServices
{
    private readonly string connectionString = DataContextHelper.GetConnectionString();
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

    public bool UpdateCourse(int course_Id,Course course)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string courseQuery = @"
            Select * from courses where id = @id;
            "; 
            using var command1 = new NpgsqlCommand(courseQuery, connection);
            command1.Parameters.AddWithValue("@id", course_Id);
            var res1 =  command1.ExecuteReader();
            if (!res1.Read())
            {
                throw new Exception("Course not found");
            }
            const string query = @"
            update courses
            set title=@title,description=@description 
            where id=@id;
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", course_Id);
            command.Parameters.AddWithValue("@title", course.Title);
            command.Parameters.AddWithValue("@description", course.Description);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Course updated successfully");
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

    public List<Course> GetCourseByStudentId(int StudentId)
    {
        try
        {
            using var connection = new  NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            select c.Id,c.Title,c.Description 
            from  courses as c 
            where c.student_id = @student_id;
            ";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("student_Id", StudentId);
            using var reader = command.ExecuteReader();
            var courses = new List<Course>();
            while (reader.Read())
            {
                var course = new Course()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("Id")),
                    Title = reader.GetString(reader.GetOrdinal("Title")),
                    Description = reader.GetString(reader.GetOrdinal("Description")),
                };
                courses.Add(course);
            }
            connection.Close();
            return courses;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public void GetCourseCountByStudent()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            SELECT s.id, s.firstname, s.lastname, COUNT(c.student_id) AS course_count
            FROM students as s
            JOIN courses as c 
            ON c.student_id =s.id
            GROUP BY s.id, s.firstname, s.lastname
            ORDER BY course_count;
        ";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["id"]} ||Name: {reader["firstname"]} {reader["lastname"]} | Shumorai kursho: {reader["course_count"]}");
            }
            connection.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}