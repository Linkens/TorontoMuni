using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpertCities.Data
{
    public class Workforce : IdentityUser<int>
    {
        public string Name { get; set; }
        public WorkforceTypeEnum Type { get; set; }
        public List<Role> Roles { get; set; }

    }
    public class Role : IdentityRole<int>
    {
        public List<Workforce> Workforce { get; set; }
    }
}
