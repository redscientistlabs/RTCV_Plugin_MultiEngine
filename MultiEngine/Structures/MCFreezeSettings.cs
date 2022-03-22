using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCFreezeSettings : MCSettingsBase
    {
        public MCFreezeSettings() : base()
        {

        }
        //public override void Extract(CorruptionEngineForm form)
        //{
        //    base.Extract(form);
        //}

        public override void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            base.UpdateUI(form, updateSelected);
        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return new BlastUnit[] { FreezeEngine.GenerateUnit(domain, address, precision, alignment) };
        }

        protected PartialSpec BuildUpdateSpec(PartialSpec partial)
        {
            //no default partial here
            return partial;
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Freeze"} [{this.Precision}]";
        }
    }
}
