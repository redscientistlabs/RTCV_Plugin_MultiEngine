using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ceras;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
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
        public string LimiterListHash => CachedSpec.Get<string>("CLUSTER_LIMITERLISTHASH");
        public int ChunkSize => CachedSpec.Get<int>("CLUSTER_SHUFFLEAMT");
        public string ShuffleType => CachedSpec.Get<string>("CLUSTER_SHUFFLETYPE");
        public int Modifier => CachedSpec.Get<int>("CLUSTER_MODIFIER");
        public bool OutputMultipleUnits => CachedSpec.Get<bool>("CLUSTER_MULTIOUT");
        public bool FilterAll => CachedSpec.Get<bool>("CLUSTER_FILTERALL");
        public string Direction => CachedSpec?.Get<string>("CLUSTER_DIR");



        public MCClusterSettings() : base()
        {

        }

        public override void Extract(CorruptionEngineForm form)
        {
            LimiterList = ((ComboBoxItem<string>)form.ClusterEngineControl.cbClusterLimiterList.SelectedItem)?.Name.ToString();
            base.Extract(form);
            //LimiterListHash = ClusterEngine.LimiterListHash;
            //ChunkSize = ClusterEngine.ChunkSize;
            //ShuffleType = ClusterEngine.ShuffleType;
            //Modifier = ClusterEngine.Modifier;
            //OutputMultipleUnits = ClusterEngine.OutputMultipleUnits;
            //FilterAll = ClusterEngine.FilterAll;
            //Direction = ClusterEngine.Direction;
        }

        protected override PartialSpec BuildUpdateSpec(PartialSpec partial)
        {
            partial.Insert(ClusterEngine.getDefaultPartial());

            //partial["CLUSTER_LIMITERLISTHASH"] = LimiterListHash;
            //partial["CLUSTER_SHUFFLETYPE"] = ShuffleType;
            //partial["CLUSTER_SHUFFLEAMT"] = ChunkSize;
            //partial["CLUSTER_MODIFIER"] = Modifier;
            //partial["CLUSTER_MULTIOUT"] = OutputMultipleUnits;
            //partial["CLUSTER_FILTERALL"] = FilterAll;
            //partial["CLUSTER_DIR"] = Direction;
            return partial;
        }

        //public override void Apply()
        //{
        //    PartialSpec partial = UpdateSpec;
        //    partial["CLUSTER_LIMITERLISTHASH"] = LimiterListHash;
        //    partial["CLUSTER_SHUFFLETYPE"] = ShuffleType;
        //    partial["CLUSTER_SHUFFLEAMT"] = ChunkSize;
        //    partial["CLUSTER_MODIFIER"] = Modifier;
        //    partial["CLUSTER_MULTIOUT"] = OutputMultipleUnits;
        //    partial["CLUSTER_FILTERALL"] = FilterAll;
        //    partial["CLUSTER_DIR"] = Direction;
        //    AllSpec.CorruptCoreSpec.Update(partial);

        //    //ClusterEngine.getDefaultPartial();
        //    //ClusterEngine.LimiterListHash = LimiterListHash;
        //    //ClusterEngine.ChunkSize = ChunkSize;
        //    //ClusterEngine.ShuffleType = ShuffleType;
        //    //ClusterEngine.Modifier = Modifier;
        //    //ClusterEngine.OutputMultipleUnits = OutputMultipleUnits;
        //    //ClusterEngine.FilterAll = FilterAll;
        //    //ClusterEngine.Direction = Direction;
        //}

        public override void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            base.UpdateUI(form, updateSelected);
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
