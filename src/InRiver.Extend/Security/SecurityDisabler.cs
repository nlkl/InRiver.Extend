using System.Linq;

namespace InRiver.Extend.Security
{
    public class SecurityDisabler : PermissionSwitcher
    {
        private SecurityDisabler()
        {
            SetPermissions("SecurityDisabler", new[] { "Administrator" }, Enumerable.Empty<string>());
        }

        public static SecurityDisabler Initialize() => new SecurityDisabler();
    }
}