using Common.Enumeracije;
using Common.Model;
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
    public class DodajLjekaraVM
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        
        public IEnumerable<Specijalizacija> VrstaSpecijalizacije
        {
            get
            {
                return Enum.GetValues(typeof(Specijalizacija)).Cast<Specijalizacija>();
            }
        }
        public Specijalizacija Specijalizacija { get; set; }

        public IEnumerable<Titula> VrstaTitule
        {
            get
            {
                return Enum.GetValues(typeof(Titula)).Cast<Titula>();
            }
        }
        public Titula Titula { get; set; }

        public IEnumerable<Odjeljenje> VrstaOdjeljenja
        {
            get
            {
                return Enum.GetValues(typeof(Odjeljenje)).Cast<Odjeljenje>();
            }
        }
        public Odjeljenje Odjeljenje { get; set; }


        public bool Sacuvano { get; set; }

        public ICommand DodavanjeLjekaraKomanda { get; set; }
        public ICommand OtkaziKomanda { get; set; }

        public Window Roditelj { get; set; }

        public DodajLjekaraVM()
        {
            Sacuvano = false;
            OtkaziKomanda = new RelayCommand(ZatvoriProzor);
            DodavanjeLjekaraKomanda = new RelayCommand(LjekarDodat, ValidacijaLjekara);
        }

        public DodajLjekaraVM(string ime, string prezime, Specijalizacija specijalizacija, Titula titula, Odjeljenje odjeljenje) : this()
        {
            Ime = ime;
            Prezime = prezime;
            Specijalizacija = specijalizacija;
            Titula = titula;
            Odjeljenje = odjeljenje;
        }

        public void ZatvoriProzor()
        {
            Roditelj.Close();
        }

        public bool ValidacijaLjekara()
        {
            return Ime != null && Ime.Length > 0 &&
                    Prezime != null && Prezime.Length > 0 &&
                    Specijalizacija.ToString() != null && Specijalizacija.ToString().Length > 0 &&
                    Titula.ToString() != null && Titula.ToString().Length > 0 &&
                    Odjeljenje.ToString() != null && Odjeljenje.ToString().Length > 0;
        }
        public void LjekarDodat()
        {
            Sacuvano = true;
            Roditelj.Close();
        }      
    }
}
