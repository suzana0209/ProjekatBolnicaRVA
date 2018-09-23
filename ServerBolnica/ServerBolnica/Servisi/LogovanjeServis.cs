using Common.Dodatno;
using Common.Interfejsi;
using log4net;
using ServerBolnica.PristupBaziPodataka;
using System;
using System.ServiceModel;

namespace ServerBolnica.Servisi
{
    public class LogovanjeServis : ILogovanjeServis
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(LogovanjeServis));

        public void OdjaviSe(Sesija sesija)
        {
            SesijaManager.Instance.ObrisiSesiju(sesija);
            log.Info("Odjavljivanje sa sistema uspjesno izvrseno");
        }

        public Sesija PrijaviSe(KorisnikZaLogovanje korisnik)
        {
            Korisnik logovaniKorisnik = DbManager.Instance.GetUserByUsername(korisnik.KorisnickoIme);
            if (logovaniKorisnik == null || logovaniKorisnik.Lozinka != korisnik.Lozinka)
            {
                log.Warn("Greska pri logovanju" + logovaniKorisnik.KorisnickoIme);

                Izuzetak ex = new Izuzetak();
                ex.Poruka = "Pogresno korisnicko ime i/ili lozinka.";
                throw new FaultException<Izuzetak>(ex);

                //throw new FaultException<Izuzetak>(new Izuzetak(""));
            }

            log.Info("Korsnik uspjesno ulogovan: " + logovaniKorisnik.KorisnickoIme);
            return SesijaManager.Instance.NapraviNovuSesiju(logovaniKorisnik);
            throw new NotImplementedException();
        }
    }
}
