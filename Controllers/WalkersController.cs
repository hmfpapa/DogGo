using DogGo.Models;
using DogGo.Models.ViewModels;
using DogGo.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;

namespace DogGo.Controllers
{
    public class WalkersController : Controller
    {
        // GET: WalkersController
        public ActionResult Index()
        {
            int userId = GetCurrentUserId();
            if (userId != 0)
            {
                Owner currentUser = _ownerRepo.GetOwnerById(userId);
                List<Walker> walkers = _walkerRepo.GetWalkersInNeighborhood(currentUser.NeighborhoodId);
                return View(walkers);
            }
            else
            {
                List<Walker> allWalkers = _walkerRepo.GetAllWalkers();
                return View(allWalkers);
            }
        }
         
        // GET: WalkersController/Details/5
        public ActionResult Details(int id)
        {
            Walker walker = _walkerRepo.GetWalkerById(id);
            List<Walks> walks = _walkRepo.GetWalksByWalkerId(id);

            WalkerProfileViewModel wm = new WalkerProfileViewModel()
            {
                Walker = walker,
                Walks = walks
            };

            return View(wm);
        }

        // GET: WalkersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WalkersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: WalkersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WalkersController/Edit/5
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

        // GET: WalkersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WalkersController/Delete/5
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

        private readonly IOwnerRepository _ownerRepo;
        private readonly IWalkerRepository _walkerRepo;
        private readonly IWalkRepository _walkRepo;
        private readonly INeighborhoodRepository _neighborhoodRepo;

        public WalkersController(
    IOwnerRepository ownerRepository,
    IWalkerRepository walkerRepository,
    IWalkRepository walkRepo,
    INeighborhoodRepository neighborhoodRepository
    )
        {
            _ownerRepo = ownerRepository;
            _walkerRepo = walkerRepository;
            _walkRepo = walkRepo;
            _neighborhoodRepo = neighborhoodRepository;
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
