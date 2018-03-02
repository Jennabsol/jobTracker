using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using jobTracker.Data;
using jobTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace jobTracker.Controllers
{
    public class JobsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        // GET: Jobs
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            var job = _context.Job
                .Where(j => j.User == user)
                .Include(j => j.AppStatus)
                .Include(j => j.Company)
                .Include(j => j.Contact)
                .ToList();

            if (job == null)
            {
                return NotFound();
            }

            return View(await _context.Job.Where(j => j.User == user).ToListAsync());
        }

        // GET: Jobs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.AppStatus)
                .Include(j => j.Company)
                .Include(j => j.Contact)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // GET: Jobs/Create
        public IActionResult Create()
        {
            ViewData["AppStatusId"] = new SelectList(_context.AppStatus, "Id", "AppStatusTitle");
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name");
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "FirstName");
            return View();
        }

        // POST: Jobs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Position,Notes,Active,CompanyId,ContactId,AppStatusId")] Job job)
        {
            ModelState.Remove("User");
            job.User = await GetCurrentUserAsync();
            if (ModelState.IsValid)
            {
                _context.Add(job);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppStatusId"] = new SelectList(_context.AppStatus, "Id", "AppStatusTitle", job.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", job.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "FirstName", job.ContactId);
            return View(job);
        }

        // GET: Jobs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job.SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }
            ViewData["AppStatusId"] = new SelectList(_context.AppStatus, "Id", "AppStatusTitle", job.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", job.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "FirstName", job.ContactId);
            return View(job);
        }

        // POST: Jobs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Position,Notes,Active,CompanyId,ContactId,AppStatusId")] Job job)
        {
            if (id != job.Id)
            {
                return NotFound();
            }
            ModelState.Remove("User");
            job.User = await GetCurrentUserAsync();
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(job);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobExists(job.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppStatusId"] = new SelectList(_context.AppStatus, "Id", "AppStatusTitle", job.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.Company, "Id", "Name", job.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.Contact, "Id", "FirstName", job.ContactId);
            return View(job);
        }

        // GET: Jobs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var job = await _context.Job
                .Include(j => j.AppStatus)
                .Include(j => j.Company)
                .Include(j => j.Contact)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (job == null)
            {
                return NotFound();
            }

            return View(job);
        }

        // POST: Jobs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var job = await _context.Job.SingleOrDefaultAsync(m => m.Id == id);
            _context.Job.Remove(job);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobExists(int id)
        {
            return _context.Job.Any(e => e.Id == id);
        }
    }
}
