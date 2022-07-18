using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.Security.Cryptography;
using System.Security.Claims;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Authorization;

using MovieWebApp.Data;
using MovieWebApp.Models;
using MovieWebApp.Dtos;

/* TO-DO
 * 
 * Доделать авторизацию и регистрацию
 */


namespace MovieWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly MovieWebAppContext _context;
        private readonly ILogger<AccountController> _logger;

        //delete later
        public User user = new User();

        public AccountController(MovieWebAppContext context, ILogger<AccountController> logger)
        {
            _logger = logger;
            _context = context;
        }


        // GET: Account
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        // -----------------------------------------------------



        //GET: Login
        public async Task<IActionResult> Login()
        {
            return View();
        }
        // -----------------------------------------------------


        //Post: Account/Login
        [HttpPost("account/login")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult<string>> Login(UserDTO req)
        {
            if (String.Equals(req.email, _context.Users.ToListAsync()))
            {
                return BadRequest("User not found hahaha " + (string)user.email + "  " + (string)req.email);
            }
            return Ok("Token");
        }
        // -----------------------------------------------------


        // Get: Registration
        public IActionResult Registration()
        {
            return View();
        }
        // -----------------------------------------------------


        // Post: Account/Registration
        [HttpPost("account/Registration")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration([Bind("email, password")] UserDTO req)
        {
            if (ModelState.IsValid)
            {

                CreatePasswordhash(req.password, out byte[] passwordSalt, out byte[] passwordHash);
                user.password = passwordHash;
                user.passwordSalt = passwordSalt;
                user.email = req.email;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }
        // -----------------------------------------------------





        //securing password functions ------------------------------------------------
        private void CreatePasswordhash(string password, out byte[] passwordSalt, out byte[] passwordHash)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(user.passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

        private bool verifyRegistration(User user)
        {
            if (checkEmail(user.email))
                return false;
            return true;
        }

        //checking on distinction
        private bool checkEmail(string email){
            foreach(var user in _context.Users.ToList())
            {
                if(user.email == email) return true;
            }

            return false;
        }

        // TODO: сделать проверку на корректность ввода пароля
        public bool checkPassword()
        {
            return true;
        }

        // ---------------------------------------------------------------------------

    }
}
