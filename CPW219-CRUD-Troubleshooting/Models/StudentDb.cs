using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        public static Student Add(Student stu, SchoolContext context)
        {
            //Mark the object as inserted
            context.Students.Add(stu);

            return stu;
        }

        public static async Task<List<Student>> GetStudents(SchoolContext context)
        {
            return await (from s in context.Students
                          select s).ToListAsync();
        }

        public static async Task<Student> GetStudent(SchoolContext context, int id)
        {
            Student? stu = await context.Students
                                       .Where(s => s.StudentId == id)
                                       .SingleOrDefaultAsync();
            return stu;
        }

        public static void Update(SchoolContext context, Student stu)
        {
            //Mark the object as updated
            context.Students.Update(stu);

        }

        public static void Delete(SchoolContext context, Student stu)
        {
            //Mark the object as deleted
            context.Students.Remove(stu);
        }


    }
}
