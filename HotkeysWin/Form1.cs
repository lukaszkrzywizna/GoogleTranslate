using Hotkeys;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HotkeysWin
{
    public partial class Form1 : Form
    {
        private GlobalHotkey ghk;
        public Form1()
        {
            InitializeComponent();
            ghk = new GlobalHotkey(Constants.ALT + Constants.SHIFT, Keys.O, this);
        }

        private void HandleHotkey()
        {
            WriteLine("Hotkey pressed!");
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Constants.WM_HOTKEY_MSG_ID)
                HandleHotkey();
            base.WndProc(ref m);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            WriteLine("Trying to register SHIFT+ALT+O");
            if (ghk.Register())
                WriteLine("Hotkey registered.");
            else
                WriteLine("Hotkey failed to register");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!ghk.Unregister())
                MessageBox.Show("Hotkey failed to unregister!");
        }

        private void WriteLine(string text)
        {
            textBox1.Text += text + Environment.NewLine;
        }

}
}
