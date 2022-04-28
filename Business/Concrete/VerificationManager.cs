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
        IUserDal _userDal;
        public VerificationManager(IUserDal userDal)
        {
            _userDal = userDal;
        }
        public IResult Validate(string UId, int claimId)
        {
            if (_userDal.AuthVerification(UId,claimId))
            {
                return new SuccessResult("doğrulama başarılı");
            }
            return new ErrorResult("doğrulama başarısız");
           }
    }
}
