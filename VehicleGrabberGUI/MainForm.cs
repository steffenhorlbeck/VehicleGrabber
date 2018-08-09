using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;
using VehicleGrabberCore;
using VehicleGrabberCore.Configuration;

namespace VehicleGrabberGUI
{
    public partial class VehicleGrabber : Form
    {
        private delegate void WriteLinetoLogWindowDelegate(string s);
        private WriteLinetoLogWindowDelegate writeLinetoLogWindowDelegate = null;

        private VGCore Core;
        private string pageContent = string.Empty;
        private string baseUrl = string.Empty;

        private string configFileName = string.Empty;

        public VehicleGrabber()
        {
            //string appDir = GetAppPath(Assembly.GetExecutingAssembly().Location);
            string appDataDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), VGCore.COMPANY_DIR, VGCore.APPNAME);
            configFileName = Path.Combine(appDataDir, VGCore.DEFAULT_CONFIG_FILE_NAME);


            Directory.CreateDirectory(appDataDir);

            this.Core = new VGCore(configFileName);

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.DoubleBuffer, true);

            this.writeLinetoLogWindowDelegate = new WriteLinetoLogWindowDelegate(this.WriteLinetoLogWindow);


           

            this.Core.TriggerLogLine = this.WriteLinetoLogWindow;
            this.Core.UpdateTriggerLogLine();
            InitializeComponent();
            this.ReadConfiguration();
        }

        private void BtnStart_Click(object sender, EventArgs e)
        {
            this.UpdateConfiguration();


            try
            {
                
                bwImport.RunWorkerAsync();
                
                /*
                int impType = -1;

                if (rbADAC.Checked) { impType = 0; }
                if (rbAutomobilio.Checked) { impType = 1; }




                Core.Import(impType);



                pageContent = Core.GetPageContent();
                baseUrl = Core.GetBaseUrl();
                browser.Navigate(baseUrl);
                tbContent.Clear();
                tbContent.AppendText(pageContent);
                */
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }                       
        }


        private void BwImport_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwImport.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bwImport.ReportProgress(0);
                return;
            }

            //Core = new VGCore();

            try
            {
                int impType = -1;

                if (rbADAC.Checked) { impType = 0; }
                if (rbAutomobilio.Checked) { impType = 1; }

                bwImport.ReportProgress(0);
                Core.Import(impType);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BwImport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolProgressBar.Value = e.ProgressPercentage;
            toollblStatus.Text = "Processing......" + toolProgressBar.Value.ToString() + "%";

            UpdateStatus();
        }

        private void UpdateStatus()
        {
            try
            {
                tooledtMakers.Text = Core.Importer.MakersList.Count.ToString();
                tooledtModels.Text = Core.Importer.modelsList.Count.ToString();
                tooledtModelTypes.Text = Core.Importer.modelTypesList.Count.ToString();
                tooledtCars.Text = Core.Importer.carDetailsList.Count.ToString();
            }
            catch (Exception ex)
            {
                // do nothing here
                string msg = ex.Message;
            }
        }


        private void BtnCancel_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = false;
            bwImport.CancelAsync();
            bwExport.CancelAsync();
        }


        private void BwImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
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

                pageContent = Core.GetPageContent();
                baseUrl = Core.GetBaseUrl();
                browser.Navigate(baseUrl);
                tbContent.Clear();
                tbContent.AppendText(pageContent);
            }

            //Change the status of the buttons on the UI accordingly
            btnStart.Enabled = true;            

            UpdateStatus();
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            btnCancel.Enabled = true;
            bwExport.RunWorkerAsync();
            /*
            if (Core != null)
            {
                this.UpdateConfiguration();

                if (this.Core.Conf.ExportCSV)
                {
                    Core.ExportToCSV();
                }

                if (this.Core.Conf.ExportMySQL)
                {
                    Core.ExportToMySQL();
                }
            }
            */
        }


        void WriteLinetoLogWindow(string logLine)
        {
            if (InvokeRequired)
            {

                Invoke(new WriteLinetoLogWindowDelegate
                    (WriteLinetoLogWindow), logLine);
            }
            else
            {
                lbLogWindow.Items.Add(logLine);
            }
        }


        /// <summary>Read the application configuration and write the values to the regarding fields/objects</summary>
        private void ReadConfiguration()
        {
            // fill the delimiter list
            this.cbDefaultCSVDelimiter.Items.Clear();
            foreach (string entry in ConstantDefs.DelimiterList)
            {
                this.cbDefaultCSVDelimiter.Items.Add(entry);
            }
            this.cbDefaultCSVDelimiter.SelectedIndex = this.Core.Conf.DefaultCSVDelimiter > -1 ? this.Core.Conf.DefaultCSVDelimiter : 0;

            // Export types
            this.chkCSV.Checked = this.Core.Conf.ExportCSV;
            this.chkMySQL.Checked = this.Core.Conf.ExportMySQL;

            //CSV Export
            this.edtDefaultCSVFileEncoding.Text = this.Core.Conf.DefaultCSVEncoding;
            this.edtDefaultCSVFileExtension.Text = this.Core.Conf.DefaultCSVFileExtension;

            //MySQL export
            this.edtMySQLServer.Text = this.Core.Conf.SQLServer;
            this.edtMySQLPort.Value = this.Core.Conf.SQLPort;
            this.chkMySQLSSLEnabled.Checked = this.Core.Conf.SQLSSLConnection;
            this.edtMySQLDataBase.Text = this.Core.Conf.SQLDataBase;
            this.edtMySQLUser.Text = this.Core.Conf.SQLUser;
            this.edtMySQLPassword.Text = this.Core.Conf.SQLPassword;
        }

        /// <summary>Read the application configuration and write the values to the regarding fields/objects</summary>
        private void UpdateConfiguration()
        {

            // Export types
            this.Core.Conf.ExportCSV = this.chkCSV.Checked;
            this.Core.Conf.ExportMySQL = this.chkMySQL.Checked;

            //CSV Export
            this.Core.Conf.DefaultCSVDelimiter = this.cbDefaultCSVDelimiter.SelectedIndex;
            this.Core.Conf.DefaultCSVEncoding = this.edtDefaultCSVFileEncoding.Text;
            this.Core.Conf.DefaultCSVFileExtension = this.edtDefaultCSVFileExtension.Text;

            //MySQL export
            this.Core.Conf.SQLServer = this.edtMySQLServer.Text;
            this.Core.Conf.SQLPort = (uint) this.edtMySQLPort.Value;
            this.Core.Conf.SQLSSLConnection = this.chkMySQLSSLEnabled.Checked;
            this.Core.Conf.SQLDataBase = this.edtMySQLDataBase.Text;
            this.Core.Conf.SQLUser = this.edtMySQLUser.Text;
            this.Core.Conf.SQLPassword = this.edtMySQLPassword.Text;
        }



#pragma warning disable IDE1006 // Naming Styles
        private void edtMySQLPort_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtMySQLServer_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void chkMySQLSSLEnabled_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtMySQLDataBase_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtMySQLUser_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtMySQLPassword_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void cbDefaultCSVDelimiter_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtDefaultCSVFileExtension_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void edtDefaultCSVFileEncoding_Validated(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
        }

        private void btnSaveConfig_Click(object sender, EventArgs e)
        {
            this.UpdateConfiguration();
            this.Core.Conf.SaveConfigurationToFile(this.configFileName);
        }

        private void btnLoadConfig_Click(object sender, EventArgs e)
        {
            this.Core.Conf.LoadConfiguration(this.configFileName);
        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void bwExport_DoWork(object sender, DoWorkEventArgs e)
        {
            if (bwImport.CancellationPending)
            {
                // Set the e.Cancel flag so that the WorkerCompleted event
                // knows that the process was cancelled.
                e.Cancel = true;
                bwImport.ReportProgress(0);
                return;
            }

            try
            {
                if (Core != null)
                {
                    this.UpdateConfiguration();

                    if (this.Core.Conf.ExportCSV)
                    {
                        Core.ExportToCSV();
                    }

                    if (this.Core.Conf.ExportMySQL)
                    {
                        Core.ExportToMySQL();
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void bwExport_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            toolProgressBar.Value = e.ProgressPercentage;
            toollblStatus.Text = "Processing......" + toolProgressBar.Value.ToString() + "%";

            UpdateStatus();
        }

        private void bwExport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                tooledtMakers.Text = Core.Importer.MakersList.Count.ToString();
                tooledtModels.Text = Core.Importer.modelsList.Count.ToString();
                tooledtModelTypes.Text = Core.Importer.modelTypesList.Count.ToString();
                tooledtCars.Text = Core.Importer.carDetailsList.Count.ToString();
            }
            catch (Exception ex)
            {
                // do nothing here
                string msg = ex.Message;
            }
        }

        private void mnuClearLog_Click(object sender, EventArgs e)
        {
            this.Core.Log.ClearLog();
            this.lbLogWindow.Items.Clear();
            this.Core.Log.GetLogLines();
        }

        private void mnuSelectAll_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.lbLogWindow.Items.Count; i++)
            {
                this.lbLogWindow.SetSelected(i, true);
            }
        }

        private void mnuLogInfo_Click(object sender, EventArgs e)
        {
            this.Core.Conf.ShowLogInfo = this.mnuLogInfo.Checked;

            RefreshLogWindow();
        }

        private void mnuLogWarning_Click(object sender, EventArgs e)
        {
            this.Core.Conf.ShowLogWarning = this.mnuLogWarning.Checked;

            RefreshLogWindow();
        }

        private void mnuLogError_Click(object sender, EventArgs e)
        {
            this.Core.Conf.ShowLogError = this.mnuLogError.Checked;

            RefreshLogWindow();
        }

        private void mnuFatal_Click(object sender, EventArgs e)
        {
            this.Core.Conf.ShowLogFatal = this.mnuFatal.Checked;

            RefreshLogWindow();
        }

        private void mnuDebug_Click(object sender, EventArgs e)
        {
            this.Core.Conf.ShowLogDebug = this.mnuDebug.Checked;
            
            RefreshLogWindow();
        }

        /// <summary>
        /// Refresh Log window lines
        /// </summary>
        private void RefreshLogWindow()
        {
            this.lbLogWindow.Items.Clear();
            foreach (string line in this.Core.Log.GetLogLines())
            {
                this.lbLogWindow.Items.Add(line);
                //this.lbLogWindow.SelectedIndex = this.lbLogWindow.Items.Count - 1;
            }
            this.lbLogWindow.SelectedIndex = this.lbLogWindow.Items.Count - 1;
        }


        private void mnuSaveToFile_Click(object sender, EventArgs e)
        {

        }

        private void tbContent_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }
    }
}
#pragma warning restore IDE1006 // Naming Styles