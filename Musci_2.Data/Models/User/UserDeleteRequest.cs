using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_2.Data.Models.User
{
    public class UserDeleteRequest
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
    }
}
