using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Models
{
    public static class StudentDb
    {
        public static Student Add(Student p, SchoolContext db)
        {
            //Mark the object as inserted
            db.Students.Add(p);

            return p;
        }

        public static async Task<List<Student>> GetStudents(SchoolContext context)
        {
            return await (from s in context.Students
                          select s).ToListAsync();
        }

        public static async Task<Student> GetStudent(SchoolContext context, int id)
        {
            Student? p2 = await context.Students
                                       .Where(s => s.StudentId == id)
                                       .SingleOrDefaultAsync();
            return p2;
        }

        public static void Update(SchoolContext context, Student p)
        {
            //Mark the object as updated
            context.Students.Update(p);

        }

        public static void Delete(SchoolContext context, Student p)
        {
            //Mark the object as deleted
            context.Students.Remove(p);
        }


    }
}
