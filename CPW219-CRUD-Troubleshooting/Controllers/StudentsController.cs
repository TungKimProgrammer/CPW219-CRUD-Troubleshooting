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
                await StudentDb.Add(stu, context);

                ViewData["Message"] = $"{stu.Name} was added succesfully!";

                return View();
            }

            //Show web page with errors
            return View(stu);
        }

        public async Task<IActionResult> Edit(int id)
        {
            //get the product by id
            Student? p = await StudentDb.GetStudent(context, id);

            //show it on web page
            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                await StudentDb.Update(context, p);

                TempData["Message"] = $"{p.Name} was updated successfully!";

                return RedirectToAction("Index");
            }

            //return view with errors
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student? p = await StudentDb.GetStudent(context, id);

            if (p == null)
            {
                return NotFound();
            }

            return View(p);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            //Get Product from database
            Student? p = await StudentDb.GetStudent(context, id);

            if (p != null)
            {
                await StudentDb.Delete(context, p);

                TempData["Message"] = $"{p.Name} was deleted successfully!";

                return RedirectToAction("Index");
            }

            TempData["Message"] = $"This game was already deleted!";

            return RedirectToAction("Index");
        }
    }
}
