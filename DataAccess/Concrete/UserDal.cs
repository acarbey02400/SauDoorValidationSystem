using Core.DataAccess.EntityFramework.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User, SauDbContext>, IUserDal
    {
        public bool AuthVerification(string UId, int claimId)
        {
            using (SauDbContext context = new SauDbContext())
            {
                var result= from p in context.Users
                            where p.UId==UId
                            join tp in context.UserTypes
                            on p.userTypeId equals tp.id
                            join cm in context.UserClaims
                            on tp.userClaimId equals cm.id
                            select new UserClaim { id = cm.id, name = cm.name };
                if (result.SingleOrDefault()?.id<=claimId)
                {
                    return true;
                }
                return false;        
            }
        }
    }
}
