using System;
using System.Text;
using System.Collections;
using System.Runtime.InteropServices;

namespace MostraJanelas
{

    /// <summary>
    /// Armazena a lista das janelas encontradas
    /// </summary>
    public class ListaJanelas : ArrayList
    {
        [DllImport("user32.dll")]
        private static extern int EnumWindows(EnumWindowsProc EnumProc, int lParam);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        public delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        private bool EnumProc(IntPtr hWnd, int lParam)
        {
            if (IsWindowVisible(hWnd))
                Add(new Janela(hWnd));
            return (true);
        }

        public ListaJanelas()
            : base()
        {
            EnumWindows(new EnumWindowsProc(EnumProc), 0);
// Remove as janelas sem título
            for (int i = Count - 1; i >= 0; i--)
                if (((Janela)this[i]).Titulo == "")
                    RemoveAt(i);
        }

    }
}
