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

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCSettingsBase
    {
        public string DisplayName { get; set; } = null;
        //TODO: remove
        //public double Weight { get; set; } = 1.0;
        public long ForcedIntensity { get; set; } = -1;
        public double Percentage { get; set; } = 1.0;
        //public int Alignment { get; private set; }
        public int Alignment => cachedSpec.Get<int>(RTCSPEC.CORE_CURRENTALIGNMENT);
        //TODO: remove
        //public int PrecisionIndex { get; private set; }
        //public int Precision { get; private set; }
        public int Precision => cachedSpec.Get<int>(RTCSPEC.CORE_CURRENTPRECISION);
        //public int Precision {
        //    get { }

        //TODO: engine by name/enum
        public int EngineIndex { get; private set; }
        public string Type { get; private set; }

        public CorruptionEngine EngineType { get; private set; }

        //[JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Domains { get; set; } = null;

        protected string PercentageString { get { if (ForcedIntensity > 0) return $"[{ForcedIntensity,7}]"; else return $"[{Percentage * 100.0,6:0.00}%]"; } }

        [Ceras.Include]
        private PartialSpec cachedSpec = null;

        [Ceras.Exclude]
        protected PartialSpec CachedSpec => cachedSpec;
        protected PartialSpec TemplateSpec { get; set; }


        /// <summary>
        /// Please call base constructor
        /// </summary>
        public MCSettingsBase() 
        {
            Type = this.GetType().Name;
        }

        /// <summary>
        /// Please call base constructor
        /// </summary>
        public MCSettingsBase(PartialSpec spec) : this()
        {
            PartialSpec partial = new PartialSpec(C.SPEC_NAME);
            partial.Insert(spec);
            partial[RTCSPEC.CORE_CURRENTPRECISION] = RtcCore.CurrentPrecision;
            partial[RTCSPEC.CORE_CURRENTALIGNMENT] = RtcCore.Alignment;

        }


        /// <summary>
        /// Only override if using a different spec from <see cref="AllSpec.CorruptCoreSpec"/>
        /// </summary>
        public virtual void ApplyPartial()
        {
            AllSpec.CorruptCoreSpec.Update(cachedSpec,false,false);
        }

        private void UpdateCache()
        {
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

        //Switch
        public virtual BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return null;
        }

        //public virtual void PreCorrupt() { }

    }
}
