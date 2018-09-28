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

        public bool PostojiUBaziKorisnik(KorisnikZaLogovanje korisnik)
        {
            
            if (SesijaManager.Instance.PostojiUBazi(korisnik))
                return true;
            else
                return false;
        }

        public Sesija PrijaviSe(KorisnikZaLogovanje korisnik)
        {
            Korisnik logovaniKorisnik = DbManager.Instance.GetUserByUsername(korisnik.KorisnickoIme);
            try
            {              
                if (logovaniKorisnik == null || logovaniKorisnik.Lozinka != korisnik.Lozinka)
                {
                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Pogresno korisnicko ime i/ili lozinka.";
                    Console.WriteLine("Greska: " + ex.Poruka);
                    return null;
                }
                else
                {
                    log.Info("Korsnik uspjesno ulogovan: " + logovaniKorisnik.KorisnickoIme);
                    return SesijaManager.Instance.NapraviNovuSesiju(logovaniKorisnik);

                }
            }
            catch (FaultException<Izuzetak> ex)
            {
                log.Warn("Greska pri logovanju" + logovaniKorisnik.KorisnickoIme);               
                return null;
            }           
        }
    }
}
