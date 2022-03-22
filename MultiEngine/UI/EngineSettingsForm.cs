using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MultiEngine.Structures;
using RTCV.Common;
using RTCV.CorruptCore;
using RTCV.NetCore;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;
using RTCV.UI.Modular;
//using RTCV.UI.Components.EngineConfig;

namespace MultiEngine.UI
{
    public partial class EngineSettingsForm : ComponentForm, IColorize
    {
        //string curEngine = "None";
        CorruptionEngineForm mySettings;

        //private CorruptionEngineForm mainSettings;
        public MCSettingsBase OutputSettings { get; private set; } = null;
        MCSettingsBase edit = null;
        //private MemoryDomainsForm myDomains;

        public EngineSettingsForm()
        {
            InitializeComponent();
            
            //TODO: get plugin engines too
            //previousPrecision = RtcCore.CurrentPrecision;

            Setup();

            bAdd.Enabled = false;
            Shown += ShownNoEdit;

            FormClosing += EngineSettingsForm_FormClosing;
        }

        private async void ShownNoEdit(object sender, EventArgs e)
        {
            //await Task.Delay(1500);
            //mySettings.cbCustomPrecision.SelectedIndex = C.PrecisionToIndex(previousPrecision);
            //SetPrecisionIndex(previousPrecision);
            bAdd.Enabled = true;
        }

        public EngineSettingsForm(MCSettingsBase edit)
        {
            InitializeComponent();
            //previousPrecision = RtcCore.CurrentPrecision;
            //editPrecision = edit.Precision;
            Setup();
            this.edit = edit;

            //Add previous
            if (edit.Domains != null && edit.Domains.Length > 0)
            {
                foreach (var item in edit.Domains)
                {
                    if (!lbMemoryDomains.Items.Contains(item))
                    {
                        lbMemoryDomains.Items.Add(item);
                    }
                }
            }

            bAdd.Enabled = false;
            bAdd.Text = "Save";
            this.Shown += ShownEdit;
            FormClosing += EngineSettingsForm_FormClosing;
            //pSettings.Controls.Add(cSettings);
        }


        private async void ShownEdit(object sender, EventArgs e)
        {
            //this.Enabled = false;
            //await Task.Delay(1500);
            edit.ApplyPartial();
            mySettings.cbSelectedEngine.SelectedIndex = edit.EngineIndex;
            mySettings.ResyncAllEngines();
            //edit.UpdateUI(mySettings,true);
            bAdd.Enabled = true;
            nmIntensity.Value = (decimal)(edit.Percentage * 100.0);
            tbName.Text = edit.DisplayName ?? "";
            if (edit.ForcedIntensity > 0 && edit.ForcedIntensity < nmForcedIntensity.Maximum)
            {
                cbForceIntensity.Checked = true;
                nmForcedIntensity.Value = edit.ForcedIntensity;
            }

            lbMemoryDomains.ClearSelected();
            if (edit.Domains != null)
            {
                foreach (var dom in edit.Domains)
                {
                    lbMemoryDomains.SetSelected(lbMemoryDomains.Items.IndexOf(dom), true);
                }
            }
        }

        void Setup()
        {

            object maxIntensity = AllSpec.VanguardSpec[VSPEC.OVERRIDE_DEFAULTMAXINTENSITY];
            if (maxIntensity != null && maxIntensity is int maxintensity)
            {
                nmForcedIntensity.Maximum = maxintensity;
            }

            //mainSettings = S.GET<CorruptionEngineForm>();
            var mainSettings = S.GET<CorruptionEngineForm>();

            mySettings = mainSettings;// new CorruptionEngineForm();
            myEngineIndex = mainSettings.cbSelectedEngine.SelectedIndex;
            mySettings.Hide();
            mySettings.Parent?.Controls.Remove(mainSettings);
            mySettings.AnchorToPanel(pSettings);
            mySettings.Show();
            //EngineSync.SyncAll(mySettings);

            //mySettings.nmAlignment.ValueChanged += (o, e) => { mainSettings.nmAlignment.Value = mySettings.nmAlignment.Value; };
            //mySettings.cbSelectedEngine.SelectedIndexChanged += (o, e) => { mainSettings.cbSelectedEngine.SelectedIndex = mySettings.cbSelectedEngine.SelectedIndex; };
            //mySettings.cbCustomPrecision.SelectedIndexChanged += (o, e) => { mainSettings.cbCustomPrecision.SelectedIndex = mySettings.cbCustomPrecision.SelectedIndex; };

            lbMemoryDomains.Items.AddRange(S.GET<MemoryDomainsForm>().lbMemoryDomains.Items);
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //TODO: extract engine settings
            Console.WriteLine($"Selected Engine: {mySettings.cbSelectedEngine.SelectedItem}");
            OutputSettings = null;
            switch (mySettings.cbSelectedEngine.SelectedItem.ToString())
            {
                case C.NightmareEngineStr:
                    OutputSettings = new MCNightmareSettings();
                    break;
                case C.PipeEngineStr:
                    OutputSettings = new MCPipeSettings();
                    break;
                case C.VectorEngineStr:
                    OutputSettings = new MCVectorSettings();
                    break;
                case C.HellgenieEngineStr:
                    OutputSettings = new MCHellgenieSettings();
                    break;
                case C.FreezeEngineStr:
                    OutputSettings = new MCFreezeSettings();
                    break;
                case C.DistortionEngineStr:
                    OutputSettings = new MCDistortionSettings();
                    break;
                case C.ClusterEngineStr:
                    OutputSettings = new MCClusterSettings();
                    MessageBox.Show("Main form UI not fully synced, settings are applied to Cluster Engine.\r\nAffected settings: Rotate Amount and Cluster Chunk Size\r\nUpdating these settings on the main UI will sync them again", "Main Form UI Not Synced");
                    break;
                default:
                    MessageBox.Show("Cannot add the selected engine type, please select another.");
                    return;
            }
            if (!string.IsNullOrWhiteSpace(tbName.Text)) OutputSettings.DisplayName = tbName.Text;
            if (cbForceIntensity.Checked)
            {
                OutputSettings.ForcedIntensity = (long)nmForcedIntensity.Value;
            }
            else
            {
                OutputSettings.Percentage = (double)nmIntensity.Value / 100.0;
            }
            //OutputSettings.Weight = 1.0;
            OutputSettings.Extract(mySettings);
            //OutputSettings.UpdateCache(); //Update partial cache

            //string[] lst = new List<string>(myDomains.lbMemoryDomains.SelectedItems.Cast<string>()).ToArray();
            string[] lst = new List<string>(lbMemoryDomains.SelectedItems.Cast<string>()).ToArray();
            OutputSettings.Domains = lst;
            DialogResult = DialogResult.OK;
            
            Close();
        }

        private void EngineSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Visible = false;
            mySettings.RestoreToPreviousPanel();
            //TODO:resync in MultiEngineForm
            //mySettings.ResyncAllEngines();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbMemoryDomains.Items.Count; i++)
            {
                lbMemoryDomains.SetSelected(i, true);
            }
        }

        private void btnUnselectDomains_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lbMemoryDomains.Items.Count; i++)
            {
                lbMemoryDomains.SetSelected(i, false);
            }
        }

        private void bAddDomain_Click(object sender, EventArgs e)
        {
            lbMemoryDomains.Items.Add(tbNewDomain.Text);
            lbMemoryDomains.SetSelected(lbMemoryDomains.Items.Count - 1, true);
            tbNewDomain.Clear();
        }

        private void cbForceIntensity_CheckedChanged(object sender, EventArgs e)
        {
            nmForcedIntensity.Enabled = cbForceIntensity.Checked;
            nmIntensity.Enabled = !cbForceIntensity.Checked;
        }
    }
}
