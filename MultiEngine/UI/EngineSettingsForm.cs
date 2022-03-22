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
            bAdd.Enabled = false;

            Setup();

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
            bAdd.Enabled = false;

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

            //var mainSettings = S.GET<CorruptionEngineForm>();

            mySettings = S.GET<CorruptionEngineForm>();
            mySettings.Hide();
            mySettings.Parent?.Controls.Remove(mySettings);
            mySettings.AnchorToPanel(pSettings);
            mySettings.Show();

            lbMemoryDomains.Items.AddRange(S.GET<MemoryDomainsForm>().lbMemoryDomains.Items);
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            //Console.WriteLine($"Selected Engine: {mySettings.cbSelectedEngine.SelectedItem}");
            OutputSettings = null;

            switch (RtcCore.SelectedEngine)
            {
                case CorruptionEngine.NIGHTMARE:
                    OutputSettings = new MCSettingsBase(NightmareEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.HELLGENIE:
                    OutputSettings = new MCSettingsBase(HellgenieEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.DISTORTION:
                    OutputSettings = new MCSettingsBase(DistortionEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.FREEZE:
                    OutputSettings = new MCSettingsBase(null);
                    break;
                case CorruptionEngine.PIPE:
                    OutputSettings = new MCSettingsBase(null);
                    break;
                case CorruptionEngine.VECTOR:
                    OutputSettings = new MCSettingsBase(VectorEngine.getDefaultPartial());
                    break;
                case CorruptionEngine.CLUSTER:
                    OutputSettings = new MCSettingsBase(ClusterEngine.getDefaultPartial());
                    break;
                default:
                    break;
            }

            if(OutputSettings == null)
            {
                MessageBox.Show("Selected engine is not supported for MultiEngine");
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

            OutputSettings.Extract(mySettings);
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
