using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace screenshotter
{
    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(String sClassName, String sAppName);
        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);
        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        private IntPtr hwnd;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            hwnd = FindWindow(null, "ScreenShotter");
            RegisterHotKey(hwnd, 1, (int)0x0002, (int)Keys.A);

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            UnregisterHotKey(hwnd, 1);
        }

        protected override void WndProc(ref Message keyPressed)
        {          
            if (keyPressed.Msg == 0x0312)
            {
                Console.WriteLine("hi");
                Rectangle bounds= Screen.AllScreens[0].Bounds;
                Bitmap captureBitmap = new Bitmap(bounds.Width,bounds.Height);
                Graphics captureGraphics = Graphics.FromImage(captureBitmap);
                captureGraphics.CopyFromScreen(bounds.Left, bounds.Top, 0, 0, bounds.Size);
                pictureBox1.Image = captureBitmap;
            }
            base.WndProc(ref keyPressed);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }

}
