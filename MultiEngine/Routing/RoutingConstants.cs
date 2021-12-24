using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiEngine
{
    internal static class PluginRouting
    {
        internal const string PREFIX = "MultiEngine";
        internal static class Endpoints
        {

            public const string EMU_SIDE = PREFIX + "_" + "EMU";
            public const string RTC_SIDE = PREFIX + "_" + "RTC";
        }

        /// <summary>
        /// Add your commands here
        /// </summary>
        internal static class Commands
        {
            public const string CORRUPT = PREFIX +"_"+ nameof(CORRUPT);
        }
    }
}
