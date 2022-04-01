using Ceras;
using MultiEngine.UI;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
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
        bool ICorruptionEngine.SupportsCustomPrecision => true;
        bool ICorruptionEngine.SupportsAutoCorrupt => true;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;

        [Exclude]
        Form ICorruptionEngine.Control { get { return engineForm; } }

        [Exclude]
        MultiEngineForm engineForm { get; set; } = null;


        public MultiEngine_InterfaceImplementation() //empty constructor required by ceras
        {

        }

        public MultiEngine_InterfaceImplementation(MultiEngineForm _control)
        {
            engineForm = _control;
        }

        BlastLayer ICorruptionEngine.GetBlastLayer(long intensity)
        {
           
            
            try
            {   
                //Cache spec
                C.CacheMasterSpec();

                var domains = RTCV.NetCore.AllSpec.UISpec["SELECTEDDOMAINS"] as string[];
                if (domains == null || domains.Length == 0)
                {
                    MessageBox.Show("Can't corrupt with no domains selected.");
                    return null;
                }
                return MultiEngineCore.Corrupt();
            }
            catch
            {
                throw;
            }
            finally
            {
                //Revert entire spec on this side, no update
                C.RestoreMasterSpec(false);
            }
        }


        string ICorruptionEngine.ToString() => engineForm?.ToString();

        void ICorruptionEngine.OnSelect()
        {
            S.GET<MultiEngineForm>().OnEngineSelected();
        }

        void ICorruptionEngine.OnDeselect()
        {
            S.GET<MultiEngineForm>().OnEngineDeselected();
        }
    }
}
