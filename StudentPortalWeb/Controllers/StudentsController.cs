using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentPortalWeb.Data;
using StudentPortalWeb.Models;
using StudentPortalWeb.Models.Entities;

namespace StudentPortalWeb.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public StudentsController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Add(AddStudentVeiwModel veiwModel)
        {
            var student = new Student
            {
                Name = veiwModel.Name,
                Email = veiwModel.Email,
                Phone = veiwModel.Phone,
                Subscribed = veiwModel.Subscribed,
            };

            await dbContext.Students.AddAsync(student);
            await dbContext.SaveChangesAsync();

            return View();
        }

        [HttpGet]

        public async Task<IActionResult> List()
        {
            var students = await dbContext.Students.ToListAsync();

            return View(students);
        }

        [HttpGet]

        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await dbContext.Students.FindAsync(id);

            return View(student);
        }

        [HttpPost]

        public async Task<IActionResult> Edit(Student veiwModel)
        {
            var student = await dbContext.Students.FindAsync(veiwModel.Id);

            if (student is not null)
            {
                student.Name = veiwModel.Name;
                student.Email = veiwModel.Email;
                student.Phone = veiwModel.Phone;
                student.Subscribed = veiwModel.Subscribed;

                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");
        }

        [HttpPost]

        public async Task<IActionResult> Delete(Student veiwModel)
        {
            var student = await dbContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == veiwModel.Id);
            if (student is not null)
            {
                dbContext.Students.Remove(veiwModel);
                await dbContext.SaveChangesAsync();
            }

            return RedirectToAction("List", "Students");

        }



    }
}
