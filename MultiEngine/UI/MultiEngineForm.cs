using MultiEngine.Structures;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Modular;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MultiEngine.UI
{
    public partial class MultiEngineForm : ComponentForm, IColorize
    {

        public static MultiCorruptSettingsPack pack
        {
            get
            {
                if (_pack == null)
                    pack = new MultiCorruptSettingsPack();

                return _pack;
            }
            set { _pack = value; }
        }
        private static MultiCorruptSettingsPack _pack = null;

        private static bool corrupting = false;

        public MultiEngineForm()
        {
            InitializeComponent();
            //pack = new MultiCorruptSettingsPack();

            ContextMenu menu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Delete Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        var item = lbEngines.SelectedItem as MCSettingsBase;
                        pack.RemoveSetting(item);
                        lbEngines.Items.Remove(item);
                        UpdateList();
                    }
                }),
                new MenuItem("Edit Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        var item = lbEngines.SelectedItem as MCSettingsBase;
                        var esf = new EngineSettingsForm(item);
                        if (esf.ShowDialog() == DialogResult.OK)
                        {
                            int idx = pack.WeightedSettings.IndexOf(item);
                            pack.WeightedSettings.RemoveAt(idx);
                            pack.WeightedSettings.Insert(idx, esf.OutputSettings);
                            UpdateList();
                            S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
                        }
                    }
                })
            });

            lbEngines.ContextMenu = menu;
            FormClosing += MultiEngineForm_FormClosing;
            //cbLimiterList.DisplayMember = "Name";
            //cbValueList.DisplayMember = "Name";
            //cbLimiterList.DataSource = RTCV.CorruptCore.RtcCore.LimiterListBindingSource;
            //cbValueList.DataSource = RTCV.CorruptCore.RtcCore.ValueListBindingSource;
        }

        private void MultiEngineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            corrupting = false;
        }

        private void btnCorrupt_Click(object sender, EventArgs e)
        {
            //Corrupt();
            //pack.GetRandomSettings
            //int ct = (int)nmCorruptCT.Value;
            //for (int i = 0; i < ct; i++)
            //{
            //    var s = pack.GetRandomSettings();
            //    s.UpdateUI(S.GET<CorruptionEngineForm>());
            //    S.GET<CoreForm>().ManualBlast(null, null);
            //}

            btnCorrupt.Enabled = false;
            try
            {
                if (!corrupting && pack.WeightedSettings.Count > 0)
                {
                    try
                    {
                        corrupting = true;
                        Corrupt();
                    }
                    finally
                    {
                        corrupting = false;
                    }
                }
            }
            finally
            {
                btnCorrupt.Enabled = true;
            }
        }

        public void Corrupt()
        {
            
            var domains = RTCV.NetCore.AllSpec.UISpec["SELECTEDDOMAINS"] as string[];
            if (domains == null || domains.Length == 0)
            {
                MessageBox.Show("Can't corrupt with no domains selected.");
                return;
            }

            //Do setup on the rtc side
            foreach (var item in pack.WeightedSettings)
            {
                item.PreCorrupt();
            }

            if (S.GET<CoreForm>().AutoCorrupt)
            {
                S.GET<CoreForm>().AutoCorrupt = false;
            }
            S.GET<StashHistoryForm>().DontLoadSelectedStash = true;
            //Corrupt here

            bool success = InnerCorrupt() != null;

            if (success)
            {
                S.GET<StashHistoryForm>().RefreshStashHistorySelectLast();
            }
        }

        //Split method 
        public static BlastLayer InnerCorrupt(bool fromForm = true)
        {

            StashKey psk = StockpileManagerUISide.CurrentSavestateStashKey;
            if (psk == null)
            {
                return null;
            }
            LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, RTCV.NetCore.Commands.Remote.ClearStepBlastUnits, null, true);
            LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.PreCorruptAction, null, true);
            
            string currentGame = (string)RTCV.NetCore.AllSpec.VanguardSpec[VSPEC.GAMENAME];
            string currentCore = (string)RTCV.NetCore.AllSpec.VanguardSpec[VSPEC.SYSTEMCORE];
            bool UseSavestates = (bool)AllSpec.VanguardSpec[VSPEC.SUPPORTS_SAVESTATES];
            if (UseSavestates && (currentGame == null || psk.GameName != currentGame || psk.SystemCore != currentCore))
            {
                LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.LoadROM, psk.RomFilename, true);
            }
            //LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.CorruptCore, "NULLS TEST AFTER LOADROM", null, true);
            StockpileManagerUISide.CurrentStashkey = new StashKey(RtcCore.GetRandomKey(), psk.ParentKey, null)
            {
                RomFilename = psk.RomFilename,
                SystemName = psk.SystemName,
                SystemCore = psk.SystemCore,
                GameName = psk.GameName,
                SyncSettings = psk.SyncSettings,
                StateLocation = psk.StateLocation
            };

            //Get blast layer

            object[] routeVal = (object[])LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.CORRUPT,
               new object[] {
                    StockpileManagerUISide.CurrentStashkey,
                    pack
               }, true);
            if (routeVal == null)
            {
                LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.PostCorruptAction);
                return null;
            }

            int[] usedIndices = routeVal[0] as int[];
            BlastLayer bl = routeVal[1] as BlastLayer;

            //Update main ui values to keep synced settings
            var ceForm = S.GET<CorruptionEngineForm>();
            for (int i = 0; i < usedIndices.Length; i++)
            {
                if (usedIndices[i] > -1)
                {
                    pack.WeightedSettings[usedIndices[i]]?.UpdateUI(ceForm);
                }
            }

            if (fromForm)
            {
                StockpileManagerUISide.CurrentStashkey.BlastLayer = bl;
                if (bl != null && bl.Layer.Count > 0)
                {
                    StockpileManagerUISide.StashHistory.Add(StockpileManagerUISide.CurrentStashkey);
                }
                else
                {
                    //throw new Exception("BL WAS NULL ON RETURN");
                }
            }

            LocalNetCoreRouter.Route(RTCV.NetCore.Endpoints.Vanguard, RTCV.NetCore.Commands.Remote.PostCorruptAction);

            return bl;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            var f = new EngineSettingsForm();
            if (f.ShowDialog() == DialogResult.OK)
            {
                pack.AddSetting(f.OutputSettings);
                UpdateList();
                S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
            }
        }

        void UpdateList()
        {
            lbEngines.SuspendLayout();
            lbEngines.Items.Clear();

            for (int i = 0; i < pack.WeightedSettings.Count; i++)
            {
                lbEngines.Items.Add(pack.WeightedSettings[i]);
            }
            lbEngines.ResumeLayout();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Multi Engine Files|*.mec" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string json = JsonHelper.Serialize(pack);
                        File.WriteAllText(sfd.FileName, json);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to save Multi Engine file: " + ex.Message);
                    }
                }
            }
        }

        private void bLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter = "Multi Engine Files|*.mec" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {

                    pack = JsonConvert.DeserializeObject<MultiCorruptSettingsPack>(File.ReadAllText(ofd.FileName), new MultiEngineJsonConverter());
                    UpdateList();
                }
            }

        }

        private void bUp_Click(object sender, EventArgs e)
        {
            if (lbEngines.SelectedItem != null)
            {
                var idx = lbEngines.SelectedIndex;
                if (idx > 0)
                {
                    var item = pack.WeightedSettings[idx];
                    pack.WeightedSettings.RemoveAt(idx);
                    pack.WeightedSettings.Insert(idx - 1, item);
                    UpdateList();
                    lbEngines.SelectedIndex = idx - 1;
                }
            }
        }

        private void bDown_Click(object sender, EventArgs e)
        {
            if (lbEngines.SelectedItem != null)
            {
                var idx = lbEngines.SelectedIndex;
                if (idx < (lbEngines.Items.Count-1))
                {
                    var item = pack.WeightedSettings[idx];
                    pack.WeightedSettings.RemoveAt(idx);
                    pack.WeightedSettings.Insert(idx + 1, item);
                    UpdateList();
                    lbEngines.SelectedIndex = idx + 1;
                }
            }
        }
    }


}
