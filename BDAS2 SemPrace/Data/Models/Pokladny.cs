using System;
using System.Collections.Generic;

namespace BDAS2_SemPrace.Models
{
    public partial class Pokladny
    {
        public decimal IdSupermarket { get; set; }
        public byte CisloPokladny { get; set; }

        public virtual Supermarkety IdSupermarketNavigation { get; set; }
    }
}
