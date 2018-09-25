using Common.Enumeracije;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Common.Model
{
    [DataContract]
    public class Ljekar
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DataMember]
        public int IdLjekara { get; set; }
        [DataMember]
        public string Ime { get; set; }
        [DataMember]
        public Odjeljenje Odjeljenje { get; set; }
        [DataMember]
        public string Prezime { get; set; }
        [DataMember]
        public Specijalizacija Specijalizacija { get; set; }
        [DataMember]
        public Titula Titula { get; set; }
        [DataMember]
        public  List<Bolnica> BolnicaRadiLjekar { get; set; }
      
        public Ljekar()
        {
            BolnicaRadiLjekar = new List<Bolnica>();
        }

        public Ljekar(string ime, Odjeljenje odjeljenje, string prezime, Specijalizacija specijalizacija, Titula titula)
        {
            Ime = ime;
            Odjeljenje = odjeljenje;
            Prezime = prezime;
            Specijalizacija = specijalizacija;
            Titula = titula;

            BolnicaRadiLjekar = new List<Bolnica>();
        }

        public Ljekar KlonirajLjekara()
        {
            Ljekar kopija = new Ljekar()
            {
                IdLjekara = IdLjekara,
                Ime = Ime,
                Prezime = Prezime,
                Odjeljenje = Odjeljenje,
                Specijalizacija = Specijalizacija,
                Titula = Titula
            };

            //kopija.BolnicaRadiLjekar = new List<Bolnica>(BolnicaRadiLjekar.Count);
            //foreach (var item in BolnicaRadiLjekar)
            //{
            //    kopija.BolnicaRadiLjekar.Add(item.KlonirajBolnicu());
            //}

            return kopija;
        }
    }
}
