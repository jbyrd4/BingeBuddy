using BingeBuddy.Models;
using BingeBuddy.Models.ViewModels;
using BingeBuddy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace BingeBuddy.Controllers
{
    public class UserShowController : Controller
    {
        private readonly IPlatformRepository _platformRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserShowRepository _userShowRepository;
        private readonly IShowRepository _showRepository;

        public UserShowController(IPlatformRepository platformRepository, ICategoryRepository categoryRepository, IUserShowRepository userShowRepository, IShowRepository showRepository)
        {
            _platformRepository = platformRepository;
            _categoryRepository = categoryRepository;
            _userShowRepository = userShowRepository;
            _showRepository = showRepository;
        }

        // GET: UserShowController
        public ActionResult Index()
        {
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                List<UserShow> userShows = _userShowRepository.GetUserShowsByUserProfileId(userId);
                return View(userShows);
            }
            else
            {
                return NotFound();
            }
        }
        public ActionResult FinishedIndex(int categoryId)
        {
            categoryId = 2;
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                List<UserShow> userShows = _userShowRepository.GetUserShowsByCategoryId(userId, categoryId);
                return View(userShows);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult LostInterestIndex(int categoryId)
        {
            categoryId = 4;
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                List<UserShow> userShows = _userShowRepository.GetUserShowsByCategoryId(userId, categoryId);
                return View(userShows);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult CaughtUpIndex(int categoryId)
        {
            categoryId = 3;
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                List<UserShow> userShows = _userShowRepository.GetUserShowsByCategoryId(userId, categoryId);
                return View(userShows);
            }
            else
            {
                return NotFound();
            }
        }

        public ActionResult PotentialIndex(int categoryId)
        {
            categoryId = 5;
            if (GetCurrentUserId() > 0)
            {
                int userId = GetCurrentUserId();
                List<UserShow> userShows = _userShowRepository.GetUserShowsByCategoryId(userId, categoryId);
                return View(userShows);
            }
            else
            {
                return NotFound();
            }
        }

        // GET: UserShowController/Create
        public ActionResult Create()
        {
            var vm = new UserShowViewModel();
            vm.CategoryOptions = _categoryRepository.GetAllCategories();
            vm.PlatformOptions = _platformRepository.GetAllPlatforms();
            vm.ShowOptions = _showRepository.GetAllShows();
            return View(vm);
        }

        // POST: UserShowController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserShowViewModel vm)
        {
            try
            {
                vm.userShow.DateUpdated = System.DateTime.Now;
                vm.userShow.UserId = GetCurrentUserId();

                _userShowRepository.Add(vm.userShow);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserShowController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserShowController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: UserShowController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserShowController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private int GetCurrentUserId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (id == null)
            {
                return 0;
            }
            else
            {
                return int.Parse(id);
            }
        }
    }
}
