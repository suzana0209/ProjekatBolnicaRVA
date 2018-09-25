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
            //try
            //{
                //DbManager.Instance.vr
                if (SesijaManager.Instance.PostojiUBazi(korisnik))
                    return true;
                else
                    return false;
            //}
            //catch (FaultException<Izuzetak> ex)
            //{
            //    Console.WriteLine("Greska: " + ex.Detail.Poruka);
            //    return false;
            //}



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

                    //NotImplementedException();
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

            //Korisnik logovaniKorisnik = DbManager.Instance.GetUserByUsername(korisnik.KorisnickoIme);
            //if (logovaniKorisnik != null && logovaniKorisnik.Lozinka == korisnik.Lozinka)
            //{
                
            //    throw new FaultException<Izuzetak>(ex);

            //    //throw new FaultException<Izuzetak>(new Izuzetak(""));
            //}

            

            //Korisnik logovaniKorisnik = DbManager.Instance.GetUserByUsername(korisnik.KorisnickoIme);
            //if (logovaniKorisnik == null || logovaniKorisnik.Lozinka != korisnik.Lozinka)
            //{
            //    log.Warn("Greska pri logovanju" + logovaniKorisnik.KorisnickoIme);

            //    Izuzetak ex = new Izuzetak();
            //    ex.Poruka = "Pogresno korisnicko ime i/ili lozinka.";
            //    throw new FaultException<Izuzetak>(ex);

            //    //throw new FaultException<Izuzetak>(new Izuzetak(""));
            //}

            //log.Info("Korsnik uspjesno ulogovan: " + logovaniKorisnik.KorisnickoIme);
            //return SesijaManager.Instance.NapraviNovuSesiju(logovaniKorisnik);
            //throw new NotImplementedException();


        }
    }
}
