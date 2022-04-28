// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities;
using DataAccess.Concrete;
using Entities.Concrete;

UserClaimManager userClaimManager = new UserClaimManager(new UserClaimDal());
//userClaimManager.add(new UserClaim { name = "KAPI 1" });
//userClaimManager.add(new UserClaim { name = "KAPI 2" });
//userClaimManager.add(new UserClaim { name = "KAPI 3" });
//userClaimManager.add(new UserClaim { name = "KAPI 4" });
//userClaimManager.add(new UserClaim { door1=true, door2=true, name="YETKİ-9"});
//UserTypeManager typeManager = new UserTypeManager(new UserTypeDal());
//typeManager.add(new UserType { name = "Dekan", userClaimId=1 });
//typeManager.add(new UserType { name = "Öğrenci", userClaimId = 3 });
//typeManager.add(new UserType { name = "Misafir", userClaimId = 4 });

UserManager userManager = new UserManager(new UserDal());
//userManager.add(new User { firstName = "Dekan", lastName = "Dekan", lastLogin = DateTime.Now, status = true, UId = "084 122", userTypeId = 1 });
//userManager.add(new User { firstName = "Öğretim", lastName = "Görevlisi", lastLogin = DateTime.Now, status = true, UId = "084 178", userTypeId = 2 });
//var result= userManager.getById(3);
//Console.WriteLine(result.Data.firstName+"  "+result.Data.lastName);
VerificationManager verification = new VerificationManager(new UserDal());
var result= verification.Validate("084 055", 3);
var result2 = verification.Validate("084 142", 6);
var result3 = verification.Validate("084 122", 10);
var result4 = verification.Validate("084 055", 3);
List<IResult> results = new List<IResult>();
results.Add(result);
results.Add(result2);
results.Add(result3);
results.Add(result4);
foreach (var item in results)
{
    Console.WriteLine(item.Message+"\n");
}

