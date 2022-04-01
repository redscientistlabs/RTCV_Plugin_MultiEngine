using MultiEngine.UI;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using System;
using System.Windows.Forms;
using MultiEngine.Structures;

namespace MultiEngine
{
    /// <summary>
    /// This lies on the Emulator(Client) side
    /// </summary>
    internal class PluginConnectorEMU : IRoutable
    {
        static object loadLock = new object();
        public PluginConnectorEMU()
        {
            LocalNetCoreRouter.registerEndpoint(this, PluginRouting.Endpoints.EMU_SIDE);
        }

        public object OnMessageReceived(object sender, NetCoreEventArgs e)
        {
            NetCoreAdvancedMessage message = e.message as NetCoreAdvancedMessage;

            switch (message.Type)
            {
                case PluginRouting.Commands.UPDATE_SETTINGS:
                    MultiEngineCore.SetSettings(message.objectValue as MultiCorruptSettingsPack);
                    break;
                default:
                    break;
            }
            return e.returnMessage;
        }

    }
}
