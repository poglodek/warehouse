using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace warehouse.Dto.User
{
    public class UserCreatedDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string HashedPassword { get; set; }

        public string Phone { get; set; }
    }
}
