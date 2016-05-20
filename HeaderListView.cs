using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Commander
{
    class HeaderListView: NativeWindow
    {
        public HeaderListView(IntPtr hWnd) 
        {
            AssignHandle (hWnd); 
        }
        protected override void WndProc (ref Message m) 
        {
            switch(m.Msg)
            {
                case 0x1200 + 5: //HDM_LAYOUT
                    System.Diagnostics.Debug.WriteLine("HDM_LAYOUT");
                    base.WndProc(ref m);
                    break;

                case 0x000F: //WM_PAINT
                    System.Diagnostics.Debug.WriteLine("WM_PAINT");
                    base.WndProc(ref m);
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }
    }
}
