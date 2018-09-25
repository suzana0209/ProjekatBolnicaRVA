using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.Komande
{
    public interface IUndoKomanda
    {
        void Vrati();
        void Izvrsi();
    }
}
