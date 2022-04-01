using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiEngine.Structures;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;

namespace MultiEngine
{
    internal static class C
    {
        public static int ClusterEngineIndex    { get; private set; } = 0;
        public static int DistortionEngineIndex { get; private set; } = 1;
        public static int FreezeEngineIndex     { get; private set; } = 2;
        public static int HellgenieEngineIndex  { get; private set; } = 3;
        public static int NightmareEngineIndex  { get; private set; } = 4;
        public static int PipeEngineIndex       { get; private set; } = 5;
        public static int VectorEngineIndex     { get; private set; } = 6;

        public const string ClusterEngineStr    = "Cluster Engine";
        public const string DistortionEngineStr = "Distortion Engine";
        public const string FreezeEngineStr     = "Freeze Engine";
        public const string HellgenieEngineStr  = "Hellgenie Engine";
        public const string NightmareEngineStr  = "Nightmare Engine";
        public const string PipeEngineStr       = "Pipe Engine";
        public const string VectorEngineStr     = "Vector Engine";

        public const string TARGET_SPEC_NAME = "RTCSpec";

        private static PartialSpec masterSpec = null;

        private static HashSet<int> supportedEngineIndices = new HashSet<int>();

        //public static int EngineIndex(this MCSettingsBase settings)
        //{
        //    if (settings == null) return -1;
        //    if (settings is MCNightmareSettings) return NightmareEngineIndex;
        //    else if (settings is MCPipeSettings) return PipeEngineIndex;
        //    else if (settings is MCVectorSettings) return VectorEngineIndex;
        //    else if (settings is MCHellgenieSettings) return HellgenieEngineIndex;
        //    else if (settings is MCFreezeSettings) return FreezeEngineIndex;
        //    else if (settings is MCDistortionSettings) return DistortionEngineIndex;
        //    else if (settings is MCClusterSettings) return ClusterEngineIndex;
        //    else return -1;
        //}

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

        public static int EngineToIndex(CorruptionEngine engine)
        {
            switch (engine)
            {
                case CorruptionEngine.NIGHTMARE:
                    return NightmareEngineIndex;
                case CorruptionEngine.HELLGENIE:
                    return HellgenieEngineIndex;
                case CorruptionEngine.DISTORTION:
                    return DistortionEngineIndex;
                case CorruptionEngine.FREEZE:
                    return FreezeEngineIndex;
                case CorruptionEngine.PIPE:
                    return PipeEngineIndex;
                case CorruptionEngine.VECTOR:
                    return VectorEngineIndex;
                case CorruptionEngine.CLUSTER:
                    return ClusterEngineIndex;
                default:
                    return 0;
            }
        }


        public static bool IsEngineSupported(int index)
        {
            return supportedEngineIndices.Contains(index);
        }

        public static void Init()
        {
            InitMasterSpec();

            //var cefI = S.GET<CorruptionEngineForm>().cbSelectedEngine.Items.Cast<object>().Where(x => x is ComboBoxItem<string>).ToList();


            //Indices length will always match cefI
            List<int> indices = new List<int>();
            int index = 0;
            var cefI = S.GET<CorruptionEngineForm>().cbSelectedEngine.Items.Cast<object>().Where(x => {
                bool isStr = x is string;
                if (isStr) indices.Add(index);
                index = index + 1;
                return isStr;
            }).Cast<string>().ToList();

            int Find(string toFind)
            {
                for (int i = 0; i < cefI.Count; i++)
                {
                    if(cefI[i] == toFind)
                    {
                        supportedEngineIndices.Add(indices[i]);
                        return indices[i];
                    }
                }
                //If not found
                return 0;
            }

            ClusterEngineIndex      = Find(ClusterEngineStr);
            DistortionEngineIndex   = Find(DistortionEngineStr);
            FreezeEngineIndex       = Find(FreezeEngineStr);
            HellgenieEngineIndex    = Find(HellgenieEngineStr);
            NightmareEngineIndex    = Find(NightmareEngineStr);
            PipeEngineIndex         = Find(PipeEngineStr);
            VectorEngineIndex       = Find(VectorEngineStr);

            //ClusterEngineStr
            //DistortionEngineStr
            //FreezeEngineStr
            //HellgenieEngineStr
            //NightmareEngineStr
            //PipeEngineStr
            //VectorEngineStr
        }

        static void InitMasterSpec()
        {
            masterSpec = new PartialSpec(C.TARGET_SPEC_NAME);
            //masterSpec[RTCSPEC.CORE_INTENSITY] = RtcCore.Intensity;
            masterSpec[RTCSPEC.CORE_CURRENTALIGNMENT] = RtcCore.Alignment;
            masterSpec[RTCSPEC.CORE_CURRENTPRECISION] = RtcCore.CurrentPrecision;
            masterSpec.Insert(RTCV.CorruptCore.NightmareEngine.getDefaultPartial());
            masterSpec.Insert(RTCV.CorruptCore.HellgenieEngine.getDefaultPartial());
            masterSpec.Insert(RTCV.CorruptCore.DistortionEngine.getDefaultPartial());
            masterSpec.Insert(RTCV.CorruptCore.VectorEngine.getDefaultPartial());
            masterSpec.Insert(RTCV.CorruptCore.ClusterEngine.getDefaultPartial());
        }

        internal static string EngineString(CorruptionEngine engine)
        {
            switch (engine)
            {
                case CorruptionEngine.NIGHTMARE:
                    return NightmareEngineStr;
                case CorruptionEngine.HELLGENIE:
                    return HellgenieEngineStr;
                case CorruptionEngine.DISTORTION:
                    return DistortionEngineStr;
                case CorruptionEngine.FREEZE:
                    return FreezeEngineStr;
                case CorruptionEngine.PIPE:
                    return PipeEngineStr;
                case CorruptionEngine.VECTOR:
                    return VectorEngineStr;
                case CorruptionEngine.CLUSTER:
                    return ClusterEngineStr;
                default:
                    return "UNSUPPORTED";
            }
        }

        public static void CacheMasterSpec()
        {
            masterSpec.ExtractFrom(AllSpec.CorruptCoreSpec);
        }

        public static void RestoreMasterSpec(bool push = false)
        {
            AllSpec.CorruptCoreSpec.Update(masterSpec, push, push);
        }

        


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

        public static void ExtractFrom(this PartialSpec to, FullSpec from)
        {
            var keys = to.GetKeys();
            foreach (var key in keys)
            {
                to[key] = from[key];
            }
        }

    }
}
