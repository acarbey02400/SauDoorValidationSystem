using Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class UserValidationDoor:IEntity
    {
        public int id { get; set; }
        public string ?name { get; set; }
        public int doorId { get; set; }
        public int userId { get; set; }
        public int startDate { get; set; }
        public int stopDate { get; set; }
        public int startTime { get; set; }
        public int stopTime { get; set; }
    }
}
