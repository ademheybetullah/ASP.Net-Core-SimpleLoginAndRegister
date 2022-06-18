using BackendTask.Identity;
using BackendTask.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTask.Controllers
{
    [Authorize(Roles = "admin")]
    public class ReportsController : Controller
    {
        private UserManager<User> _userManager;
        public ReportsController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult UserReports()
        {
            UsersReportModel report = new UsersReportModel();
            var users = _userManager.Users;
            foreach (var item in users)
            {
                if (item.IsOnline)
                    report.OnlineUsers++;
                TimeSpan ts = DateTime.Now - item.RegistrationDate;
                if (ts.Days>=1&&item.EmailConfirmed==false)
                    report.UnconfirmedUsers++;
            }
            return View(report);
        }
        [HttpPost]
        public ActionResult LoginTimeReport(DateTime date)
        {
            LoginReportModel loginReportModel = LoginReportModel.getModel();
            if(date== loginReportModel.SearchedDate && date.Date!=DateTime.Now.Date)
            {
                return View(loginReportModel);
            }
            loginReportModel.SearchedDate = date;
            float loginTime = 0;
            float loginCount = 0;
            var contextOptions = new DbContextOptionsBuilder<IdentityContext>().UseSqlServer(@"data source=(localdb)\MSSQLLocalDB; initial catalog=DBBackendTask; integrated security=true;").Options;
            using (var context = new IdentityContext(contextOptions))
            {

                foreach (var item in context.Logins)
                {
                    if (date.Date == item.LoginDate.Date)
                    {
                        loginCount++;
                        loginTime += item.LoginTime;
                    }
                }
                if (loginCount != 0)
                {
                    loginTime = loginTime / loginCount;
                    loginReportModel.LoginTime = (loginTime / 1000);
                }
                else
                    loginReportModel.LoginTime = 0;
            }
            return View(loginReportModel);
        }
        [HttpGet]
        public ActionResult LoginTimeReport()
        {
            LoginReportModel _model = LoginReportModel.getModel();
            return View(_model);
        }
        [HttpPost]
        public ActionResult RegistrationReport(DateTime startDate, DateTime endDate)
        {
            RegistrationReportModel model = new RegistrationReportModel();
            model.StartDate = startDate;
            model.EndDate = endDate;
            var users = _userManager.Users;
            int registrationCount = 0;
            foreach (var item in users)
            {
                if (item.RegistrationDate <= endDate && item.RegistrationDate >= startDate && item.EmailConfirmed==true)
                    registrationCount++;
            }
            model.RegistrationCount = registrationCount;

            return View(model);
        }
        [HttpGet]
        public ActionResult RegistrationReport(RegistrationReportModel model)
        {
            return View(model);
        }
    }
}
