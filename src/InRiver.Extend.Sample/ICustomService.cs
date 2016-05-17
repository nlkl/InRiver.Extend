using inRiver.Remoting.Faults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace InRiver.Extend.Sample
{
    [ServiceContract(Namespace = "http://inRiver.ServiceModel.Custom")]
    public interface ICustomService
    {
        [FaultContract(typeof(SecurityFault))]
        [OperationContract]
        string Reverse(string text);
    }
}
