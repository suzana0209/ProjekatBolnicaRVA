using Common.Model;
using KlijentBolnica.Komande;
using KlijentBolnica.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.Komande
{
    public class KomandaObrisiLjekara : IUndoKomanda
    {
        DodajBolnicuVM dodajBolnicuVM = null;
        Ljekar ljekarZaBrisanje;
        int idLjekara;

        public KomandaObrisiLjekara(DodajBolnicuVM viewModel, Ljekar ljekarZaBrisanje, int idLjekara)
        {
            this.dodajBolnicuVM = viewModel;
            this.ljekarZaBrisanje = ljekarZaBrisanje;
            this.idLjekara = idLjekara;
        }

        public void Izvrsi()
        {
            dodajBolnicuVM.ListaLjekaraIzTabele.RemoveAt(idLjekara);
            //throw new NotImplementedException();
        }

        public void Vrati()
        {
            dodajBolnicuVM.ListaLjekaraIzTabele.Insert(idLjekara, ljekarZaBrisanje);
            //throw new NotImplementedException();
        }
    }
}
