using inRiver.Server.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Security
{
    public static class ContextExtensions
    {
        public static bool HasPermission(this inRiverContext context, string permissionName)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));
            if (permissionName == null) throw new ArgumentNullException(nameof(permissionName));
            return context.Roles.Contains("Administrator") || context.Permissions.Contains(permissionName);
        }
    }
}
