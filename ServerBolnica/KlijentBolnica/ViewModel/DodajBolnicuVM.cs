using Common.Enumeracije;
using Common.Model;
using Common.ObjektiDTO;
using KlijentBolnica.Komande;
using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.RellayCommand;
using log4net;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace KlijentBolnica.ViewModel
{
    public class DodajBolnicuVM
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DodajBolnicuVM));

        //polja 
        public string NazivBolnice { get; set; }
        public int BrojLjekara { get; set; }
        public int BrojOdjeljenja { get; set; }

        public IEnumerable<VrstaBolnice> VrstaBolniceDodavanje
        {
            get
            {
                return Enum.GetValues(typeof(VrstaBolnice)).Cast<VrstaBolnice>();
            }
        }
        public VrstaBolnice IzabranaVrstaBolnice { get; set; } = VrstaBolnice.DjecijaBolnica;

        public Bolnica TrenutnaBolnica { get; set; }

        public ObservableCollection<Ljekar> ListaLjekara { get; set; }
        public ObservableCollection<Pacijent> ListaPacijenata { get; set; }

        public ObservableCollection<Ljekar> ListaLjekaraIzTabele { get; set; }
        public ObservableCollection<Pacijent> ListaPacijenataIzTabele { get; set; }


        public Ljekar selektovaniLjekar { get; set; }
        public Pacijent selektovaniPacijent { get; set; }

        public Ljekar selektovaniLjekarIzTabele { get; set; }
        public Pacijent selektovaniPacijentIzTabele { get; set; }
        public int IdSelektovanogLjekara { get; set; }
        public int IdSelektovanogPacijenta { get; set; }

        public string PomocniJmbg { get; set; }


        public ICommand DodajLjekaraKomanda { get; set; }
        public ICommand ObrisiLjekaraKomanda { get; set; }
        public ICommand ObrisiPacijentaKomanda { get; set; }
        public ICommand DodajPacijentaKomanda { get; set; }
        public ICommand DodajBolnicuKomanda { get; set; }
        public ICommand UndoKomanda { get; set; }
        public ICommand RedoKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        CommandExecutor commandExecutor = new CommandExecutor();

        //public event PropertyChangedEventHandler PropertyChanged;

        public Window Roditelj { get; set; }

        public DodajBolnicuVM()
        {
            ListaLjekaraIzTabele = new ObservableCollection<Ljekar>();
            ListaPacijenataIzTabele = new ObservableCollection<Pacijent>();

            List<Ljekar> ljekariIzBaze = KreirajKomunikaciju.Komunikacija.VratiLjekare();
            List<Pacijent> pacijentiIzBaze = KreirajKomunikaciju.Komunikacija.VratiPacijente();

            ListaLjekara = new ObservableCollection<Ljekar>(ljekariIzBaze);
            ListaPacijenata = new ObservableCollection<Pacijent>(pacijentiIzBaze);

            selektovaniLjekar = ListaLjekara.FirstOrDefault();
            selektovaniPacijent = ListaPacijenata.FirstOrDefault();
            //PomocniPacijent pomocni = new PomocniPacijent()
            //{
            //    Ime = selektovaniPacijent.Ime,
            //    Prezime = selektovaniPacijent.Prezime,
            //    Jmbg = selektovaniPacijent.Jmbg.ToString()
            //};


            DodajLjekaraKomanda = new RelayCommand(DodajLjekara);
            ObrisiLjekaraKomanda = new RelayCommand(ObrisiLjekara, SelektovanLjekar);
            DodajPacijentaKomanda = new RelayCommand(DodajPacijenta);
            ObrisiPacijentaKomanda = new RelayCommand(ObrisiPacijenta, SelektovanPacijent);

            DodajBolnicuKomanda = new RelayCommand(SacuvajBolnicu, ValidacijaSacuvajBolnicu);

            UndoKomanda = new RelayCommand(commandExecutor.Undo, commandExecutor.ValidacijaUndo);
            RedoKomanda = new RelayCommand(commandExecutor.Redo, commandExecutor.ValidacijaRedo);

            OtkaziKomanda = new RelayCommand(ZatvoriProzor);
        }

        public DodajBolnicuVM(Bolnica trenutnaBolnica) : this()
        {
            TrenutnaBolnica = trenutnaBolnica;

            ListaLjekaraIzTabele = new ObservableCollection<Ljekar>(trenutnaBolnica.LjekariUBolnici);
            ListaPacijenataIzTabele = new ObservableCollection<Pacijent>(trenutnaBolnica.PacijentiUBolnici);

            NazivBolnice = TrenutnaBolnica.Naziv;
            BrojLjekara = TrenutnaBolnica.BrojLjekara;
            BrojOdjeljenja = TrenutnaBolnica.BrojOdjeljenja;
            IzabranaVrstaBolnice = TrenutnaBolnica.Vrsta;

            //ListaLjekaraIzTabele.Add(selektovaniLjekar);
        }

        public void DodajLjekara()
        {
            IUndoKomanda komandaDodajLjekar = new KomandaDodajLjekara(this, 
                selektovaniLjekar.Ime, selektovaniLjekar.Prezime, selektovaniLjekar.Specijalizacija, selektovaniLjekar.Titula, selektovaniLjekar.Odjeljenje);
            commandExecutor.DodajIIzvrsi(komandaDodajLjekar);
        }

        public void DodajPacijenta()
        {
            IUndoKomanda komandaDodajPacijenta = new KomandaDodajPacijenta(this, selektovaniPacijent.Ime, selektovaniPacijent.Prezime, selektovaniPacijent.Jmbg);
            commandExecutor.DodajIIzvrsi(komandaDodajPacijenta);
        }

        public void ObrisiLjekara()
        {
            IUndoKomanda obrisiLjekara = new KomandaObrisiLjekara(this, selektovaniLjekarIzTabele, IdSelektovanogLjekara);
            commandExecutor.DodajIIzvrsi(obrisiLjekara);
        }

        public void ObrisiPacijenta()
        {
            IUndoKomanda obrisiPacijenta = new KomandaObrisiPacijenta(this, selektovaniPacijentIzTabele, IdSelektovanogPacijenta);
            commandExecutor.DodajIIzvrsi(obrisiPacijenta);
        }

        public bool SelektovanLjekar()
        {
            return selektovaniLjekarIzTabele != null;
        }
        public bool SelektovanPacijent()
        {
            return selektovaniPacijentIzTabele != null;
        }

        public void SacuvajBolnicu()
        {
            if (TrenutnaBolnica == null)
            {
                BolnicaKreirajDTO kreiranaBolnica = new BolnicaKreirajDTO()
                {
                    NazivBolnice = NazivBolnice,
                    VrstaBol = IzabranaVrstaBolnice,
                    BrojLjekara = BrojLjekara,
                    BrojOdjeljenja = BrojOdjeljenja
                };
                TrenutnaBolnica = KreirajKomunikaciju.Komunikacija.KreirajBolnicu(kreiranaBolnica);
            }

            BolnicaIzmijeniDTO izmijeniBolnicuDTO = new BolnicaIzmijeniDTO()
            {
                NoviNazivBolnice = TrenutnaBolnica.Naziv,
                IdBolnice = TrenutnaBolnica.IdBolnice,
                NoviBrojLjekara = TrenutnaBolnica.BrojLjekara,
                NoviBrojOdjeljenja = TrenutnaBolnica.BrojOdjeljenja,
                NovaVrstaBol = TrenutnaBolnica.Vrsta,
                NovaListaLjekara = ListaLjekaraIzTabele.ToList(),
                NovaListaPacijenata = ListaPacijenataIzTabele.ToList(),
                Verzija = TrenutnaBolnica.Verzija
                //ForceUpdate = true
            };

            bool uspjesnoIzmijenjen = KreirajKomunikaciju.Komunikacija.IzmijeniBolnicu(izmijeniBolnicuDTO);

            if (!uspjesnoIzmijenjen)
            {
                //MessageBoxResult dialogResult = MessageBox.Show("Bolnica je vec izmijenjena od strane drugog korisnika. Da li zelite pregaziti tudje izmjene", MessageBoxButton.YesNoCancel, MessageBoxImage.Question);
                //switch (dialogResult)
                //{
                //    case MessageBoxResult.Yes:
                //        log.Warn("Pregazi tudje izmjene");
                //        izmijeniBolnicuDTO.Azurirano = true;
                //        uspjesnoIzmijenjen = KreirajKomunikaciju.Komunikacija.IzmijeniBolnicu(izmijeniBolnicuDTO);
                //        break;
                //    case MessageBoxResult.Cancel:
                //        return;
                //}
            }
            Roditelj.Close();
        }

        public bool ValidacijaSacuvajBolnicu()
        {
            bool uspjesno = (NazivBolnice != null && NazivBolnice.Length > 0 &&
                ListaLjekaraIzTabele != null && ListaLjekaraIzTabele.Count > 0 &&
                ListaPacijenataIzTabele != null && ListaPacijenataIzTabele.Count > 0) ? true : false;

            return uspjesno;
        }

        public void ZatvoriProzor()
        {
            Roditelj.Close();
        }
    }
}

