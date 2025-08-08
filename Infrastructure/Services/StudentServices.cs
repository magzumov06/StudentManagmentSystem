using Domain.Models;
using Npgsql;
using Infrastructure.Interfaces;

namespace Infrastructure.Services;

public class StudentServices : IStudentServices
{
    private readonly string connectionString =
        "Server=localhost;Database=StudentManagmentSystem_DB;Username=postgres;Password=12345";

    public bool AddStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            insert into students(firstname,lastname,birthdate,address) 
            values(@firstname,@lastname,@birthdate,@address);
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("firstname", student.FirstName);
            command.Parameters.AddWithValue("lastname", student.LastName);
            command.Parameters.AddWithValue("birthdate", student.BirthDate);
            command.Parameters.AddWithValue("address", student.Address);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Added successfully");
                return true;
            }

            Console.WriteLine("Student could not be added");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool UpdateStudent(Student student)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            update students
            set firstname=@firstname,lastname=@lastname,birthdate=@birthdate,address=@address
             where id=@id;
";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("@id", student.Id);
            command.Parameters.AddWithValue("@firstname", student.FirstName);
            command.Parameters.AddWithValue("@lastname", student.LastName);
            command.Parameters.AddWithValue("@birthdate", student.BirthDate);
            command.Parameters.AddWithValue("@address", student.Address);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Updated successfully");
                return true;
            }

            Console.WriteLine("Student could not be updated");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public bool DeleteStudent(int id)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"delete from students where id=@id";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("id", id);
            var result = command.ExecuteNonQuery();
            connection.Close();
            if (result > 0)
            {
                Console.WriteLine("Student Deleted successfully");
                return true;
            }

            Console.WriteLine("Student could not be deleted");
            return false;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public List<Student> GetAllStudents()
    {
        try
        {
            var students = new List<Student>();
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"select id, firstname, lastname, birthdate, address from students order by id;";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var student = new Student
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    BirthDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthdate")))
                };
                students.Add(student);
            }

            connection.Close();
            if (students.Count > 0)
            {
                Console.WriteLine($"{students.Count} students found");
                return students;
            }

            Console.WriteLine("students not found");
            return null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public List<Student> GetStudentsByName(string name)
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"select * from students where firstname like @name;";
            using var command = new NpgsqlCommand(query, connection);
            command.Parameters.AddWithValue("name", $"%{name}%");
            using var reader = command.ExecuteReader();
            var students = new List<Student>();
            while (reader.Read())
            {
                var student = new Student()
                {
                    Id = reader.GetInt32(reader.GetOrdinal("id")),
                    FirstName = reader.GetString(reader.GetOrdinal("firstname")),
                    LastName = reader.GetString(reader.GetOrdinal("lastname")),
                    Address = reader.GetString(reader.GetOrdinal("address")),
                    BirthDate = DateOnly.FromDateTime(reader.GetDateTime(reader.GetOrdinal("birthdate")))
                };
                students.Add(student);
            }
            connection.Close();

            if (students.Count > 0)
            {
                return students;
            }

            Console.WriteLine("students not found");
            return new List<Student>();
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
    public void GetStudentCoursesWithJoin()
    {
        try
        {
            using var connection = new NpgsqlConnection(connectionString);
            connection.Open();
            const string query = @"
            SELECT s.id AS student_id, s.firstname, s.lastname, c.title AS course_title
            FROM students s
            JOIN courses c ON c.student_id = s.id
            ORDER BY s.id;
        ";
            using var command = new NpgsqlCommand(query, connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine($"ID: {reader["student_id"]} ||NAme: {reader["firstname"]} {reader["lastname"]} ||Kurs: {reader["course_title"]}");
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