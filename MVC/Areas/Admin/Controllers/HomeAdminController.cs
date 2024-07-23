using API.Data;
using Microsoft.AspNetCore.Mvc;
using NET104_ASM.Models.Authentication;

namespace MVC.Areas.Admin.Controllers
{
    [Authentication]

    [Area("Admin")]
    [Route("Admin")]

    public class HomeAdminController : Controller
    {
        public IActionResult IndexAdmin()
        {
            return View();
        }
    }
}
