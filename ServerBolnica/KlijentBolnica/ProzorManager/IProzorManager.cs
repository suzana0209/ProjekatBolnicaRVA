﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.WindowManager
{
    public interface IProzorManager
    {
        void PrethodnaStrana();
        void PrikaziStranu(StanjeProzora sledecaStrana);
    }
}
