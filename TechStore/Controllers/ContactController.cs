using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechStore.DAL;
using TechStore.Models;
using TechStore.ViewModels.ContactV;

namespace TechStore.Controllers
{
    public class ContactController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public ContactController(AppDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ContactVM contactVM = new ContactVM
            {
                Contacts = await _context.Contacts.Where(c => c.IsDeleted == false).ToListAsync(),
                LeaveUsReplies = await _context.LeaveUsReplies.ToListAsync()

            };
            return View(contactVM);
        }

        //[HttpPost]
        //public async Task<IActionResult> Index(string Subject, string Message, string Name, string Surname)
        //{
        //    AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    };

        //    if (string.IsNullOrWhiteSpace(Subject))
        //    {
        //        ModelState.AddModelError("Subject", "required");
        //        return View();
        //    }
        //    if (string.IsNullOrWhiteSpace(Message))
        //    {
        //        ModelState.AddModelError("Message", "required");
        //        return View();
        //    }

        //    if (string.IsNullOrWhiteSpace(Name))
        //    {
        //        ModelState.AddModelError("Name", "required");
        //        return View();
        //    }
        //    if (string.IsNullOrWhiteSpace(Surname))
        //    {
        //        ModelState.AddModelError("Surname", "required");
        //        return View();
        //    }
           
        //    LeaveUsReply leaveUsReply = new LeaveUsReply
        //    {
        //        Name = user.Name,
        //        Surname = user.SurName,
        //        Comment = Message,
        //        Subject = Subject
        //    };
        //    _context.LeaveUsReplies.Add(leaveUsReply);
        //    _context.SaveChanges();

        //    IEnumerable<LeaveUsReply> leaveUs = _context.LeaveUsReplies.ToList();
        //    return RedirectToAction("Index");


        //}

    }
}
