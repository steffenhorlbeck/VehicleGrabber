namespace VehicleGrabberGUI
{
    partial class VehicleGrabber
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnStart = new System.Windows.Forms.Button();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.rbAutomobilio = new System.Windows.Forms.RadioButton();
            this.rbADAC = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toollblMaker = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtMakers = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblModels = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtModels = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblTypes = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtModelTypes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblCars = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtCars = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwImport = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnExport = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.lbLogWindow = new System.Windows.Forms.ListBox();
            this.mnuContextLog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mnuClearLog = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuShowTypes = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogInfo = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogWarning = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuLogError = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFatal = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDebug = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSaveToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tabBrowser = new System.Windows.Forms.TabPage();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tabSource = new System.Windows.Forms.TabPage();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.grpConfig = new System.Windows.Forms.GroupBox();
            this.tabConfig = new System.Windows.Forms.TabControl();
            this.tabExportTypes = new System.Windows.Forms.TabPage();
            this.chkMySQL = new System.Windows.Forms.CheckBox();
            this.chkCSV = new System.Windows.Forms.CheckBox();
            this.tabConfigCSV = new System.Windows.Forms.TabPage();
            this.edtDefaultCSVFileEncoding = new System.Windows.Forms.TextBox();
            this.lblDefaultCSVFileEncoding = new System.Windows.Forms.Label();
            this.edtDefaultCSVFileExtension = new System.Windows.Forms.TextBox();
            this.lblDefaultCSVFileExtension = new System.Windows.Forms.Label();
            this.cbDefaultCSVDelimiter = new System.Windows.Forms.ComboBox();
            this.lblDefaultCSVDelimiter = new System.Windows.Forms.Label();
            this.tabConfigMySQL = new System.Windows.Forms.TabPage();
            this.chkMySQLSSLEnabled = new System.Windows.Forms.CheckBox();
            this.lblMySQLPassword = new System.Windows.Forms.Label();
            this.lblMySQLUser = new System.Windows.Forms.Label();
            this.lblMySQLDatabase = new System.Windows.Forms.Label();
            this.lblMySQLPort = new System.Windows.Forms.Label();
            this.lblMySQLServer = new System.Windows.Forms.Label();
            this.edtMySQLPort = new System.Windows.Forms.NumericUpDown();
            this.edtMySQLPassword = new System.Windows.Forms.TextBox();
            this.edtMySQLUser = new System.Windows.Forms.TextBox();
            this.edtMySQLDataBase = new System.Windows.Forms.TextBox();
            this.edtMySQLServer = new System.Windows.Forms.TextBox();
            this.btnLoadConfig = new System.Windows.Forms.Button();
            this.btnSaveConfig = new System.Windows.Forms.Button();
            this.bwExport = new System.ComponentModel.BackgroundWorker();
            this.edtLimitRecords = new System.Windows.Forms.NumericUpDown();
            this.lblLimitRecords = new System.Windows.Forms.Label();
            this.grpSource.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.mnuContextLog.SuspendLayout();
            this.tabBrowser.SuspendLayout();
            this.tabSource.SuspendLayout();
            this.grpConfig.SuspendLayout();
            this.tabConfig.SuspendLayout();
            this.tabExportTypes.SuspendLayout();
            this.tabConfigCSV.SuspendLayout();
            this.tabConfigMySQL.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtMySQLPort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLimitRecords)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStart.Location = new System.Drawing.Point(986, 14);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(97, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Import";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.BtnStart_Click);
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.rbAutomobilio);
            this.grpSource.Controls.Add(this.rbADAC);
            this.grpSource.Location = new System.Drawing.Point(39, 13);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(268, 187);
            this.grpSource.TabIndex = 5;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Quelle";
            // 
            // rbAutomobilio
            // 
            this.rbAutomobilio.AutoSize = true;
            this.rbAutomobilio.Enabled = false;
            this.rbAutomobilio.Location = new System.Drawing.Point(7, 51);
            this.rbAutomobilio.Name = "rbAutomobilio";
            this.rbAutomobilio.Size = new System.Drawing.Size(118, 17);
            this.rbAutomobilio.TabIndex = 1;
            this.rbAutomobilio.Text = "Automobilio.info (IT)";
            this.rbAutomobilio.UseVisualStyleBackColor = true;
            // 
            // rbADAC
            // 
            this.rbADAC.AutoSize = true;
            this.rbADAC.Checked = true;
            this.rbADAC.Location = new System.Drawing.Point(7, 20);
            this.rbADAC.Name = "rbADAC";
            this.rbADAC.Size = new System.Drawing.Size(114, 17);
            this.rbADAC.TabIndex = 0;
            this.rbADAC.TabStop = true;
            this.rbADAC.Text = "ADAC Autokatalog";
            this.rbADAC.UseVisualStyleBackColor = true;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolProgressBar,
            this.toollblMaker,
            this.tooledtMakers,
            this.toollblModels,
            this.tooledtModels,
            this.toollblTypes,
            this.tooledtModelTypes,
            this.toollblCars,
            this.tooledtCars,
            this.toollblStatus});
            this.statusStrip1.Location = new System.Drawing.Point(0, 543);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1112, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolProgressBar
            // 
            this.toolProgressBar.Name = "toolProgressBar";
            this.toolProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toollblMaker
            // 
            this.toollblMaker.Name = "toollblMaker";
            this.toollblMaker.Size = new System.Drawing.Size(60, 17);
            this.toollblMaker.Text = "Hersteller:";
            // 
            // tooledtMakers
            // 
            this.tooledtMakers.Name = "tooledtMakers";
            this.tooledtMakers.Size = new System.Drawing.Size(13, 17);
            this.tooledtMakers.Text = "0";
            // 
            // toollblModels
            // 
            this.toollblModels.Name = "toollblModels";
            this.toollblModels.Size = new System.Drawing.Size(53, 17);
            this.toollblModels.Text = "Modelle:";
            // 
            // tooledtModels
            // 
            this.tooledtModels.Name = "tooledtModels";
            this.tooledtModels.Size = new System.Drawing.Size(13, 17);
            this.tooledtModels.Text = "0";
            // 
            // toollblTypes
            // 
            this.toollblTypes.Name = "toollblTypes";
            this.toollblTypes.Size = new System.Drawing.Size(84, 17);
            this.toollblTypes.Text = "Modell-Typen:";
            // 
            // tooledtModelTypes
            // 
            this.tooledtModelTypes.Name = "tooledtModelTypes";
            this.tooledtModelTypes.Size = new System.Drawing.Size(13, 17);
            this.tooledtModelTypes.Text = "0";
            // 
            // toollblCars
            // 
            this.toollblCars.Name = "toollblCars";
            this.toollblCars.Size = new System.Drawing.Size(64, 17);
            this.toollblCars.Text = "Fahrzeuge:";
            // 
            // tooledtCars
            // 
            this.tooledtCars.Name = "tooledtCars";
            this.tooledtCars.Size = new System.Drawing.Size(13, 17);
            this.tooledtCars.Text = "0";
            // 
            // toollblStatus
            // 
            this.toollblStatus.Name = "toollblStatus";
            this.toollblStatus.Size = new System.Drawing.Size(42, 17);
            this.toollblStatus.Text = "Status:";
            // 
            // bwImport
            // 
            this.bwImport.WorkerReportsProgress = true;
            this.bwImport.WorkerSupportsCancellation = true;
            this.bwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.BwImport_DoWork);
            this.bwImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.BwImport_ProgressChanged);
            this.bwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.BwImport_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(986, 36);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(97, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(986, 58);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(97, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.BtnExport_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Controls.Add(this.tabBrowser);
            this.tabControl1.Controls.Add(this.tabSource);
            this.tabControl1.Location = new System.Drawing.Point(39, 219);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1054, 311);
            this.tabControl1.TabIndex = 9;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.lbLogWindow);
            this.tabLog.Location = new System.Drawing.Point(4, 22);
            this.tabLog.Name = "tabLog";
            this.tabLog.Padding = new System.Windows.Forms.Padding(3);
            this.tabLog.Size = new System.Drawing.Size(1046, 285);
            this.tabLog.TabIndex = 2;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // lbLogWindow
            // 
            this.lbLogWindow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbLogWindow.ContextMenuStrip = this.mnuContextLog;
            this.lbLogWindow.FormattingEnabled = true;
            this.lbLogWindow.HorizontalScrollbar = true;
            this.lbLogWindow.Location = new System.Drawing.Point(6, 6);
            this.lbLogWindow.Name = "lbLogWindow";
            this.lbLogWindow.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lbLogWindow.Size = new System.Drawing.Size(1034, 264);
            this.lbLogWindow.TabIndex = 2;
            // 
            // mnuContextLog
            // 
            this.mnuContextLog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuClearLog,
            this.mnuSelectAll,
            this.mnuShowTypes,
            this.mnuSaveToFile});
            this.mnuContextLog.Name = "mnuContextLog";
            this.mnuContextLog.Size = new System.Drawing.Size(137, 92);
            // 
            // mnuClearLog
            // 
            this.mnuClearLog.Name = "mnuClearLog";
            this.mnuClearLog.Size = new System.Drawing.Size(136, 22);
            this.mnuClearLog.Text = "Clear Log";
            this.mnuClearLog.Click += new System.EventHandler(this.mnuClearLog_Click);
            // 
            // mnuSelectAll
            // 
            this.mnuSelectAll.Name = "mnuSelectAll";
            this.mnuSelectAll.Size = new System.Drawing.Size(136, 22);
            this.mnuSelectAll.Text = "Select All";
            this.mnuSelectAll.Visible = false;
            this.mnuSelectAll.Click += new System.EventHandler(this.mnuSelectAll_Click);
            // 
            // mnuShowTypes
            // 
            this.mnuShowTypes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuLogInfo,
            this.mnuLogWarning,
            this.mnuLogError,
            this.mnuFatal,
            this.mnuDebug});
            this.mnuShowTypes.Name = "mnuShowTypes";
            this.mnuShowTypes.Size = new System.Drawing.Size(136, 22);
            this.mnuShowTypes.Text = "Show Types";
            // 
            // mnuLogInfo
            // 
            this.mnuLogInfo.Checked = true;
            this.mnuLogInfo.CheckOnClick = true;
            this.mnuLogInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuLogInfo.Name = "mnuLogInfo";
            this.mnuLogInfo.Size = new System.Drawing.Size(137, 22);
            this.mnuLogInfo.Text = "Information";
            this.mnuLogInfo.Click += new System.EventHandler(this.mnuLogInfo_Click);
            // 
            // mnuLogWarning
            // 
            this.mnuLogWarning.Checked = true;
            this.mnuLogWarning.CheckOnClick = true;
            this.mnuLogWarning.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuLogWarning.Name = "mnuLogWarning";
            this.mnuLogWarning.Size = new System.Drawing.Size(137, 22);
            this.mnuLogWarning.Text = "Warnings";
            this.mnuLogWarning.Click += new System.EventHandler(this.mnuLogWarning_Click);
            // 
            // mnuLogError
            // 
            this.mnuLogError.Checked = true;
            this.mnuLogError.CheckOnClick = true;
            this.mnuLogError.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuLogError.Name = "mnuLogError";
            this.mnuLogError.Size = new System.Drawing.Size(137, 22);
            this.mnuLogError.Text = "Errors";
            this.mnuLogError.Click += new System.EventHandler(this.mnuLogError_Click);
            // 
            // mnuFatal
            // 
            this.mnuFatal.Checked = true;
            this.mnuFatal.CheckOnClick = true;
            this.mnuFatal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuFatal.Name = "mnuFatal";
            this.mnuFatal.Size = new System.Drawing.Size(137, 22);
            this.mnuFatal.Text = "Fatal";
            this.mnuFatal.Click += new System.EventHandler(this.mnuFatal_Click);
            // 
            // mnuDebug
            // 
            this.mnuDebug.Checked = true;
            this.mnuDebug.CheckOnClick = true;
            this.mnuDebug.CheckState = System.Windows.Forms.CheckState.Checked;
            this.mnuDebug.Name = "mnuDebug";
            this.mnuDebug.Size = new System.Drawing.Size(137, 22);
            this.mnuDebug.Text = "Debug";
            this.mnuDebug.Click += new System.EventHandler(this.mnuDebug_Click);
            // 
            // mnuSaveToFile
            // 
            this.mnuSaveToFile.Name = "mnuSaveToFile";
            this.mnuSaveToFile.Size = new System.Drawing.Size(136, 22);
            this.mnuSaveToFile.Text = "Save To File";
            this.mnuSaveToFile.Visible = false;
            this.mnuSaveToFile.Click += new System.EventHandler(this.mnuSaveToFile_Click);
            // 
            // tabBrowser
            // 
            this.tabBrowser.Controls.Add(this.browser);
            this.tabBrowser.Location = new System.Drawing.Point(4, 22);
            this.tabBrowser.Name = "tabBrowser";
            this.tabBrowser.Padding = new System.Windows.Forms.Padding(3);
            this.tabBrowser.Size = new System.Drawing.Size(1046, 285);
            this.tabBrowser.TabIndex = 3;
            this.tabBrowser.Text = "Website";
            this.tabBrowser.UseVisualStyleBackColor = true;
            this.tabBrowser.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(5, 3);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(1037, 276);
            this.browser.TabIndex = 5;
            // 
            // tabSource
            // 
            this.tabSource.Controls.Add(this.tbContent);
            this.tabSource.Location = new System.Drawing.Point(4, 22);
            this.tabSource.Name = "tabSource";
            this.tabSource.Padding = new System.Windows.Forms.Padding(3);
            this.tabSource.Size = new System.Drawing.Size(1046, 285);
            this.tabSource.TabIndex = 4;
            this.tabSource.Text = "Source";
            this.tabSource.UseVisualStyleBackColor = true;
            // 
            // tbContent
            // 
            this.tbContent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbContent.Location = new System.Drawing.Point(5, 6);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbContent.Size = new System.Drawing.Size(1037, 273);
            this.tbContent.TabIndex = 6;
            // 
            // grpConfig
            // 
            this.grpConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpConfig.Controls.Add(this.tabConfig);
            this.grpConfig.Location = new System.Drawing.Point(331, 14);
            this.grpConfig.Name = "grpConfig";
            this.grpConfig.Size = new System.Drawing.Size(637, 186);
            this.grpConfig.TabIndex = 10;
            this.grpConfig.TabStop = false;
            this.grpConfig.Text = "Konfiguration";
            // 
            // tabConfig
            // 
            this.tabConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabConfig.Controls.Add(this.tabExportTypes);
            this.tabConfig.Controls.Add(this.tabConfigCSV);
            this.tabConfig.Controls.Add(this.tabConfigMySQL);
            this.tabConfig.Location = new System.Drawing.Point(17, 22);
            this.tabConfig.Name = "tabConfig";
            this.tabConfig.SelectedIndex = 0;
            this.tabConfig.Size = new System.Drawing.Size(605, 145);
            this.tabConfig.TabIndex = 2;
            // 
            // tabExportTypes
            // 
            this.tabExportTypes.Controls.Add(this.lblLimitRecords);
            this.tabExportTypes.Controls.Add(this.edtLimitRecords);
            this.tabExportTypes.Controls.Add(this.chkMySQL);
            this.tabExportTypes.Controls.Add(this.chkCSV);
            this.tabExportTypes.Location = new System.Drawing.Point(4, 22);
            this.tabExportTypes.Name = "tabExportTypes";
            this.tabExportTypes.Padding = new System.Windows.Forms.Padding(3);
            this.tabExportTypes.Size = new System.Drawing.Size(597, 119);
            this.tabExportTypes.TabIndex = 0;
            this.tabExportTypes.Text = "Export-Typen";
            this.tabExportTypes.UseVisualStyleBackColor = true;
            // 
            // chkMySQL
            // 
            this.chkMySQL.AutoSize = true;
            this.chkMySQL.Location = new System.Drawing.Point(21, 39);
            this.chkMySQL.Name = "chkMySQL";
            this.chkMySQL.Size = new System.Drawing.Size(94, 17);
            this.chkMySQL.TabIndex = 3;
            this.chkMySQL.Text = "MySQL Export";
            this.chkMySQL.UseVisualStyleBackColor = true;
            // 
            // chkCSV
            // 
            this.chkCSV.AutoSize = true;
            this.chkCSV.Location = new System.Drawing.Point(21, 15);
            this.chkCSV.Name = "chkCSV";
            this.chkCSV.Size = new System.Drawing.Size(80, 17);
            this.chkCSV.TabIndex = 2;
            this.chkCSV.Text = "CSV Export";
            this.chkCSV.UseVisualStyleBackColor = true;
            // 
            // tabConfigCSV
            // 
            this.tabConfigCSV.Controls.Add(this.edtDefaultCSVFileEncoding);
            this.tabConfigCSV.Controls.Add(this.lblDefaultCSVFileEncoding);
            this.tabConfigCSV.Controls.Add(this.edtDefaultCSVFileExtension);
            this.tabConfigCSV.Controls.Add(this.lblDefaultCSVFileExtension);
            this.tabConfigCSV.Controls.Add(this.cbDefaultCSVDelimiter);
            this.tabConfigCSV.Controls.Add(this.lblDefaultCSVDelimiter);
            this.tabConfigCSV.Location = new System.Drawing.Point(4, 22);
            this.tabConfigCSV.Name = "tabConfigCSV";
            this.tabConfigCSV.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigCSV.Size = new System.Drawing.Size(597, 119);
            this.tabConfigCSV.TabIndex = 1;
            this.tabConfigCSV.Text = "CSV";
            this.tabConfigCSV.UseVisualStyleBackColor = true;
            // 
            // edtDefaultCSVFileEncoding
            // 
            this.edtDefaultCSVFileEncoding.Location = new System.Drawing.Point(25, 80);
            this.edtDefaultCSVFileEncoding.Name = "edtDefaultCSVFileEncoding";
            this.edtDefaultCSVFileEncoding.Size = new System.Drawing.Size(200, 20);
            this.edtDefaultCSVFileEncoding.TabIndex = 14;
            this.edtDefaultCSVFileEncoding.Validated += new System.EventHandler(this.edtDefaultCSVFileEncoding_Validated);
            // 
            // lblDefaultCSVFileEncoding
            // 
            this.lblDefaultCSVFileEncoding.AutoSize = true;
            this.lblDefaultCSVFileEncoding.Location = new System.Drawing.Point(22, 65);
            this.lblDefaultCSVFileEncoding.Name = "lblDefaultCSVFileEncoding";
            this.lblDefaultCSVFileEncoding.Size = new System.Drawing.Size(132, 13);
            this.lblDefaultCSVFileEncoding.TabIndex = 13;
            this.lblDefaultCSVFileEncoding.Text = "Default CSV File Encoding";
            // 
            // edtDefaultCSVFileExtension
            // 
            this.edtDefaultCSVFileExtension.Location = new System.Drawing.Point(263, 29);
            this.edtDefaultCSVFileExtension.Name = "edtDefaultCSVFileExtension";
            this.edtDefaultCSVFileExtension.Size = new System.Drawing.Size(200, 20);
            this.edtDefaultCSVFileExtension.TabIndex = 12;
            this.edtDefaultCSVFileExtension.Validated += new System.EventHandler(this.edtDefaultCSVFileExtension_Validated);
            // 
            // lblDefaultCSVFileExtension
            // 
            this.lblDefaultCSVFileExtension.AutoSize = true;
            this.lblDefaultCSVFileExtension.Location = new System.Drawing.Point(260, 13);
            this.lblDefaultCSVFileExtension.Name = "lblDefaultCSVFileExtension";
            this.lblDefaultCSVFileExtension.Size = new System.Drawing.Size(133, 13);
            this.lblDefaultCSVFileExtension.TabIndex = 11;
            this.lblDefaultCSVFileExtension.Text = "Default CSV File Extension";
            // 
            // cbDefaultCSVDelimiter
            // 
            this.cbDefaultCSVDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbDefaultCSVDelimiter.DropDownWidth = 200;
            this.cbDefaultCSVDelimiter.FormattingEnabled = true;
            this.cbDefaultCSVDelimiter.Location = new System.Drawing.Point(25, 28);
            this.cbDefaultCSVDelimiter.Name = "cbDefaultCSVDelimiter";
            this.cbDefaultCSVDelimiter.Size = new System.Drawing.Size(200, 21);
            this.cbDefaultCSVDelimiter.TabIndex = 10;
            this.cbDefaultCSVDelimiter.Validated += new System.EventHandler(this.cbDefaultCSVDelimiter_Validated);
            // 
            // lblDefaultCSVDelimiter
            // 
            this.lblDefaultCSVDelimiter.AutoSize = true;
            this.lblDefaultCSVDelimiter.Location = new System.Drawing.Point(22, 12);
            this.lblDefaultCSVDelimiter.Name = "lblDefaultCSVDelimiter";
            this.lblDefaultCSVDelimiter.Size = new System.Drawing.Size(108, 13);
            this.lblDefaultCSVDelimiter.TabIndex = 9;
            this.lblDefaultCSVDelimiter.Text = "Default CSV Delimiter";
            // 
            // tabConfigMySQL
            // 
            this.tabConfigMySQL.Controls.Add(this.chkMySQLSSLEnabled);
            this.tabConfigMySQL.Controls.Add(this.lblMySQLPassword);
            this.tabConfigMySQL.Controls.Add(this.lblMySQLUser);
            this.tabConfigMySQL.Controls.Add(this.lblMySQLDatabase);
            this.tabConfigMySQL.Controls.Add(this.lblMySQLPort);
            this.tabConfigMySQL.Controls.Add(this.lblMySQLServer);
            this.tabConfigMySQL.Controls.Add(this.edtMySQLPort);
            this.tabConfigMySQL.Controls.Add(this.edtMySQLPassword);
            this.tabConfigMySQL.Controls.Add(this.edtMySQLUser);
            this.tabConfigMySQL.Controls.Add(this.edtMySQLDataBase);
            this.tabConfigMySQL.Controls.Add(this.edtMySQLServer);
            this.tabConfigMySQL.Location = new System.Drawing.Point(4, 22);
            this.tabConfigMySQL.Name = "tabConfigMySQL";
            this.tabConfigMySQL.Padding = new System.Windows.Forms.Padding(3);
            this.tabConfigMySQL.Size = new System.Drawing.Size(597, 119);
            this.tabConfigMySQL.TabIndex = 2;
            this.tabConfigMySQL.Text = "MySQL";
            this.tabConfigMySQL.UseVisualStyleBackColor = true;
            // 
            // chkMySQLSSLEnabled
            // 
            this.chkMySQLSSLEnabled.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkMySQLSSLEnabled.AutoSize = true;
            this.chkMySQLSSLEnabled.Location = new System.Drawing.Point(472, 32);
            this.chkMySQLSSLEnabled.Name = "chkMySQLSSLEnabled";
            this.chkMySQLSSLEnabled.Size = new System.Drawing.Size(125, 17);
            this.chkMySQLSSLEnabled.TabIndex = 11;
            this.chkMySQLSSLEnabled.Text = "Use SSL Connection";
            this.chkMySQLSSLEnabled.UseVisualStyleBackColor = true;
            this.chkMySQLSSLEnabled.Validated += new System.EventHandler(this.chkMySQLSSLEnabled_Validated);
            // 
            // lblMySQLPassword
            // 
            this.lblMySQLPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMySQLPassword.AutoSize = true;
            this.lblMySQLPassword.Location = new System.Drawing.Point(472, 63);
            this.lblMySQLPassword.Name = "lblMySQLPassword";
            this.lblMySQLPassword.Size = new System.Drawing.Size(91, 13);
            this.lblMySQLPassword.TabIndex = 10;
            this.lblMySQLPassword.Text = "MySQL Password";
            // 
            // lblMySQLUser
            // 
            this.lblMySQLUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMySQLUser.AutoSize = true;
            this.lblMySQLUser.Location = new System.Drawing.Point(305, 63);
            this.lblMySQLUser.Name = "lblMySQLUser";
            this.lblMySQLUser.Size = new System.Drawing.Size(67, 13);
            this.lblMySQLUser.TabIndex = 9;
            this.lblMySQLUser.Text = "MySQL User";
            // 
            // lblMySQLDatabase
            // 
            this.lblMySQLDatabase.AutoSize = true;
            this.lblMySQLDatabase.Location = new System.Drawing.Point(16, 63);
            this.lblMySQLDatabase.Name = "lblMySQLDatabase";
            this.lblMySQLDatabase.Size = new System.Drawing.Size(92, 13);
            this.lblMySQLDatabase.TabIndex = 8;
            this.lblMySQLDatabase.Text = "MySQL DataBase";
            // 
            // lblMySQLPort
            // 
            this.lblMySQLPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMySQLPort.AutoSize = true;
            this.lblMySQLPort.Location = new System.Drawing.Point(305, 14);
            this.lblMySQLPort.Name = "lblMySQLPort";
            this.lblMySQLPort.Size = new System.Drawing.Size(64, 13);
            this.lblMySQLPort.TabIndex = 7;
            this.lblMySQLPort.Text = "MySQL Port";
            // 
            // lblMySQLServer
            // 
            this.lblMySQLServer.AutoSize = true;
            this.lblMySQLServer.Location = new System.Drawing.Point(15, 14);
            this.lblMySQLServer.Name = "lblMySQLServer";
            this.lblMySQLServer.Size = new System.Drawing.Size(76, 13);
            this.lblMySQLServer.TabIndex = 6;
            this.lblMySQLServer.Text = "MySQL Server";
            // 
            // edtMySQLPort
            // 
            this.edtMySQLPort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMySQLPort.Location = new System.Drawing.Point(305, 30);
            this.edtMySQLPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.edtMySQLPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.edtMySQLPort.Name = "edtMySQLPort";
            this.edtMySQLPort.Size = new System.Drawing.Size(147, 20);
            this.edtMySQLPort.TabIndex = 5;
            this.edtMySQLPort.Value = new decimal(new int[] {
            3306,
            0,
            0,
            0});
            this.edtMySQLPort.Validated += new System.EventHandler(this.edtMySQLPort_Validated);
            // 
            // edtMySQLPassword
            // 
            this.edtMySQLPassword.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMySQLPassword.Location = new System.Drawing.Point(472, 80);
            this.edtMySQLPassword.Name = "edtMySQLPassword";
            this.edtMySQLPassword.PasswordChar = '*';
            this.edtMySQLPassword.Size = new System.Drawing.Size(100, 20);
            this.edtMySQLPassword.TabIndex = 4;
            this.edtMySQLPassword.Validated += new System.EventHandler(this.edtMySQLPassword_Validated);
            // 
            // edtMySQLUser
            // 
            this.edtMySQLUser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMySQLUser.Location = new System.Drawing.Point(305, 80);
            this.edtMySQLUser.Name = "edtMySQLUser";
            this.edtMySQLUser.Size = new System.Drawing.Size(147, 20);
            this.edtMySQLUser.TabIndex = 3;
            this.edtMySQLUser.Validated += new System.EventHandler(this.edtMySQLUser_Validated);
            // 
            // edtMySQLDataBase
            // 
            this.edtMySQLDataBase.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMySQLDataBase.Location = new System.Drawing.Point(16, 80);
            this.edtMySQLDataBase.Name = "edtMySQLDataBase";
            this.edtMySQLDataBase.Size = new System.Drawing.Size(269, 20);
            this.edtMySQLDataBase.TabIndex = 2;
            this.edtMySQLDataBase.Validated += new System.EventHandler(this.edtMySQLDataBase_Validated);
            // 
            // edtMySQLServer
            // 
            this.edtMySQLServer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.edtMySQLServer.Location = new System.Drawing.Point(16, 29);
            this.edtMySQLServer.Name = "edtMySQLServer";
            this.edtMySQLServer.Size = new System.Drawing.Size(269, 20);
            this.edtMySQLServer.TabIndex = 0;
            this.edtMySQLServer.Validated += new System.EventHandler(this.edtMySQLServer_Validated);
            // 
            // btnLoadConfig
            // 
            this.btnLoadConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadConfig.Location = new System.Drawing.Point(986, 103);
            this.btnLoadConfig.Name = "btnLoadConfig";
            this.btnLoadConfig.Size = new System.Drawing.Size(97, 23);
            this.btnLoadConfig.TabIndex = 11;
            this.btnLoadConfig.Text = "Load Config";
            this.btnLoadConfig.UseVisualStyleBackColor = true;
            this.btnLoadConfig.Click += new System.EventHandler(this.btnLoadConfig_Click);
            // 
            // btnSaveConfig
            // 
            this.btnSaveConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSaveConfig.Location = new System.Drawing.Point(986, 128);
            this.btnSaveConfig.Name = "btnSaveConfig";
            this.btnSaveConfig.Size = new System.Drawing.Size(97, 23);
            this.btnSaveConfig.TabIndex = 12;
            this.btnSaveConfig.Text = "Save Config";
            this.btnSaveConfig.UseVisualStyleBackColor = true;
            this.btnSaveConfig.Click += new System.EventHandler(this.btnSaveConfig_Click);
            // 
            // bwExport
            // 
            this.bwExport.WorkerReportsProgress = true;
            this.bwExport.WorkerSupportsCancellation = true;
            this.bwExport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwExport_DoWork);
            this.bwExport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwExport_ProgressChanged);
            this.bwExport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwExport_RunWorkerCompleted);
            // 
            // edtLimitRecords
            // 
            this.edtLimitRecords.Location = new System.Drawing.Point(207, 36);
            this.edtLimitRecords.Name = "edtLimitRecords";
            this.edtLimitRecords.Size = new System.Drawing.Size(120, 20);
            this.edtLimitRecords.TabIndex = 4;
            // 
            // lblLimitRecords
            // 
            this.lblLimitRecords.AutoSize = true;
            this.lblLimitRecords.Location = new System.Drawing.Point(205, 17);
            this.lblLimitRecords.Name = "lblLimitRecords";
            this.lblLimitRecords.Size = new System.Drawing.Size(97, 13);
            this.lblLimitRecords.TabIndex = 5;
            this.lblLimitRecords.Text = "Record Limit (0=all)";
            // 
            // VehicleGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1112, 565);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSaveConfig);
            this.Controls.Add(this.btnLoadConfig);
            this.Controls.Add(this.grpConfig);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpSource);
            this.Controls.Add(this.btnStart);
            this.Name = "VehicleGrabber";
            this.Text = "VehicleGrabber";
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.mnuContextLog.ResumeLayout(false);
            this.tabBrowser.ResumeLayout(false);
            this.tabSource.ResumeLayout(false);
            this.tabSource.PerformLayout();
            this.grpConfig.ResumeLayout(false);
            this.tabConfig.ResumeLayout(false);
            this.tabExportTypes.ResumeLayout(false);
            this.tabExportTypes.PerformLayout();
            this.tabConfigCSV.ResumeLayout(false);
            this.tabConfigCSV.PerformLayout();
            this.tabConfigMySQL.ResumeLayout(false);
            this.tabConfigMySQL.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.edtMySQLPort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.edtLimitRecords)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox grpSource;
        private System.Windows.Forms.RadioButton rbAutomobilio;
        private System.Windows.Forms.RadioButton rbADAC;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toollblMaker;
        private System.Windows.Forms.ToolStripStatusLabel tooledtMakers;
        private System.Windows.Forms.ToolStripStatusLabel toollblModels;
        private System.Windows.Forms.ToolStripStatusLabel tooledtModels;
        private System.Windows.Forms.ToolStripStatusLabel toollblTypes;
        private System.Windows.Forms.ToolStripStatusLabel tooledtModelTypes;
        private System.Windows.Forms.ToolStripStatusLabel toollblCars;
        private System.Windows.Forms.ToolStripStatusLabel tooledtCars;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripStatusLabel toollblStatus;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.GroupBox grpConfig;
        private System.Windows.Forms.TabPage tabLog;
        public System.Windows.Forms.ListBox lbLogWindow;
        private System.Windows.Forms.TabControl tabConfig;
        private System.Windows.Forms.TabPage tabExportTypes;
        private System.Windows.Forms.CheckBox chkMySQL;
        private System.Windows.Forms.CheckBox chkCSV;
        private System.Windows.Forms.TabPage tabConfigCSV;
        private System.Windows.Forms.TabPage tabConfigMySQL;
        private System.Windows.Forms.TextBox edtDefaultCSVFileEncoding;
        private System.Windows.Forms.Label lblDefaultCSVFileEncoding;
        private System.Windows.Forms.TextBox edtDefaultCSVFileExtension;
        private System.Windows.Forms.Label lblDefaultCSVFileExtension;
        private System.Windows.Forms.ComboBox cbDefaultCSVDelimiter;
        private System.Windows.Forms.Label lblDefaultCSVDelimiter;
        private System.Windows.Forms.NumericUpDown edtMySQLPort;
        private System.Windows.Forms.TextBox edtMySQLPassword;
        private System.Windows.Forms.TextBox edtMySQLUser;
        private System.Windows.Forms.TextBox edtMySQLDataBase;
        private System.Windows.Forms.TextBox edtMySQLServer;
        private System.Windows.Forms.Label lblMySQLPassword;
        private System.Windows.Forms.Label lblMySQLUser;
        private System.Windows.Forms.Label lblMySQLDatabase;
        private System.Windows.Forms.Label lblMySQLPort;
        private System.Windows.Forms.Label lblMySQLServer;
        private System.Windows.Forms.Button btnLoadConfig;
        private System.Windows.Forms.Button btnSaveConfig;
        private System.Windows.Forms.CheckBox chkMySQLSSLEnabled;
        internal System.ComponentModel.BackgroundWorker bwImport;
        internal System.ComponentModel.BackgroundWorker bwExport;
        private System.Windows.Forms.ContextMenuStrip mnuContextLog;
        private System.Windows.Forms.ToolStripMenuItem mnuClearLog;
        private System.Windows.Forms.ToolStripMenuItem mnuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem mnuShowTypes;
        private System.Windows.Forms.ToolStripMenuItem mnuLogInfo;
        private System.Windows.Forms.ToolStripMenuItem mnuLogWarning;
        private System.Windows.Forms.ToolStripMenuItem mnuLogError;
        private System.Windows.Forms.ToolStripMenuItem mnuFatal;
        private System.Windows.Forms.ToolStripMenuItem mnuDebug;
        private System.Windows.Forms.ToolStripMenuItem mnuSaveToFile;
        private System.Windows.Forms.TabPage tabBrowser;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.TabPage tabSource;
        private System.Windows.Forms.TextBox tbContent;
        private System.Windows.Forms.Label lblLimitRecords;
        private System.Windows.Forms.NumericUpDown edtLimitRecords;
    }
}

