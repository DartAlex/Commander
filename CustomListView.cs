using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing;

namespace Commander
{
    class CustomListView: ListView
    {      
        const int LVM_FIRST = 0x1000;
        const int LVM_GETHEADER = LVM_FIRST + 31;

        [DllImport("User32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int uMsg, int wParam, IntPtr lParam);
        protected override void OnHandleCreated(EventArgs e)
        {

            IntPtr hWndHeader = SendMessage(Handle, LVM_GETHEADER, 0, IntPtr.Zero);
            HeaderListView header = new HeaderListView(hWndHeader);

            base.OnHandleCreated(e);
        }
    }
}
