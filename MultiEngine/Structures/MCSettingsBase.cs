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

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCSettingsBase
    {
        [Ceras.Exclude]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string DisplayName { get; set; } = null;
        //TODO: remove
        [JsonProperty]
        public double Weight { get; set; } = 1.0;
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public long ForcedIntensity { get; set; } = -1;

        [JsonProperty]
        public double Percentage { get; set; } = 1.0;
        [JsonProperty]
        public int Alignment { get; private set; }
        [Ceras.Exclude]
        [JsonProperty]
        public int PrecisionIndex { get; private set; }
        [JsonProperty]
        public int Precision { get; private set; }
        [Ceras.Exclude]
        [JsonProperty]
        public int EngineIndex { get; private set; }
        [Ceras.Exclude]
        [JsonProperty]
        public string Type { get; private set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string[] Domains { get; set; } = null;

        [Ceras.Exclude]
        [JsonIgnore]
        protected string PercentageString { get { if (ForcedIntensity > 0) return $"[{ForcedIntensity,7}]"; else return $"[{Percentage * 100.0,6:0.00}%]"; } }


        /// <summary>
        /// Please call base constructor
        /// </summary>
        public MCSettingsBase() 
        {
            Type = this.GetType().Name;
        }

        /// <summary>
        /// Please call base.Apply
        /// </summary>
        public virtual void Apply()
        {
            RtcCore.CurrentPrecision = Precision;
            RtcCore.Alignment = Alignment;

        }

        public virtual void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            if (updateSelected)
            {
                form.cbSelectedEngine.SelectedIndex = EngineIndex;
                form.cbCustomPrecision.SelectedIndex = PrecisionIndex;
                form.nmAlignment.Value = Alignment;
            }
        }

        public virtual void Extract(CorruptionEngineForm form)
        {
            Alignment = (int)form.nmAlignment.Value;
            PrecisionIndex = form.cbCustomPrecision.SelectedIndex;
            Precision = (int)Math.Pow(2, PrecisionIndex);
            EngineIndex = form.cbSelectedEngine.SelectedIndex;
        }

        public virtual BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return null;
        }

        /// <summary>
        /// Setup on the RTC side
        /// </summary>
        public virtual void PreCorrupt() { }

    }
}
