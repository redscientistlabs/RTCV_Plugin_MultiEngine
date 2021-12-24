using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    class MCHellgenieSettings : MCSettingsBase
    {
        [JsonProperty]
        public ulong MinValue { get; private set; }
        [JsonProperty]
        public ulong MaxValue { get; private set; }

        public MCHellgenieSettings() : base()
        {

        }

        public override void Apply()
        {
            base.Apply();
            switch (Precision)
            {
                case 1:
                    HellgenieEngine.MinValue8Bit = MinValue;
                    HellgenieEngine.MaxValue8Bit = MaxValue;
                    break;
                case 2:
                    HellgenieEngine.MinValue16Bit = MinValue;
                    HellgenieEngine.MaxValue16Bit = MaxValue;
                    break;
                case 4:
                    HellgenieEngine.MinValue32Bit = MinValue;
                    HellgenieEngine.MaxValue32Bit = MaxValue;
                    break;
                case 8:
                    HellgenieEngine.MinValue64Bit = MinValue;
                    HellgenieEngine.MaxValue64Bit = MaxValue;
                    break;
            }
        }

        public override void Extract(CorruptionEngineForm form)
        {
            base.Extract(form);

            switch (RtcCore.CurrentPrecision)
            {
                case 1:
                    MinValue = HellgenieEngine.MinValue8Bit;
                    MaxValue = HellgenieEngine.MaxValue8Bit;
                    break;
                case 2:
                    MinValue = HellgenieEngine.MinValue16Bit;
                    MaxValue = HellgenieEngine.MaxValue16Bit;
                    break;
                case 4:
                    MinValue = HellgenieEngine.MinValue32Bit;
                    MaxValue = HellgenieEngine.MaxValue32Bit;
                    break;
                case 8:
                    MinValue = HellgenieEngine.MinValue64Bit;
                    MaxValue = HellgenieEngine.MaxValue64Bit;
                    break;
            }

        }

        public override void UpdateUI(CorruptionEngineForm form)
        {
            base.UpdateUI(form);
            var c = form.HellgenieEngineControl;
            c.nmMaxValueHellgenie.Value = MaxValue;
            c.nmMinValueHellgenie.Value = MinValue;
        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return new BlastUnit[] { HellgenieEngine.GenerateUnit(domain,address,precision,alignment) };
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Hellgenie"} [{this.Precision}, {MinValue.ToString("X")}-{MaxValue.ToString("X")}]";
        }
    }
}
