using Common.Model;
using KlijentBolnica.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.Komande
{
    public class KomandaDodajPacijenta : IUndoKomanda
    {
        DodajBolnicuVM dodajBolnicuVM = null;
        Pacijent noviPacijent = null;
        string Ime { get; set; }
        string Prezime { get; set; }
        string Jmbg { get; set; }

        public KomandaDodajPacijenta(DodajBolnicuVM viewModel, string ime, string prezime, string jmbg)
        {
            dodajBolnicuVM = viewModel;
            this.Ime = ime;
            this.Prezime = prezime;
            this.Jmbg = jmbg;
        }

        public void Izvrsi()
        {
            noviPacijent = new Pacijent()
            {
                Ime = Ime,
                Prezime = Prezime,
                Jmbg = Jmbg
            };
            dodajBolnicuVM.ListaPacijenataIzTabele.Add(noviPacijent);
            //throw new NotImplementedException();
        }

        public void Vrati()
        {
            dodajBolnicuVM.ListaPacijenataIzTabele.Remove(noviPacijent);
            //throw new NotImplementedException();
        }
    }
}
