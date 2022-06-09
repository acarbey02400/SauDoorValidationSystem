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
    public class DoorRoleManager : IDoorRoleService
    {
        IDoorRoleDal _userClaimDal;
        public DoorRoleManager(IDoorRoleDal userClaimDal)
        {
            _userClaimDal = userClaimDal;
        }
        public IResult add(DoorRole doorRole)
        {
           _userClaimDal.Add(doorRole);
            return new SuccessResult();
        }

        public IResult delete(DoorRole doorRole)
        {
            _userClaimDal.Delete(doorRole);
            return new SuccessResult();
        }

        public IDataResult<List<DoorRole>> getAll()
        {
            return new SuccessDataResult<List<DoorRole>>(_userClaimDal.GetAll());
        }
        public IDataResult<DoorRole> getById(int id)
        {
            return new SuccessDataResult<DoorRole>(_userClaimDal.Get(p => p.id == id));
        }

        public IResult update(DoorRole userClaim)
        {
            _userClaimDal.Update(userClaim);
            return new SuccessResult();
        }
    }
}
