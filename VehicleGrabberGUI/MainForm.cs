using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VehicleGrabberCore;

namespace VehicleGrabberGUI
{
    public partial class VehicleGrabber : Form
    {
        private VGCore core;
        private string pageContent = string.Empty;
        private string baseUrl = string.Empty;

        public VehicleGrabber()
        {
            InitializeComponent();            
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            
            core = new VGCore();

            try
            {
                int impType = -1;

                if (rbADAC.Checked) { impType = 0; }
                if (rbAutomobilio.Checked) { impType = 1; }

                core.Import(impType);

                pageContent = core.GetPageContent();
                baseUrl = core.GetBaseUrl();
                browser.Navigate(baseUrl);
                tbContent.Clear();
                tbContent.AppendText(pageContent);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            

            
        }

        private void bwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwImport.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bwImport.ReportProgress(0);
                return;
            }

            core = new VGCore();

            try
            {
                int impType = -1;

                if (rbADAC.Checked) { impType = 0; }
                if (rbAutomobilio.Checked) { impType = 1; }

                core.Import(impType);

                pageContent = core.GetPageContent();
                baseUrl = core.GetBaseUrl();
                browser.Navigate(baseUrl);
                tbContent.Clear();
                tbContent.AppendText(pageContent);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolProgressBar.Value = e.ProgressPercentage;
            toollblStatus.Text = "Processing......" + toolProgressBar.Value.ToString() + "%";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bwImport.CancelAsync();
        }

        private void bwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            // The background process is complete. We need to inspect
            // our response to see if an error occurred, a cancel was
            // requested or if we completed successfully.  
            if (e.Cancelled)
            {
                toollblStatus.Text = "Task Cancelled.";
            }

            // Check to see if an error occurred in the background process.

            else if (e.Error != null)
            {
                toollblStatus.Text = "Error while performing background operation.";
            }
            else
            {
                // Everything completed normally.
                toollblStatus.Text = "Task Completed...";
            }

            //Change the status of the buttons on the UI accordingly
            btnStart.Enabled = true;
            btnCancel.Enabled = false;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(core != null)
            {
                core.ExportToCSV();

                core.ExportToMySQL();
            }
        }
    }
}
