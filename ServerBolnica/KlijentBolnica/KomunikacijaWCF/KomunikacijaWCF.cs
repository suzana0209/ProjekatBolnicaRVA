using Common.Dodatno;
using Common.Interfejsi;
using Common.Model;
using Common.ObjektiDTO;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace KlijentBolnica.KomunikacijaWCF
{
    public class KomunikacijaWCF
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(KomunikacijaWCF));

        private ChannelFactory<ILogovanjeServis> logovanjeServisFactory;
        private ChannelFactory<IDataServis> dataServisFactory;
        private ChannelFactory<IKorisnikServis> korisnikServisFactory;

        private ILogovanjeServis logovanjeServisProxy;
        private IDataServis dataServisProxy;
        private IKorisnikServis korisnikServisProxy;

        private Sesija sesija = null;

        public KomunikacijaWCF()
        {
            logovanjeServisFactory = new ChannelFactory<ILogovanjeServis>(typeof(ILogovanjeServis).ToString());
            dataServisFactory = new ChannelFactory<IDataServis>(typeof(IDataServis).ToString());
            korisnikServisFactory = new ChannelFactory<IKorisnikServis>(typeof(IKorisnikServis).ToString());

            logovanjeServisProxy = logovanjeServisFactory.CreateChannel();
            dataServisProxy = dataServisFactory.CreateChannel();
            korisnikServisProxy = korisnikServisFactory.CreateChannel();

        }

        public void PrijaviSe(KorisnikZaLogovanje korisnik)
        {
            sesija = logovanjeServisProxy.PrijaviSe(korisnik);
            log.Info("Prijava uspjesno izvrsena!");
        }

        public void OdjaviSe()
        {
            logovanjeServisProxy.OdjaviSe(sesija);
            log.Info("Odjava uspjesno izvrsena!");
        }

        public KorisnikDTO VratiInfoKorisnika()
        {
            if(sesija == null)
            {
                return null;
            }
            log.Info("Informacije o prijavljenom korisnika");
            return korisnikServisProxy.VratiInfoKorisnika(sesija);
        }

        public void IzmijeniInfoKorisnika(KorisnikDTO korisnik)
        {
            korisnikServisProxy.IzmijeniInfoKorisnika(sesija, korisnik);
            log.Info("Informacije korisnika su izmijenjene!");
        }

        public void DodajKorisnika(Korisnik korisnik)
        {
            log.Info("Dodavanje novog korisnika: " + korisnik.KorisnickoIme);
            korisnikServisProxy.DodajKorisnika(sesija, korisnik);
        }

        public bool KorisnikPostojiUBP(KorisnikZaLogovanje korisnik)
        {
            return logovanjeServisProxy.PostojiUBaziKorisnik(korisnik);
        }

        public List<Bolnica> VratiBolnice()
        {
            log.Info("Vrati sve bolnice...");
            return dataServisProxy.VratiBolnice(sesija);
        }

        public int DodajPacijenta(Pacijent pacijent)
        {
            log.Info("Dodavanje pacijenta sa id-em: " + pacijent.IdPacijenta);
            return dataServisProxy.DodajPacijenta(sesija, pacijent);
        }

        public int DodajLjekara(Ljekar ljekar)
        {
            log.Info("Dodavanje ljekara sa id-em: " + ljekar.IdLjekara);
            return dataServisProxy.DodajLjekara(sesija, ljekar);
        }

        public Ljekar VratiLjekara(int idLjekara)
        {
            log.Info("Vrati informacije o ljekaru sa id-em: " + idLjekara);
            return dataServisProxy.VratiLjekara(sesija, idLjekara);
        }

        public Pacijent VratiPacijenta(int idPacijenta)
        {
            log.Info("Vrati informacije o pacijentu sa id-em: " + idPacijenta);
            return dataServisProxy.VratiPacijenta(sesija, idPacijenta);
        }

        public List<Pacijent> VratiPacijente()
        {
            log.Info("Vrati sve pacijente...");
            return dataServisProxy.VratiPacijente(sesija);
        }

        public List<Ljekar> VratiLjekare()
        {
            log.Info("Vrati sve ljekare...");
            return dataServisProxy.VratiLjekare(sesija);
        }

        public void ObrisiLjekara(int idLjekara)
        {
            dataServisProxy.ObrisiLjekara(sesija, idLjekara);
            log.Info("Brisanje ljekara sa id-em: " + idLjekara);
        }

        public void ObrisiPacijenta(int idPacijenta)
        {
            dataServisProxy.ObrisiPacijenta(sesija, idPacijenta);
            log.Info("Brisanje pacijenta sa id-em: " + idPacijenta);
        }

        public void ObrisiBolnicu(int idBolnice)
        {
            dataServisProxy.ObrisiBolnicu(sesija, idBolnice);
            log.Info("Brisanje bolnice sa id-em: " + idBolnice);
        }

        public Bolnica KreirajBolnicu(BolnicaKreirajDTO bolnica)
        {
            log.Info("Kreiranje bolnice" );
            return dataServisProxy.KreirajBolnicu(sesija, bolnica);
        }

        public bool IzmijeniBolnicu(BolnicaIzmijeniDTO bolnicaDTO)
        {
            log.Info("Podaci o bolnici su izmijenjeni");
            return dataServisProxy.IzmijeniBolnicu(sesija, bolnicaDTO);
        }

        public Bolnica DuplirajBolnicu(int idBolnice)
        {
            log.Info("Kloniranje bolnice sa id-em" + idBolnice);
            return dataServisProxy.DuplirajBolnicu(sesija, idBolnice);
        }

    }
}
