using CYQ.Data.Table;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace CYQ.Visualizer
{
    internal static class FormCreate
    {
        public static Form CreateForm(string title)
        {
            Form form = new Form();
            form.Text = title;
            form.ClientSize = new Size(600, 400);
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.HorizontalScroll.Enabled = true;
            form.VerticalScroll.Enabled = true;
            return form;
        }
        public static DataGridView CreateGrid(Form form)
        {
            DataGridView dg = new DataGridView();
            form.Controls.Add(dg);
            dg.ReadOnly = true;
            dg.Dock = DockStyle.Fill;
            dg.ScrollBars = ScrollBars.Both;
            dg.AutoSize = true;

            StatusBar sb = new StatusBar();
            LinkLabel label = new LinkLabel();
            label.Width = 400;
            label.LinkBehavior = LinkBehavior.HoverUnderline;
            label.LinkColor = Color.Black;
            label.Text = "  Author：路过秋天 : http://cyq1162.cnblogs.com";
            label.TextAlign = ContentAlignment.MiddleLeft;
            sb.Dock = DockStyle.Bottom;
            sb.Controls.Add(label);
            label.Click += sb_Click;
            form.Controls.Add(sb);
            return dg;
        }

        static void sb_Click(object sender, EventArgs e)
        {
            StartHttp("http://cyq1162.cnblogs.com");
        }
        #region 默认浏览器打开网址
        [DllImport("shell32.dll")]
        static extern IntPtr ShellExecute(
            IntPtr hwnd,
            string lpOperation,
            string lpFile,
            string lpParameters,
            string lpDirectory,
            ShowCommands nShowCmd);
        public enum ShowCommands : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMAXIMIZED = 3,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_FORCEMINIMIZE = 11,
            SW_MAX = 11
        }
        /// <summary>
        /// 用默认浏览器打开网址
        /// </summary>
        private static void StartHttp(string url)
        {
            try
            {
                ShellExecute(IntPtr.Zero, "open", url, "", "", ShowCommands.SW_SHOWNOACTIVATE);
            }
            catch
            {
                System.Diagnostics.Process.Start("IEXPLORE.EXE", url);
            }
        }
        #endregion
        private static void AutoSizeColumn(DataGridView dgv)
        {
            int width = 0;
            //使列自使用宽度
            //对于DataGridView的每一个列都调整
            for (int i = 0; i < dgv.Columns.Count; i++)
            {
                //将每一列都调整为自动适应模式
                dgv.AutoResizeColumn(i, DataGridViewAutoSizeColumnMode.AllCells);
                //记录整个DataGridView的宽度
                width += dgv.Columns[i].Width;
            }
            //判断调整后的宽度与原来设定的宽度的关系，如果是调整后的宽度大于原来设定的宽度，
            //则将DataGridView的列自动调整模式设置为显示的列即可，
            //如果是小于原来设定的宽度，将模式改为填充。
            if (width > dgv.Size.Width)
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgv.Columns[0].Frozen = true;
                if (dgv.Columns.Count > 1)
                {
                    //冻结某列 从左开始 0，1，2
                    dgv.Columns[1].Frozen = true;
                }
            }
            else
            {
                dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
        public static void BindTable(IDialogVisualizerService windowService, MDataTable dt, string title)
        {
            if (dt == null) { return; }
            if (string.IsNullOrEmpty(title))
            {
                title = string.Format("TableName : {0}    Rows： {1}    Columns： {2}", dt.TableName, dt.Rows.Count, dt.Columns.Count);
            }
            if (dt.Rows.Count > 200)
            {
                dt = dt.Select(200, null);
                title = title + " Show: Top 200 Rows";
            }
            Form form = FormCreate.CreateForm(title);
            DataGridView dg = FormCreate.CreateGrid(form);
            try
            {
                //插入行号
                MCellStruct ms = new MCellStruct("[No.]", System.Data.SqlDbType.Int);
                dt.Columns.Insert(0, ms);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i][0].Value = i + 1;
                }
                dt.Bind(dg);
                AutoSizeColumn(dg);
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message);
            }

            windowService.ShowDialog(form);
        }
    }
}
