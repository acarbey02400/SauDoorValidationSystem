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
    public class UserValidationDoorManager : IUserValidationDoorService
    {
        IUserValidationDoorDal _userValidationDoorDal;
        IDoorRoleService _doorRoleService;
        public UserValidationDoorManager(IUserValidationDoorDal userValidationDoorDal, IDoorRoleService doorRoleService)
        {
            _userValidationDoorDal = userValidationDoorDal;
            _doorRoleService = doorRoleService;
        }
        public IResult add(UserValidationDoor userValidationDoor)
        {
            _userValidationDoorDal.Add(userValidationDoor);
            return new SuccessResult();
        }

        public IResult delete(UserValidationDoor userValidationDoor)
        {
            _userValidationDoorDal.Delete(userValidationDoor);
            return new SuccessResult();
        }

        public IDataResult<List<UserValidationDoor>> getAll()
        {
            return new SuccessDataResult<List<UserValidationDoor>>(_userValidationDoorDal.GetAll());
        }

        public IDataResult<UserValidationDoor> getById(int id)
        {
            return new SuccessDataResult<UserValidationDoor>(_userValidationDoorDal.Get(p => p.id == id));
        }

        public IResult update(UserValidationDoor userValidationDoor)
        {
            _userValidationDoorDal.Update(userValidationDoor);
            return new SuccessResult();
        }

        public IResult Validate(string UId, int doorId)
        {
            DateTime dateTime = DateTime.Now;
            var result = _userValidationDoorDal.UserAuthDoor(UId, doorId);
            
            if (result.Any())
            {
                foreach (var item in result)
                {
                    if ( (item.doorId == doorId) && (item.startDate<=dateTime)
                        && (item.stopDate>=dateTime) && (item.status==true))
                    {
                        return new SuccessResult("doğrulandı(ekstra yetki)");
                    }
                    else if (item.startDate.Day <= DateTime.Now.Day && item.stopDate.Day >= DateTime.Now.Day && item.doorId == doorId &&
                        item.startDate.Hour <= DateTime.Now.Hour && item.stopDate.Hour >= DateTime.Now.Hour && item.status == false)
                    {
                        return new ErrorResult("Doğrulanamadı(yetkisel hata)");
                    }
                }
            }

            var _result = _doorRoleService.Verification(UId, doorId);
            if (_result.Success)
            {
                return new SuccessResult("doğrulandı");
            }
            return new ErrorResult("doğrulanamadı");
        }
    }
}
