using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MultiCorruptSettingsPack
    {
        //TODO: remove weighted stuff
        public List<MCSettingsBase> Settings { get; private set; }
        private static Random rand = new Random();

        public MultiCorruptSettingsPack()
        {
            //Weighted doesn't work :/
            Settings = new List<MCSettingsBase>();
        }

        public void AddSetting(MCSettingsBase setting)
        {
            Settings.Add(setting);
            //RatioSum = WeightedSettings.Sum(p => p.Weight);
        }
        public void RemoveSetting(MCSettingsBase setting)
        {
            Settings.Remove(setting);
           // RatioSum = WeightedSettings.Sum(p => p.Weight);
        }

        public MCSettingsBase GetRandomSettings()
        {
            if (Settings.Count == 0) return null;
            else return Settings[rand.Next(Settings.Count)];
            //double numericValue = r.NextDouble() * RatioSum;

            //foreach (var engineSettings in WeightedSettings)
            //{
            //    numericValue -= engineSettings.Weight;

            //    if (!(numericValue <= 0))
            //    {
            //        continue;
            //    }

            //    return engineSettings;
            //}

            ////Will never call this probably
            //return null;
        }
    }

}
