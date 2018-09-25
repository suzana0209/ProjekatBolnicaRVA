using Common.Model;
using KlijentBolnica.Komande;
using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.RellayCommand;
using KlijentBolnica.View;
using KlijentBolnica.WindowManager;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace KlijentBolnica.ViewModel
{
    public class LjekariVM : ProzorManagingVM
    {
        public ObservableCollection<Ljekar> listaLjekara { get; set; }
        public Ljekar selektovanLjekar { get; set; }

        public ICommand DodajLjekaraKom { get; set; }
        public ICommand IzmijeniLjekaraKomanda { get; set; }
        public ICommand ObrisiLjekaraKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public LjekariVM(IProzorManager prozorManager) : base(prozorManager)
        {
            List<Ljekar> listaLjekaraIzBaze = KreirajKomunikaciju.Komunikacija.VratiLjekare();
            listaLjekara = new ObservableCollection<Ljekar>(listaLjekaraIzBaze);

            DodajLjekaraKom = new RelayCommand(DodajLjekara);
            IzmijeniLjekaraKomanda = new RelayCommand(IzmijeniLjekara, SelektovanLjekar);
            ObrisiLjekaraKomanda = new RelayCommand(ObrisiLjekara, SelektovanLjekar);
            OtkaziKomanda = new KomandaOtkazi(this);
        }

        public Ljekar SacuvajLjekara(Ljekar ljekar)
        {
            if(ljekar == null)
            {
                ljekar = new Ljekar();
            }

            DodajLjekaraVM dodajLjekaraVM = new DodajLjekaraVM(ljekar.Ime, ljekar.Prezime, ljekar.Specijalizacija, ljekar.Titula, ljekar.Odjeljenje);
            DodajLjekara dodajLjekara = new DodajLjekara(dodajLjekaraVM);
            dodajLjekara.ShowDialog();

            if(dodajLjekaraVM.Sacuvano && ValidacijaPodataka(dodajLjekaraVM.Ime, dodajLjekaraVM.Prezime))
            {
                ljekar.Ime = dodajLjekaraVM.Ime;
                ljekar.Prezime = dodajLjekaraVM.Prezime;
                ljekar.Specijalizacija = dodajLjekaraVM.Specijalizacija;
                ljekar.Titula = dodajLjekaraVM.Titula;
                ljekar.Odjeljenje = dodajLjekaraVM.Odjeljenje;

                ljekar.IdLjekara = KreirajKomunikaciju.Komunikacija.DodajLjekara(ljekar);

                return ljekar;
            }
            else
            {
                NevalidanUnos nevalidanUnos = new NevalidanUnos();
                nevalidanUnos.ShowDialog();
            }

            return null;           
        }

        public void DodajLjekara()
        {
            Ljekar noviLjekar = SacuvajLjekara(null);

            if(noviLjekar != null)
            {
                listaLjekara.Add(noviLjekar);
            }
        }

        public bool SelektovanLjekar()
        {
            return selektovanLjekar != null;
        }

        public void IzmijeniLjekara()
        {
            SacuvajLjekara(selektovanLjekar);
        }

        public void ObrisiLjekara()
        {
            KreirajKomunikaciju.Komunikacija.ObrisiLjekara(selektovanLjekar.IdLjekara);
            listaLjekara.Remove(selektovanLjekar);
        }

        public bool ValidacijaPodataka(string ime, string prezime)
        {
            Regex r = new Regex("^[a-zA-Z]*$");
            if (r.IsMatch(ime) && r.IsMatch(prezime))
            {
                return true;
            }
            return false;
        }
    }
}
