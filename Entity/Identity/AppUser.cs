using Entity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Identity
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserCourse> Courses { get; set; }

    }
}
