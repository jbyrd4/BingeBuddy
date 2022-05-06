using BingeBuddy.Models;
using BingeBuddy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using X.PagedList.Mvc;
using X.PagedList;

namespace BingeBuddy.Controllers
{
    public class ShowController : Controller
    {

        private readonly IShowRepository _showRepository;
        public ShowController(IShowRepository showRepository)
        {
            _showRepository = showRepository;
        }

        // GET: ShowController
        public ActionResult Index(int? i)
        {
            var shows = _showRepository.GetAllShows();
            return View(shows.ToPagedList(i ?? 1, 10));
        }

        // GET: Gets approved shows
        public ActionResult ApprovedShowsIndex(int? i, bool approved = true)
        {
            var shows = _showRepository.GetShowsByApproved(approved);
            return View(shows.ToPagedList(i ?? 1, 10));
        }

        // GET: Gets unapproved shows
        public ActionResult UnapprovedShowsIndex(int? i, bool approved = false)
        {
            var shows = _showRepository.GetShowsByApproved(approved);
            return View(shows.ToPagedList(i ?? 1, 10));
        }

        // GET: ShowController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShowController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Show show)
        {
            try
            {
                _showRepository.Add(show);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ShowController/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("True"))
            {
                Show show = _showRepository.GetShowById(id);
                if (show != null)
                {
                    return View(show);
                }
                else
                {
                    return NotFound();
                }
            }
            else
            {
                return NotFound();
            }
        }

        // POST: ShowController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Show show)
        {
            try
            {
                _showRepository.Update(show);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(show);
            }
        }

        // GET: ShowController/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("True"))
            {
                Show show = _showRepository.GetShowById(id);
                if (show != null)
                {
                    return View(show);
                }
                else
                {
                    return NotFound();
                }

            }
            else
            {
                return NotFound();
            }
        }

        // POST: ShowController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Show show)
        {
            try
            {
                _showRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(show);
            }
        }
    }
}
