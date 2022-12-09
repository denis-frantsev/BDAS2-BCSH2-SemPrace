using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class PultyZbozi
    {
        public long ZboziIdZbozi { get; set; }
        public long PultCisloPultu { get; set; }
        public long PultIdSupermarket { get; set; }

        public virtual Zbozi ZboziIdZboziNav { get; set; }
    }
}
