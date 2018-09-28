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
        string Ime;
        string Prezime;
        Odjeljenje Odjeljenje;
        Specijalizacija Specijalizacija;
        Titula Titula;

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
            noviLjekar = new Ljekar();

            noviLjekar.Ime = Ime;
            noviLjekar.Prezime = Prezime;
            noviLjekar.Titula = Titula;
            noviLjekar.Specijalizacija = Specijalizacija;
            noviLjekar.Odjeljenje = Odjeljenje;
            
            
            dodajBolnicuVM.ListaLjekaraIzTabele.Add(noviLjekar);
        }

        public void Vrati()
        {
            dodajBolnicuVM.ListaLjekaraIzTabele.Remove(noviLjekar);
        }
    }
}
