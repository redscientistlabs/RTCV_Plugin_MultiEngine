using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiEngine.Structures;
using RTCV.CorruptCore;
using RTCV.NetCore;

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

        public const string SPEC_NAME = "RTCSpec";

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

        public static CorruptionEngine EngineFromName(string engineName)
        {
            if (string.IsNullOrWhiteSpace(engineName)) return CorruptionEngine.NONE;

            switch (engineName)
            {
                case NightmareEngineStr:
                    return CorruptionEngine.NIGHTMARE;
                case HellgenieEngineStr:
                    return CorruptionEngine.HELLGENIE;
                case DistortionEngineStr:
                    return CorruptionEngine.DISTORTION;
                case FreezeEngineStr:
                    return CorruptionEngine.FREEZE;
                case PipeEngineStr:
                    return CorruptionEngine.PIPE;
                case VectorEngineStr:
                    return CorruptionEngine.VECTOR;
                case ClusterEngineStr:
                    return CorruptionEngine.CLUSTER;
                default:
                    return CorruptionEngine.NONE;
            }
        }


        //public static int EngineIndex(this string engineName)
        //{
        //    if (string.IsNullOrWhiteSpace(engineName)) return -1;

        //    switch (engineName)
        //    {

        //        default:
        //            break;
        //    }

        //    //if (settings is MCNightmareSettings) return NightmareEngine;
        //    //else if (settings is MCPipeSettings) return PipeEngine;
        //    //else if (settings is MCVectorSettings) return VectorEngine;
        //    //else if (settings is MCHellgenieSettings) return HellgenieEngine;
        //    //else if (settings is MCFreezeSettings) return FreezeEngine;
        //    //else if (settings is MCDistortionSettings) return DistortionEngine;
        //    //else if (settings is MCClusterSettings) return ClusterEngine;

        //    //else return -1;
        //    return -1;
        //}


        public static int PrecisionToIndex(int precision)
        {

            switch (precision)
            {
                case 1:
                    return 0;
                case 2:
                    return 1;
                case 4:
                    return 2;
                case 8:
                    return 3;
                default:
                    return 0;
            }
        }
        public static int IndexToPrecision(int index)
        {

            switch (index)
            {
                case 0:
                    return 1;
                case 1:
                    return 2;
                case 2:
                    return 4;
                case 3:
                    return 8;
                default:
                    return 1;
            }
        }


        public static void ExtractFrom(this PartialSpec to, PartialSpec from)
        {
            var keys = to.GetKeys();
            foreach (var key in keys)
            {
                to[key] = from[key];
            }
        }

    }
}
