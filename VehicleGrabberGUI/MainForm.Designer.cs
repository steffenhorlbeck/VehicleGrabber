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
            this.btnStart = new System.Windows.Forms.Button();
            this.browser = new System.Windows.Forms.WebBrowser();
            this.tbContent = new System.Windows.Forms.TextBox();
            this.grpSource = new System.Windows.Forms.GroupBox();
            this.rbAutomobilio = new System.Windows.Forms.RadioButton();
            this.rbADAC = new System.Windows.Forms.RadioButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toollblMaker = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.toollblModels = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblTypes = new System.Windows.Forms.ToolStripStatusLabel();
            this.toollblCars = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtMakers = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtModels = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtModelTypes = new System.Windows.Forms.ToolStripStatusLabel();
            this.tooledtCars = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwImport = new System.ComponentModel.BackgroundWorker();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toollblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnExport = new System.Windows.Forms.Button();
            this.grpSource.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(713, 13);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 2;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // browser
            // 
            this.browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.browser.Location = new System.Drawing.Point(430, 96);
            this.browser.MinimumSize = new System.Drawing.Size(20, 20);
            this.browser.Name = "browser";
            this.browser.ScriptErrorsSuppressed = true;
            this.browser.Size = new System.Drawing.Size(351, 331);
            this.browser.TabIndex = 3;
            // 
            // tbContent
            // 
            this.tbContent.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.tbContent.Location = new System.Drawing.Point(39, 96);
            this.tbContent.Multiline = true;
            this.tbContent.Name = "tbContent";
            this.tbContent.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.tbContent.Size = new System.Drawing.Size(385, 331);
            this.tbContent.TabIndex = 4;
            // 
            // grpSource
            // 
            this.grpSource.Controls.Add(this.rbAutomobilio);
            this.grpSource.Controls.Add(this.rbADAC);
            this.grpSource.Location = new System.Drawing.Point(39, 13);
            this.grpSource.Name = "grpSource";
            this.grpSource.Size = new System.Drawing.Size(385, 68);
            this.grpSource.TabIndex = 5;
            this.grpSource.TabStop = false;
            this.grpSource.Text = "Quelle";
            // 
            // rbAutomobilio
            // 
            this.rbAutomobilio.AutoSize = true;
            this.rbAutomobilio.Location = new System.Drawing.Point(192, 20);
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
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 6;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toollblMaker
            // 
            this.toollblMaker.Name = "toollblMaker";
            this.toollblMaker.Size = new System.Drawing.Size(60, 17);
            this.toollblMaker.Text = "Hersteller:";
            // 
            // toolProgressBar
            // 
            this.toolProgressBar.Name = "toolProgressBar";
            this.toolProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // toollblModels
            // 
            this.toollblModels.Name = "toollblModels";
            this.toollblModels.Size = new System.Drawing.Size(53, 17);
            this.toollblModels.Text = "Modelle:";
            // 
            // toollblTypes
            // 
            this.toollblTypes.Name = "toollblTypes";
            this.toollblTypes.Size = new System.Drawing.Size(84, 17);
            this.toollblTypes.Text = "Modell-Typen:";
            // 
            // toollblCars
            // 
            this.toollblCars.Name = "toollblCars";
            this.toollblCars.Size = new System.Drawing.Size(64, 17);
            this.toollblCars.Text = "Fahrzeuge:";
            // 
            // tooledtMakers
            // 
            this.tooledtMakers.Name = "tooledtMakers";
            this.tooledtMakers.Size = new System.Drawing.Size(13, 17);
            this.tooledtMakers.Text = "0";
            // 
            // tooledtModels
            // 
            this.tooledtModels.Name = "tooledtModels";
            this.tooledtModels.Size = new System.Drawing.Size(13, 17);
            this.tooledtModels.Text = "0";
            // 
            // tooledtModelTypes
            // 
            this.tooledtModelTypes.Name = "tooledtModelTypes";
            this.tooledtModelTypes.Size = new System.Drawing.Size(13, 17);
            this.tooledtModelTypes.Text = "0";
            // 
            // tooledtCars
            // 
            this.tooledtCars.Name = "tooledtCars";
            this.tooledtCars.Size = new System.Drawing.Size(13, 17);
            this.tooledtCars.Text = "0";
            // 
            // bwImport
            // 
            this.bwImport.WorkerReportsProgress = true;
            this.bwImport.WorkerSupportsCancellation = true;
            this.bwImport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwImport_DoWork);
            this.bwImport.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwImport_ProgressChanged);
            this.bwImport.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwImport_RunWorkerCompleted);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(713, 35);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 7;
            this.btnCancel.Text = "Abbruch";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // toollblStatus
            // 
            this.toollblStatus.Name = "toollblStatus";
            this.toollblStatus.Size = new System.Drawing.Size(42, 17);
            this.toollblStatus.Text = "Status:";
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(713, 57);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(75, 23);
            this.btnExport.TabIndex = 8;
            this.btnExport.Text = "Export";
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // VehicleGrabber
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnExport);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.grpSource);
            this.Controls.Add(this.tbContent);
            this.Controls.Add(this.browser);
            this.Controls.Add(this.btnStart);
            this.Name = "VehicleGrabber";
            this.Text = "VehicleGrabber";
            this.grpSource.ResumeLayout(false);
            this.grpSource.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.WebBrowser browser;
        private System.Windows.Forms.TextBox tbContent;
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
        private System.ComponentModel.BackgroundWorker bwImport;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolStripStatusLabel toollblStatus;
        private System.Windows.Forms.Button btnExport;
    }
}

