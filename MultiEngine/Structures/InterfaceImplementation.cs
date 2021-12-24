using Ceras;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MultiEngine_InterfaceImplementation : ICorruptionEngine
    {
        bool ICorruptionEngine.SupportsCustomPrecision => false;
        bool ICorruptionEngine.SupportsAutoCorrupt => true;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;

        [Exclude]
        Form ICorruptionEngine.Control { get { return control; } }

        [Exclude]
        Form control { get; set; } = null;


        public MultiEngine_InterfaceImplementation() //empty constructor required by ceras
        {

        }

        public MultiEngine_InterfaceImplementation(Form _control)
        {
            control = _control;
        }

        BlastLayer ICorruptionEngine.GetBlastLayer(long intensity)
        {

            var domains = RTCV.NetCore.AllSpec.UISpec["SELECTEDDOMAINS"] as string[];
            if (domains == null || domains.Length == 0)
            {
                MessageBox.Show("Can't corrupt with no domains selected.");
                return null;
            }

            //Do setup on the rtc side
            foreach (var item in UI.MultiEngineForm.Pack.WeightedSettings)
            {
                item.PreCorrupt();
            }


            // TODO REMOVE THIS AFTER OPTIMIZATION
            if (S.GET<CoreForm>().AutoCorrupt)
            {
                S.GET<CoreForm>().AutoCorrupt = false;
            }


            //S.GET<StashHistoryForm>().DontLoadSelectedStash = true;
            ////Corrupt here
            var last = C.GetEngineArray();
            return MultiEngineCore.Corrupt(last);
            //return UI.MultiEngineForm.InnerCorrupt(false);

            //if (success)
            //{
            //    S.GET<StashHistoryForm>().RefreshStashHistorySelectLast();
            //}
        }


        string ICorruptionEngine.ToString() => control?.ToString();
    }
}
