using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabControls.Common
{
    public class RangesUpdateArgs
    {
        public Range Ranges { get; private set; }

        public RangesUpdateArgs(Range[] Rnages)
        {
            this.Ranges = Ranges;
        }
    }
}
