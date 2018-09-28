using Common.Dodatno;
using Common.Model;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace ServerBolnica.PristupBaziPodataka
{
    public class DbManager
    {
        private static DbManager instance = null;
        
        public BolnicaContext bolnicaContext = null;
        
        private DbManager()
        {
            bolnicaContext = new BolnicaContext();
        }

        public static DbManager Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new DbManager();
                }
                return instance;
            }
        }

        public void SacuvajPromjene()
        {
            lock(bolnicaContext)
            {
                bolnicaContext.SaveChanges();
            }
        }

 
        public Korisnik GetUserByUsername(string korisnickoIme)
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnickoIme);
            }
        }

        public Korisnik ProvjeriLozinkaIspravna(KorisnikZaLogovanje korisnik)
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnik.KorisnickoIme && 
                                                                k.Lozinka == korisnik.Lozinka);
            }
        }

        public void DodajKorisnika(Korisnik korisnik)
        {
            lock(bolnicaContext)
            {
                bool nadjeno = false;
                List<Korisnik> listaKorisnika = bolnicaContext.Korisnici.ToList();
                foreach (var item in listaKorisnika)
                {
                    if(item.KorisnickoIme == korisnik.KorisnickoIme)
                    {
                        nadjeno = true;
                        break;
                    }
                }

                if(nadjeno == false)
                {
                    Korisnik korisnikPostoji = bolnicaContext.Korisnici.Find(korisnik.IdKorisnika);
                    
                    bolnicaContext.Korisnici.Add(korisnik);
                    bolnicaContext.SaveChanges();
                    
                }
                else
                {
                    Izuzetak ex = new Izuzetak();
                    ex.Poruka = "Korisnik vec postoji u bazi.";
                    throw new FaultException<Izuzetak>(ex);
                }
            }
        }

        public void IzmijeniKorisnika(Korisnik korisnik)
        {
            lock(bolnicaContext)
            {
                Korisnik korisnikIzBaze = bolnicaContext.Korisnici.FirstOrDefault(k => k.KorisnickoIme == korisnik.KorisnickoIme);
                korisnikIzBaze.Ime = korisnik.Ime;
                korisnikIzBaze.Prezime = korisnik.Prezime;
                korisnikIzBaze.Lozinka = korisnik.Lozinka;
                korisnikIzBaze.Tip = korisnik.Tip;
                bolnicaContext.SaveChanges();
            }
        }

        public Ljekar DodajLjekara(Ljekar ljekar)
        {
            Ljekar povratnaVrijednost = null;

            lock(bolnicaContext)
            {              
                povratnaVrijednost = bolnicaContext.Ljekari.Add(ljekar);
                bolnicaContext.SaveChanges();              
            }
            return povratnaVrijednost;
        }

        public Pacijent DodajPacijenta(Pacijent pacijent)
        {
            Pacijent povratnaVrijednost = null;
            lock(bolnicaContext)
            {
                povratnaVrijednost = bolnicaContext.Pacijenti.Add(pacijent);
                bolnicaContext.SaveChanges();
            }
            return povratnaVrijednost;
        }

        public Bolnica DodajBolnicu(Bolnica bolnica)
        {
            Bolnica povratnaVrijednost = null;
            lock(bolnicaContext)
            {
                povratnaVrijednost = bolnicaContext.Bolnice.Add(bolnica);
                bolnicaContext.SaveChanges();
            }
            return povratnaVrijednost;
        }
        
        public Ljekar VratiLjekaraPrekoId(int idLjekara)
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Ljekari.Find(idLjekara);
            }
        }

        public Pacijent VratiPacijentaPrekoId(int idPacijenta)
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Pacijenti.Find(idPacijenta);
            }
        }

        public Pacijent VratiPacijentaPrekoJmbg(string jmbg)
        {
            Pacijent p = null;
            List<Pacijent> pacijenti = VratiSvePacijente().ToList();
            lock(bolnicaContext)
            {
                foreach (var item in pacijenti)
                {
                    if(item.Jmbg == jmbg)
                    {
                        p = item;
                        break;
                    }
                }
                return p;
            }
        }

        public List<Ljekar> VratiSveLjekare()
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Ljekari.ToList();
            }
        }

        public List<Pacijent> VratiSvePacijente()
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Pacijenti.ToList();
            }
        }

        public void ObrisiLjekara(int idLjekara)
        {
            lock(bolnicaContext)
            {
                Ljekar ljekar = bolnicaContext.Ljekari.Find(idLjekara);
                bolnicaContext.Ljekari.Remove(ljekar);
                bolnicaContext.SaveChanges();
            }
        }

        public void ObrisiPacijenta(int idPacijenta)
        {
            lock(bolnicaContext)
            {
                Pacijent pacijent = bolnicaContext.Pacijenti.Find(idPacijenta);
                bolnicaContext.Pacijenti.Remove(pacijent);
                bolnicaContext.SaveChanges();
            }
        }

        public List<Bolnica> VratiSveBolnice()
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Bolnice.ToList();
            }
        }

        public void DodajPacijentaUBolnicu(int idBolnice, Pacijent pacijent)
        {
            
            lock(bolnicaContext)
            {
                bool nadjen = false;
                Bolnica bolnica = bolnicaContext.Bolnice.Find(idBolnice);
                foreach (var item in bolnica.PacijentiUBolnici)
                {
                    if(item.Equals(pacijent))
                    {
                        nadjen = true;
                        break;
                    }
                }
                if(!nadjen)
                {
                    bolnica.PacijentiUBolnici.Add(pacijent);
                    bolnicaContext.SaveChanges();
                }
            }
        }

        public void DodajLjekaraUBolnicu(int idBolnice, Ljekar ljekar)
        {
            lock(bolnicaContext)
            {
                bool nadjen = false;

                Bolnica bolnica = bolnicaContext.Bolnice.Find(idBolnice);
                foreach (var item in bolnica.LjekariUBolnici)
                {
                    if(item.Equals(ljekar))
                    {
                        nadjen = true;
                        break;
                    }
                }
                if(!nadjen)
                {
                    bolnica.LjekariUBolnici.Add(ljekar);
                    bolnicaContext.SaveChanges();
                }               
            }
        }

        public void ObrisiLjekaraIzBolnice(int idBolnice)
        {
            lock(bolnicaContext)
            {
                Bolnica bolnica = bolnicaContext.Bolnice.Find(idBolnice);
                bolnicaContext.Ljekari.RemoveRange(bolnica.LjekariUBolnici);
                bolnicaContext.SaveChanges();
            }
        }

        public void ObrisiPacijentaIzBolnice(int idBolnice)
        {
            lock(bolnicaContext)
            {
                Bolnica bolnica = bolnicaContext.Bolnice.Find(idBolnice);
                bolnicaContext.Pacijenti.RemoveRange(bolnica.PacijentiUBolnici);
                bolnicaContext.SaveChanges();
            }
        }

        public void ObrisiBolnicu(int idBolnice)
        {
            lock(bolnicaContext)
            {
                Bolnica bolnica = bolnicaContext.Bolnice.Find(idBolnice);

                ObrisiLjekaraIzBolnice(idBolnice);
                ObrisiPacijentaIzBolnice(idBolnice);
                SacuvajPromjene();

                bolnicaContext.Bolnice.Remove(bolnica);
                bolnicaContext.SaveChanges();
            }
        }

        public Bolnica VratiBolnicu(int idBolnice)
        {
            lock(bolnicaContext)
            {
                return bolnicaContext.Bolnice.Find(idBolnice);
            }
        }      
    }
}
