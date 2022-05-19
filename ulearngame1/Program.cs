using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ulearngame1
{
    static class Program
    {
        public static Form1 form = null;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            form = new Form1() { WindowState = FormWindowState.Maximized, BackColor = Color.Black, FormBorderStyle = FormBorderStyle.None };
            Application.Run(form);
        }
    }
}
