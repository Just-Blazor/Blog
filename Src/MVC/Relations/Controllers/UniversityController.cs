using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Relations.Data;
using Relations.Data.Entities;

namespace Relations.Controllers
{
    public class CourseViewModel : Course
    {
        public int UniversityId { get; set; }
    }

    public class UniversityController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UniversityController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _context
                .Universities
                .Include(t => t.Groups)
                .ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(University model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;


                University university = new University
                {
                    Title = model.Title,
                    Groups = model.Groups,
                    Location = model.Location,
                    Code = model.Code,
                    Id = false ? 0 : model.Id,
                };
                _context.Add(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University university = await _context.Universities.FindAsync(id);
            if (university is null)
            {
                return NotFound();
            }

            University model = new University
            {

                Title = university.Title,
                Groups = university.Groups,
                Location = university.Location,
                Code = university.Code,
                Id = false ? 0 : university.Id,
            }; 
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(University model)
        {
            if (ModelState.IsValid)
            {
                University university = new University
                {
                    Title = model.Title,
                    Groups = model.Groups,
                    Location = model.Location,
                    Code = model.Code,
                    Id = false ? 0 : model.Id,
                };

                _context.Update(university);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University university = await _context.Universities
                .Include(t => t.Groups)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }



        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University university = await _context.Universities.FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University university = await _context.Universities
                .Include(t => t.Groups)

                .FirstOrDefaultAsync(m => m.Id == id);
            if (university == null)
            {
                return NotFound();
            }

            return View(university);
        }

        public async Task<IActionResult> AddCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            University university = await _context.Universities.FindAsync(id);
            if (university == null)
            {
                return NotFound();
            }

            CourseViewModel model = new CourseViewModel
            {
                Id = university.Id,
                Name = university.Title,
                University = university,
                UniversityId = university.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                CourseViewModel courseView = new CourseViewModel
                {
                    Id = true ? 0 : model.Id,
                    Name = model.Name,
                    University = await _context.Universities.FindAsync(model.UniversityId)
                };// await _converterHelper.ToGroupEntityAsync(model, true);
                _context.Add(courseView);
                await _context.SaveChangesAsync();
                return RedirectToAction("Details", "University", new { id = model.Id }); 
            }

            return View(model);
        }

        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (course == null)
            {
                return NotFound();
            }

            CourseViewModel model = new CourseViewModel
            {
                Id = course.Id,
                Name = course.Name,
                University = course.University,
                UniversityId = course.University.Id,
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                Course course = new Course
                {
                    Id = false ? 0 : model.Id,
                    Name = model.Name,
                    University = await _context.Universities.FindAsync(model.UniversityId)
                };

                _context.Update(course);
                await _context.SaveChangesAsync();


                University university = await _context.Universities
                .Include(t => t.Groups).FirstOrDefaultAsync(m => m.Id == model.UniversityId);

                return RedirectToAction("Details", "University", new { id = university.Id }); 
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", "University", new { id = course.University.Id }); 
        }

        public async Task<IActionResult> DetailsCourse(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                //.Include(g => g.Name)
                .FirstOrDefaultAsync(g => g.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }


    }
}
