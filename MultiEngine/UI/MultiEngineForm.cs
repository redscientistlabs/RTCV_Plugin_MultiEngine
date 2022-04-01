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
                            int idx = Pack.Settings.IndexOf(item);
                            Pack.Settings.RemoveAt(idx);
                            Pack.Settings.Insert(idx, esf.OutputSettings);
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

            FormClosing += MultiEngineForm_FormClosing;
            //cbLimiterList.DisplayMember = "Name";
            //cbValueList.DisplayMember = "Name";
            //cbLimiterList.DataSource = RTCV.CorruptCore.RtcCore.LimiterListBindingSource;
            //cbValueList.DataSource = RTCV.CorruptCore.RtcCore.ValueListBindingSource;
        }



        public void PushSettings()
        {
            LocalNetCoreRouter.Route(PluginRouting.Endpoints.EMU_SIDE, PluginRouting.Commands.UPDATE_SETTINGS, Pack, true);
        }

        private void MultiEngineForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            corrupting = false;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //Cache before changes
            C.CacheMasterSpec();
            int multiEngineIndex = S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex;
            var f = new EngineSettingsForm();
            gbMain.Enabled = false;
            if (f.ShowDialog() == DialogResult.OK)
            {
                Pack.AddSetting(f.OutputSettings);
                UpdateList();
                PushSettings();
            }
            //Restore spec
            C.RestoreMasterSpec(true);
            //Resync UI
            S.GET<CorruptionEngineForm>().ResyncAllEngines();
            //Set us back to multi engine
            S.GET<CorruptionEngineForm>().cbSelectedEngine.SelectedIndex = multiEngineIndex;
            gbMain.Enabled = true;
        }

        void UpdateList()
        {
            lbEngines.SuspendLayout();
            lbEngines.Items.Clear();

            for (int i = 0; i < Pack.Settings.Count; i++)
            {
                lbEngines.Items.Add(Pack.Settings[i]);
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

        ////Copied from TCPLink.cs line 235
        private static CerasSerializer CreateSerializer()
        {
            var config = new SerializerConfig();
            config.Advanced.PersistTypeCache = false;
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
                    //Pack = JsonConvert.DeserializeObject<MultiCorruptSettingsPack>(File.ReadAllText(ofd.FileName));
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
                    var item = Pack.Settings[idx];
                    Pack.Settings.RemoveAt(idx);
                    Pack.Settings.Insert(idx - 1, item);
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
                    var item = Pack.Settings[idx];
                    Pack.Settings.RemoveAt(idx);
                    Pack.Settings.Insert(idx + 1, item);
                    UpdateList();
                    PushSettings();
                    lbEngines.SelectedIndex = idx + 1;
                }
            }
        }

        public void OnEngineSelected()
        {
            
        }

        public void OnEngineDeselected()
        {

        }

    }


}
