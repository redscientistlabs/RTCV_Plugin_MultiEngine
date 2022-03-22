using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;
using Ceras;
using RTCV.CorruptCore;
using Newtonsoft.Json;
using RTCV.NetCore;
using RTCV.Common;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCSettingsBase
    {
        public string DisplayName { get; set; } = null;
        public long ForcedIntensity { get; set; } = -1;
        public double Percentage { get; set; } = 1.0;
        public int Alignment => cachedSpec.Get<int>(RTCSPEC.CORE_CURRENTALIGNMENT);
        public int Precision => cachedSpec.Get<int>(RTCSPEC.CORE_CURRENTPRECISION);
        public int EngineIndex { get; private set; }

        public CorruptionEngine EngineType { get; private set; }
        public string[] Domains { get; set; } = null;

        protected string PercentageString { get { if (ForcedIntensity > 0) return $"[{ForcedIntensity,7}]"; else return $"[{Percentage * 100.0,6:0.00}%]"; } }

        [Ceras.Include]
        private PartialSpec cachedSpec = null;

        [Ceras.Exclude]
        protected PartialSpec CachedSpec => cachedSpec;

        /// <summary>
        /// for ceras
        /// </summary>
        public MCSettingsBase() 
        {

        }

        /// <summary>
        /// Please call base constructor
        /// </summary>
        public MCSettingsBase(PartialSpec template)
        {
            PartialSpec partial = new PartialSpec(C.SPEC_NAME);
            if(template != null) partial.Insert(template);

            partial[RTCSPEC.CORE_CURRENTPRECISION] = RtcCore.CurrentPrecision;
            partial[RTCSPEC.CORE_CURRENTALIGNMENT] = RtcCore.Alignment;
            cachedSpec = partial;
            EngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
            EngineType = RtcCore.SelectedEngine;
            UpdateCache();
        }


        /// <summary>
        /// Only override if using a different spec from <see cref="AllSpec.CorruptCoreSpec"/>. Updates without pushing
        /// </summary>
        public virtual void ApplyPartial()
        {
            AllSpec.CorruptCoreSpec.Update(cachedSpec,false,false);
        }

        private void UpdateCache()
        {
            //Update all 
            cachedSpec.ExtractFrom(AllSpec.CorruptCoreSpec.GetPartialSpec());

            //cachedSpec = PopulatePartial(CreateBaseUpdateSpec());
        }

        ///// <summary>
        ///// Override to set up a partial spec.
        ///// </summary>
        ///// <param name="partial"></param>
        ///// <returns></returns>
        //protected virtual PartialSpec BuildUpdateSpec(PartialSpec partial);
        //{
        //    return partial;
        //}

        //private PartialSpec PopulatePartial(PartialSpec partial)
        //{

        //    var keyList = partial.GetKeys();
        //    foreach (var key in keyList)
        //    {
        //        partial[key] = AllSpec.CorruptCoreSpec[key];
        //    }
        //    return partial;
        //}

        ////TODO: just save the partial spec to the file with ceras instead of caching
        //private PartialSpec CreateBaseUpdateSpec()
        //{
        //    PartialSpec partial = new PartialSpec(C.SPEC_NAME);
        //    partial[RTCSPEC.CORE_CURRENTPRECISION] = RtcCore.CurrentPrecision;
        //    partial[RTCSPEC.CORE_CURRENTALIGNMENT] = RtcCore.Alignment;
        //    return partial;
        //}

        //TODO: remove
        [Obsolete]
        public virtual void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            SyncObjectSingleton.FormExecute(() =>
            {
                if (updateSelected)
                {
                    form.cbSelectedEngine.SelectedIndex = EngineIndex;
                    form.cbCustomPrecision.SelectedIndex = C.PrecisionToIndex(Precision);
                    form.nmAlignment.Value = Alignment;
                }
            });
        }

        public virtual void Extract(CorruptionEngineForm form)
        {
            EngineIndex = form.cbSelectedEngine.SelectedIndex;
            EngineType = RtcCore.SelectedEngine;
            UpdateCache();
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "TODO"} ";
        }

        //Switch
        public virtual BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            switch (EngineType)
            {
                case CorruptionEngine.NIGHTMARE:
                    return new BlastUnit[] { NightmareEngine.GenerateUnit(domain, address, precision, alignment) };
                case CorruptionEngine.HELLGENIE:
                    return new BlastUnit[] { HellgenieEngine.GenerateUnit(domain, address, precision, alignment) };
                case CorruptionEngine.DISTORTION:
                    return new BlastUnit[] { DistortionEngine.GenerateUnit(domain, address, precision, alignment) };
                case CorruptionEngine.FREEZE:
                    return new BlastUnit[] { FreezeEngine.GenerateUnit(domain, address, precision, alignment) };
                case CorruptionEngine.PIPE:
                    return new BlastUnit[] { PipeEngine.GenerateUnit(domain, address, precision, alignment) };
                case CorruptionEngine.VECTOR:
                    return new BlastUnit[] { VectorEngine.GenerateUnit(domain, address, alignment) };
                case CorruptionEngine.CLUSTER:
                    var clusterResult = ClusterEngine.GenerateUnit(domain, address, alignment);
                    if (clusterResult == null || clusterResult.Length == 0)
                    {
                        return new BlastUnit[] { null };
                    }
                    else
                    {
                        return clusterResult;
                    }
                default:
                    return null;
            }
        }

        //public virtual void PreCorrupt() { }

    }
}
