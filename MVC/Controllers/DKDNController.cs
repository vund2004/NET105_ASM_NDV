using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC.Controllers
{
    public class DKDNController : Controller
    {
       
        private readonly CompanyContext _context;

        public DKDNController(CompanyContext context)
        {
            _context = context;
        }
        public IActionResult Login()
        {

            if (HttpContext.Session.GetString("UserName") == null)
            {
                return View();
            }
            // Trả về một trang khác hoặc thực hiện hành động khác nếu người dùng đã đăng nhập
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult Login(User user)
        {
            if (HttpContext.Session.GetString("Email") == null)
            {
                var u = _context.Users.FirstOrDefault(x => x.Email.Equals(user.Email) && x.Password.Equals(user.Password));
                if (u != null)
                {
                    HttpContext.Session.SetString("HoTen", u.Fullname.ToString());
                    HttpContext.Session.SetString("Sdt", u.Phone.ToString());
                    HttpContext.Session.SetString("Role", u.Role.ToString());
                    HttpContext.Session.SetString("Email", u.Email.ToString());
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.LoginFail = "Đăng nhập thất bại, vui lòng kiểm tra lại !";
                }
            }
            return View();
        }

        public ActionResult Dangky()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Dangky(UserVM userVM)
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
            return RedirectToAction("Login");

        }
        public IActionResult Logout()
        {

            HttpContext.Session.Clear();
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Index", "Home");
        }
    }
}
