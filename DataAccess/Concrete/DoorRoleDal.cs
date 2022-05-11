﻿using Core.DataAccess.EntityFramework.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class DoorRoleDal:EfEntityRepositoryBase<DoorRole,SauDbContext>,IDoorRoleDal
    {
        public List<DoorRole> AuthVerification(string UId)
        {
            using (SauDbContext context = new SauDbContext())
            {
                var result = from p in context.Users
                             where p.UId == UId
                             join dr in context.DoorRole
                             on p.userTypeId equals dr.userTypeId
                             select new DoorRole { id =dr.id, doorId=dr.doorId, userTypeId=p.userTypeId };

                return result.ToList();
            }
        }
    }
}