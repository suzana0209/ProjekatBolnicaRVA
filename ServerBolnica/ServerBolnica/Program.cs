using Common.Dodatno;
using Common.Enumeracije;
using Common.Model;
using log4net;
using ServerBolnica.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerBolnica
{
    class Program
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(Program));

        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            path = path.Substring(0, path.LastIndexOf("bin")) + "DB";
            AppDomain.CurrentDomain.SetData("DataDirectory", path);

            InicijalniPodaciZaBazu();

            OpenCloseMetode openClose = new OpenCloseMetode();

            openClose.Open();
            Console.WriteLine("Server je pokrenut...");

            Console.ReadKey();

            openClose.Close();
            Console.WriteLine("Server je zaustavljen...");

        }

        private static void InicijalniPodaciZaBazu()
        {
            if (!(DbManager.Instance.bolnicaContext.Bolnice.Count() == 0 &&
                DbManager.Instance.bolnicaContext.Ljekari.Count() == 0 &&
                DbManager.Instance.bolnicaContext.Pacijenti.Count() == 0 &&
                DbManager.Instance.bolnicaContext.Korisnici.Count() == 0))
            {
                return;
            }

            Korisnik admin = new Korisnik()
            {
                KorisnickoIme = "admin",
                Lozinka = "admin",
                Ime = "Suzana",
                Prezime = "Jovic",
                Tip = TipKorisnika.Admin
            };

            DbManager.Instance.DodajKorisnika(admin);

            Pacijent pacijent1 = new Pacijent()
            {
                Ime = "Marko",
                Prezime = "Markovic",
                Jmbg = "1234567891234".ToCharArray()
            };

            Pacijent pacijent2 = new Pacijent()
            {
                Ime = "Jovan",
                Prezime = "Jovanovic",
                Jmbg = "1234567891234".ToCharArray()
            };
            Pacijent pacijent3 = new Pacijent()
            {
                Ime = "Petar",
                Prezime = "Petrovic",
                Jmbg = "1234567891234".ToCharArray()
            };

            pacijent1 = DbManager.Instance.DodajPacijenta(pacijent1);
            pacijent2 = DbManager.Instance.DodajPacijenta(pacijent2);
            pacijent3 = DbManager.Instance.DodajPacijenta(pacijent3);

            Ljekar ljekar1 = new Ljekar()
            {
                Ime = "Dusan",
                Prezime = "Markovic",
                Odjeljenje = Odjeljenje.IntenzivnaNjega,
                Specijalizacija = Specijalizacija.Fizijatar,
                Titula = Titula.Profesor
            };
            Ljekar ljekar2 = new Ljekar()
            {
                Ime = "Lazar",
                Prezime = "Markovic",
                Odjeljenje = Odjeljenje.Kardiologija,
                Specijalizacija = Specijalizacija.Hirurg,
                Titula = Titula.Primarijus
            };
            Ljekar ljekar3 = new Ljekar()
            {
                Ime = "Dejan",
                Prezime = "Markovic",
                Odjeljenje = Common.Enumeracije.Odjeljenje.IntenzivnaNjega,
                Specijalizacija = Common.Enumeracije.Specijalizacija.Internista,
                Titula = Common.Enumeracije.Titula.Docent
            };

            ljekar1 = DbManager.Instance.DodajLjekara(ljekar1);
            ljekar2 = DbManager.Instance.DodajLjekara(ljekar2);
            ljekar3 = DbManager.Instance.DodajLjekara(ljekar3);

            Bolnica bolnica1 = new Bolnica()
            {
                BrojLjekara = 3,
                BrojOdjeljenja = 2,
                Naziv = "Klinicki centar",
                Vrsta = VrstaBolnice.Pedijatrija
            };

            //bolnica1 = DbManager.Instance.DodajBolnicu(bolnica1);
            bolnica1.LjekariUBolnici.Add(ljekar1);
            bolnica1.LjekariUBolnici.Add(ljekar2);
            bolnica1.PacijentiUBolnici.Add(pacijent1);
            bolnica1.PacijentiUBolnici.Add(pacijent2);

            bolnica1 = DbManager.Instance.DodajBolnicu(bolnica1);

            Console.WriteLine("Zavrseno");
        }
    }
}
