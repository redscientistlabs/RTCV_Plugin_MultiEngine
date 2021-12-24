using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiEngine.Structures;

namespace MultiEngine
{
    internal static class C
    {
        public const int ClusterEngine = 0;
        //public const int CustomEngine = 1;
        public const int DistortionEngine = 1;
        public const int FreezeEngine = 2;
        public const int HellgenieEngine = 3;
        public const int NightmareEngine = 4;
        public const int PipeEngine = 5;
        public const int VectorEngine = 6;

        public const string ClusterEngineStr    = "Cluster Engine";
        //public const string CustomEngineStr     = "Custom Engine";
        public const string DistortionEngineStr = "Distortion Engine";
        public const string FreezeEngineStr     = "Freeze Engine";
        public const string HellgenieEngineStr  = "Hellgenie Engine";
        public const string NightmareEngineStr  = "Nightmare Engine";
        public const string PipeEngineStr       = "Pipe Engine";
        public const string VectorEngineStr     = "Vector Engine";

        public static int[] GetEngineArray()
        {
            return new int[7] { -1, -1, -1, -1, -1, -1, -1, };
        }

        public static int EngineIndex(this MCSettingsBase settings)
        {
            if (settings == null) return -1;
            if (settings is MCNightmareSettings) return NightmareEngine;
            else if (settings is MCPipeSettings) return PipeEngine;
            else if (settings is MCVectorSettings) return VectorEngine;
            else if (settings is MCHellgenieSettings) return HellgenieEngine;
            else if (settings is MCFreezeSettings) return FreezeEngine;
            else if (settings is MCDistortionSettings) return DistortionEngine;
            else if (settings is MCClusterSettings) return ClusterEngine;

            else return -1;
        }

    }
}
