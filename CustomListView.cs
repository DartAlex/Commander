﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Commander
{
    class CustomListView : ListView
    {       
        public CustomListView()
        {
            this.OwnerDraw = true;
            this.FullRowSelect = true;
            this.HideSelection = true;
            // without flicker
            this.DoubleBuffered = true;
            this.View = View.Details;
            this.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
            this.BackColor = Color.Black;
        }

        private bool isInWmPaintMsg=false;

        [StructLayout(LayoutKind.Sequential)]
        public struct NMHDR
        {
            public IntPtr hwndFrom;
            public IntPtr idFrom;
            public int code;
        }
       
        protected override void WndProc(ref Message m)
        {
            // For always selected
            // Swallow mouse messages that are not in the client area
            if (m.Msg >= 0x201 && m.Msg <= 0x209)
            {
                Point pos = new Point(m.LParam.ToInt32() & 0xffff, m.LParam.ToInt32() >> 16);
                var hit = this.HitTest(pos);
                switch (hit.Location)
                {
                    case ListViewHitTestLocations.AboveClientArea:
                    case ListViewHitTestLocations.BelowClientArea:
                    case ListViewHitTestLocations.LeftOfClientArea:
                    case ListViewHitTestLocations.RightOfClientArea:
                    case ListViewHitTestLocations.None:
                        return;
                }
            }

            // Flickering text in listview
            switch (m.Msg)
            {
                case 0x0F: // WM_PAINTs
                    this.isInWmPaintMsg = true;
                    base.WndProc(ref m);
                    this.isInWmPaintMsg = false;

                    // top index
                    //if (nmhdr2.code == -181 && !this.TopItem.Equals(lastTopItem))
                    if (!this.TopItem.Equals(lastTopItem))
                    {
                        OnTopItemChanged(EventArgs.Empty);
                        lastTopItem = this.TopItem;
                    }
                    // -----------
                    break;

                case 0x204E: // WM_REFLECT_NOTIFY
                    NMHDR nmhdr = (NMHDR)m.GetLParam(typeof(NMHDR));
                    if (nmhdr.code == -12)
                    // NM_CUSTOMDRAW
                    { 
                        if (this.isInWmPaintMsg)
                        {
                            base.WndProc(ref m);
                        }                     
                    }
                    else
                    {
                        base.WndProc(ref m);
                    }                       
                    break;

                default:
                    base.WndProc(ref m);
                    break;
            }
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            e.DrawDefault = false;
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            Rectangle rec;
            if (e.ColumnIndex == 0)
            {
                rec = new Rectangle(e.Bounds.X + 19, e.Bounds.Y + 2, e.Bounds.Width - 17, e.Bounds.Height - 4);
            }
            else
            {
                if (this.Columns[e.ColumnIndex].TextAlign == HorizontalAlignment.Left)
                {
                    rec = new Rectangle(e.Bounds.X + 3, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Height - 4);
                }
                else
                {
                    rec = new Rectangle(e.Bounds.X + 2, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Height - 4);
                }
            }

            //TODO  Confirm combination of TextFormatFlags.EndEllipsis and TextFormatFlags.ExpandTabs works on all systems.  MSDN claims they're exclusive but on Win7-64 they work.               
            TextFormatFlags align;
            switch (this.Columns[e.ColumnIndex].TextAlign)
            {
                case HorizontalAlignment.Right:
                    align = TextFormatFlags.Right;
                    break;
                case HorizontalAlignment.Center:
                    align = TextFormatFlags.HorizontalCenter;
                    break;
                default:
                    align = TextFormatFlags.Left;
                    break;
            }

            TextFormatFlags flags = align | TextFormatFlags.EndEllipsis | TextFormatFlags.ExpandTabs | TextFormatFlags.SingleLine;

            //If a different tabstop than the default is needed, will have to p/invoke DrawTextEx from win32.               
            using (Font font = new Font("Microsoft since self", 8, FontStyle.Bold))
            {
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, font, rec, Color.FromArgb(0, 255, 0), flags);
            }

            if (e.ColumnIndex == 0)
            {
                e.Graphics.DrawImage(this.SmallImageList.Images[e.Item.ImageIndex], new Point(e.Bounds.Left + 4, e.Bounds.Top));
            }

            if (e.Item.Selected && this.Focused)
            {
                if (e.ColumnIndex == 0)
                {
                    e.Graphics.DrawImage(this.SmallImageList.Images[e.Item.ImageIndex], new Point(e.Bounds.Left + 4, e.Bounds.Top));

                    int width = 0;
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        width = width + this.Columns[i].Width;
                    }

                    rec = new Rectangle(e.Bounds.X, e.Bounds.Y - 1, width, e.Bounds.Height);
                    rec.Inflate(-1, -1);
                    using (Pen pen = new Pen(Color.Red, 1.5f))
                    {
                        e.Graphics.DrawRectangle(pen, rec);
                    }
                }
            }
        }

        // top index
        public event EventHandler TopItemChanged;

        protected virtual void OnTopItemChanged(EventArgs e)
        {
            var handler = TopItemChanged;
            if (handler != null) handler(this, e);
        }

        private ListViewItem lastTopItem = null;
    }
}
