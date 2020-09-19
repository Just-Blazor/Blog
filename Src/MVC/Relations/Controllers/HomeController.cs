using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Relations.Data;
using Relations.Data.Entities;
using Relations.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Relations.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }



        public async Task<IActionResult> Index()
        {
            return View(await _context.Universities.Include(t => t.Groups).ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(University model)
        {
            if (ModelState.IsValid)
            {
                string path = string.Empty;
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            University university = await _context.Universities.FindAsync(id);
            if (university is null)
            {
                return NotFound();
            }

            return View(university);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(University model)
        {
            if (ModelState.IsValid)
            {
                _context.Update(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(model);
        }


        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            University university = await _context.Universities
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (university is null)
            {
                return NotFound();
            }

            return View(university);
        }


        [ActionName("Delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            University university = await _context.Universities
                .Include(g => g.Groups)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (university is null)
            {
                return NotFound();
            }

            _context.Universities.Remove(university);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            University university = await _context.Universities
                .Include(t => t.Groups)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (university is null)
            {
                return NotFound();
            }

            return View(university);
        }

        public async Task<IActionResult> AddCourse(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            University university = await _context.Universities.FindAsync(id);
            if (university is null)
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
        public async Task<IActionResult> AddCourse(CourseViewModel model)
        {
            if (ModelState.IsValid)
            {
                bool isnew = true;

                CourseViewModel courseView = new CourseViewModel
                {
                    Id = isnew ? 0 : model.Id, // TODO:
                    Name = model.Name,
                    University = await _context.Universities.FindAsync(model.UniversityId)
                };
                _context.Add(courseView);
                await _context.SaveChangesAsync();

                return RedirectToAction("Details", "Home", new
                {
                    id = model.Id
                });
            }

            return View(model);
        }

        public async Task<IActionResult> EditCourse(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (course is null)
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

                return RedirectToAction("Details", "Home", new 
                { 
                    id = university.Id 
                });
            }

            return View(model);
        }

        public async Task<IActionResult> DeleteCourse(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (course is null)
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



        [ActionName("DeleteCourse")]
        [HttpPost]
        public async Task<IActionResult> DeleteCourseConfirmed(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (course is null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();


            return RedirectToAction("Details", "Home", new { course.University.Id });
        }

        public async Task<IActionResult> DetailsCourse(int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            Course course = await _context.Courses
                .Include(g => g.University)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (course is null)
            {
                return NotFound();
            }

            return View(course);
        }





        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel 
            { 
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier 
            });
        }
    }
}
