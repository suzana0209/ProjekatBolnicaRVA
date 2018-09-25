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
    public class KomandaObrisiPacijenta : IUndoKomanda
    {
        Pacijent pacijentZaBrisanje = null;
        int idPacijenta;
        DodajBolnicuVM dodajBolnicuVM = null;

        public KomandaObrisiPacijenta(DodajBolnicuVM viewModel, Pacijent pacijent, int idPacijent)
        {
            this.dodajBolnicuVM = viewModel;
            this.pacijentZaBrisanje = pacijent;
            this.idPacijenta = idPacijent;
        }


        public void Izvrsi()
        {
            dodajBolnicuVM.ListaPacijenataIzTabele.RemoveAt(idPacijenta);
            //throw new NotImplementedException();
        }

        public void Vrati()
        {
            dodajBolnicuVM.ListaPacijenataIzTabele.Insert(idPacijenta, pacijentZaBrisanje);
            //throw new NotImplementedException();
        }
    }
}
