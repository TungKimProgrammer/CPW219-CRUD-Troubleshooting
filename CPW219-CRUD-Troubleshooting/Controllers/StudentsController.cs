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
        public async Task<IActionResult> Create(Student p)
        {
            if (ModelState.IsValid)
            {
                // Add to DB
                StudentDb.Add(p, context);          // Prepares insert
                await context.SaveChangesAsync();   // Executes pending statements

                ViewData["Message"] = $"{p.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(p);
        }

        public IActionResult Edit(int id)
        {
            //get the product by id
            Student? p = StudentDb.GetStudent(context, id);

            //show it on web page
            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost]
        public IActionResult Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                StudentDb.Update(context, p);
                //context.SaveChangesAsync();

                TempData["Message"] = $"{p.Name} was updated successfully!";
                return RedirectToAction("Index");
            }
            //return view with errors
            return View(p);
        }

        public IActionResult Delete(int id)
        {
            Student? p = StudentDb.GetStudent(context, id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            //Get Product from database
            Student? p = StudentDb.GetStudent(context, id);

            if (p != null)
            {
                StudentDb.Delete(context, p);

                context.SaveChangesAsync();
                TempData["Message"] = $"{p.Name} was deleted successfully!";

                return RedirectToAction("Index");
            }

            TempData["Message"] = $"This game was already deleted!";
            

            return RedirectToAction("Index");
        }
    }
}
