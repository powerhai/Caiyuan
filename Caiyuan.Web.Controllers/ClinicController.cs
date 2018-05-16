using System.Linq;
using Caiyuan.DB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Caiyuan.DB.Access;
using Caiyuan.Web.Models.Clinic;

namespace Caiyuan.Web.Controllers
{
    public class ClinicController : Controller
    {
        private const string CREATED_MESSAGE = "恭喜，菜园创建成功了！";
        private const string EDITED_MESSAGE = "哈喽，信息保存成功了！";
        private readonly AppDbContext mContext;
        public ClinicController (AppDbContext context)
        {
            mContext = context;
        }
        public IActionResult Index(ClinicMessageId? message)
        {
            if(message.HasValue)
            {
                ViewData["HasMessage"] = true;
            
                ViewData["Message"] =  
                    message == ClinicMessageId.ClinicCreated ? CREATED_MESSAGE
                    : message == ClinicMessageId.ClinicEdited ? EDITED_MESSAGE
                    : "";

            }

            var clc = mContext.Clinics.FirstOrDefault();
            var model = new ClinicInformationViewModel()
            {
                Address = clc.Address,
                Mobile = clc.Mobile,
                Tel =  clc.Tel,
                Title = clc.Title,
                Recommend = clc.Recommend
            };
            
            return View(model);
        }

        public IActionResult Details ()
        {
            return View();
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult EditClinicByMaster ()
        {
            var clc = mContext.Clinics.FirstOrDefault();
            var model = new EditClinicByMasterViewModel()
            {
                Address = clc.Address,
                Mobile = clc.Mobile,
                Tel = clc.Tel,
                Title = clc.Title,
                Recommend = clc.Recommend
            };

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult EditClinicByMaster (EditClinicByMasterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var clc = mContext.Clinics.FirstOrDefault();
            clc.Tel = model.Tel;
            clc.Address = model.Address;
            clc.Title = model.Title;
            clc.Mobile = model.Mobile;
            clc.Recommend = model.Recommend;

            mContext.SaveChanges();
            return RedirectToAction(nameof(Index), new { Message = ClinicMessageId.ClinicEdited });
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateClinicByMaster ()
        { 
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        public IActionResult CreateClinicByMaster(CreateClinicByMasterViewModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            var clc = new Clinic()
            {
                Address = model.Address,
                Mobile = model.Mobile,
                Tel = model.Tel,
                Recommend = model.Recommend,
                Title = model.Title
            };
            mContext.Clinics.Add(clc);
            mContext.SaveChanges();
            return RedirectToAction(nameof(Index), new { Message = ClinicMessageId.ClinicCreated });
        } 

        public enum  ClinicMessageId
        {
            ClinicCreated,
            ClinicEdited
        }
    }
}