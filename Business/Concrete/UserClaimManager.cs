using Business.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class UserClaimManager : IUserClaimService
    {
        IUserClaimDal _userClaimDal;
        public UserClaimManager(IUserClaimDal userClaimDal)
        {
            _userClaimDal = userClaimDal;
        }
        public IResult add(UserClaim userClaim)
        {
           _userClaimDal.Add(userClaim);
            return new SuccessResult();
        }

        public IResult delete(UserClaim userClaim)
        {
            _userClaimDal.Delete(userClaim);
            return new SuccessResult();
        }

        public IDataResult<List<UserClaim>> getAll()
        {
            return new SuccessDataResult<List<UserClaim>>(_userClaimDal.GetAll());
        }
        public IDataResult<UserClaim> getById(int id)
        {
            return new SuccessDataResult<UserClaim>(_userClaimDal.Get(p => p.id == id));
        }

        public IResult update(UserClaim userClaim)
        {
            _userClaimDal.Update(userClaim);
            return new SuccessResult();
        }
    }
}
