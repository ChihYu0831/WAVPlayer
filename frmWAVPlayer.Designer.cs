namespace WAVPlayer
{
    partial class frmWAVPlayer
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.grpPath = new System.Windows.Forms.GroupBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.grpButton = new System.Windows.Forms.GroupBox();
            this.btnEnd = new System.Windows.Forms.Button();
            this.btnStop = new System.Windows.Forms.Button();
            this.btnLoop = new System.Windows.Forms.Button();
            this.btnPlay = new System.Windows.Forms.Button();
            this.ofdWAVFile = new System.Windows.Forms.OpenFileDialog();
            this.trackBarProgress = new System.Windows.Forms.TrackBar();
            this.timerProgress = new System.Windows.Forms.Timer(this.components);
            this.lblTime = new System.Windows.Forms.Label();
            this.grpPath.SuspendLayout();
            this.grpButton.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).BeginInit();
            this.SuspendLayout();
            // 
            // grpPath
            // 
            this.grpPath.Controls.Add(this.btnBrowse);
            this.grpPath.Controls.Add(this.txtPath);
            this.grpPath.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpPath.Location = new System.Drawing.Point(12, 25);
            this.grpPath.Name = "grpPath";
            this.grpPath.Size = new System.Drawing.Size(486, 100);
            this.grpPath.TabIndex = 0;
            this.grpPath.TabStop = false;
            this.grpPath.Text = "音效位置";
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(385, 31);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 47);
            this.btnBrowse.TabIndex = 3;
            this.btnBrowse.Text = "瀏覽";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(28, 41);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(324, 29);
            this.txtPath.TabIndex = 2;
            // 
            // grpButton
            // 
            this.grpButton.Controls.Add(this.btnEnd);
            this.grpButton.Controls.Add(this.btnStop);
            this.grpButton.Controls.Add(this.btnLoop);
            this.grpButton.Controls.Add(this.btnPlay);
            this.grpButton.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.grpButton.Location = new System.Drawing.Point(12, 158);
            this.grpButton.Name = "grpButton";
            this.grpButton.Size = new System.Drawing.Size(486, 100);
            this.grpButton.TabIndex = 1;
            this.grpButton.TabStop = false;
            this.grpButton.Text = "播放按鈕";
            // 
            // btnEnd
            // 
            this.btnEnd.Location = new System.Drawing.Point(365, 33);
            this.btnEnd.Name = "btnEnd";
            this.btnEnd.Size = new System.Drawing.Size(85, 47);
            this.btnEnd.TabIndex = 7;
            this.btnEnd.Text = "結束程式";
            this.btnEnd.UseVisualStyleBackColor = true;
            this.btnEnd.Click += new System.EventHandler(this.btnEnd_Click);
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(257, 33);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(85, 47);
            this.btnStop.TabIndex = 6;
            this.btnStop.Text = "停止播放";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // btnLoop
            // 
            this.btnLoop.Location = new System.Drawing.Point(149, 33);
            this.btnLoop.Name = "btnLoop";
            this.btnLoop.Size = new System.Drawing.Size(85, 47);
            this.btnLoop.TabIndex = 5;
            this.btnLoop.Text = "重複播放";
            this.btnLoop.UseVisualStyleBackColor = true;
            this.btnLoop.Click += new System.EventHandler(this.btnLoop_Click);
            // 
            // btnPlay
            // 
            this.btnPlay.Location = new System.Drawing.Point(41, 33);
            this.btnPlay.Name = "btnPlay";
            this.btnPlay.Size = new System.Drawing.Size(85, 47);
            this.btnPlay.TabIndex = 4;
            this.btnPlay.Text = "播放一次";
            this.btnPlay.UseVisualStyleBackColor = true;
            this.btnPlay.Click += new System.EventHandler(this.btnPlay_Click);
            // 
            // ofdWAVFile
            // 
            this.ofdWAVFile.DefaultExt = "wav";
            // 
            // trackBarProgress
            // 
            this.trackBarProgress.Location = new System.Drawing.Point(12, 308);
            this.trackBarProgress.Name = "trackBarProgress";
            this.trackBarProgress.Size = new System.Drawing.Size(352, 45);
            this.trackBarProgress.TabIndex = 2;
            this.trackBarProgress.TickStyle = System.Windows.Forms.TickStyle.None;
            this.trackBarProgress.MouseDown += new System.Windows.Forms.MouseEventHandler(this.trackBarProgress_MouseDown);
            this.trackBarProgress.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarProgress_MouseUp);
            // 
            // timerProgress
            // 
            this.timerProgress.Tick += new System.EventHandler(this.timerProgress_Tick);
            // 
            // lblTime
            // 
            this.lblTime.Font = new System.Drawing.Font("微軟正黑體", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.lblTime.Location = new System.Drawing.Point(370, 308);
            this.lblTime.Name = "lblTime";
            this.lblTime.Size = new System.Drawing.Size(128, 23);
            this.lblTime.TabIndex = 3;
            this.lblTime.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmWAVPlayer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(512, 380);
            this.Controls.Add(this.lblTime);
            this.Controls.Add(this.trackBarProgress);
            this.Controls.Add(this.grpButton);
            this.Controls.Add(this.grpPath);
            this.Name = "frmWAVPlayer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WAV 音效檔播放器";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmWAVPlayer_FormClosing);
            this.grpPath.ResumeLayout(false);
            this.grpPath.PerformLayout();
            this.grpButton.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.trackBarProgress)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox grpPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.GroupBox grpButton;
        private System.Windows.Forms.Button btnEnd;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.Button btnLoop;
        private System.Windows.Forms.Button btnPlay;
        private System.Windows.Forms.OpenFileDialog ofdWAVFile;
        private System.Windows.Forms.TrackBar trackBarProgress;
        private System.Windows.Forms.Timer timerProgress;
        private System.Windows.Forms.Label lblTime;
    }
}

