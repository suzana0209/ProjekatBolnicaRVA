using Common.Model;
using KlijentBolnica.Komande;
using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.RellayCommand;
using KlijentBolnica.View;
using KlijentBolnica.WindowManager;
using Microsoft.CSharp.RuntimeBinder;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;

namespace KlijentBolnica.ViewModel
{
    public class PacijentiVM : ProzorManagingVM, INotifyPropertyChanged
    {
        public ObservableCollection<Pacijent> listaPacijenata { get; set; }

        public Pacijent selektovanPacijent { get; set; }
        
        public ICommand DodajPacijentaKomanda { get; set; }
        public ICommand ObrisiPacijentaKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }


        public PacijentiVM(IProzorManager prozorManager) : base(prozorManager)
        {
            List<Pacijent> listaPacijenataIzBaze = KreirajKomunikaciju.Komunikacija.VratiPacijente();
            listaPacijenata = new ObservableCollection<Pacijent>(listaPacijenataIzBaze);

            DodajPacijentaKomanda = new RelayCommand(DodajPacijenta);
            ObrisiPacijentaKomanda = new RelayCommand(ObrisiPacijenta, SelektovanPacijent);
            OtkaziKomanda = new KomandaOtkazi(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Pacijent SacuvajPacijenta(Pacijent pacijent)
        {
            if(pacijent == null)
            {
                pacijent = new Pacijent();
            }

                        
            DodajPacijentaVM dodajPacijentaVM = new DodajPacijentaVM(pacijent.Ime, pacijent.Prezime, pacijent.Jmbg);
            DodajPacijenta dodajPacijenta = new DodajPacijenta(dodajPacijentaVM);
            dodajPacijenta.ShowDialog();

            if (dodajPacijentaVM.Sacuvano && ValidacijaPodataka(dodajPacijentaVM))
            {
                pacijent.Ime = dodajPacijentaVM.Ime;
                pacijent.Prezime = dodajPacijentaVM.Prezime;
                pacijent.Jmbg = dodajPacijentaVM.Jmbg;

                pacijent.IdPacijenta = KreirajKomunikaciju.Komunikacija.DodajPacijenta(pacijent);
                if(pacijent.IdPacijenta != -1)
                    return pacijent;
            }
            else
            {
                NevalidanUnos unos = new NevalidanUnos();
                unos.ShowDialog();
            }
            return null;
        }

        public void DodajPacijenta()
        {
            Pacijent noviPacijent = SacuvajPacijenta(null);
            if(noviPacijent != null)
            {
                listaPacijenata.Add(noviPacijent);
            }
        }

        public bool SelektovanPacijent()
        {
            return selektovanPacijent != null;
        }
       

        public void ObrisiPacijenta()
        {
            KreirajKomunikaciju.Komunikacija.ObrisiPacijenta(selektovanPacijent.IdPacijenta);
            listaPacijenata.Remove(selektovanPacijent);
        }

        public bool ValidacijaPodataka(DodajPacijentaVM pacijent)
        {
            Regex r = new Regex("^[a-zA-Z]*$");
            
            if (!r.IsMatch(pacijent.Ime) || !r.IsMatch(pacijent.Prezime))
            {
                return false;
            }

            if(string.IsNullOrEmpty(pacijent.Jmbg))
            {
                return false;
            }

            bool isNumeric = int.TryParse(pacijent.Jmbg, out int n);
            if (pacijent.Jmbg.Length != 13 || isNumeric)
            {
                return false;
            }
            
            return true ;
        }
    }
}
