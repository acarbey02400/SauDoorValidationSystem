using Business.Abstract;
using Core.Utilities;
using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class VerificationManager : IVerificationService
    {
        IDoorRoleDal _doorRoleDal;
        IUserValidationDoorDal _userValidationDoorDal;
        public VerificationManager(IDoorRoleDal doorRoleDal, IUserValidationDoorDal userValidationDoorDal)
        {
            _doorRoleDal = doorRoleDal;
            _userValidationDoorDal = userValidationDoorDal;
        }
        public IResult Validate(string UId, int doorId, DateTime dateTime)
        {
            var result = _doorRoleDal.AuthVerification(UId);
            foreach (var item in result)
            {
                if (item.doorId == doorId)
                {
                    return UserValidationDoor(UId, doorId, dateTime);
                }
            }

            if (_userValidationDoorDal.UserAuthDoor(UId, doorId).Count > 0)
            {
                return UserValidationDoor(UId, doorId, dateTime);
            }
            return new ErrorResult("doğrulanamadı.");
        }
        public IResult UserValidationDoor(string UId, int doorId, DateTime dateTime)
        {
            var result = _userValidationDoorDal.UserAuthDoor(UId, doorId);
            if (result.Count == 0)
            {
                return new SuccessResult("doğrulandı");
            }
            foreach (var item in result)
            {

                if (item.startDate.Day <= dateTime.Day && item.stopDate.Day >= dateTime.Day && item.startDate.Hour <= dateTime.Hour && item.stopDate.Hour >= dateTime.Hour)
                {
                    return new SuccessResult("doğrulandı");
                }

            }
            return new ErrorResult("doğrulanamadı");
        }

    }
}
