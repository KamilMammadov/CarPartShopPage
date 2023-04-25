using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Extension;
using TechStore.Models;

namespace TechStore.Areas.Manage.Controllers
{
    [Area("Manage")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TeamController : Controller
    {
        private readonly AppDbContext _appDbContext;
        private readonly IWebHostEnvironment _env;

        public TeamController(AppDbContext appDbContext, IWebHostEnvironment env)
        {
            _appDbContext = appDbContext;
            _env = env;

        }
        public async Task<IActionResult> Index(int page = 1)
        {
            IEnumerable<Team> teams = await _appDbContext.Teams.Where(b => b.IsDeleted == false).ToListAsync();
            ViewBag.PageIndex = page;
            ViewBag.PageCount = Math.Ceiling((double)teams.Count() / 5);
            return View(teams.Skip((page - 1) * 5).Take(5));
        }
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Team team, int page = 1)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (string.IsNullOrWhiteSpace(team.FullName))
            {
                ModelState.AddModelError("FullName", "can not be empty");
                return View();
            }

            if (string.IsNullOrWhiteSpace(team.Position))
            {
                ModelState.AddModelError("Position", "can not be empty");
                return View();
            }
            if (team.File == null)
            {
                ModelState.AddModelError("File", "image is required");
                return View();
            }
            if (team.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (team.File.Length > 20000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            team.Image = team.File.CreateFile(_env, "assets", "images", "ourTeam");

            team.CreatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.Teams.AddAsync(team);
            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return BadRequest("id can not be null");
            }
            Team team = await _appDbContext.Teams.FirstOrDefaultAsync(b => b.IsDeleted == false && b.Id == id);
            if (team == null)
            {
                return NotFound("could not found");
            }
            return View(team);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Team team, int? id, int page = 1)
        {
            Team dbTeam = await _appDbContext.Teams.FirstOrDefaultAsync(b => b.Id == id && b.IsDeleted == false);

            if (dbTeam == null) return NotFound();

            if (!ModelState.IsValid)
            {
                return View(dbTeam);
            }
            if (team.Id != id)
            {
                return NotFound("team not found");
            }

            if (string.IsNullOrWhiteSpace(team.FullName))
            {
                ModelState.AddModelError("FullName", "can not submit white space");
                return View(dbTeam);
            }
            if (string.IsNullOrWhiteSpace(team.Position))
            {
                ModelState.AddModelError("Position", "can not submit white space");
                return View(dbTeam);
            }
            if (team.File == null)
            {
                ModelState.AddModelError("File", "Sekil Mutleq Olmalidi");
                return View(dbTeam);
            }
            if (team.File.ContentType != "image/jpeg")
            {
                ModelState.AddModelError("File", "File type should be jpeg");
                return View();
            }
            if (team.File.Length > 20000)
            {
                ModelState.AddModelError("File", "File length should be less than 20k");
                return View();
            }
            if (team.File != null)
            {

                if (dbTeam.Image != null)
                {
                    Helper.DeleteFile(_env, dbTeam.Image, "assets", "images", "ourTeam");
                }

                dbTeam.Image = team.File.CreateFile(_env, "assets", "images", "ourTeam");
            }

            dbTeam.FullName = team.FullName.Trim().ToLower();
            dbTeam.Position = team.Position.Trim().ToLower();
            dbTeam.UpdatedAt = DateTime.UtcNow.AddHours(4);

            await _appDbContext.SaveChangesAsync();

            return RedirectToAction("Index", new { page });
        }
        public async Task<IActionResult> Delete(int? id, int page = 1)
        {
            if (id == null) return BadRequest("id can not be null");

            Team dbTeam = await _appDbContext.Teams
                .FirstOrDefaultAsync(c => c.IsDeleted == false && c.Id == id);

            if (dbTeam == null) return NotFound();

            IEnumerable<Team> teams = await _appDbContext.Teams.Where(t => t.IsDeleted == false).ToListAsync();

            if (teams.Count() > 6)
            {
            dbTeam.IsDeleted = true;
            dbTeam.DeletedAt = DateTime.UtcNow.AddHours(4);
            await _appDbContext.SaveChangesAsync();

            }

            return RedirectToAction("Index", new { page });
        }
    }
}
