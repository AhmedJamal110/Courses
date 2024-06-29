using Entity.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity.Services
{
    public interface ITokenServices
    {

        Task<string> CreatTokenAsync(AppUser user);
    }
}
