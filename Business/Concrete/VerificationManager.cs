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
        public VerificationManager(IDoorRoleDal doorRoleDal)
        {
            _doorRoleDal = doorRoleDal;
        }
        public IResult Validate(string UId,int doorId)
        {
            var result = _doorRoleDal.AuthVerification(UId);
            foreach (var item in result)
            {
                if (item.doorId==doorId)
                {
                    return new SuccessResult(item.name+" doğrulandı.");
                }
            }
            return new ErrorResult("doğrulanamadı.");
        }
    }
}
