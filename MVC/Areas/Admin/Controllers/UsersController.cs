using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET104_ASM.Models.Authentication;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.Areas.Admin.Controllers
{
    [Authentication]

    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly CompanyContext _context;

        public UsersController(CompanyContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var users = await _context.Users.OrderByDescending(p => p.UserId).ToListAsync();
            return View(users);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            var user = new User
            {
                Username = userVM.Username,
                Password = userVM.Password,
                ConfirmPassword = userVM.ConfirmPassword,
                Email = userVM.Email,
                Fullname = userVM.Fullname,
                Phone = userVM.Phone,
                Address = userVM.Address,
                Role = userVM.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var userVM = new UserVM
            {
                Username = user.Username,
                Password = user.Password,
                ConfirmPassword = user.ConfirmPassword,
                Email = user.Email,
                Fullname = user.Fullname,
                Phone = user.Phone,
                Address = user.Address,
                Role = user.Role
            };
            ViewData["UserId"] = user.UserId;

            return View(userVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, UserVM userVM)
        {
            if (!ModelState.IsValid)
            {
                return View(userVM);
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            user.Username = userVM.Username;
            user.Password = userVM.Password;
            user.ConfirmPassword = userVM.ConfirmPassword;
            user.Email = userVM.Email;
            user.Fullname = userVM.Fullname;
            user.Phone = userVM.Phone;
            user.Address = userVM.Address;
            user.Role = userVM.Role;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Details(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            return View(user);
        }

        public IActionResult Delete(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                // Optionally log that the user was not found
                return RedirectToAction("Index");
            }

            try
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log the exception
                // Optionally handle the error (e.g., return an error view or message)
                // Consider redirecting to an error page or showing a user-friendly message
                return RedirectToAction("Error"); // Example of redirecting to an error page
            }

            return RedirectToAction("Index");
        }


        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return RedirectToAction(nameof(Index));
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
