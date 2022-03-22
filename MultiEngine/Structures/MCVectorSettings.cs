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
    class MCVectorSettings : MCSettingsBase
    {

        //[JsonProperty]
        public string LimiterList { get; set; }
        //[JsonProperty]
        public string LimiterListHash => CachedSpec?.Get<string>(RTCSPEC.VECTOR_LIMITERLISTHASH);
        //[JsonProperty]
        public string ValueList { get; set; }
        //[JsonProperty]
        public string ValueListHash => CachedSpec?.Get<string>(RTCSPEC.VECTOR_VALUELISTHASH);
        //[JsonProperty]
        public bool UnlockPrecision => CachedSpec.Get<bool>(RTCSPEC.VECTOR_UNLOCKPRECISION);

        public MCVectorSettings() : base()
        {
            
        }

        //public override void Apply()
        //{
        //    base.Apply();
        //    VectorEngine.UnlockPrecision = UnlockPrecision;
        //    VectorEngine.ValueListHash = ValueListHash;
        //    VectorEngine.LimiterListHash = LimiterListHash;
        //}

        public override void UpdateUI(CorruptionEngineForm form, bool updateSelected = true)
        {
            base.UpdateUI(form, updateSelected);

            //Do first
            form.VectorEngineControl.cbVectorUnlockPrecision.Checked = UnlockPrecision;

            var cbL = form.VectorEngineControl.cbVectorLimiterList;
            var cbV = form.VectorEngineControl.cbVectorValueList;

            var cblItems = cbL.Items;
            var cbvItems = cbV.Items;

            bool found = false;
            foreach (var cblItem in cblItems)
            {
                if (((ComboBoxItem<string>)cblItem).Name == LimiterList)
                {
                    foreach (var cbvItem in cbvItems)
                    {
                        if (((ComboBoxItem<string>)cbvItem).Name == ValueList)
                        {
                            cbL.SelectedItem = cblItem;
                            VectorEngine.LimiterListHash = ((ComboBoxItem<string>)cblItem).Value;
                            cbV.SelectedItem = cbvItem;
                            VectorEngine.ValueListHash = ((ComboBoxItem<string>)cbvItem).Value;
                            found = true;
                            break;
                        }
                    }

                    if (found) break;
                }
            }
        }

        public override void Extract(CorruptionEngineForm form)
        {
            
            LimiterList = ((ComboBoxItem<string>)form.VectorEngineControl.cbVectorLimiterList.SelectedItem)?.Name.ToString();
            ValueList = ((ComboBoxItem<string>)form.VectorEngineControl.cbVectorValueList.SelectedItem)?.Name.ToString();

            //UnlockPrecision = form.VectorEngineControl.cbVectorUnlockPrecision.Checked;

            ////idk
            //var cbL = form.VectorEngineControl.cbVectorLimiterList;
            //var cbV = form.VectorEngineControl.cbVectorValueList;

            //var cblItems = cbL.Items;
            //var cbvItems = cbV.Items;

            //bool found = false;
            //foreach (var cblItem in cblItems)
            //{
            //    if (((ComboBoxItem<string>)cblItem).Name == LimiterList)
            //    {
            //        foreach (var cbvItem in cbvItems)
            //        {
            //            if (((ComboBoxItem<string>)cbvItem).Name == ValueList)
            //            {
            //                LimiterListHash = ((ComboBoxItem<string>)cblItem).Value;
            //                ValueListHash = ((ComboBoxItem<string>)cbvItem).Value;
            //                found = true;
            //                break;
            //            }
            //        }

            //        if (found) break;
            //    }
            //}

            base.Extract(form);

        }

        public override BlastUnit[] GetBlastUnits(string domain, long address, int precision, int alignment)
        {
            return new BlastUnit[] { VectorEngine.GenerateUnit(domain,address,alignment) };
        }

        ////Re-Get the hashes just in case
        //public override void PreCorrupt()
        //{
        //    //var cbL = S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorLimiterList;
        //    //var cbV = S.GET<CorruptionEngineForm>().VectorEngineControl.cbVectorValueList;

        //    //var cblItems = cbL.Items;
        //    //var cbvItems = cbV.Items;

        //    //bool found = false;
        //    //foreach (var cblItem in cblItems)
        //    //{
        //    //    if (((ComboBoxItem<string>)cblItem).Name == LimiterList)
        //    //    {
        //    //        foreach (var cbvItem in cbvItems)
        //    //        {
        //    //            if (((ComboBoxItem<string>)cbvItem).Name == ValueList)
        //    //            {
        //    //                LimiterListHash = ((ComboBoxItem<string>)cblItem).Value;
        //    //                ValueListHash = ((ComboBoxItem<string>)cbvItem).Value;
        //    //                found = true;
        //    //                break;
        //    //            }
        //    //        }
        //    //        if (found) break;
        //    //    }
        //    //}

        //    //if (!found)
        //    //{
        //    //    //Throw warning?
        //    //}


        //}

        public override string ToString()
        {
            return $"{PercentageString} {DisplayName ?? "Vector"} [{LimiterList},{ValueList}]";
        }

        protected override PartialSpec BuildUpdateSpec(PartialSpec partial)
        {
            partial.Insert(VectorEngine.getDefaultPartial());
            return partial;
        }
    }
}
