using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext context;

        public StudentsController(SchoolContext dbContext)
        {
            context = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            List<Student> products = await StudentDb.GetStudents(context);
            return View(products);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Student stu)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                //Mark the object as inserted
                StudentDb.Add(stu, context);

                //Send insert query to database
                await context.SaveChangesAsync();

                ViewData["Message"] = $"{stu.Name} was added succesfully!";

                return View();
            }

            //Show web page with errors
            return View(stu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student? studentToUpdate = await StudentDb.GetStudent(context, id);

            //show it on web page
            if (studentToUpdate == null)
            {
                return NotFound();
            }

            return View(studentToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student studentToUpdate)
        {
            if (ModelState.IsValid)
            {
                //Mark the object as updated
                StudentDb.Update(context, studentToUpdate);

                //Send update query to database
                await context.SaveChangesAsync();

                TempData["Message"] = $"{studentToUpdate.Name} was updated successfully!";

                return RedirectToAction("Index");
            }

            //return view with errors
            return View(studentToUpdate);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student? studentToDelete = await StudentDb.GetStudent(context, id);

            if (studentToDelete == null)
            {
                return NotFound();
            }

            return View(studentToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Get Product from database
            Student? studentToDelete = await StudentDb.GetStudent(context, id);

            if (studentToDelete != null)
            {
                //Mark the object as deleted
                StudentDb.Delete(context, studentToDelete);

                //Send delete query to database
                await context.SaveChangesAsync();

                TempData["Message"] = $"{studentToDelete.Name} was deleted successfully!";

                return RedirectToAction("Index");
            }

            TempData["Message"] = $"This game was already deleted!";

            return RedirectToAction("Index");
        }
    }
}
