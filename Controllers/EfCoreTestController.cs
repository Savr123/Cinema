using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using System.Diagnostics;

using MovieWebApp.Data;
using MovieWebApp.Models;
using MovieWebApp.Dtos;


namespace MovieWebApp.Controllers
{
    public class EfCoreTestController : Controller
    {
        private readonly MovieWebAppContext _context;
        private readonly ILogger _logger;
        public EfCoreTestController(MovieWebAppContext context, ILogger<AccountController> logger)
        {
            _logger = logger;
            _context = context;
        }

        //Get: EfCoreTest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        //----------------------------------------------------------------------

        //GET: Error
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        //----------------------------------------------------------------------
        

        //CRUD Operations
        public async Task<ActionResult<string>> UpdateData()
        {
                User Tom = new User
                {
                    name = "Tom",
                    email = "Tom@gmail.com",
                    password = new byte[] { 1, 2, 3, 4, 5, 6, 7, 21 }
                };
                _context.Users.Add(Tom);
                foreach(var user in _context.Users.ToList().Where(user => user.name == "Tom"))
                {
                    _logger.LogInformation(user.name);
                }
                _context.SaveChanges();
            return Ok("smth");
        }
    }
}
