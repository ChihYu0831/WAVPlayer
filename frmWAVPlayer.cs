using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WAVPlayer
{
    public partial class frmWAVPlayer : Form
    {
        // 匯入 Windows 多媒體 API，僅用於讀取長度和跳轉，不作為主要播放器
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        private string aliasName = "WavAudio";
        private bool isDragging = false;
        private bool isLooping = false;  // 記錄是否處於循環播放模式
        
        // 保留您原本的 SoundPlayer
        private SoundPlayer player;
        
        // 自己建立一個虛擬的計時變數，用來推動進度條
        private int currentVirtualPosition = 0; 

        public frmWAVPlayer()
        {
            InitializeComponent();
            lblTime.Text = "00:00 / 00:00";
        }

        /// <summary>
        /// 當使用者按下「瀏覽」按鈕時，開啟檔案對話框讓使用者選擇 WAV 檔案，並將選擇的檔案路徑顯示在 txtFilePath 文字方塊中。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            // 過濾條件設定為WAV檔案
            ofdWAVFile.Filter = "WAV Files(*.wav)|*.wav";
            // 打開檔案對話方塊
            if (ofdWAVFile.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = ofdWAVFile.FileName;
            }
        }

        // ======================= 按鈕功能 =======================

        private void btnPlay_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPath.Text) || !File.Exists(txtPath.Text))
                {
                    MessageBox.Show("請確認檔案路徑是否正確!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                mciSendString($"close {aliasName}", null, 0, IntPtr.Zero);
                mciSendString($"open \"{txtPath.Text}\" type waveaudio alias {aliasName}", null, 0, IntPtr.Zero);

                // 取得音檔總長度 (毫秒)
                StringBuilder lengthBuf = new StringBuilder(32);
                mciSendString($"status {aliasName} length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
                if (int.TryParse(lengthBuf.ToString(), out int length))
                {
                    trackBarProgress.Maximum = length;
                    TimeSpan totalTime = TimeSpan.FromMilliseconds(length);
                    lblTime.Text = $"00:00 / {totalTime.ToString(@"mm\:ss")}";
                }

                isLooping = false; // 【新增】標記為一般播放
                mciSendString($"play {aliasName}", null, 0, IntPtr.Zero);
                timerProgress.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法播放音效檔!\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLoop_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtPath.Text) || !File.Exists(txtPath.Text))
                {
                    MessageBox.Show("請確認檔案路徑是否正確!", "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // 先停止並關閉之前播放的音檔
                mciSendString($"stop {aliasName}", null, 0, IntPtr.Zero);
                mciSendString($"close {aliasName}", null, 0, IntPtr.Zero);

                // 重新開啟音檔
                mciSendString($"open \"{txtPath.Text}\" type waveaudio alias {aliasName}", null, 0, IntPtr.Zero);

                // 取得音檔總長度
                StringBuilder lengthBuf = new StringBuilder(32);
                mciSendString($"status {aliasName} length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);

                if (int.TryParse(lengthBuf.ToString(), out int length))
                {
                    trackBarProgress.Maximum = length;
                    trackBarProgress.Value = 0;

                    TimeSpan totalTime = TimeSpan.FromMilliseconds(length);
                    lblTime.Text = $"00:00 / {totalTime.ToString(@"mm\:ss")}";
                }

                // 記錄目前是循環播放模式
                isLooping = true;

                // 從頭開始播放，並設定 repeat 循環播放
                mciSendString($"play {aliasName} from 0", null, 0, IntPtr.Zero);

                // 啟動進度條計時器
                timerProgress.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("無法循環播放音效檔!\n" + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mciSendString($"stop {aliasName}", null, 0, IntPtr.Zero);

            isLooping = false;

            timerProgress.Stop();
            trackBarProgress.Value = 0;

            TimeSpan totalTime = TimeSpan.FromMilliseconds(trackBarProgress.Maximum);
            lblTime.Text = $"00:00 / {totalTime.ToString(@"mm\:ss")}";
        }

        private void btnEnd_Click(object sender, EventArgs e)
        {
            Application.Exit();
            //this.Close();
        }

        private void frmWAVPlayer_FormClosing(object sender, FormClosingEventArgs e)
        {
            var result = MessageBox.Show("確定要關閉應用程式嗎？", "關閉確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                // 關閉程式時釋放資源
                mciSendString($"close {aliasName}", null, 0, IntPtr.Zero);
            }
        }

        // ================= 時間軸功能實作 =================

        // Timer 的 Tick 事件：用來不斷更新目前的 TrackBar 位置
        private void timerProgress_Tick(object sender, EventArgs e)
        {
            if (isDragging) return;

            StringBuilder posBuf = new StringBuilder(32);
            mciSendString($"status {aliasName} position", posBuf, posBuf.Capacity, IntPtr.Zero);

            if (int.TryParse(posBuf.ToString(), out int position))
            {
                if (position <= trackBarProgress.Maximum)
                {
                    trackBarProgress.Value = position;

                    TimeSpan currentTime = TimeSpan.FromMilliseconds(position);
                    TimeSpan totalTime = TimeSpan.FromMilliseconds(trackBarProgress.Maximum);
                    lblTime.Text = $"{currentTime.ToString(@"mm\:ss")} / {totalTime.ToString(@"mm\:ss")}";
                }

                // 如果是循環播放，而且已經接近結尾，就從頭再播一次
                if (isLooping && position >= trackBarProgress.Maximum - 200)
                {
                    trackBarProgress.Value = 0;
                    mciSendString($"play {aliasName} from 0", null, 0, IntPtr.Zero);
                }
            }
        }

        private void trackBarProgress_MouseDown(object sender, MouseEventArgs e)
        {
            isDragging = true;
        }

        private void trackBarProgress_MouseUp(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                int newPosition = trackBarProgress.Value;

                // 拖曳到新的位置後，從該位置繼續播放
                mciSendString($"play {aliasName} from {newPosition}", null, 0, IntPtr.Zero);

                // 立刻更新畫面上的時間
                TimeSpan currentTime = TimeSpan.FromMilliseconds(newPosition);
                TimeSpan totalTime = TimeSpan.FromMilliseconds(trackBarProgress.Maximum);
                lblTime.Text = $"{currentTime.ToString(@"mm\:ss")} / {totalTime.ToString(@"mm\:ss")}";

                isDragging = false;
            }
        }

        
    }
}
