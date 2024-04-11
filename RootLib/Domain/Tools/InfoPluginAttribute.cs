using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tools
{
    [AttributeUsage(AttributeTargets.Assembly)]
    public class InfoPluginAttribute : Attribute
    {
        public string ServiceProvide { get; set; } = "";
        public string Description { get; set; } = "";
        public string CurrentVersionMeta { get; set; } = "";
        public string CompatibleVersion { get; set; } = "";

    }

}
