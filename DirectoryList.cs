using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Commander
{
    class DirectoryList
    {
        public Icon icon { get; set; }
        public string directory { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public string size { get; set; }
        public DateTime date { get; set; }
        public string attributes { get; set; }
    }
}
