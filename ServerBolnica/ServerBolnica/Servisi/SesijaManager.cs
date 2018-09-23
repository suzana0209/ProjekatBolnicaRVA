using Common.Dodatno;
using Common.Enumeracije;
using log4net;
using ServerBolnica.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServerBolnica.Servisi
{
    public class SesijaManager
    {
        private static SesijaManager instance = null;
        private static readonly ILog log = LogManager.GetLogger(typeof(SesijaManager));

        Dictionary<string, SessionInstance> sesije = new Dictionary<string, SessionInstance>();

        private SesijaManager() { }

        public static SesijaManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new SesijaManager();
                }
                return instance;
            }
        }

        public Sesija NapraviNovuSesiju(Korisnik korisnikSesije)
        {
            string idSesije = null;
            do
            {
                idSesije = Guid.NewGuid().ToString("n");
            }
            while (sesije.ContainsKey(idSesije));

            Sesija sesija = new Sesija()
            {
                IdSesije = idSesije
            };

            SessionInstance sessionInstance = new SessionInstance()
            {
                KorisnikSesije = korisnikSesije,
                SesijaCookie = sesija
            };

            sesije.Add(idSesije, sessionInstance);

            return sesija;
        }

        public bool ObrisiSesiju(Sesija sesija)
        {
            return sesije.Remove(sesija.IdSesije);
        }

        public bool ProvjeraAutentifikacije(Sesija sesija)
        {
            if(sesija == null)
            {
                return false;
            }
            try
            {
                if (sesije.ContainsKey(sesija.IdSesije))
                {
                    return true;
                }
                else
                {

                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Ne postoji sesija!.";
                    throw new FaultException<Izuzetak>(ex);
                }
            }
            catch (FaultException<NullReferenceException> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail);
                return false;
            }




        }

        public bool ProvjeriAdministratora(Sesija sesija)
        {
            SessionInstance instance = null;
            if(sesije.TryGetValue(sesija.IdSesije, out instance))
            {
                return instance.KorisnikSesije.Tip == TipKorisnika.Admin;
            }
            return false;
        }

        public Korisnik VratiKorisnika(Sesija sesija)
        {
            if(sesija == null)
            {
                return null;
            }

            SessionInstance sessionInstance = sesije[sesija.IdSesije];
            return DbManager.Instance.GetUserByUsername(sessionInstance.KorisnikSesije.KorisnickoIme);
        }

        public void AutentifikacijaIzuzetak(Sesija sesija)
        {
            try
            {
                if (!ProvjeraAutentifikacije(sesija))
                {
                    log.Warn("Korisnik nije autentifikovan!");

                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Korisnik nije autentifikovan!";
                    throw new FaultException<Izuzetak>(ex);
                    //throw new FaultException<Izuzetak>(new Izuzetak("Korisnik nije autentifikovan!"));
                }
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }          
        }

        public void AdministratorIzuzetak(Sesija sesija)
        {
            if(!ProvjeriAdministratora(sesija))
            {
                log.Warn("Korisnik nije administrator");

                Izuzetak ex = new Izuzetak();
                ex.Poruka = "Korisnik nije administrator!";
                throw new FaultException<Izuzetak>(ex);

                //throw new FaultException<Izuzetak>(new Izuzetak("Korisnik nije admin!"));
            }
        }

        public bool PostojiUBazi(KorisnikZaLogovanje korisnik)
        {
            if (DbManager.Instance.GetUserByUsername(korisnik.KorisnickoIme) == null)
                return false;
            else
            {
                //Izuzetak ex = new Izuzetak();
                //ex.Poruka = "Ne postoji.";
                //throw new FaultException<Izuzetak>(ex);
                return true;
                


            }
        }
    }
}
