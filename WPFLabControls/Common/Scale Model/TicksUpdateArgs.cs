using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabControls.Common
{
    public class TicksUpdateArgs
    {
        public ScaleTick[] Ticks { get; private set; }

        public TicksUpdateArgs(ScaleTick[] Ticks)
        {
            this.Ticks = Ticks;
        }
    }
}
