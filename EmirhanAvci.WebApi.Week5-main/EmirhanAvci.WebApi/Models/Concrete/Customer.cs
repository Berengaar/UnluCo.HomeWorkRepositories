using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmirhanAvci.WebApi.Models.Concrete
{
    public class Customer
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
    }
}
