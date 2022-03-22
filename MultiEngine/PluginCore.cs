using MultiEngine.UI;
using RTCV.Common;
using RTCV.NetCore;
using RTCV.PluginHost;
using RTCV.UI;
using System;
using System.ComponentModel.Composition;
using System.Windows.Forms;

namespace MultiEngine
{
    [Export(typeof(IPlugin))]
    public class PluginCore : IPlugin, IDisposable
    {
        public string Name => "Multi Engine";
        public string Description => "Allows you to blast with multiple configurations";

        public string Author => "NullShock78";

        public Version Version => new Version(1, 0, 0);

        /// <summary>
        /// Defines which sides will call Start
        /// </summary>
        public RTCSide SupportedSide => RTCSide.Both;
        internal static RTCSide CurrentSide = RTCSide.Both;

        internal static PluginConnectorEMU connectorEMU = null;
        internal static PluginConnectorRTC connectorRTC = null;
        public void Dispose()
        {
        }

        public bool Start(RTCSide side)
        {

            C.InitMasterSpec();

            if (side == RTCSide.Client)
            {
                connectorEMU = new PluginConnectorEMU();
            }
            else if (side == RTCSide.Server)
            {
                connectorRTC = new PluginConnectorRTC();

                var form = new MultiEngineForm();
                S.SET<MultiEngineForm>(form);
                form.TopLevel = false;

                S.GET<CorruptionEngineForm>().RegisterPluginEngine(new Structures.MultiEngine_InterfaceImplementation(form));

                //form.Activate();

                /*
                S.GET<OpenToolsForm>().RegisterTool("Multi Engine", "Multi Engine", () =>
                {
                    var form = new MultiEngineForm();
                    S.SET<MultiEngineForm>(form);
                    form.Show();
                    form.Activate();
                });
                */

            }
            CurrentSide = side;
            return true;
        }

        public bool StopPlugin()
        {
            if (CurrentSide == RTCSide.Server && !S.ISNULL<MultiEngineForm>() && !S.GET<MultiEngineForm>().IsDisposed)
            {
                S.GET<MultiEngineForm>().Close();
            }
            return true;
        }
    }
}
