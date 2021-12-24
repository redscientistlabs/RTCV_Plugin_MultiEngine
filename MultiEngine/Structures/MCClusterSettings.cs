using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.Structures
{
    [Serializable]
    [Ceras.MemberConfig(TargetMember.All)]
    public class MCClusterSettings : MCSettingsBase
    {
        [JsonProperty]
        public string LimiterList { get; private set; }
        [JsonProperty]
        public string LimiterListHash { get; private set; }
        [JsonProperty]
        public int ChunkSize { get; private set; }
        [JsonProperty]
        public string ShuffleType { get; private set; }
        [JsonProperty]
        public int Modifier { get; private set; }
        [JsonProperty]
        public bool OutputMultipleUnits { get; private set; }
        [JsonProperty]
        public bool FilterAll { get; private set; }
        [JsonProperty]
        public string Direction { get; private set; }


        public MCClusterSettings() : base()
        {

        }

        public override void Extract(CorruptionEngineForm form)
        {
            base.Extract(form);
            LimiterList = ((ComboBoxItem<string>)form.ClusterEngineControl.cbClusterLimiterList.SelectedItem)?.Name.ToString();
            LimiterListHash = ClusterEngine.LimiterListHash;
            ChunkSize = ClusterEngine.ChunkSize;
            ShuffleType = ClusterEngine.ShuffleType;
            Modifier = ClusterEngine.Modifier;
            OutputMultipleUnits = ClusterEngine.OutputMultipleUnits;
            FilterAll = ClusterEngine.FilterAll;
            Direction = ClusterEngine.Direction;
        }

        public override void Apply()
        {
            base.Apply();
            ClusterEngine.LimiterListHash = LimiterListHash;
            ClusterEngine.ChunkSize = ChunkSize;
            ClusterEngine.ShuffleType = ShuffleType;
            ClusterEngine.Modifier = Modifier;
            ClusterEngine.OutputMultipleUnits = OutputMultipleUnits;
            ClusterEngine.FilterAll = FilterAll;
            ClusterEngine.Direction = Direction;
        }

        public override void UpdateUI(CorruptionEngineForm form)
        {
            base.UpdateUI(form);
            var ce = form.ClusterEngineControl;
            ce.cbClusterMethod.SelectedIndex = ce.cbClusterMethod.Items.IndexOf(this.ShuffleType);

            var cbL = ce.cbClusterLimiterList;

            var cblItems = cbL.Items;

            foreach (var cblItem in cblItems)
            {
                if (((ComboBoxItem<string>)cblItem).Name == LimiterList)
                {
                    cbL.SelectedItem = cblItem;
                    break;
                }
            }

            //var ll = ce.cbClusterMethod.Items.IndexOf(LimiterList);// .Cast<string>().FirstOrDefault(x => x == LimiterList);
            //ce.cbClusterLimiterList.SelectedIndex = ll;
            ce.clusterDirection.SelectedIndex = ce.clusterDirection.Items.IndexOf(Direction);
            ce.clusterSplitUnits.Checked = OutputMultipleUnits;
            ce.clusterFilterAll.Checked = FilterAll;
            
            //internal, can't change yet
            //clusterChunkModifier
            //clusterChunkSize
        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            var u = ClusterEngine.GenerateUnit(domain, address, alignment);
            if (u == null || u.Length == 0)
            {
                return new BlastUnit[] { null };
            }
            else
            {
                return u;
            }
        }

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Cluster"} [{LimiterList},{this.ShuffleType}]";
        }

    }
}
