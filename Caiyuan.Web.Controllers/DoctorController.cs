using System.Collections.Generic;
using Caiyuan.DB.Models; 
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Caiyuan.DB.Access;
using Caiyuan.Web.Models.Doctor;

namespace Caiyuan.Web.Controllers
{
    public class DoctorController : Controller
    {
        private readonly AppDbContext mContext;
        public DoctorController(AppDbContext context)
        {
            mContext = context;
        }


        public IActionResult Index ()
        {
            var model = new ClinicDoctorsViewModel();
            var list = new List<DoctorViewModel>();
            foreach (var item in mContext.Doctors)
            {
                list.Add(new DoctorViewModel()
                {
                    Name = item.Name,
                    Experience = item.Experience,
                    Level = item.Level,
                    School = item.School,
                    Position = item.Position
                });
            }
            model.Doctors = list;
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateDoctor ()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateDoctor(DoctorViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var doctor = new Doctor()
            {
                Name = model.Name,
                Experience = model.Experience,
                Level = model.Level,
                School = model.School,
                Position = model.Position
            };
            mContext.Doctors.Add(doctor);
            mContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}