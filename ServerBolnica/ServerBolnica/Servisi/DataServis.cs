using Common.Dodatno;
using Common.Interfejsi;
using Common.Model;
using Common.ObjektiDTO;
using log4net;
using ServerBolnica.PristupBaziPodataka;
using System;
using System.Collections.Generic;
using System.ServiceModel;

namespace ServerBolnica.Servisi
{
    public class DataServis : IDataServis
    {
        private static readonly ILog log = LogManager.GetLogger(typeof(DataServis));

        public Bolnica DuplirajBolnicu(Sesija sesija, int idBolnice)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Bolnica bolnicaZaKloniranje = DbManager.Instance.VratiBolnicu(idBolnice);

                Bolnica kloniranaBolnica = bolnicaZaKloniranje.KlonirajBolnicu();

                List<Ljekar> kopijaLjekaraUBolnici = kloniranaBolnica.LjekariUBolnici;
                List<Pacijent> kopijaPacijenataUBolnici = kloniranaBolnica.PacijentiUBolnici;

                kloniranaBolnica.IdBolnice = 0;
                kloniranaBolnica.Naziv += "-kopija";

                Bolnica dupliranaBolnica = DbManager.Instance.DodajBolnicu(kloniranaBolnica);

                log.Info("Klonirana bolnica sa id-em: " + dupliranaBolnica.IdBolnice);

                return dupliranaBolnica;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Error: " + ex.Detail.Poruka);
                return null;
            }

           
            //throw new NotImplementedException();
        }

        public bool IzmijeniBolnicu(Sesija sesija, BolnicaIzmijeniDTO bolnicaIzmijeniDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Bolnica izmijenjenaBolnica = DbManager.Instance.VratiBolnicu(bolnicaIzmijeniDTO.IdBolnice);
                if (!bolnicaIzmijeniDTO.Azurirano &&
                bolnicaIzmijeniDTO.Verzija != izmijenjenaBolnica.Verzija)
                {
                    return false;
                }
                if (bolnicaIzmijeniDTO.NoviNazivBolnice != null)
                {
                    izmijenjenaBolnica.Naziv = bolnicaIzmijeniDTO.NoviNazivBolnice;
                }
                if (bolnicaIzmijeniDTO.NoviBrojLjekara >= 0)
                {
                    izmijenjenaBolnica.BrojLjekara = bolnicaIzmijeniDTO.NoviBrojLjekara;
                }
                if (bolnicaIzmijeniDTO.NoviBrojOdjeljenja >= 0)
                {
                    izmijenjenaBolnica.BrojOdjeljenja = bolnicaIzmijeniDTO.NoviBrojOdjeljenja;
                }
                if (bolnicaIzmijeniDTO.NovaVrstaBol.ToString() != "")
                {
                    izmijenjenaBolnica.Vrsta = bolnicaIzmijeniDTO.NovaVrstaBol;
                }

                if (bolnicaIzmijeniDTO.NovaListaLjekara != null)
                {
                    if (izmijenjenaBolnica.LjekariUBolnici != null)
                    {
                        DbManager.Instance.ObrisiLjekaraIzBolnice(izmijenjenaBolnica.IdBolnice);
                    }
                    foreach (var item in bolnicaIzmijeniDTO.NovaListaLjekara)
                    {
                        Ljekar ljekar = new Ljekar()
                        {
                            Ime = item.Ime,
                            Odjeljenje = item.Odjeljenje,
                            Prezime = item.Prezime,
                            Specijalizacija = item.Specijalizacija,
                            Titula = item.Titula
                        };

                        izmijenjenaBolnica.LjekariUBolnici.Add(ljekar);
                        DbManager.Instance.DodajLjekaraUBolnicu(izmijenjenaBolnica.IdBolnice, ljekar);
                    }
                }

                if (bolnicaIzmijeniDTO.NovaListaPacijenata != null)
                {
                    if (izmijenjenaBolnica.PacijentiUBolnici != null)
                    {
                        DbManager.Instance.ObrisiPacijentaIzBolnice(izmijenjenaBolnica.IdBolnice);
                    }
                    foreach (var item in bolnicaIzmijeniDTO.NovaListaPacijenata)
                    {
                        Pacijent pacijent = new Pacijent()
                        {
                            Ime = item.Ime,
                            Prezime = item.Prezime,
                            Jmbg = item.Jmbg
                        };

                        izmijenjenaBolnica.PacijentiUBolnici.Add(pacijent);
                        DbManager.Instance.DodajPacijentaUBolnicu(izmijenjenaBolnica.IdBolnice, pacijent);

                        Bolnica b = DbManager.Instance.VratiBolnicu(izmijenjenaBolnica.IdBolnice);

                    }
                }

                ++izmijenjenaBolnica.Verzija;
                DbManager.Instance.SacuvajPromjene();
                log.Info("Bolnica sa id-em " + izmijenjenaBolnica.IdBolnice + " je izmijenjena");
                return true;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return false;
            }  
        }

        public Bolnica KreirajBolnicu(Sesija sesija, BolnicaKreirajDTO bolnicaDTO)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);

                Bolnica novaBolnica = new Bolnica()
                {
                    BrojLjekara = bolnicaDTO.BrojLjekara,
                    BrojOdjeljenja = bolnicaDTO.BrojOdjeljenja,
                    Naziv = bolnicaDTO.NazivBolnice,
                    Vrsta = bolnicaDTO.VrstaBol,
                    Verzija = 1
                };

                novaBolnica = DbManager.Instance.DodajBolnicu(novaBolnica);

                log.Info("Bolnica sa id-em " + novaBolnica.IdBolnice + " je sacuvana!");

                return novaBolnica;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public void ObrisiBolnicu(Sesija sesija, int idBolnice)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                DbManager.Instance.ObrisiBolnicu(idBolnice);
                log.Info("Bolnica sa id-em " + idBolnice + " je obrisana!");

            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void ObrisiLjekara(Sesija sesija, int idLjekara)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                DbManager.Instance.ObrisiLjekara(idLjekara);
                log.Info("Ljekar sa id-em " + idLjekara + " je obrisan!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public void ObrisiPacijenta(Sesija sesija, int idPacijenta)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                DbManager.Instance.ObrisiPacijenta(idPacijenta);
                log.Info("Pacijent sa id-em " + idPacijenta + " je obrisan!");
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
            }
        }

        public int DodajLjekara(Sesija sesija, Ljekar ljekar)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Ljekar ljekarPostoji = DbManager.Instance.VratiLjekaraPrekoId(ljekar.IdLjekara);
                if (ljekarPostoji == null)
                {
                    ljekarPostoji = DbManager.Instance.DodajLjekara(ljekar);
                }
                else
                {
                    ljekarPostoji.Ime = ljekar.Ime;
                    ljekarPostoji.Prezime = ljekar.Prezime;
                    ljekarPostoji.Odjeljenje = ljekar.Odjeljenje;
                    ljekarPostoji.Specijalizacija = ljekar.Specijalizacija;
                    ljekarPostoji.Titula = ljekar.Titula;

                    DbManager.Instance.SacuvajPromjene();
                }

                log.Info("Ljekar sa id-em" + ljekarPostoji.IdLjekara + " je sacuvan!");
                return ljekarPostoji.IdLjekara;

            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return -1;
            }
        }

        public int DodajPacijenta(Sesija sesija, Pacijent pacijent)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                Pacijent pacijentPostoji = DbManager.Instance.VratiPacijentaPrekoId(pacijent.IdPacijenta);

                if (pacijentPostoji == null)
                {
                    Pacijent p = DbManager.Instance.VratiPacijentaPrekoJmbg(pacijent.Jmbg);
                    if (p == null)
                    {
                        pacijentPostoji = DbManager.Instance.DodajPacijenta(pacijent);
                    }
                    else
                        return -1;
                }
                else
                {
                    Pacijent pom = DbManager.Instance.VratiPacijentaPrekoJmbg(pacijent.Jmbg);

                    if (pom == null)
                    {
                        pacijentPostoji.Ime = pacijent.Ime;
                        pacijentPostoji.Prezime = pacijent.Prezime;
                        pacijentPostoji.Jmbg = pacijent.Jmbg;
                        DbManager.Instance.SacuvajPromjene();
                    }
                    else
                        return -1;                   
                }

                log.Info("Pacijent sa id-em" + pacijentPostoji.IdPacijenta + " je sacuvan!");

                return pacijentPostoji.IdPacijenta;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return -1;
            }
            catch (FaultException<VecPostojiIzuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return -1;
            }
        }

        public List<Bolnica> VratiBolnice(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                List<Bolnica> bolnice = DbManager.Instance.VratiSveBolnice();
                List<Bolnica> kloniraneBolnice = new List<Bolnica>(bolnice.Count);

                foreach (var item in bolnice)
                {
                    kloniraneBolnice.Add(item.KlonirajBolnicu());
                }

                return kloniraneBolnice;
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public Ljekar VratiLjekara(Sesija sesija, int idLjekara)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.VratiLjekaraPrekoId(idLjekara);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public List<Ljekar> VratiLjekare(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.VratiSveLjekare();
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public Pacijent VratiPacijenta(Sesija sesija, int idPacijenta)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.VratiPacijentaPrekoId(idPacijenta);
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }

        public List<Pacijent> VratiPacijente(Sesija sesija)
        {
            try
            {
                SesijaManager.Instance.AutentifikacijaIzuzetak(sesija);
                return DbManager.Instance.VratiSvePacijente();
            }
            catch (FaultException<Izuzetak> ex)
            {
                Console.WriteLine("Greska: " + ex.Detail.Poruka);
                return null;
            }
        }
    }
}
