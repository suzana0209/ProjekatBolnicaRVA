using Common.Enumeracije;
using Common.ObjektiDTO;
using KlijentBolnica.KomunikacijaWCF;
using KlijentBolnica.WindowManager;
using System.Windows;

namespace KlijentBolnica.View
{
    /// <summary>
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Window
    {
        public Visibility DugmeDodajKorisnikaVidljivo { get; set; }

        public IProzorManager ProzorManager { get; private set; }

        public string ImePrezime { get; set; }

        public Pocetna(IProzorManager prozorManager)
        {
            ProzorManager = prozorManager;
            JaSamAdmin();
            KorisnikDTO korisnikDTO = TrenutnoUlogovan();

            ImePrezime = korisnikDTO.Ime + " " + korisnikDTO.Prezime;
            InitializeComponent();
            DataContext = this;
        }

        private void DodajKorisnikaDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.DodajKorisnika);
        }

        private void OdjavaDugme_Click(object sender, RoutedEventArgs e)
        {
            KreirajKomunikaciju.Komunikacija.OdjaviSe();
            ProzorManager.PrethodnaStrana();
        }


        private void IzmijeniPodatkeDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.IzmijeniPodatkeKorisniku);
        }

        private void BolniceDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.Bolnice);
        }

        private void PrikaziLjekareDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.Ljekari);
        }

        private void PrikaziPacijenteDugme_Click(object sender, RoutedEventArgs e)
        {
            ProzorManager.PrikaziStranu(StanjeProzora.Pacijenti);
        }

        private void JaSamAdmin()
        {
            //if(KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika() == null)
            //{
            //    return;
            //}

            

            if (KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika().Tip == TipKorisnika.Admin)
            {
                DugmeDodajKorisnikaVidljivo = Visibility.Visible;
            }
            else
            {
                DugmeDodajKorisnikaVidljivo = Visibility.Hidden;
            }
            
        }

        private KorisnikDTO TrenutnoUlogovan()
        {
            return KreirajKomunikaciju.Komunikacija.VratiInfoKorisnika();
        }
    }
}
