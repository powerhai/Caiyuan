using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Caiyuan.DB.Models;
using Caiyuan.Domain;
using Caiyuan.DB.Access;
using Caiyuan.Web.Models.Reservation;
using Caiyuan.Domain.Caiyuan.Domain;

namespace Caiyuan.Web.Controllers
{
    public class ReservationController : Controller
    {
        private AppDbContext mContext;
        private const string CREATED_MESSAGE = "预约已创建";
        private const string UPDATED_MESSAGE = "预约更新成功";
        public ReservationController(AppDbContext context)
        {
            mContext = context;
        }

        [AllowAnonymous]
        public IActionResult Index (ReservationMessageId? message)
        {
            if (message.HasValue)
            {
                ViewData["HasMessage"] = true;

                ViewData["Message"] =
                    message == ReservationMessageId.ReservationCreated ? CREATED_MESSAGE
                    : message == ReservationMessageId.ReservationUpdated ? UPDATED_MESSAGE
                    : ""; 
            }
            var model = new ReservationInfoViewModel();
            foreach (var item in mContext.Reservations)
            {
                model.Reservations.Add(new ReservationInfoViewModel.ReservationData()
                {
                    ID = item.Id,
                    DateTime = item.DateTime,
                    Objects = item.Objects,
                    Status = item.Status
                });
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateReservationByPatient()
        {
            var model = new ReservationViewModel() {DateTime = DateTime.Now.AddDays(3)};
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateReservationByPatient(ReservationViewModel model)
        {
            if(!ModelState.IsValid)
                return View(model);
            var rer = new Reservation()
            {
                Objects = model.Objects ,
                DateTime = model.DateTime,
                Status = ReservationStatus.Init
            };
            mContext.Reservations.Add(rer);
            mContext.SaveChanges();
            return RedirectToAction(nameof(Index), new {Message = ReservationMessageId.ReservationCreated });
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditReservationByPatient (long id)
        {
            var res = mContext.Reservations.FirstOrDefault(a => a.Id == id);

            var model = new ReservationViewModel()
            {
               ID= res.Id,
               Objects = res.Objects,
               DateTime = res.DateTime
            };
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult UpdateReservationByPatient (ReservationViewModel model)
        {
            var res = mContext.Reservations.FirstOrDefault(a => a.Id == model.ID);
            res.Objects = model.Objects;
            res.DateTime = model.DateTime;
            mContext.SaveChanges();
            return RedirectToAction(nameof(Index), new { Message = ReservationMessageId.ReservationUpdated });
        }

        public enum ReservationMessageId
        {
            ReservationCreated,
            ReservationUpdated,
            ReservationDeleted
        }
    }
}