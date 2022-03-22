using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCPipeSettings : MCSettingsBase
    {
        //not much to do here
        public MCPipeSettings() : base()
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
            return new BlastUnit[] { PipeEngine.GenerateUnit(domain,address,precision,alignment) };
        }

        protected override PartialSpec BuildUpdateSpec(PartialSpec partial)
        {
            //No partial for pipe
            //partial.Insert(PipeEngine.getDefaultPartial());
            return partial;
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Pipe"} [{this.Precision}]";
        }
    }
}
