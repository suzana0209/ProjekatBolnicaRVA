using Common.Enumeracije;
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
    public class KomandaDodajLjekara : IUndoKomanda
    {
        DodajBolnicuVM dodajBolnicuVM;
        Ljekar noviLjekar = null;
        string Ime { get; set; }
        string Prezime { get; set; }
        Odjeljenje Odjeljenje { get; set; }
        Specijalizacija Specijalizacija { get; set; }
        Titula Titula { get; set; }

        public KomandaDodajLjekara(DodajBolnicuVM viewModel, string ime, string prezime, Specijalizacija specijalizacija, 
            Titula titula, Odjeljenje odjeljenje)
        {
            this.dodajBolnicuVM = viewModel;
            this.Ime = ime;
            this.Prezime = prezime;
            this.Specijalizacija = specijalizacija;
            this.Odjeljenje = odjeljenje;
            this.Titula = titula;
        }

        public void Izvrsi()
        {
            noviLjekar = new Ljekar()
            {
                Ime = Ime,
                Prezime = Prezime,
                Titula = Titula,
                Specijalizacija = Specijalizacija,
                Odjeljenje = Odjeljenje
            };
            
            dodajBolnicuVM.ListaLjekaraIzTabele.Add(noviLjekar);
            //throw new NotImplementedException();
        }

        public void Vrati()
        {
            dodajBolnicuVM.ListaLjekaraIzTabele.Remove(noviLjekar);
            //throw new NotImplementedException();
        }
    }
}
