using Ceras;
using MultiEngine.Structures;
using Newtonsoft.Json;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.NetCore.NetCoreExtensions;
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

        public static MultiCorruptSettingsPack Pack
        {
            get
            {
                if (_pack == null)
                    Pack = new MultiCorruptSettingsPack();

                return _pack;
            }
            set { _pack = value; }
        }
        private static MultiCorruptSettingsPack _pack = null;

        private static bool corrupting = false;

        private static CerasSerializer saveSerializer;


        private CorruptionEngineForm originalEngineForm = null;

        public MultiEngineForm()
        {
            InitializeComponent();
            //pack = new MultiCorruptSettingsPack();
            saveSerializer = CreateSerializer();
           
            ContextMenu menu = new ContextMenu(new MenuItem[]
            {
                new MenuItem("Delete Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        var item = lbEngines.SelectedItem as MCSettingsBase;
                        Pack.RemoveSetting(item);
                        lbEngines.Items.Remove(item);
                        PushSettings();
                        UpdateList();
                    }
                }),
                new MenuItem("Edit Selected", (o,e) =>
                {
                    if(lbEngines.SelectedItem != null)
                    {
                        int multiEngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
                        C.CacheMasterSpec();
                        gbMain.Enabled = false;
                        var item = lbEngines.SelectedItem as MCSettingsBase;
                        var esf = new EngineSettingsForm(item);

                        if (esf.ShowDialog() == DialogResult.OK)
                        {
                            int idx = Pack.WeightedSettings.IndexOf(item);
                            Pack.WeightedSettings.RemoveAt(idx);
                            Pack.WeightedSettings.Insert(idx, esf.OutputSettings);
                            UpdateList();
                            PushSettings();
                            S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
                        }
                        C.RestoreMasterSpec(true);
                        S.GET<CorruptionEngineForm>().ResyncAllEngines();
                        S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = multiEngineIndex;
                        gbMain.Enabled = true;
                    }
                })
            });
            originalEngineForm = S.GET<CorruptionEngineForm>();
            lbEngines.ContextMenu = menu;

            imgWarning.Image = System.Drawing.SystemIcons.Warning.ToBitmap();
            ToolTip warningToolTip = new ToolTip();
            warningToolTip.ToolTipIcon = ToolTipIcon.Warning;
            warningToolTip.ToolTipTitle = "Desync Warning";
            warningToolTip.SetToolTip(imgWarning, "Using multi engine will desync other engine controls.\r\nPlease update all fields including precision and alignment if using other engines");
            FormClosing += MultiEngineForm_FormClosing;
            //cbLimiterList.DisplayMember = "Name";
            //cbValueList.DisplayMember = "Name";
            //cbLimiterList.DataSource = RTCV.CorruptCore.RtcCore.LimiterListBindingSource;
            //cbValueList.DataSource = RTCV.CorruptCore.RtcCore.ValueListBindingSource;
        }



        private void PushSettings()
        {
            LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SETTINGS, Pack, true);
        }

        private void MultiEngineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            corrupting = false;
        }

        

        

        //Todo: remove
        [Obsolete]
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
                if (!corrupting && Pack.WeightedSettings.Count > 0)
                {
                    try
                    {
                        corrupting = true;
                        //Corrupt();
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

        private async void bAdd_Click(object sender, EventArgs e)
        {
            //Cache before changes
            C.CacheMasterSpec();
            //PartialSpec pspec = AllSpec.CorruptCoreSpec.GetPartialSpec();
            int multiEngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
            var f = new EngineSettingsForm();
            gbMain.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                Pack.AddSetting(f.OutputSettings);
                UpdateList();
                LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SETTINGS, Pack, true);
                //TODO: remove?
                //S.GET<MemoryDomainsForm>().RefreshDomainsAndKeepSelected();
            }
            //Reset spec
            C.RestoreMasterSpec(true);
            S.GET<CorruptionEngineForm>().ResyncAllEngines();
            S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = multiEngineIndex;
            gbMain.Enabled = true;
            //TODO: Make sure we are selected?
            //RtcCore.SelectedEngine = CorruptionEngine.PLUGIN;
            //AllSpec.CorruptCoreSpec.Update(RTCSPEC.CORE_SELECTEDENGINE, CorruptionEngine.PLUGIN);

        }

        void UpdateList()
        {
            lbEngines.SuspendLayout();
            lbEngines.Items.Clear();

            for (int i = 0; i < Pack.WeightedSettings.Count; i++)
            {
                lbEngines.Items.Add(Pack.WeightedSettings[i]);
            }
            lbEngines.ResumeLayout();
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Multi Engine Files|*.me2" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //string json = JsonHelper.Serialize(Pack);
                        //File.WriteAllText(sfd.FileName, json);
                        var bytes = saveSerializer.Serialize(Pack);
                        File.WriteAllBytes(sfd.FileName, bytes);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Unable to save Multi Engine file: " + ex.Message);
                    }
                }
            }
        }

        //Copied from TCPLink.cs line 235
        private static CerasSerializer CreateSerializer()
        {
            var config = new SerializerConfig();
            config.Advanced.PersistTypeCache = true;
            config.Advanced.UseReinterpretFormatter = false; //While faster, leads to some weird bugs due to threading abuse
            config.Advanced.RespectNonSerializedAttribute = false;
            config.OnResolveFormatter.Add((c, t) =>
            {
                if (t == typeof(HashSet<byte[]>))
                {
                    return new HashSetFormatterThatKeepsItsComparer();
                }
                else if (t == typeof(HashSet<byte?[]>))
                {
                    return new NullableByteHashSetFormatterThatKeepsItsComparer();
                }

                return null; // continue searching
            });
            return new CerasSerializer(config);
        }


        private void bLoad_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() {Filter = "Multi Engine Files|*.me2" })
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    Pack = saveSerializer.Deserialize<MultiCorruptSettingsPack>(File.ReadAllBytes(ofd.FileName));
                    //Pack = JsonConvert.DeserializeObject<MultiCorruptSettingsPack>(File.ReadAllText(ofd.FileName), new MultiEngineJsonConverter());
                    UpdateList();
                    PushSettings();
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
                    var item = Pack.WeightedSettings[idx];
                    Pack.WeightedSettings.RemoveAt(idx);
                    Pack.WeightedSettings.Insert(idx - 1, item);
                    UpdateList();
                    PushSettings();
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
                    var item = Pack.WeightedSettings[idx];
                    Pack.WeightedSettings.RemoveAt(idx);
                    Pack.WeightedSettings.Insert(idx + 1, item);
                    UpdateList();
                    PushSettings();
                    lbEngines.SelectedIndex = idx + 1;
                }
            }
        }

        public void OnEngineSelected()
        {
            //pspec = null;//Copy main spec //AllSpec.CorruptCoreSpec.GetPartialSpec();
        }

        public void OnEngineDeselected()
        {
            //AllSpec.CorruptCoreSpec.Update(pspec, true, true); //Revert entire spec
        }

    }


}
