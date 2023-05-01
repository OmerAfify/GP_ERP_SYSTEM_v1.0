using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP_Domians.Models;

namespace ERP_Domians.IServices
{
    public interface ITokenService
    {
        public string CreateToken(ApplicationUser applicationUser);

    }
}
