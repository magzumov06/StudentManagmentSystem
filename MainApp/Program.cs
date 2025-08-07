using Domain.Models;
using Infrastructure.Services;

StudentServices service = new StudentServices();
CourseServices courseService = new CourseServices();
while (true)
{
    Console.WriteLine("""
                      1. Иловаи донишҷӯ
                      2. Иловаи курс
                      3. Намоиши донишҷӯён
                      4. Намоиши курсҳои донишҷӯ
                      5. Тағйири донишҷӯ
                      6. Тағйири курс
                      7. Ҳазфи донишҷӯ
                      8. Ҳазфи курс
                      9. Ҷустуҷӯ бо номи донишҷӯ
                      10. Шумораи курсҳо барои ҳар донишҷӯ
                      11. JOIN — донишҷӯ ва курсҳояш
                      0. Хуруҷ
                      """);
    var choice =int.Parse(Console.ReadLine());
    switch (choice)
    {
        case 1:
            Student newStudent = new Student();
            Console.Write("Nomi donishjuro vorid kuned: ");
            newStudent.FirstName = Console.ReadLine();
            Console.Write("Nasab donishjuro vorid kuned: ");
            newStudent.LastName = Console.ReadLine();
            Console.Write("Addresi donishjuro vorid kuned: ");
            newStudent.Address = Console.ReadLine();
            Console.Write("soli tavvaludi donishjuro vorid kuned: ");
            newStudent.BirthDate = DateOnly.Parse(Console.ReadLine());
            service.AddStudent(newStudent);
            break;
        case 2:
            Course newCourse = new Course();
            Console.Write("titli kursro vorid kuned: ");
            newCourse.Title = Console.ReadLine();
            Console.Write("Description-i kursro vorid kuned: ");
            newCourse.Description = Console.ReadLine();
            courseService.AddCourse(newCourse);
            break;
        case 3:
            var res= service.GetAllStudents();
            foreach (var r in res )
            {
                Console.WriteLine("Id " +r.Id+" | Nom: "+r.FirstName+" | Namsab: "+r.LastName+" | Adress: "+r.Address+" | soli tavvalud: "+r.BirthDate);
            }
            break;
        case 5:
            Student newStudent2 = new Student();
            Console.Write("id donishjuro baroi update kardan vorid kuned: ");
            newStudent2.Id = int.Parse(Console.ReadLine());
            Console.Write("Nomi donishjuro baroi update kardan vorid kuned: ");
            newStudent2.FirstName = Console.ReadLine();
            Console.Write("Nasab donishjuro baroi update kardan vorid kuned: ");
            newStudent2.LastName = Console.ReadLine();
            Console.Write("Addresi donishjuro baroi update kardan vorid kuned: ");
            newStudent2.Address = Console.ReadLine();
            Console.Write("soli tavvaludi donishjuro baroi update kardan vorid kuned:");
            newStudent2.BirthDate = DateOnly.Parse(Console.ReadLine());
            service.UpdateStudent(newStudent2);
            break;
        case 6:
            var id=int.Parse(Console.ReadLine());
            courseService.UpdateCourse(id);
            break;
        case 7:
            Console.Write("Id donishjuro baroi nest kardan vorid kuned: ");
            var Id= int.Parse(Console.ReadLine());
            service.DeleteStudent(Id);
            break;
        case 8:
            Console.Write("Id kursro baroi nest kardan vorid kuned: ");
            var Id1= int.Parse(Console.ReadLine());
            courseService.DeleteCourse(Id1);
            break;
        case 0:
            return;
            break;
    }
}