using Core.Utilities;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IUserClaimService
    { 
        public IResult add(UserClaim userClaim);
        public IResult update(UserClaim userClaim);
        public IResult delete(UserClaim userClaim);
        public IDataResult<List<UserClaim>> getAll();
        public IDataResult<UserClaim> getById(int id);
    }
}
