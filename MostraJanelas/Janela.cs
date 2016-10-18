using System;
using System.Text;
using System.Runtime.InteropServices;

namespace MostraJanelas
{
    /// <summary>
    /// Classe Janela - Armazena os dados da janela
    /// </summary>
    public class Janela
    {
        private string titulo;
        private string executavel;
        private IntPtr wnd;

        public IntPtr Wnd
        {
            get { return wnd; }
        }
        public string Titulo
        {
            get { return titulo; }
        }
        public string Executavel
        {
            get { return executavel; }
        }
        
        [DllImport("user32.dll")]
        private static extern int GetWindowModuleFileName(IntPtr hWnd, StringBuilder exec, int size);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder tit, int size);

        public Janela(IntPtr Handle)
	    {
            wnd = Handle;

            StringBuilder tit = new StringBuilder(256);
            GetWindowText(Wnd, tit, 256);

            titulo = tit.ToString();

            StringBuilder exec = new StringBuilder(256);
            GetWindowModuleFileName(Wnd, exec, 256);
            executavel = exec.ToString();
	    }
    }
}

