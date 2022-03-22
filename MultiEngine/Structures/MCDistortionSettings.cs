using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    class MCDistortionSettings : MCSettingsBase
    {
        //[JsonProperty]
        public int Delay => CachedSpec.Get<int>(RTCSPEC.DISTORTION_DELAY);

        public MCDistortionSettings() : base()
        {

        }

        //public override void Apply()
        //{
        //    PartialSpec partial = CreateBaseUpdateSpec();
        //    partial[RTCSPEC.DISTORTION_DELAY] = Delay;
        //    AllSpec.CorruptCoreSpec.Update(partial);
        //    //DistortionEngine.Delay = Delay;
        //}

        protected override PartialSpec BuildUpdateSpec(PartialSpec partial)
        {
            partial.Insert(DistortionEngine.getDefaultPartial());
            return partial;
        }

        public override void Extract(CorruptionEngineForm form)
        {
            //Delay = (int)form.DistortionEngineControl.nmDistortionDelay.Value;
            base.Extract(form);
        }

        public override void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            base.UpdateUI(form, updateSelected);
            form.DistortionEngineControl.nmDistortionDelay.Value = Delay;
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
