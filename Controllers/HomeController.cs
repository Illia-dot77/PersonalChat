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
    /// <summary>
    /// Main controller class in this application
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<ChatUser> _userManager;
        private readonly ApplicationDbContext _context;
        private  readonly IHubContext<ChatHub> _hubContext;

        /// <summary>
        /// Default constructor of HomeController
        /// </summary>
        /// <param name="context">Parametr to setup database context in this class with dependency injection
        /// </param>
        /// <param name="logger">Parametr to setup logger object in this class with dependency injection
        /// </param>
        /// <param name="userManager">Parametr to setup user manager in this class with dependency injection
        /// </param>
        /// <param name="hubcontext">Parametr to setup hub context in this class with dependency injection
        /// </param>
        public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, 
            UserManager<ChatUser> userManager, IHubContext<ChatHub> hubcontext)
        {
            _logger = logger;
            _userManager = userManager;
            _context = context;
            _hubContext = hubcontext;
        }
        /// <summary>
        /// Method that return main page of the application with chat history
        /// </summary>
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
        /// <summary>
        /// Method that catch message object from client-side and save it to the database
        /// </summary>
        /// <param name="message">Message type object that represent message to save
        /// </param>
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
        /// <summary>
        /// Method to send error whe something go wrong
        /// </summary>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
