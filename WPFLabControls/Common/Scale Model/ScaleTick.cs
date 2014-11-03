using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace LabControls.Common
{
    public class ScaleTick 
    {
        public double Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
            }
        }
        double value = 0.0;

        public ScaleTick(double Value)
        {
            this.Value = Value;
        }
    }
}
