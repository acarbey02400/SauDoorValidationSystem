// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities;
using DataAccess.Concrete;
using Entities.Concrete;

UserValidationDoorManager manager = new UserValidationDoorManager(new UserValidationDoorDal());
//manager.add(new UserValidationDoor { name = "ibrahim kısıt", doorId = 5, startDate = 1, stopDate = 15, startTime = 8, stopTime = 17, userId = 1 });
//manager.add(new UserValidationDoor { name="ibrahim yetki", doorId=7, startDate=1, stopDate = 15, startTime = 8, stopTime = 17,userId = 1 });
VerificationManager verificationManager = new VerificationManager(new DoorRoleDal(), new UserValidationDoorDal());
var result = verificationManager.Validate("084 055", 5, 16, 15);
var result2 = verificationManager.Validate("084 055", 7, 15, 10);
var result3 = verificationManager.Validate("084 055", 7, 15, 22);
var result4 = verificationManager.Validate("084 055", 7, 15, 10);
var result5 = verificationManager.Validate("084 178", 5,14,15);
var result6 = verificationManager.Validate("084 178", 5, 31, 22);
var result7 = verificationManager.Validate("084 178", 9, 31, 22);


List<IResult> results = new List<IResult>();
results.Add(result);
results.Add(result2);
results.Add(result3);
results.Add(result4);
results.Add(result5);
results.Add(result6);
results.Add(result7);
foreach (var item in results)
{
    Console.WriteLine(item.Message);
}


