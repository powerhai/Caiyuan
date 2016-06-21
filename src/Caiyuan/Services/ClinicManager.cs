using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Caiyuan.Data;
using Caiyuan.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace Caiyuan.Services
{
    public class ClinicManager
    {
        private readonly ApplicationDbContext mDbContext;

        public ClinicManager(ApplicationDbContext dbContext)
        {
            mDbContext = dbContext;
        }

        public void CreateClinic(string title, string address , ApplicationUser muster)
        {
            var clinic = new Clinic() {Address = address, Title = title, CreatedTime = DateTime.Now, Muster = muster};
            mDbContext.Add(clinic);
            mDbContext.SaveChanges();

        }

        public void DisableClinic(long id,string reason)
        {
            
        }

        public void EnableClinic(long id)
        {
            
        }
    }
}
