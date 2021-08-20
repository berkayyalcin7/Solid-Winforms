using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolidOtomasyon.Takip.Common.Enums
{
    public enum EvetHayir:byte
    {
        [Description("Evet")]
        Evet = 1,
        [Description("Hayır")]
        Hayir = 2
    }
}
