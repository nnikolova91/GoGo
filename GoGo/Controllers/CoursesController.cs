using GoGo.Models;
using GoGo.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ViewModels;
using X.PagedList;

namespace GoGo.Controllers
{
    public class CoursesController : Controller
    {
        private ICoursesService coursesService;
        private UserManager<GoUser> userManager;
        private SignInManager<GoUser> signInManager;

        public CoursesController(ICoursesService coursesService, UserManager<GoUser> userManager, SignInManager<GoUser> signInManager)
        {
            this.coursesService = coursesService;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View();
            }
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.coursesService.AddCourse(model, user);

            return Redirect("/Courses/All");
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var dest = this.coursesService.FindCourse(id);

            return View(dest);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditCourseViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.coursesService.EditCourse(model, user);

            return Redirect($"/Courses/Details/{model.Id}");
        }

        [HttpGet]
        public IActionResult Delete(string id)
        {
            var curse = this.coursesService.FindCourseForDelete(id);

            return View(curse);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id, DeleteCourseViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.coursesService.DeleteCourse(id, user);

            return Redirect($"/Courses/All");
        }

        public IActionResult All(int? page)
        {
            var courses = this.coursesService.GetAllCourses();

            var nextPage = page ?? 1;
            var pageViewModels = courses.ToPagedList(nextPage, 2);

            return View(pageViewModels);
        }

        public async Task<IActionResult> Details(int? page, string id)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var course = this.coursesService.GetDetails(page, id);

            if (course == null)
            {
                ViewData["CurrentUser"] = user.Id;
            }

            return View(course);
        }

        public async Task<IActionResult> My()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var courses = this.coursesService.GetMyCourses(user.Id);

            return View(courses);
        }

        public async Task<IActionResult> SignIn(string id) // id(courseId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.coursesService.AddUserToCourse(id, user);

            return Redirect($"/Courses/Details/{id}");
        }

        [Authorize]
        public async Task<IActionResult> AddResults(string id) // id(courseId)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            var participants = this.coursesService.GetAllParticipants(id, user);

            return View(participants);
        }

        [HttpPost]
        public async Task<IActionResult> CreateResult(UsersResultsViewModel model)
        {
            var user = await userManager.GetUserAsync(HttpContext.User);

            await this.coursesService.AddResultToUsersCourses(model, user);

            return Redirect($"/Courses/AddResults/{model.CourseId}");
        }
    }
}
