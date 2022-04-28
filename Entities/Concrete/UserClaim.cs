using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserClaim : IEntity
    {
        public int id { get; set; }
        public string? name { get; set; }
        public bool? door1 { get; set; }
        public bool? door2 { get; set; }
        public bool? door3 { get; set; }
        public bool? door4 { get; set; }
        public bool? door5 { get; set; }
        public bool? door6 { get; set; }
        public bool? door7 { get; set; }
        public bool? door8 { get; set; }
        public bool? door9 { get; set; }
        public bool? door10 { get; set; }
    }
}
