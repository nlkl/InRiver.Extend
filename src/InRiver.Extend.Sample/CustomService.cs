using inRiver.Remoting.Faults;
using inRiver.Remoting.Security;
using inRiver.Server.Context;
using InRiver.Extend.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Sample
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class CustomService : ICustomService
    {
        public string Reverse(string text)
        {
            if (!inRiverContext.Default.HasPermission(UserPermission.View))
            {
                throw new FaultException<SecurityFault>(new SecurityFault { Message = "User does not have sufficient permissions." });
            }

            text = text ?? string.Empty;
            return new string(text.Reverse().ToArray());
        }
    }
}
