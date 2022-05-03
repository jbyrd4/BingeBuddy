using BingeBuddy.Models;
using BingeBuddy.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BingeBuddy.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserProfileRepository _userProfileRepository;

        public UserProfileController(IUserProfileRepository userProfileRepository)
        {
            _userProfileRepository = userProfileRepository;
        }

        // GET: UserProfileController
        public ActionResult Index()
        {
            if (User.IsInRole("True"))
            {
                var userProfiles = _userProfileRepository.GetAll();
                return View(userProfiles);

            }
            else
            {
                return NotFound();
            }
        }

        // GET: UserProfileController/Edit/5
        public ActionResult Edit(int id)
        {
            if (User.IsInRole("True"))
            {
                UserProfile userProfile = _userProfileRepository.GetById(id);
                if (userProfile != null)
                {
                    return View(userProfile);
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

        // POST: UserProfileController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UserProfile userProfile)
        {
            try
            {
                _userProfileRepository.Edit(userProfile);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Edit), new { id });
            }
        }

        // GET: UserProfileController/Delete/5
        public ActionResult Delete(int id)
        {
            if (User.IsInRole("True"))
            {
                UserProfile userProfile = _userProfileRepository.GetById(id);
                if (userProfile != null)
                {
                    return View(userProfile);
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

        // POST: UserProfileController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UserProfile userProfile)
        {
            try
            {
                _userProfileRepository.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(userProfile);
            }
        }
    }
}