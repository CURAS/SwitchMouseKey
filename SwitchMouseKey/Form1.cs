using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SwitchMouseKey
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private extern static bool SwapMouseButton(bool fSwap);

        [DllImport("user32.dll")]
        private extern static int GetSystemMetrics(int index);

        private MouseButtons PrimeKey()
        {
            int flag = GetSystemMetrics(23);
            if (flag == 0)
                return MouseButtons.Left;
            else
                return MouseButtons.Right;
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Hide();
            this.ShowInTaskbar = false;

            if (PrimeKey() == MouseButtons.Left)
            {
                this.trayIcon.Icon = Properties.Resources.Left;
            }
            else
            {
                this.trayIcon.Icon = Properties.Resources.Right;
            }
            this.trayIcon.Visible = true;
        }

        private void trayIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (PrimeKey() == MouseButtons.Left)
                {
                    SwapMouseButton(true);
                    this.trayIcon.Icon = Properties.Resources.Right;
                }
                else
                {
                    SwapMouseButton(false);
                    this.trayIcon.Icon = Properties.Resources.Left;
                }
            }
        }

        private void exitMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

    }
}
