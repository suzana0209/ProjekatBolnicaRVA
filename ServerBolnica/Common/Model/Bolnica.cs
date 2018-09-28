using Common.Enumeracije;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace Common.Model
{
    [DataContract]
    public class Bolnica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdBolnice { get; set; }
        [DataMember]
        public int BrojLjekara { get; set; }
        [DataMember]
        public int BrojOdjeljenja { get; set; }
        [DataMember]
        public string Naziv { get; set; }
        [DataMember]
        public VrstaBolnice Vrsta { get; set; }
        [DataMember]
        public int Verzija { get; set; }
        [DataMember]
        public  virtual List<Ljekar> LjekariUBolnici { get; set; }
        [DataMember]       
        public virtual List<Pacijent> PacijentiUBolnici { get; set; }

        public Bolnica KlonirajBolnicu()
        {
            Bolnica kopija = new Bolnica()
            {
                IdBolnice = IdBolnice,
                BrojLjekara = BrojLjekara,
                BrojOdjeljenja = BrojOdjeljenja,
                Naziv = Naziv,
                Vrsta = Vrsta,
                Verzija = Verzija
            };
            
            kopija.LjekariUBolnici = new List<Ljekar>(LjekariUBolnici.Count);
            foreach (var item in LjekariUBolnici)
            {
                kopija.LjekariUBolnici.Add(item.KlonirajLjekara());
            }

       
            kopija.PacijentiUBolnici = new List<Pacijent>(PacijentiUBolnici.Count);
            foreach (var item in PacijentiUBolnici)
            {
                kopija.PacijentiUBolnici.Add(item.KlonirajPacijenta());
            }

            return kopija;
        }

        public Bolnica()
        {
            LjekariUBolnici = new List<Ljekar>();
            PacijentiUBolnici = new List<Pacijent>();
        }

        public Bolnica(int brojLjekara, int brojOdjeljenja, string naziv, VrstaBolnice vrsta)
        {
            BrojLjekara = brojLjekara;
            BrojOdjeljenja = brojOdjeljenja;
            Naziv = naziv;
            Vrsta = vrsta;

            LjekariUBolnici = new List<Ljekar>();
            PacijentiUBolnici = new List<Pacijent>();
        }

        ~Bolnica()
        {

        }
    }
}
