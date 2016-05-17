using inRiver.Remoting.Objects;
using System;
using System.Linq;

namespace InRiver.Extend.Security
{
    public class UserSwitcher : PermissionSwitcher
    {
        private UserSwitcher(User user)
        {
            SetPermissions(user.Username, user.Roles.Select(r => r.Name), user.Permissions.Select(p => p.Name));
        }

        public static UserSwitcher Initialize(User user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));
            return new UserSwitcher(user);
        }
    }
}