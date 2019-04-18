using System;
using System.Collections.Generic;
using System.Text;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models;

namespace DAL.Repositories
{
    public class ActivityRepository : GenericRepository<Activity>, IActivityRepository
    {
        public ActivityRepository(DbContext dbContext) : base(dbContext)
        {

        }
    }
}
