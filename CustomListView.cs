using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
        }

        // for always selected
        protected override void WndProc(ref Message m)
        {
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
            base.WndProc(ref m);
        }

        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (!this.Focused && !e.Item.Selected)
            {
                e.DrawDefault = true;
            }
        }

        protected override void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
        }

        protected override void OnDrawSubItem(DrawListViewSubItemEventArgs e)
        {
            if (e.Item.Selected && this.Focused)
            {
                Rectangle rec;
                if (e.ColumnIndex == 0)
                {
                    rec = new Rectangle(e.Bounds.X + 19, e.Bounds.Y + 2, e.Bounds.Width - 4, e.Bounds.Height - 4);
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
                TextRenderer.DrawText(e.Graphics, e.SubItem.Text, e.Item.ListView.Font, rec, e.SubItem.ForeColor, flags);

                if (e.ColumnIndex == 0)
                {
                    e.Graphics.DrawImage(this.SmallImageList.Images[e.ItemIndex], new Point(e.Bounds.Left + 4, e.Bounds.Top));

                    int width = 0;
                    for (int i = 0; i < this.Columns.Count; i++)
                    {
                        width = width + this.Columns[i].Width;
                    }

                    rec = new Rectangle(e.Bounds.X, e.Bounds.Y, width, e.Bounds.Height);
                    rec.Inflate(-1, -1);
                    using (Pen pen = new Pen(Color.Red, 1.5f))
                    {
                        e.Graphics.DrawRectangle(pen, rec);
                    }
                         
                }               
            }
            else
            {
                e.DrawDefault = true;
            }                      
        }
    }
}
