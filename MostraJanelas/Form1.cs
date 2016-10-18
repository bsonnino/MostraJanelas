using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MostraJanelas
{

    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        private static extern bool PrintWindow(IntPtr hwnd, IntPtr hdcBlt, uint nFlags);

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }

       [DllImport("user32.dll")]
        private static extern int GetWindowRect(IntPtr hwnd,ref RECT rect);

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ListaJanelas Janelas = new ListaJanelas();

            bindingSource1.DataSource = Janelas;
            bindingSource1.Filter = "Titulo = ''";
            listBox1.DataSource = bindingSource1;
            listBox1.DisplayMember = "Titulo";
            listBox1.ValueMember = "Wnd";
        }


        private void listBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex >= 0) 
            {
                IntPtr hWnd = ((Janela)listBox1.SelectedItem).Wnd;
                RECT rect = new RECT();
                int rc = GetWindowRect(hWnd, ref rect);
                if (rc != 0)
                {
                    Bitmap bm = new Bitmap(rect.right - rect.left, rect.bottom - rect.top);

                    if (bm != null)
                    {
                        using (Graphics g = Graphics.FromImage(bm))
                        {
                            System.IntPtr bmDC = g.GetHdc();
                            bool ok = PrintWindow(hWnd, bmDC, 0);
                            g.ReleaseHdc(bmDC);
                            if (ok)
                                pictureBox1.Image = bm;
                            else
                                pictureBox1.Image = null;
                        }
                    }
                }
                else
                    pictureBox1.Image = null;
            } 
        }
    }
}
