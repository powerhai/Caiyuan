using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Services;
using Microsoft.AspNetCore.Mvc;

namespace Caiyuan.Controllers
{
    public class HomeController : Controller
    {
        public IEmailSender MailSender
        {
            get;
            set;
        }
        public HomeController (IEmailSender mailSender)
        {
            MailSender = mailSender;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
