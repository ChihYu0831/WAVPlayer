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
        // 匯入 Windows 多媒體 API
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string command, StringBuilder returnValue, int returnLength, IntPtr winHandle);

        private string aliasName = "WavAudio"; // 替多媒體指令取一個別名
        private bool isDragging = false;       // 判斷使用者是否正在拖曳進度條

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

                    // 【新增】將總毫秒轉換為 mm:ss 格式，並初始化 Label
                    TimeSpan totalTime = TimeSpan.FromMilliseconds(length);
                    lblTime.Text = $"00:00 / {totalTime.ToString(@"mm\:ss")}";
                }

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
                mciSendString($"close {aliasName}", null, 0, IntPtr.Zero);
                mciSendString($"open \"{txtPath.Text}\" type waveaudio alias {aliasName}", null, 0, IntPtr.Zero);

                // 重複播放加上 repeat 參數
                mciSendString($"play {aliasName} repeat", null, 0, IntPtr.Zero);
                timerProgress.Start();
            }
            catch { }
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            mciSendString($"stop {aliasName}", null, 0, IntPtr.Zero);
            timerProgress.Stop();
            trackBarProgress.Value = 0; // 重置進度條
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

                    // 【新增】將目前播放進度(毫秒)與總長度轉換為 mm:ss 格式
                    TimeSpan currentTime = TimeSpan.FromMilliseconds(position);
                    TimeSpan totalTime = TimeSpan.FromMilliseconds(trackBarProgress.Maximum);
                    lblTime.Text = $"{currentTime.ToString(@"mm\:ss")} / {totalTime.ToString(@"mm\:ss")}";
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
                // 使用 play 指令的 from 參數來指定從哪邊開始播 (單位：毫秒)
                mciSendString($"play {aliasName} from {newPosition}", null, 0, IntPtr.Zero);
                isDragging = false;
            }
        }

        
    }
}
