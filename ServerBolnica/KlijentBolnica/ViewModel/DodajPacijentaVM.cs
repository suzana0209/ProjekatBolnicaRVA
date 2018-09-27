using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.RellayCommand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace KlijentBolnica.ViewModel
{
    public class DodajPacijentaVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Jmbg { get; set; }

        public bool Sacuvano { get; set; }
        public ICommand DodavanjePacijentaKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public Window Roditelj { get; set; }

        public DodajPacijentaVM()
        {
            Sacuvano = false;
            OtkaziKomanda = new RelayCommand(ZatvoriProzor);
            DodavanjePacijentaKomanda = new RelayCommand(PacijentDodat, ValidacijaPacijenta);
        }

        public DodajPacijentaVM(string ime, string prezime, string jmbg) : this()
        {
            Ime = ime;
            Prezime = prezime;
            Jmbg = jmbg;
        }

        public void ZatvoriProzor()
        {
            Roditelj.Close();
        }

        public bool ValidacijaPacijenta()
        {
            return Ime != null && Ime.Length > 0 &&
                Prezime != null && Prezime.Length > 0;
                 
        }

        public void PacijentDodat()
        {
            Sacuvano = true;
            Roditelj.Close();
        }

       
    }
}
