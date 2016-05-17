using inRiver.Server.Context;
using System;
using System.Collections.Generic;

namespace InRiver.Extend.Security
{
    public abstract class PermissionSwitcher : IDisposable
    {
        private const string userKey = "CurrentUser";
        private const string rolesKey = "CurrentRoles";
        private const string permissionsKey = "CurrentPermissions";

        private readonly object actualUser;
        private readonly object actualRoles;
        private readonly object actualPermissions;

        protected PermissionSwitcher()
        {
            actualUser = GetCurrentItemOrNull(userKey);
            actualRoles = GetCurrentItemOrNull(rolesKey);
            actualPermissions = GetCurrentItemOrNull(permissionsKey);
        }

        public void Dispose() =>
            SetPermissions(actualUser, actualRoles, actualPermissions);

        protected void SetPermissions(string user, IEnumerable<string> roles, IEnumerable<string> permissions) =>
            SetPermissions(user, string.Join(",", roles), string.Join(",", permissions));

        private void SetPermissions(object user, object roles, object permissions)
        {
            SetOrRemoveCurrentItem(userKey, user);
            SetOrRemoveCurrentItem(rolesKey, roles);
            SetOrRemoveCurrentItem(permissionsKey, permissions);
        }

        private object GetCurrentItemOrNull(string key)
        {
            if (inRiverOperationContext.Current.Items.ContainsKey(key))
            {
                return inRiverOperationContext.Current.Items[key];
            }

            return null;
        }

        private void SetOrRemoveCurrentItem(string key, object value)
        {
            if (value == null && inRiverOperationContext.Current.Items.ContainsKey(key))
            {
                inRiverOperationContext.Current.Items.Remove(key);
            }
            else
            {
                inRiverOperationContext.Current.Items[key] = value;
            }
        }
    }
}