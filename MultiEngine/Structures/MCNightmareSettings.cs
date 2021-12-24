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
    class MCNightmareSettings : MCSettingsBase
    {
        [JsonProperty]
        public NightmareAlgo Algo { get; private set; }
        [JsonProperty]
        public int AlgoIndex { get; private set; }
        [JsonProperty]
        public ulong MinValue { get; private set; }
        [JsonProperty]
        public ulong MaxValue { get; private set; }
        public MCNightmareSettings() : base() { }
        public void ApplySettings() { }

        /// <summary>
        /// For non-ui based updates
        /// </summary>
        public override void Apply()
        {
            base.Apply();
            RTCV.CorruptCore.NightmareEngine.Algo = Algo;
            switch (Precision)
            {
                case 1:
                    NightmareEngine.MinValue8Bit = MinValue;
                    NightmareEngine.MaxValue8Bit = MaxValue;
                    break;
                case 2:
                    NightmareEngine.MinValue16Bit = MinValue;
                    NightmareEngine.MaxValue16Bit = MaxValue;
                    break;
                case 4:
                    NightmareEngine.MinValue32Bit = MinValue;
                    NightmareEngine.MaxValue32Bit = MaxValue;
                    break;
                case 8:
                    NightmareEngine.MinValue64Bit = MinValue;
                    NightmareEngine.MaxValue64Bit = MaxValue;
                    break;
            }
        }

        /// <summary>
        /// Do this on main settings form to apply
        /// </summary>
        /// <param name="form"></param>
        public override void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            base.UpdateUI(form, updateSelected);
            var c = form.NightmareEngineControl;
            c.cbBlastType.SelectedIndex = AlgoIndex;
            c.nmMaxValueNightmare.Value = MaxValue;
            c.nmMinValueNightmare.Value = MinValue;
        }

        public override void Extract(CorruptionEngineForm form)
        {
            base.Extract(form);
            //Do
            var ne = form.NightmareEngineControl;
            AlgoIndex = ne.cbBlastType.SelectedIndex;
            switch (ne.cbBlastType.SelectedItem.ToString())
            {
                case "RANDOM":
                    Algo = NightmareAlgo.RANDOM;
                    break;

                case "RANDOMTILT":
                    Algo = NightmareAlgo.RANDOMTILT;
                    break;
                case "TILT":
                    Algo = NightmareAlgo.TILT;
                    break;
            }

            MinValue = Convert.ToUInt64(ne.nmMinValueNightmare.Value);
            MaxValue = Convert.ToUInt64(ne.nmMaxValueNightmare.Value);
        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return new BlastUnit[] { NightmareEngine.GenerateUnit(domain, address, precision, alignment) };
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Nightmare"} [{this.Precision},{MinValue.ToString("X")}-{MaxValue.ToString("X")}]";
        }

    }

    

}
