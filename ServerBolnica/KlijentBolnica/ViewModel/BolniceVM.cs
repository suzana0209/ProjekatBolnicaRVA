using Common.Model;
using KlijentBolnica.Komande;
using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.RellayCommand;
using KlijentBolnica.View;
using KlijentBolnica.WindowManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace KlijentBolnica.ViewModel
{
    public class BolniceVM : ProzorManagingVM
    {
        public ObservableCollection<Bolnica> Bolnice { get; set; }

        public Bolnica SelektovanaBolnica { get; set; }

        //Komande
        public ICommand OtkaziKomanda { get; set; }
        public ICommand DodajBolnicuKomanda { get; set; }
        public ICommand IzmijeniBolnicuKomanda { get; set; }
        public ICommand ObrisiBolnicuKomanda { get; set; }
        public ICommand KlonirajBolnicuKomanda { get; set; }
        public ICommand PretraziBolniceKomanda { get; set; }

        public IEnumerable<KriterijumiPretrage> TipoviPretrage
        {
            get
            {
                return Enum.GetValues(typeof(KriterijumiPretrage)).Cast<KriterijumiPretrage>();
            }
        }

        public KriterijumiPretrage TipoviPretrageEnum { get; set; } = KriterijumiPretrage.NazivBolnice;

        public string KriterijumPretrageTekst { get; set; }

        List<Bolnica> listaBolnicaIzBaze;

        public BolniceVM(IProzorManager prozorManager) : base(prozorManager)
        {
            listaBolnicaIzBaze = KreirajKomunikaciju.Komunikacija.VratiBolnice();
            Bolnice = new ObservableCollection<Bolnica>(listaBolnicaIzBaze);
            //komande
            OtkaziKomanda = new KomandaOtkazi(this);
            DodajBolnicuKomanda = new RelayCommand(DodajBolnicu);
            IzmijeniBolnicuKomanda = new RelayCommand(IzmijeniBolnicu, IzabranaBolnica);
            KlonirajBolnicuKomanda = new RelayCommand(KlonirajBolnicu, IzabranaBolnica);
            ObrisiBolnicuKomanda = new RelayCommand(ObrisiBolnicu, IzabranaBolnica);
            PretraziBolniceKomanda = new RelayCommand(PretraziBolnicu);

            PretraziBolnicu();
        }

        //private void StartUpdateTimer()
        //{
        //    Console.WriteLine("Starting timer...");
        //    var updateTimer = new Timer(3000);
        //    updateTimer.Elapsed += CheckForUpdatesElapsed;
        //    updateTimer.AutoReset = true;
        //    updateTimer.Enabled = true;
        //}

        //private void CheckForUpdatesElapsed(object sender, ElapsedEventArgs eva)
        //{
        //    try
        //    {
        //        bool newListSeted = PostaviNoveVrijednostiUServer();
        //        if (newListSeted)
        //        {
        //            Console.WriteLine("CheckForUpdatesElapsed: seting new list!");
        //            Application.Current.Dispatcher.Invoke(new Action(() => { PretraziBolnicu(); }));
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e.Message);
        //        Console.WriteLine(e.StackTrace);
        //    }
        //}


        public bool IzabranaBolnica()
        {
            bool selektovano = (SelektovanaBolnica != null) ? true : false;
            return selektovano;
        }

        public void DodajBolnicu()
        {
            DodajBolnicuVM sacuvajBolnicuVM = new DodajBolnicuVM();
            DodajBolnicu sacuvajBolnicu = new DodajBolnicu(sacuvajBolnicuVM);
            sacuvajBolnicu.ShowDialog();

            ResetujListuBolnica();
        }

        public void IzmijeniBolnicu()
        {
            DodajBolnicuVM sacuvajBolnicuVM = new DodajBolnicuVM(SelektovanaBolnica);
            DodajBolnicu sacuvajBolnicu = new DodajBolnicu(sacuvajBolnicuVM);
            sacuvajBolnicu.ShowDialog();

            ResetujListuBolnica();
        }

        public void KlonirajBolnicu()
        {
            KreirajKomunikaciju.Komunikacija.DuplirajBolnicu(SelektovanaBolnica.IdBolnice);

            ResetujListuBolnica();
        }

        public void ObrisiBolnicu()
        {
            KreirajKomunikaciju.Komunikacija.ObrisiBolnicu(SelektovanaBolnica.IdBolnice);

            ResetujListuBolnica();
        }

        public void PretraziBolnicu()
        {
            IEnumerable<Bolnica> pretrazeneBolnice = null;

            if (KriterijumPretrageTekst == null || KriterijumPretrageTekst.Trim().Length == 0)
            {
                pretrazeneBolnice = listaBolnicaIzBaze;
            }
            else
            {
                switch (TipoviPretrageEnum)
                {
                    case KriterijumiPretrage.BrojLjekara:
                        pretrazeneBolnice = listaBolnicaIzBaze.Where(bolnica => bolnica.BrojLjekara.ToString().Equals(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.IdBolnice:
                        pretrazeneBolnice = listaBolnicaIzBaze.Where(bolnica => bolnica.IdBolnice.ToString().Equals(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.BrojOdjeljenja:
                        pretrazeneBolnice = listaBolnicaIzBaze.Where(bolnica => bolnica.BrojOdjeljenja.ToString().Equals(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.NazivBolnice:
                        pretrazeneBolnice = listaBolnicaIzBaze.Where(bolnica => bolnica.Naziv.ToString().Contains(KriterijumPretrageTekst));
                        break;
                    case KriterijumiPretrage.VrstaBolnice:
                        pretrazeneBolnice = listaBolnicaIzBaze.Where(bolnica => bolnica.Vrsta.ToString().Equals(KriterijumPretrageTekst));
                        break;
                }
            }
            Bolnice.Clear();
            foreach (var item in pretrazeneBolnice)
            {
                Bolnice.Add(item);
            }
        } 

        public void ResetujListuBolnica()
        {
            bool novaListaIzmijenjena = PostaviNoveVrijednostiUServer();
            if (novaListaIzmijenjena)
            {
                Console.WriteLine("Lista resetovana!!!");
                PretraziBolnicu();
            }
        }

        private bool PostaviNoveVrijednostiUServer()
        {
            List<Bolnica> novaListaBolnica = KreirajKomunikaciju.Komunikacija.VratiBolnice();
            bool izmijenjeno = false;

            izmijenjeno = novaListaBolnica.Count != listaBolnicaIzBaze.Count;

            if (!izmijenjeno)
            {
                for (int i = 0; i < novaListaBolnica.Count; i++)
                {
                    if (novaListaBolnica[i].IdBolnice != listaBolnicaIzBaze[i].IdBolnice ||
                        novaListaBolnica[i].Verzija != listaBolnicaIzBaze[i].Verzija)
                    {
                        izmijenjeno = true;
                        break;
                    }
                }
            }

            if (izmijenjeno)
            {
                listaBolnicaIzBaze = novaListaBolnica;
                return true;
            }

            return false;
        }

    }
    public enum KriterijumiPretrage
    {
        IdBolnice, BrojLjekara, BrojOdjeljenja, NazivBolnice, VrstaBolnice
    }
}
