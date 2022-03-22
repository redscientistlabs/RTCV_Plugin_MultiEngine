using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RTCV.Common;
using RTCV.UI;
using RTCV.UI.Components.EngineConfig.EngineControls;

namespace MultiEngine.UI
{
    /// <summary>
    /// Workaround class
    /// </summary>
    public static class EngineSync
    {
        [Obsolete]
        public static void SyncAll(CorruptionEngineForm form)
        {
            SyncNightmare(form.NightmareEngineControl);
            SyncVector(form.VectorEngineControl);
            SyncHellgenie(form.HellgenieEngineControl);
            SyncPipe(form.PipeEngineControl);
            //Freeze doesn't need extra sync
            SyncDistortion(form.DistortionEngineControl);
            SyncCluster(form.ClusterEngineControl);
        }

        public static void SyncNightmare(NightmareEngineControl ctrl)
        {
            var ceForm = S.GET<CorruptionEngineForm>();
            ctrl.cbBlastType.SelectedIndexChanged += (o, e) => ceForm.NightmareEngineControl.cbBlastType.SelectedIndex = ctrl.cbBlastType.SelectedIndex;
            ctrl.nmMinValueNightmare.ValueChanged += (o, e) => ceForm.NightmareEngineControl.nmMinValueNightmare.Value = ctrl.nmMinValueNightmare.Value;
            ctrl.nmMaxValueNightmare.ValueChanged += (o, e) => ceForm.NightmareEngineControl.nmMaxValueNightmare.Value = ctrl.nmMaxValueNightmare.Value;
        }

        public static void SyncVector(VectorEngineControl ctrl)
        {
            var ceForm = S.GET<CorruptionEngineForm>();
            ctrl.cbVectorLimiterList.SelectedIndexChanged += (o, e) => ceForm.VectorEngineControl.cbVectorLimiterList.SelectedIndex = ctrl.cbVectorLimiterList.SelectedIndex;
            ctrl.cbVectorValueList.SelectedIndexChanged += (o, e) => ceForm.VectorEngineControl.cbVectorValueList.SelectedIndex = ctrl.cbVectorValueList.SelectedIndex;
            ctrl.cbVectorUnlockPrecision.CheckedChanged += (o, e) => ceForm.VectorEngineControl.cbVectorUnlockPrecision.Checked = ctrl.cbVectorUnlockPrecision.Checked;
        }

        public static void SyncHellgenie(HellgenieEngineControl ctrl)
        {
            var ceForm = S.GET<CorruptionEngineForm>();
            ctrl.nmMaxValueHellgenie.ValueChanged += (o, e) => ceForm.HellgenieEngineControl.nmMaxValueHellgenie.Value = ctrl.nmMaxValueHellgenie.Value;
            ctrl.nmMinValueHellgenie.ValueChanged += (o, e) => ceForm.HellgenieEngineControl.nmMinValueHellgenie.Value = ctrl.nmMinValueHellgenie.Value;
        }

        static void SyncPipe(PipeEngineControl ctrl)
        {
            var ceForm = S.GET<CorruptionEngineForm>();
            ctrl.updownMaxPipes.ValueChanged += (o, e) => ceForm.PipeEngineControl.updownMaxPipes.Value = ctrl.updownMaxPipes.Value;
            ctrl.cbClearPipesOnRewind.CheckedChanged += (o, e) => ceForm.PipeEngineControl.cbClearPipesOnRewind.Checked = ctrl.cbClearPipesOnRewind.Checked; 
            ctrl.cbLockPipes.CheckedChanged += (o, e) => ceForm.PipeEngineControl.cbLockPipes.Checked = ctrl.cbLockPipes.Checked; 
        }

        static void SyncDistortion(DistortionEngineControl ctrl)
        {
            var ceForm = S.GET<CorruptionEngineForm>();
            ctrl.nmDistortionDelay.ValueChanged += (o, e) => ceForm.DistortionEngineControl.nmDistortionDelay.Value = ctrl.nmDistortionDelay.Value;
        }

        static void SyncCluster(ClusterEngineControl ctrl)
        {
            var ceC = S.GET<CorruptionEngineForm>().ClusterEngineControl;
            ctrl.cbClusterMethod.SelectedIndexChanged += (o, e) => ceC.cbClusterMethod.SelectedIndex = ctrl.cbClusterMethod.SelectedIndex;
            ctrl.cbClusterLimiterList.SelectedIndexChanged += (o, e) => ceC.cbClusterLimiterList.SelectedIndex = ctrl.cbClusterLimiterList.SelectedIndex;
            ctrl.clusterFilterAll.CheckedChanged += (o, e) => ceC.clusterFilterAll.Checked = ctrl.clusterFilterAll.Checked;
            ctrl.clusterSplitUnits.CheckedChanged += (o, e) => ceC.clusterSplitUnits.Checked = ctrl.clusterSplitUnits.Checked;
        }

    }

}
