using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PersonalChat.Data;
using PersonalChat.Hubs;
using PersonalChat.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalChat.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ChatUser> _userManager;
        private readonly ApplicationDbContext _context;
        private  readonly IHubContext<ChatHub> _hubContext;

        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, 
            UserManager<ChatUser> userManager, IHubContext<ChatHub> hubcontext)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _hubContext = hubcontext;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = await _userManager.GetUserAsync(User);
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.CurrentUserName = currentUser.UserName;
            }
            var messages = await _context.Messages.ToListAsync();
            return View(messages);
        }

        public async Task <IActionResult> Create(Message message)
        {
            if (ModelState.IsValid)
            {
                message.UserName = User.Identity.Name;
                var sender = await _userManager.GetUserAsync(User);
                message.UserId = sender.Id;
                await _context.Messages.AddAsync(message);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return Error();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
