using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Home()
        {
            return View();
        }
    }
}
