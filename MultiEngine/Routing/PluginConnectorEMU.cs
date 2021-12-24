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
                case PluginRouting.Commands.CORRUPT:

                    var val = message.objectValue as object[];

                    StashKey sk = val[0] as StashKey;
                    MultiCorruptSettingsPack pack = val[1] as MultiCorruptSettingsPack;

                    BlastLayer bl = null;
                    bool UseSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES];
                    int[] lsu = C.GetEngineArray();
                    void a()
                    {
                        lock (loadLock)
                        {
                            if (UseSavestates)
                            {
                                SyncObjectSingleton.FormExecute(() =>
                                {
                                    StockpileManagerEmuSide.LoadRomNet(sk);
                                });

                                StockpileManagerEmuSide.LoadStateNet(sk, false);
                            }

                            bl = MultiEngineCore.Corrupt(lsu);
                            bl?.Apply(false);
                        }
                    }

                    if ((bool?)AllSpec.VanguardSpec[VSPEC.LOADSTATE_USES_CALLBACKS] ?? false)
                    {
                        SyncObjectSingleton.FormExecute(a);
                        e.setReturnValue(LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.ResumeEmulation, true));
                    }
                    else //We can just do everything on the emulation thread as it'll block
                    {
                        SyncObjectSingleton.EmuThreadExecute(a, true);
                    }

                    if (message.requestGuid != null)
                    {
                        //Set return value to object[] {prevUsed,bl}
                        e.setReturnValue(new object[] { lsu, bl });
                    }
                    break;
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
