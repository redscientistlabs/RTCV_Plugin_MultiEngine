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
        public List<MCSettingsBase> WeightedSettings { get; private set; }
        private static Random rand;

        public double RatioSum
        {
            get; private set;
        }

        static MultiCorruptSettingsPack()
        {
            rand = new Random();
        }

        public MultiCorruptSettingsPack()
        {
            //Weighted doesn't work :/
            WeightedSettings = new List<MCSettingsBase>();
        }

        public void AddSetting(MCSettingsBase setting)
        {
            WeightedSettings.Add(setting);
            //RatioSum = WeightedSettings.Sum(p => p.Weight);
        }
        public void RemoveSetting(MCSettingsBase setting)
        {
            WeightedSettings.Remove(setting);
           // RatioSum = WeightedSettings.Sum(p => p.Weight);
        }

        /// <summary>
        /// Update main UI so there is no desync
        /// </summary>
        public void ApplyMostRecentUISettings()
        {

        }

        public MCSettingsBase GetRandomSettings()
        {
            if (WeightedSettings.Count == 0) return null;
            else return WeightedSettings[rand.Next(WeightedSettings.Count)];
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
