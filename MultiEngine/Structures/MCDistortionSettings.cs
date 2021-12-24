using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    class MCDistortionSettings : MCSettingsBase
    {
        [JsonProperty]
        public int Delay { get; private set; }

        public MCDistortionSettings() : base()
        {

        }

        public override void Apply()
        {
            base.Apply();
            DistortionEngine.Delay = Delay;
        }

        public override void Extract(CorruptionEngineForm form)
        {
            base.Extract(form);
            Delay = (int)form.distortionEngineControl.nmDistortionDelay.Value;
        }

        public override void UpdateUI(CorruptionEngineForm form)
        {
            base.UpdateUI(form);
            form.distortionEngineControl.nmDistortionDelay.Value = Delay;
        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return new BlastUnit[] { DistortionEngine.GenerateUnit(domain, address, precision, alignment) };
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Distortion"} [{this.Precision}, {Delay,3}]";
        }

    }
}
