using RTCV.CorruptCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MultiEngine.Structures
{
    public class MultiEngine_InterfaceImplementation : ICorruptionEngine
    {
        bool ICorruptionEngine.SupportsCustomPrecision => false;
        bool ICorruptionEngine.SupportsAutoCorrupt => true;
        bool ICorruptionEngine.SupportsGeneralParameters => true;
        bool ICorruptionEngine.SupportsMemoryDomains => true;
        Form ICorruptionEngine.Control { get { return control; } }

        Form control { get; set; } = null;

        public MultiEngine_InterfaceImplementation(Form _control)
        {
            control = _control;
        }

        BlastLayer ICorruptionEngine.GetBlastLayer(long intensity)
        {
            return null;
        }

        string ICorruptionEngine.ToString() => control?.ToString();
    }
}
