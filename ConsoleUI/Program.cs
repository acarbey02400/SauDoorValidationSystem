// See https://aka.ms/new-console-template for more information
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities;
using DataAccess.Concrete;
using Entities.Concrete;


VerificationManager verificationManager = new VerificationManager(new DoorRoleDal());
var result= verificationManager.Validate("084 142", 8);
var result2 = verificationManager.Validate("084 123", 1);
var result3 = verificationManager.Validate("084 178", 10);
var result4 = verificationManager.Validate("084 178", 5);


List<IResult> results = new List<IResult>();
results.Add(result);
results.Add(result2);
results.Add(result3);
results.Add(result4);
foreach (var item in results)
{
    Console.WriteLine(item.Message+"\n");
}

