using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;
using MultiEngine.Structures;
using RTCV.NetCore;
using RTCV.Common.CustomExtensions;

namespace MultiEngine
{
    static class MultiEngineCore
    {
        static MultiCorruptSettingsPack pack = null;

        public static void SetSettings(MultiCorruptSettingsPack settingPack)
        {
            pack = settingPack;
        }

        //TODO: remove
        public static BlastLayer Corrupt(int[] lastUsedIndices)
        {
            List<BlastUnit> bus = new List<BlastUnit>();
            if (pack == null) return new BlastLayer(bus);

            for (int settingInd = 0; settingInd < pack.WeightedSettings.Count; settingInd++)
            {
                var setting = pack.WeightedSettings[settingInd];
                lastUsedIndices[setting.EngineIndex()] = settingInd;//For updating UI
                setting.ApplyPartial();
                long intensity = RtcCore.Intensity;
                int precision = RtcCore.CurrentPrecision;
                int alignment = RtcCore.Alignment;

                long settingIntensity = setting.ForcedIntensity > 0 ? setting.ForcedIntensity : (long)(setting.Percentage * intensity);
                var validDoms = (setting.Domains != null && setting.Domains.Length > 0)  ? GetValidDomains(setting.Domains) : null;
                string[] domains = validDoms != null ? validDoms : (string[])AllSpec.UISpec["SELECTEDDOMAINS"];

                if (domains == null || domains.Length == 0) continue;

                for (int i = 0; i < settingIntensity; i++)
                {
                    string dom = domains[RtcCore.RND.Next(domains.Length)];
                    MemoryInterface mi = MemoryDomains.GetInterface(dom);
                    //TODO: switch on engine name
                    bus.AddRange(setting.GetBlastUnits(dom, RtcCore.RND.NextLong(0, mi.Size-(precision*2)), precision, alignment));
                }
            }
            return new BlastLayer(bus.Where(x => x != null).ToList());
        }

        private static string[] GetValidDomains(params string[] doms)
        {
            return doms.Where(x => MemoryDomains.AllMemoryInterfaces.ContainsKey(x)).ToArray();
        }

    }

}
