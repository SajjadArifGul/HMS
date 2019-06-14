using HMS.Data;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMS.Services
{
    public class HMSRoleManager : RoleManager<IdentityRole>
    {
        public HMSRoleManager(IRoleStore<IdentityRole, string> roleStore) : base(roleStore)
        {
        }
        public static HMSRoleManager Create(IdentityFactoryOptions<HMSRoleManager> options, IOwinContext context)
        {
            return new HMSRoleManager(new RoleStore<IdentityRole>(context.Get<HMSContext>()));
        }
    }
}
